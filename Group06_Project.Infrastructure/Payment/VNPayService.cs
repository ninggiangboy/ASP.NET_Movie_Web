using System.Globalization;
using System.Net;
using System.Web;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Group06_Project.Infrastructure.Payment;

public class VnPayService : IPaymentService
{
    private const int VnPayAmountMultiplier = 100;
    private const int PointMultiplier = 1_000;
    private readonly string _hashSecret;
    private readonly string _returnUrl;
    private readonly string _tmnCode;
    private readonly string _url;

    public VnPayService(IConfiguration configuration)
    {
        _hashSecret = configuration["VnPay:HashSecret"];
        _returnUrl = configuration["VnPay:ReturnUrl"];
        _tmnCode = configuration["VnPay:TmnCode"];
        _url = configuration["VnPay:Url"];
    }

    public Task<string> CreatePaymentUrl(TransactionModel transaction)
    {
        var hostName = Dns.GetHostName();
        var clientIpAddress = Dns.GetHostAddresses(hostName)
            .GetValue(0)?.ToString() ?? "";
        var amount = transaction.Amount * VnPayAmountMultiplier * PointMultiplier;
        var paymentUrl = VnPayUtil.BuilderUrl(_url, _hashSecret)
            .RequestData("vnp_Version", "2.1.0")
            .RequestData("vnp_Command", "pay")
            .RequestData("vnp_TmnCode", _tmnCode)
            .RequestData("vnp_Amount", Math.Round(amount).ToString(CultureInfo.InvariantCulture))
            .RequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"))
            .RequestData("vnp_CurrCode", "VND")
            .RequestData("vnp_IpAddr", clientIpAddress)
            .RequestData("vnp_Locale", "vn")
            .RequestData("vnp_OrderInfo", transaction.Info)
            .RequestData("vnp_OrderType", "other")
            .RequestData("vnp_ReturnUrl", _returnUrl)
            .RequestData("vnp_TxnRef", transaction.TransactionReference)
            .Build();
        return Task.FromResult(paymentUrl);
    }

    public Task CheckPayment(string queryString)
    {
        var json = HttpUtility.ParseQueryString(queryString);
        var vnpResponseCode = json["vnp_ResponseCode"] ?? string.Empty;
        var vnpSecureHash = json["vnp_SecureHash"] ?? string.Empty;
        var pos = queryString.IndexOf("&vnp_SecureHash", StringComparison.Ordinal);
        var checkSignature = ValidateSignature(queryString.Substring(1, pos - 1), vnpSecureHash, _hashSecret);
        if (!vnpResponseCode.Equals("00") || !checkSignature)
            throw new ArgumentException(GetErrorMessage(vnpResponseCode));

        return Task.CompletedTask;
    }

    public Task<string> GetTransactionReference(string queryString)
    {
        var json = HttpUtility.ParseQueryString(queryString);
        var transactionId = json["vnp_TxnRef"] ?? throw new ArgumentException("Not found transaction");
        return Task.FromResult(transactionId);
    }

    private static string GetErrorMessage(string responseCode)
    {
        // 07 	Trừ tiền thành công. Giao dịch bị nghi ngờ (liên quan tới lừa đảo, giao dịch bất thường).
        // 09 	Giao dịch không thành công do: Thẻ/Tài khoản của khách hàng chưa đăng ký dịch vụ InternetBanking tại ngân hàng.
        // 10 	Giao dịch không thành công do: Khách hàng xác thực thông tin thẻ/tài khoản không đúng quá 3 lần
        // 11 	Giao dịch không thành công do: Đã hết hạn chờ thanh toán. Xin quý khách vui lòng thực hiện lại giao dịch.
        // 12 	Giao dịch không thành công do: Thẻ/Tài khoản của khách hàng bị khóa.
        // 13 	Giao dịch không thành công do Quý khách nhập sai mật khẩu xác thực giao dịch (OTP). Xin quý khách vui lòng thực hiện lại giao dịch.
        // 24 	Giao dịch không thành công do: Khách hàng hủy giao dịch
        // 51 	Giao dịch không thành công do: Tài khoản của quý khách không đủ số dư để thực hiện giao dịch.
        // 65 	Giao dịch không thành công do: Tài khoản của Quý khách đã vượt quá hạn mức giao dịch trong ngày.
        // 75 	Ngân hàng thanh toán đang bảo trì.
        // 79 	Giao dịch không thành công do: KH nhập sai mật khẩu thanh toán quá số lần quy định. Xin quý khách vui lòng thực hiện lại giao dịch
        // 99 	Các lỗi khác (lỗi còn lại, không có trong danh sách mã lỗi đã liệt kê)
        return responseCode switch
        {
            "07" => "Payment successful. Transaction is suspicious (related to fraud, unusual transaction).",
            "09" =>
                "Transaction failed: The customer's card/account has not registered for InternetBanking at the bank.",
            "10" =>
                "Transaction failed: Customer has incorrectly authenticated card/account information more than 3 times",
            "11" => "Transaction failed: Payment waiting period has expired. Please perform the transaction again.",
            "12" => "Transaction failed: The customer's card/account is locked.",
            "13" =>
                "Transaction failed: You have entered the wrong transaction authentication password (OTP). Please perform the transaction again.",
            "24" => "Transaction failed: Customer cancelled the transaction",
            "51" => "Transaction failed: Your account does not have enough balance to perform the transaction.",
            "65" => "Transaction failed: Your account has exceeded the transaction limit for the day.",
            "75" => "Payment bank is under maintenance.",
            "79" =>
                "Transaction failed: Customer has entered the wrong payment password more than the allowed number of times. Please perform the transaction again",
            "99" => "Other errors (remaining errors, not on the list of listed error codes)",
            _ => "Unknown response code"
        };
    }

    private static bool ValidateSignature(string rawResponse, string inputHash, string secretKey)
    {
        var checksum = VnPayUtil.HmacSha512(secretKey, rawResponse);
        return checksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
    }
}
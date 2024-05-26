using System.Globalization;
using System.Net;
using System.Web;
using Group06_Project.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace Group06_Project.Infrastructure.Payment;

public class VnPayService : IPaymentService
{
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

    public Task<string> CreatePaymentUrl(string transactionId, string info, decimal amount)
    {
        var hostName = Dns.GetHostName();
        var clientIpAddress = Dns.GetHostAddresses(hostName).GetValue(0)?.ToString() ?? "";
        var builder = new VnPayUtil();

        builder.AddRequestData("vnp_Version", "2.1.0");
        builder.AddRequestData("vnp_Command", "pay");
        builder.AddRequestData("vnp_TmnCode", _tmnCode);
        builder.AddRequestData("vnp_Amount", Math.Round(amount * 100).ToString(CultureInfo.InvariantCulture));
        builder.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
        builder.AddRequestData("vnp_CurrCode", "VND");
        builder.AddRequestData("vnp_IpAddr", clientIpAddress);
        builder.AddRequestData("vnp_Locale", "vn");
        builder.AddRequestData("vnp_OrderInfo", info);
        builder.AddRequestData("vnp_OrderType", "other");
        builder.AddRequestData("vnp_ReturnUrl", _returnUrl);
        builder.AddRequestData("vnp_TxnRef", transactionId);

        var paymentUrl = builder.BuildRequestUrl(_url, _hashSecret);
        return Task.FromResult(paymentUrl);
    }

    public Task PaymentConfirm(string queryString)
    {
        var json = HttpUtility.ParseQueryString(queryString);
        var transactionId = json["vnp_TxnRef"];
        var info = json["vnp_OrderInfo"];
        var vnpTranId = Convert.ToInt64(json["vnp_TransactionNo"]);
        var vnpResponseCode = json["vnp_ResponseCode"];
        var vnpSecureHash = json["vnp_SecureHash"];
        var pos = queryString.IndexOf("&vnp_SecureHash", StringComparison.Ordinal);
        var checkSignature = ValidateSignature(queryString.Substring(1, pos - 1), vnpSecureHash, _hashSecret);
        return Task.CompletedTask;
    }

    private static bool ValidateSignature(string rawResponse, string inputHash, string secretKey)
    {
        var checksum = VnPayUtil.HmacSha512(secretKey, rawResponse);
        return checksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
    }
}
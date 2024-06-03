using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Group06_Project.Infrastructure.Payment;

public static class VnPayUtil
{

    public static UrlBuilder BuilderUrl(string baseUrl, string hashSecret)
    {
        return new UrlBuilder(baseUrl, hashSecret);
    }
    
    public class UrlBuilder
    {
        private readonly SortedList<string, string> _requestData = new(new VnPayCompare());
        private readonly string _baseUrl;
        private readonly string _vnpHashSecret;

        public UrlBuilder(string baseUrl, string hashSecret)
        {
            _baseUrl = baseUrl;
            _vnpHashSecret = hashSecret;
        }
        
        public UrlBuilder RequestData(string key, string value)
        {
            if (!string.IsNullOrEmpty(value)) _requestData.Add(key, value);
            return this;
        }

        public string Build()
        {
            var data = new StringBuilder();
            foreach (var kv in _requestData.Where(kv => !string.IsNullOrEmpty(kv.Value)))
                data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
            var queryString = data.ToString();

            var url = _baseUrl + "?" + queryString;
            var signData = queryString;
            if (signData.Length > 0) signData = signData.Remove(data.Length - 1, 1);
            var vnpSecureHash = HmacSha512(_vnpHashSecret, signData);
            url += "vnp_SecureHash=" + vnpSecureHash;
            
            return url;
        }
        
        private class VnPayCompare : IComparer<string>
        {
            public int Compare(string? x, string? y)
            {
                if (x == y) return 0;
                if (x == null) return -1;
                if (y == null) return 1;
                var vnpCompare = CompareInfo.GetCompareInfo("en-US");
                return vnpCompare.Compare(x, y, CompareOptions.Ordinal);
            }
        }
    }

    public static string HmacSha512(string key, string inputData)
    {
        if (inputData == null) throw new ArgumentNullException(nameof(inputData));
        var hash = new StringBuilder();
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var inputBytes = Encoding.UTF8.GetBytes(inputData);
        using (var hmac = new HMACSHA512(keyBytes))
        {
            var hashValue = hmac.ComputeHash(inputBytes);
            foreach (var theByte in hashValue) hash.Append(theByte.ToString("x2"));
        }

        return hash.ToString();
    }
}
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace GameCenterAPI.Application.Helpers
{
    static public class CustomEncoders
    {
        public static string UrlEncode(this string value)
            => WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(value));


        public static string UrlDecode(this string value)
            => Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(value));
    }
}

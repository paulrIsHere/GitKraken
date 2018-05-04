using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ivey.Utils
{
    public static partial class Extentions
    {

        
        public static bool IsNumeric(this string value)
        {
            long retNum;
            return long.TryParse(value, System.Globalization.NumberStyles.Integer, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        }

        public static string RemoveSpecialCharacters(this string value)
        {

            value = value.Replace("\n", " ");

            value = value.Where(character => !char.IsControl(character)).Aggregate(new StringBuilder(), (builder, character) => builder.Append(character)).ToString();

            StringBuilder sb = new StringBuilder();
            foreach (char c in value)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || c == ' ')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

		public static string ToStringNullSafe(this object value)
        {
            return (value ?? string.Empty).ToString();
        }

        /// <summary>
        /// Returns TRUE if the string contains any HTML tags or &lt; or &gt;
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool ContainsHtmlMarkup(this string value)
        {
            var retCheck = false;
            if (!string.IsNullOrWhiteSpace(value))
            {
                string pattern = @"<(.|\n)*?>";
                retCheck = (Regex.IsMatch(value, pattern) || value.Contains("&lt;") || value.Contains("&gt;"));
            }

            return retCheck;
        }

        /// <summary>
        /// Returns a string replacing SOME HTML tags from an Orbis feed string
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string CleanOrbisFeedData(this string value)
        {
            return (string.IsNullOrWhiteSpace(value)) ? string.Empty : value.Replace("<br>", "\n").Replace("<br/>", "\n").Replace("<br />", "\n");
        }

        public static string Decrypt(this string value)
        {
            byte[] data = Convert.FromBase64String(value);
            return System.Text.ASCIIEncoding.ASCII.GetString(data);
        }

    }
}

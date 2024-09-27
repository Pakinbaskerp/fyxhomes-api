using System;
using System.Text.RegularExpressions;

namespace Entities.Util
{
    /// <summary>
    /// Class <c>StringExtensions</c> an extension class consists of string manipulation
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// This method converts the string to snake case.
        /// </summary>
        /// <param name="input">The string input to be converted as snake case.</param>
        /// <returns>A string convered to snake case.</returns>
        /// <example>
        /// Use this method with any string to be converted as snake case
        /// Usage: string.ConvertToSnakeCase()
        /// For e.g. The given input string is PersonName will be converted to 
        /// person_name.
        /// </example>
        public static string ConvertToSnakeCase(this string input)
        {
            if (System.String.IsNullOrEmpty(input)) { return input; }

            Match startUnderscores = Regex.Match(input, @"^_+");
            return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }

        /// <summary>
        /// This method converts the string to pascal case.
        /// </summary>
        /// <param name="input">The string input to be converted as pascal case.</param>
        /// <returns>A string convered to pascal case.</returns>
        /// <example>
        /// Use this method with any string to be converted as pascal case
        /// Usage: string.ConvertToPascalCase()
        /// For e.g. The given input string is person_name will be converted to 
        /// PersonName.
        /// </example>
        public static string ConvertToPascalCase(this string input)
        {
            if (System.String.IsNullOrEmpty(input)) { return input; }

            if (!input.Contains(" ")) {
                input = Regex.Replace(input, "(?<=[a-z])(?=[A-Z])", " ");
            }
            string s = System.Globalization.CultureInfo.CurrentCulture.
                    TextInfo.ToTitleCase(input.ToLower()).Replace(" ", "").Replace("_", "");

            return s;
        }

        /// <summary>
        /// This method converts the string to 9 digit hex string. This is basically used to convert the Electornic serial number of the device to Scanner Sr Number
        /// </summary>
        /// <param name="input">The string input to be converted as hex.</param>
        /// <returns>A string convered to hex.</returns>
        public static string ConvertToHex(this string input)
        {
            if (System.String.IsNullOrEmpty(input)) { return "-"; }
            string hexString = string.Format("{0:X}", UInt64.Parse(input));
            if (hexString.Length != 9)
            {
                int length = 9 - hexString.Length;
                for (int i = 0; i < length; i++)
                {
                    hexString = "0" + hexString;
                }
            }
            return hexString;
        }
    }
}
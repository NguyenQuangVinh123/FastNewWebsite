using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FastNews.Common
{
    public class ConvertToUnSign
    {
        public static string utf8Convert(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            var charsToRemove = new string[] { "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", ",", ".", ":", ";", "<", ">", "'", "\"", "`", "~", "[", "]", "{", "}", "+", "=", "_", "|", "\\", "/", "?" };

            string result = regex.Replace(temp, String.Empty).Replace('\u0111', 'd')
                .Replace('\u0110', 'D').ToLower();
            // Bo ky tu dac biet
            // Ngoai tru "-"
            for (int i = 0; i < charsToRemove.Length; i++)
            {
                result = result.Replace("--", "-").Replace(charsToRemove[i], string.Empty);
            }
            return result;
        }
    }
}
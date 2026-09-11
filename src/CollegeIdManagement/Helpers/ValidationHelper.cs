using System;
using System.Text.RegularExpressions;

namespace CollegeIdManagement.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return true; // Optional field
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
        }

        public static bool IsValidMobile(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile)) return false;
            var clean = mobile.Replace(" ", "").Replace("-", "").Replace("+91", "");
            return clean.Length >= 10 && Regex.IsMatch(clean, @"^\d+$");
        }

        public static bool IsValidPinCode(string pinCode)
        {
            if (string.IsNullOrWhiteSpace(pinCode)) return true;
            return Regex.IsMatch(pinCode, @"^\d{6}$");
        }
    }
}

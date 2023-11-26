using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WPF_KnowItAll_App.Data_layer.Models;

namespace WPF_KnowItAll_App.App_layer.Services
{
    public static class DataValidatorService
    {
        public static bool IsPasswordValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            return true;
        }

        public static bool IsNameValid(string name)
        {
            if(string.IsNullOrWhiteSpace(name) || name.Any(char.IsDigit))
            {
                return false;
            }

            return true;
        }

        public static bool IsEmailValid(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";

            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        public static bool IsEmailRegistered(string email, IEnumerable<User> users)
        {
            return users.Select(u => u.Email).Contains(email);
        }
    }
}

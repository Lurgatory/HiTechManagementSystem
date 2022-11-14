using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Hi_Tech_Management_System.Business;
using System.IO;

namespace Hi_Tech_Management_System.Validation
{
    public class Validator
    {
        public static bool IsValidedId(string id)
        {
            if (Regex.IsMatch(id, @"^\d{4}$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidedNumber(string id)
        {
            if (Regex.IsMatch(id, @"\d"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidPostalCode(string post)
        {
            if (Regex.IsMatch(post, @"\w{3}?[-.\s]?\w{3}"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidPhoneNumber(string phone)
        {
            if (Regex.IsMatch(phone, @"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidedName(string input)
        {
            if ((Regex.IsMatch(input, @"^[a-zA-Z]+(?:[\s.]+[a-zA-Z]+)*$")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValideDate(string date)
        {
            if (Regex.IsMatch(date, @"\d"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
    }
}

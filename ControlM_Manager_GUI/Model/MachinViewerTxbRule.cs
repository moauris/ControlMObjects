using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ControlM_Manager_GUI.Model
{
    class MachinViewerTxbRule : ValidationRule
    {
        /// <summary>
        /// The Regular Expression for Validation
        /// </summary>
        public string RegexString { get; set; }
        public string ErrorMessage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            Regex rxValid = new Regex(RegexString);
            if (!rxValid.IsMatch((string)value))
            {
                
                return new ValidationResult(false
                    , ErrorMessage);
            }

            return ValidationResult.ValidResult;
            
        }
    }

    class MachineViewerIPRule : ValidationRule
    {
        public enum IP
        {
            v4 = 4,
            v6 = 16
        }
        public IP CheckMode { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            IPAddress TestedIP;
            try
            {
                TestedIP = IPAddress.Parse((string)value);
                
                if (TestedIP.GetAddressBytes().Length != (int)CheckMode)
                {
                    return new ValidationResult(false
                        , $"Not an IP{CheckMode} Address");
                }
            }
            catch (Exception e)
            {
                return new ValidationResult(false, e.Message);
            }
            return ValidationResult.ValidResult;
        }
    }
}

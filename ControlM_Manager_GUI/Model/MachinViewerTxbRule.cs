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
        public string Tag { get; set; }
        public string ErrorMessage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            Regex rxValid = new Regex(RegexString);
            if (!rxValid.IsMatch((string)value))
            {
                TextboxValidationLost();
                return new ValidationResult(false
                    , ErrorMessage);
            }
            TextboxValidated();
            return ValidationResult.ValidResult;
            
        }

        public event EventHandler TextboxValidatedEventHandler;
        protected virtual void TextboxValidated()
        {
            TextboxValidatedEventHandler?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler TextboxValidationLostEventHandler;
        protected virtual void TextboxValidationLost()
        {
            TextboxValidationLostEventHandler?.Invoke(this, EventArgs.Empty);
        }
    }

    class MachineViewerIPRule : MachinViewerTxbRule
    {
        public enum IP
        {
            v4 = 4,
            v6 = 16
        }
        public IP CheckMode { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            //Setting an empty IPAddress type
            IPAddress TestedIP;
            try
            {
                //Try parse it, if can be parsed, check if they fit ipv6 or ipv4.
                TestedIP = IPAddress.Parse((string)value);
                
                if (TestedIP.GetAddressBytes().Length != (int)CheckMode)
                {
                    TextboxValidationLost();
                    return new ValidationResult(false
                        , $"Not an IP{CheckMode} Address");
                }
            }
            catch (Exception e)
            {
                TextboxValidationLost();
                return new ValidationResult(false, e.Message);
            }
            TextboxValidated();
            return ValidationResult.ValidResult;
        }
    }
}

/*
  // Added By Shahzad Ali: For Validating Basic Controls on Windows Forms
 */

using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Altodownloading
{
    class ValidationForm
    {
        #region "Validation"
        /// <summary>
        /// Test for empty Image in the Picture box and
        /// return the results
        /// </summary>
        /// <returns>boolean</returns>
        public static bool EmptyPictureBoxIsValid(ref PictureBox pbxEmptyImage, ref Button btnOpen, ref ErrorProvider erpCntl)
        {
            RefreshErrorProvider(ref erpCntl);
            if (pbxEmptyImage.Image == null)
            {
                erpCntl.SetError(btnOpen, "Please select Map.");
                pbxEmptyImage.Focus();
                return true;
            }
            else
            {
                erpCntl.SetError(btnOpen, "");
                return false;
            }
        }



        /// <summary>
        /// Test for empty string in the text box and
        /// return the results
        /// </summary>
        /// <returns>boolean</returns>
        public static bool EmptyStringIsValid(ref TextBox txtEmptyString, ref ErrorProvider erpCntl)
        {
            RefreshErrorProvider(ref erpCntl);
            if (txtEmptyString.Text == string.Empty)
            {
                erpCntl.SetError(txtEmptyString, "This field must contain text");
                txtEmptyString.Focus();
                return true;
            }
            else
            {
                erpCntl.SetError(txtEmptyString, "");
                return false;
            }
        }

        /// <summary>
        /// Test for non-alpha values in the text box and
        /// return the results
        /// </summary>
        /// <returns>boolean</returns>
        public static bool AlphaNumericStringIsValid(ref TextBox txtAlphaNumericString, ref ErrorProvider erpCntl)
        {
            RefreshErrorProvider(ref erpCntl);
            // Make sure the string is not empty first
            if (txtAlphaNumericString.Text == string.Empty)
            {
                erpCntl.SetError(txtAlphaNumericString, "This field must contain only alphanumerics");
                txtAlphaNumericString.Focus();
                return true;
            }

            // check for alphanumeric values
            char[] testArr = txtAlphaNumericString.Text.ToCharArray();
            bool testBool = false;

            for (int i = 0; i < testArr.Length; i++)
            {
                if (!char.IsLetter(testArr[i]) && !char.IsNumber(testArr[i]))
                {
                    testBool = true;
                    break;
                }
            }
            if (testBool == true)
            {
                erpCntl.SetError(txtAlphaNumericString, "This field must contain only alphanumerics");
                txtAlphaNumericString.Focus();
            }
            else
            {
                erpCntl.SetError(txtAlphaNumericString, "");
            }
            return testBool;
        }

        /// <summary>
        /// Test for non-alpha values in the text box and
        /// also make sure that the textbox is not empty
        /// </summary>
        /// <returns>boolean</returns>
        public static bool AlphaStringIsValid(ref TextBox txtAlphaString, ref ErrorProvider erpCntl)
        {
            RefreshErrorProvider(ref erpCntl);
            // first make sure the textbox contains something
            if (txtAlphaString.Text == string.Empty)
            {
                erpCntl.SetError(txtAlphaString, "This field must contain only alphas");
                txtAlphaString.Focus();
                return true;
            }

            // test each character in the textbox
            char[] testArr = txtAlphaString.Text.ToCharArray();
            bool testBool = false;

            for (int i = 0; i < testArr.Length; i++)
            {
                if (!char.IsLetter(testArr[i]))
                {
                    testBool = true;
                    break;
                }
            }

            if (testBool == true)
            {
                erpCntl.SetError(txtAlphaString, "This field must contain only alphas");
                txtAlphaString.Focus();
            }
            else
            {
                erpCntl.SetError(txtAlphaString, "");
            }

            return testBool;
        }

        /// <summary>
        /// Test to see that the textbox contains a minimum number
        /// of characters
        /// </summary>
        /// <returns>boolean</returns>
        public static bool MinLengthTestIsValid(ref TextBox txtMinLengthTest, ref ErrorProvider erpCntl)
        {
            RefreshErrorProvider(ref erpCntl);
            if (txtMinLengthTest.Text.Trim() == String.Empty)
            {
                erpCntl.SetError(txtMinLengthTest, "This field must contain at least 3 characters");
                txtMinLengthTest.Focus();
                return true;
            }

            char[] testArr = txtMinLengthTest.Text.ToCharArray();
            bool testBool = true;

            if (testArr.Length <= 3)
            {
                testBool = false;
            }

            if (testBool == true)
            {
                erpCntl.SetError(txtMinLengthTest, "This field must contain at least 3 characters");
                txtMinLengthTest.Focus();
            }
            else
            {
                erpCntl.SetError(txtMinLengthTest, "");
            }

            return testBool;
        }

        /// <summary>
        /// Test for non-numeric values in the text box and
        /// also make sure the textbox is not empty
        /// </summary>
        /// <returns>boolean</returns>
        public static bool NumericStringIsValid(ref TextBox txtNumericString, ref ErrorProvider erpCntl)
        {
            RefreshErrorProvider(ref erpCntl);
            if (txtNumericString.Text == string.Empty)
            {
                erpCntl.SetError(txtNumericString, "This field must contain only numbers");
                txtNumericString.Focus();
                return true;
            }

            char[] testArr = txtNumericString.Text.ToCharArray();
            bool testBool = false;

            for (int i = 0; i < testArr.Length; i++)
            {
                if (!char.IsNumber(testArr[i]))
                {
                    testBool = true;
                    break;
                }
            }

            if (testBool == true)
            {
                erpCntl.SetError(txtNumericString, "This field must contain only numbers");
                txtNumericString.Focus();
            }
            else
            {
                erpCntl.SetError(txtNumericString, "");
            }

            return testBool;

        }

        /// <summary>
        /// Test for numeric values between 50 and 100 in the text box and
        /// return the results
        /// </summary>
        /// <returns>boolean</returns>
        public static bool RangeValidationIsValid(ref TextBox txtRangeValidation, ref ErrorProvider erpCntl)
        {
            RefreshErrorProvider(ref erpCntl);
            int tmpVal = 0;

            try
            {
                tmpVal = Convert.ToInt32(txtRangeValidation.Text);
            }
            catch { }

            bool testBool = true;

            if (tmpVal < 100 && tmpVal > 50)
            {
                testBool = false;
            }

            if (testBool == true)
            {
                erpCntl.SetError(txtRangeValidation, "This field must contain a number between 50 and 100");
                txtRangeValidation.Focus();
                //   this.errorProvider2.SetError(txtRangeValidation, "");
            }
            else
            {
                erpCntl.SetError(txtRangeValidation, "");
                //   this.errorProvider2.SetError(txtRangeValidation, "The value is between 50 and 100");
            }

            return testBool;
        }

        /// <summary>
        /// Test for a regex expression match in the text box and
        /// return the results - the example uses a regular
        /// expression used to validate a phone number
        /// </summary>
        /// <returns>boolean</returns>
        public static bool RegExPhoneNoIsValid(ref TextBox txtRegExString, ref ErrorProvider erpCntl)
        {

            RefreshErrorProvider(ref erpCntl);

            if (txtRegExString.Text == string.Empty)
            {
                erpCntl.SetError(txtRegExString, "This field must contain a phone number (123-123-1234)");
                txtRegExString.Focus();
                return true;
            }

            Regex TheRegExpression;
            string TheTextToValidate;
            // string TheRegExTest = @"[1-9]\d{2}-\d{3}-\d{4}";
            // string TheRegExTest = @"(^\d{2}$)(^-\d{3}$)(^-\d{4}$)";
            string TheRegExTest = @"^\(?\d{3}\)?-?\d{3}-?\d{4}$";

            TheTextToValidate = txtRegExString.Text;
            TheRegExpression = new Regex(TheRegExTest);

            // test text with expression
            if (TheRegExpression.IsMatch(TheTextToValidate))
            {
                erpCntl.SetError(txtRegExString, "");
                return false;
            }
            else
            {
                erpCntl.SetError(txtRegExString, "This field must contain a phone number (123-123-1234)");
                txtRegExString.Focus();
                return true;
            }

        }

        /// <summary>
        /// Test for special character values in the textbox
        /// </summary>
        /// <returns>boolean</returns>
        public static bool SpecialCharsIsValid(ref TextBox txtSpecialChars, ref ErrorProvider erpCntl)
        {
            RefreshErrorProvider(ref erpCntl);
            if (txtSpecialChars.Text == string.Empty)
            {
                erpCntl.SetError(txtSpecialChars, "This field must contain only special characters");
                txtSpecialChars.Focus();
                return true;
            }

            char[] arr = new char[26];
            arr[0] = '.';
            arr[1] = '/';
            arr[2] = '\\';
            arr[3] = '|';
            arr[4] = '}';
            arr[5] = '{';

            arr[6] = '[';
            arr[7] = ':';
            arr[8] = ';';
            arr[9] = '+';
            arr[10] = '=';
            arr[11] = '_';

            arr[12] = ')';
            arr[13] = '(';
            arr[14] = '*';
            arr[15] = '&';
            arr[16] = '^';
            arr[17] = '%';

            arr[18] = '$';
            arr[19] = '#';
            arr[20] = '@';
            arr[21] = '!';
            arr[22] = '~';
            arr[23] = '`';
            arr[24] = '-';
            arr[25] = '"';
            Boolean testBool = true;
            char[] strArr = txtSpecialChars.Text.ToCharArray();
            for (Int32 i = 0; i < strArr.Length; i++)
            {
                testBool = true;
                for (Int32 j = 0; j < arr.Length; j++)
                {
                    if (strArr[i] == arr[j])
                    {
                        testBool = false;
                        break;
                    }
                }
                if (testBool == true)
                {
                    break;
                }
            }

            if (testBool == true)
            {
                erpCntl.SetError(txtSpecialChars, "This field must contain only special characters");
                txtSpecialChars.Focus();
            }
            else
            {
                erpCntl.SetError(txtSpecialChars, "");
            }

            return testBool;
        }

        static private void RefreshErrorProvider(ref ErrorProvider erpCntl)
        {
            erpCntl.Clear();
        }
        #endregion
    }
}

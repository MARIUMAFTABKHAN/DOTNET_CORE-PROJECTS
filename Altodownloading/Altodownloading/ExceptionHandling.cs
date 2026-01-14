using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Altodownloading
{
    public static  class ExceptionHandling
    {
        public  static void Application_ThreadException( Exception e)
        {
            try
            {
                if (e is System.Data.SqlClient.SqlException)
                {
                    SqlException ex = (SqlException)e;
                    if (ex.Number == 547)
                        ErrorMessage("Record cannot be deleted or changed " +
                                       "as it is being used somewhere else");

                    else if (ex.Number == 2627)
                        ErrorMessage("Record cannot be saved, as another " +
                                     "record with this key already exists");

                    else
                        ErrorMessage(ex.Message.ToString());
                }
                else
                    ErrorMessage("System Error :" + e.Message.ToString());

            }
            catch (Exception ex)
            {
                ErrorMessage("System Error: Reporting to log");
            }
        }

        private static void ErrorMessage(string p)
        {
            MessageBox.Show(p, "Informaiton", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
        }
    }
}

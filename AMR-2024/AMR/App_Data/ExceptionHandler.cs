using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMR
{
    public static class ExceptionHandler
    {
        public static string GetException(Exception ex)
        {
            string excep = "";
            try
            {

                if (ex.InnerException.InnerException.Message.Contains("Violation of PRIMARY KEY"))
                {
                    // switch (ex.Data.err .InnerException.Message.  
                    excep = "Violation of primary key.";
                }
                if (ex.InnerException.InnerException.Message.Contains("FOREIGN KEY"))
                {
                    // switch (ex.Data.err .InnerException.Message.  
                    excep = "Foreign key constraint.";
                }
                else if (ex.InnerException.InnerException.Message.Contains("conflicted "))
                {
                    excep = "Record is in used and can't be deleted.";
                }
                else if (ex.InnerException.InnerException.Message.Contains("unique index"))
                {
                    excep = "Duplicate record found, please check suppliers";
                }
                else if (ex.InnerException.InnerException.Message.Contains("duplicate"))
                {
                    excep = "Record already exists.";
                }               
                else
                {
                    excep = ex.InnerException.Message;
                }
            }
            catch (Exception)
            {
                excep = "Unknown errror, Please contact administrator....";
            }

            return excep;
        }
    }
}
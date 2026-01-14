/***************************************************************************************
'*
'*      Class Name         :    DepartmentDB
'*      Class Description  :    Provides the Entity Level Functionality of table tblDepartment
'*      Task Owned By      :    Shahzad Ali
'*      Creation Date      :    30-Dec-2010 11:45 AM
'***************************************************************************************/

using System;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for DepartmentDB
/// </summary>
namespace Altodownloading
{
    public class DepartmentDB
    {

        public DepartmentDB()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable GetAllDepartment()
        {
            
            SqlConnection dbConn = new SqlConnection(Helper.GetJTSDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllDepartment", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;


            DataTable dtDepartment = new DataTable("Department");

            dbAdapter.Fill(dtDepartment);

            return dtDepartment;
        }
    }
}
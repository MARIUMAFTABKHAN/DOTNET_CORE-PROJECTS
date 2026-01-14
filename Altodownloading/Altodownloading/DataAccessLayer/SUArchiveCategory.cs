using System;
using System.Data;
using System.Data.SqlClient;

namespace Altodownloading.DataAccessLayer
{
    class SUArchiveCategory
    {
        public DataTable GetAllRchiveCategory(object categoryID, object category, object createdBy, object createdOn, object editedBy, object editedOn)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllArchiveCategoryForDesktop", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

           

            DataTable dtRchiveCategory = new DataTable("ArchiveCategoryDB");

            dbAdapter.Fill(dtRchiveCategory);

            return dtRchiveCategory;
        }
    }



}

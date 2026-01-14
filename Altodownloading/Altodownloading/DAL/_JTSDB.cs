using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

/// <summary>
/// Summary description for JTSDB
/// </summary>
namespace Altodownloading
{
    public class _JTSDB
    {
        public _JTSDB()
        {
        }
        #region Methods

        public DataTable ValidateUser(String Username, String Password)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetJTSDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_ValidateUser", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            /*Input Parameters*/
            dbAdapter.SelectCommand.Parameters.AddWithValue("@p_strUsername", Username);
            dbAdapter.SelectCommand.Parameters.AddWithValue("@p_strPassword", Password);

            DataTable dtUser = new DataTable("User");

            dbAdapter.Fill(dtUser);

            return dtUser;
        }

        public DataTable GetAllChannel()
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetJTSDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllChannelForDesktop", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtChannel = new DataTable("Channel");
            dbAdapter.Fill(dtChannel);
            return dtChannel;
        }
    
        public DataTable GetAllChannel(object channelId, object name, object languageId, object createdByUserId, object createdOn, object lastModifiedByUserId, object lastModifiedOn, object isActive)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetJTSDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllChannel", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (channelId != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nChannelId", channelId);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nChannelId", System.DBNull.Value);
            }
            if (name != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_strName", name);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_strName", System.DBNull.Value);
            }
            if (languageId != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nLanguageId", languageId);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nLanguageId", System.DBNull.Value);
            }
            if (createdByUserId != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nCreatedByUserId", createdByUserId);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nCreatedByUserId", System.DBNull.Value);
            }
            if (createdOn != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_dtCreatedOn", createdOn);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_dtCreatedOn", System.DBNull.Value);
            }
            if (lastModifiedByUserId != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nLastModifiedByUserId", lastModifiedByUserId);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nLastModifiedByUserId", System.DBNull.Value);
            }
            if (lastModifiedOn != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_dtLastModifiedOn", lastModifiedOn);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_dtLastModifiedOn", System.DBNull.Value);
            }
            if (isActive != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_bIsActive", isActive);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_bIsActive", System.DBNull.Value);
            }

            DataTable dtChannel = new DataTable("Channel");

            dbAdapter.Fill(dtChannel);

            return dtChannel;
        }
        public DataTable GetReportersFromBureau(Int32 nBureauId)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetJTSDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetRepotersByBureauId", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nBureauId", nBureauId);


            DataTable dtUser = new DataTable("Reporters");

            dbAdapter.Fill(dtUser);

            return dtUser;
        }
        public DataTable GetCameramanFromBureau(Int32 nBureauId)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetJTSDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetCameramanFromBureau", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nBureauId", nBureauId);


            DataTable dtUser = new DataTable("Cameraman");

            dbAdapter.Fill(dtUser);

            return dtUser;
        }
        public DataTable GetAllActiveBureauForCombo()
        {
            SqlConnection dbConn = new SqlConnection(Helper.GetJTSDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllBureauForCombo", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable("CFG");
            dbAdapter.Fill(dt);
            return dt;
        }
        public DataTable GetAllUser(object userId, object fullName, object userName, object password, object email, object cityId, object roleId, object userPriority, object offDay, object isActive, object createdByUserId, object createdOn, object lastModifiedByUserId, object lastModifiedOn)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetJTSDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllUser", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (userId != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nUserId", userId);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nUserId", System.DBNull.Value);
            }
            if (fullName != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_strFullName", fullName);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_strFullName", System.DBNull.Value);
            }
            if (userName != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_strUserName", userName);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_strUserName", System.DBNull.Value);
            }
            if (password != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_strPassword", password);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_strPassword", System.DBNull.Value);
            }
            if (email != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_strEmail", email);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_strEmail", System.DBNull.Value);
            }
            if (cityId != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nCityId", cityId);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nCityId", System.DBNull.Value);
            }
            if (roleId != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nRoleId", roleId);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nRoleId", System.DBNull.Value);
            }
            if (userPriority != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_bUserPriority", userPriority);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_bUserPriority", System.DBNull.Value);
            }
            if (offDay != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_strOffDay", offDay);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_strOffDay", System.DBNull.Value);
            }
            if (isActive != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_bIsActive", isActive);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_bIsActive", System.DBNull.Value);
            }
            if (createdByUserId != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nCreatedByUserId", createdByUserId);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nCreatedByUserId", System.DBNull.Value);
            }
            if (createdOn != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_dtCreatedOn", createdOn);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_dtCreatedOn", System.DBNull.Value);
            }
            if (lastModifiedByUserId != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nLastModifiedByUserId", lastModifiedByUserId);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nLastModifiedByUserId", System.DBNull.Value);
            }
            if (lastModifiedOn != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_dtLastModifiedOn", lastModifiedOn);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_dtLastModifiedOn", System.DBNull.Value);
            }

            DataTable dtUser = new DataTable("User");

            dbAdapter.Fill(dtUser);

            return dtUser;
        }
        public DataTable GetAllReporterFromJTS()
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetJTSDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetALLRepoterFromJTS", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable("Reporter");

            dbAdapter.Fill(dt);

            return dt;
        }
        public DataTable GetAllCity(object cityId, object name, object bureauId, object createdByUserId, object createdOn, object lastModifiedByUserId, object lastModifiedOn, object isActive)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetJTSDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllCity", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (cityId != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nCityId", cityId);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nCityId", System.DBNull.Value);
            }
            if (name != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_strName", name);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_strName", System.DBNull.Value);
            }
            if (bureauId != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nBureauId", bureauId);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nBureauId", System.DBNull.Value);
            }
            if (createdByUserId != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nCreatedByUserId", createdByUserId);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nCreatedByUserId", System.DBNull.Value);
            }
            if (createdOn != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_dtCreatedOn", createdOn);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_dtCreatedOn", System.DBNull.Value);
            }
            if (lastModifiedByUserId != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nLastModifiedByUserId", lastModifiedByUserId);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nLastModifiedByUserId", System.DBNull.Value);
            }
            if (lastModifiedOn != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_dtLastModifiedOn", lastModifiedOn);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_dtLastModifiedOn", System.DBNull.Value);
            }
            if (isActive != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_bIsActive", isActive);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_bIsActive", System.DBNull.Value);
            }

            DataTable dtCity = new DataTable("City");

            dbAdapter.Fill(dtCity);

            return dtCity;
        }
        public DataTable GetAllMediaCategory(object mediaCategory, object category)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllMediaCategory", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (mediaCategory != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nMediaCategory", mediaCategory);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_nMediaCategory", System.DBNull.Value);
            }
            if (category != null)
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_strCategory", category);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.AddWithValue("@p_strCategory", System.DBNull.Value);
            }

            DataTable dtEdiaCategory = new DataTable("EdiaCategory");

            dbAdapter.Fill(dtEdiaCategory);

            return dtEdiaCategory;
        }

        #endregion

    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.Entity.Core.EntityClient;
using System.Data;

namespace CDSN
{
    [WebService(Namespace = "http://cdsn.example.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService] // ✅ Needed for AJAX calls
    public class CPRService : WebService
    {
        public class MapPoint
        {
            public double Lat { get; set; }
            public double Lng { get; set; }
            public string InfoWindowText { get; set; }
            public string IconImage { get; set; }
        }

        [WebMethod(EnableSession = true)]
        public string TestSession()
        {
            if (HttpContext.Current.Session["userid"] == null)
            {
                return "Session userid is NULL";
            }
            return $"Session userid = {HttpContext.Current.Session["userid"]}";
        }

        [WebMethod(EnableSession = true)]
        public List<MapPoint> GetOperatorMap(int operatorId)
        {
            // ✅ Session check
            if (HttpContext.Current.Session["userid"] == null)
            {
                throw new InvalidOperationException("Authentication failed.");
            }

            List<MapPoint> points = new List<MapPoint>();

            // 🔹 Convert EF connection string to plain SQL connection string
            var entityBuilder = new EntityConnectionStringBuilder(
                ConfigurationManager.ConnectionStrings["CDSEntities"].ConnectionString
            );
            string sqlConnectionString = entityBuilder.ProviderConnectionString;


            using (SqlConnection con = new SqlConnection(sqlConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_GetMergedGoogleData", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OperatorId", operatorId);
                con.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var iconFile = rdr["IconImage"].ToString().Trim();
                        if (!iconFile.StartsWith("/", StringComparison.OrdinalIgnoreCase))
                        {
                            iconFile = "/" + iconFile;
                        }
                        if (!iconFile.StartsWith("/Content/Images/", StringComparison.OrdinalIgnoreCase))
                        {
                            iconFile = "/Content/" + iconFile.TrimStart('/');
                        }

                        points.Add(new MapPoint
                        {
                            Lat = Convert.ToDouble(rdr["Lat"]),
                            Lng = Convert.ToDouble(rdr["Lng"]),
                            IconImage = iconFile,
                             InfoWindowText = rdr["InfoWindowText"].ToString()
                            //InfoWindowText = $"<a href='#' onclick='showOperatorsModal({rdr["SubAreaId"]})'>{rdr["SubAreaName"]}</a>"

                        });

                    }
                }
            }
            return points;
        }

        [WebMethod(EnableSession = true)]
        public List<OperatorInfo> GetOperatorsBySubArea(int subAreaId)
        {
            if (HttpContext.Current.Session["userid"] == null)
                throw new InvalidOperationException("Authentication failed.");

            List<OperatorInfo> list = new List<OperatorInfo>();

            var entityBuilder = new EntityConnectionStringBuilder(
                ConfigurationManager.ConnectionStrings["CDSEntities"].ConnectionString
            );
            string sqlConnectionString = entityBuilder.ProviderConnectionString;

            using (SqlConnection con = new SqlConnection(sqlConnectionString))
            using (SqlCommand cmd = new SqlCommand("getoperatorbysubarea", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SubAreaId", subAreaId);
                con.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new OperatorInfo
                        {
                            OperatorId = Convert.ToInt32(rdr["OperatorId"]),
                            OperatorName = rdr["OperatorName"].ToString(),
                            SubAreaName = rdr["SubAreaName"].ToString(),
                            Subscribers = rdr["Subscribers"] != DBNull.Value ? Convert.ToInt32(rdr["Subscribers"]) : 0
                        });
                    }
                }
            }

            return list;
        }

        public class OperatorInfo
        {
            public int OperatorId { get; set; }
            public string OperatorName { get; set; }
            public string SubAreaName { get; set; }
            public int Subscribers { get; set; }
        }

        [WebMethod(EnableSession = true)]
        public OperatorProprietorInfo GetOperatorProprietorInfo(int operatorId)
        {
            if (HttpContext.Current.Session["userid"] == null)
                throw new InvalidOperationException("Authentication failed.");

            var entityBuilder = new EntityConnectionStringBuilder(
                ConfigurationManager.ConnectionStrings["CDSEntities"].ConnectionString
            );
            string sqlConnectionString = entityBuilder.ProviderConnectionString;

            OperatorProprietorInfo info = null;

            using (SqlConnection con = new SqlConnection(sqlConnectionString))
            using (SqlCommand cmd = new SqlCommand("usp_GetOperatorProperitorInfo", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OperatorInfo", operatorId);
                con.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        info = new OperatorProprietorInfo
                        {
                            OperatorName = rdr["Name"].ToString(),
                            Address = rdr["Address"].ToString(),
                            Cell = rdr["Cell"].ToString(),
                            LandLine = rdr["LandLine"].ToString(),
                            LicCategory = rdr["LicCategory"].ToString(),
                            LicReviewDate = Convert.ToDateTime(rdr["LicReviewDate"]).ToString("dd/MM/yyyy"),
                            LicStatus = rdr["Lic_Status"].ToString(),
                            ProprietorName = rdr["PropName"].ToString(),
                            CNIC = rdr["CNIC"].ToString(),
                            Email = rdr["Email"].ToString(),
                            ContactNo = rdr["ContactNo"].ToString(),
                            CellNo = rdr["CellNo"].ToString(),
                            CityName = rdr["CityName"].ToString(),
                            PropAddress = rdr["pAddress"].ToString()
                        };
                    }
                }
            }
            return info;
        }

        public class OperatorProprietorInfo
        {
            public string OperatorName { get; set; }
            public string Address { get; set; }
            public string Cell { get; set; }
            public string LandLine { get; set; }
            public string LicCategory { get; set; }
            public string LicReviewDate { get; set; }
            public string LicStatus { get; set; }
            public string ProprietorName { get; set; }
            public string CNIC { get; set; }
            public string Email { get; set; }
            public string ContactNo { get; set; }
            public string CellNo { get; set; }
            public string CityName { get; set; }
            public string PropAddress { get; set; }
        }

    }
}

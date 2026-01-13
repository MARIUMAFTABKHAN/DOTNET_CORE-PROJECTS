using System;
using System.Data;
using System.Data.SqlClient;

namespace CDSN
{
    /// <summary>
    /// Minimal VS2022 version of SubAreaDB.
    /// Focuses on the method your page actually uses: GetOperatorList(lat,lng).
    /// Uses ADO.NET and your EF connection string (CDSEntities).
    /// </summary>
    public class SubAreaDB
    {
        private readonly string _connString;

        // Preferred: pass your EF context so we read its connection string
        public SubAreaDB(CDSEntities ef)
        {
            if (ef == null) throw new ArgumentNullException(nameof(ef));
            _connString = ef.Database.Connection.ConnectionString;
        }

        // Optional: if you ever want to construct without EF
        public SubAreaDB(string connectionString)
        {
            _connString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        /// <summary>
        /// Replacement for old: GetOperatorList(DBManager db, string lat, string lng)
        /// Calls [sp_GetOperatorListbyLatLng] and returns the first result table.
        /// </summary>
        public DataTable GetOperatorList(string lat, string lng)
        {
            var dt = new DataTable("OperatorList");

            using (var cn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand("sp_GetOperatorListbyLatLng", cn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Preserve old behavior (strings passed through as-is)
                cmd.Parameters.Add("@lat", SqlDbType.VarChar, 50).Value = (object)(lat ?? string.Empty);
                cmd.Parameters.Add("@lng", SqlDbType.VarChar, 50).Value = (object)(lng ?? string.Empty);

                cn.Open();
                da.Fill(dt);
            }

            return dt;
        }

        // --- If later you need the other old methods (Insert/Update/AllSubAreas, etc),
        // add them here using the same pattern: (SqlConnection, SqlCommand, parameters, Execute/Fill).
        // I’ve omitted them now because your page only uses GetOperatorList(...).
    }
}

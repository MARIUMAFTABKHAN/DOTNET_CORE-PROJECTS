using System;
using System.Data;
using System.Data.SqlClient;

namespace Altodownloading
{
    public class ArchiveDB
    {
        #region  Attributes 

		private Int64 m_lArchiveID = 0;

		private Int64 m_lMediaNo = 0;

		private Int32 m_shChannelID = 0;

		private Int32 m_shCategoryID = 0;

		private Int32 m_nMediaTypeID = 0;

		private Int32 m_nClassificationID = 0;

		private DateTime m_dtEntryDate = Helper.GetDefaultDateTime();

		private DateTime m_dtShootDate = Helper.GetDefaultDateTime();

		private Int32 m_nSourceID = 0;

		private Int32 m_nFootageTypeID = 0;

		private String m_strFileName = String.Empty;

		private String m_strJTSFileName = String.Empty;

		private String m_strJTSTicketNo = String.Empty;

		private Int32 m_nJTSReporterID = 0;

		private Int32 m_nJTSCameramanID = 0;

		private Int32 m_nJTSBureau = 0;

		private String m_strJTSJobType = String.Empty;

		private String m_strJTSCreatedBy = String.Empty;

		private String m_strJTSSlug = String.Empty;

		private String m_strJTSDuration = String.Empty;

		private String m_strRackNo = String.Empty;

		private String m_strShelfNo = String.Empty;

		private Int32 m_shProducerID = 0;

		private String m_strDirector = String.Empty;

		private String m_strSinger_Band = String.Empty;

		private String m_strDuration = String.Empty;

		private String m_strProgrammeTitleOrName = String.Empty;

		private String m_strProgramGenre_Lyrics = String.Empty;

		private String m_strSubTitleOrCaption_Cast = String.Empty;

		private DateTime m_dtTelecastDate = Helper.GetDefaultDateTime();

		private Int32 m_nMidbreaks = 0;

		private String m_strEpisodeNo = String.Empty;

		private String m_strWriter = String.Empty;

		private String m_strNOCNo = String.Empty;

		private Int32 m_shLanguageID = 0;

		private String m_strTheme = String.Empty;

		private String m_strRemarks = String.Empty;

		private String m_strAlbum = String.Empty;

		private String m_strOrigin = String.Empty;

		private String m_strFootageDetail = String.Empty;

		private String m_strClient = String.Empty;

		private DateTime m_dtCdate = Helper.GetDefaultDateTime();

		private DateTime m_dtCTime = Helper.GetDefaultDateTime();

		private String m_strSeaChangeCode = String.Empty;

		private Int32 m_shMediaStatusID = 0;

		private Int32 m_nInputTypeID = 0;

		private Decimal m_dTotalCapacity = 0;

		private Decimal m_dCapacityUtilized = 0;

		private Decimal m_dCapacityAvailable = 0;

		private String m_strDVDTitle = String.Empty;

		private Boolean m_bIsHighClip = false;

		private Boolean m_bIsLowClip = false;

		private Int64 m_lDVDNo = 0;

		private Boolean m_bIsIssued = false;

		private Int64 m_lOnsiteBackup = 0;

		private Int64 m_lOffsiteBackup = 0;

		private String m_strTitleOfDVD = String.Empty;

		private Int32 m_nCreatedBy = 0;

		private DateTime m_dtCreatedOn = Helper.GetDefaultDateTime();

		private Int32 m_nEditedBy = 0;

		private DateTime m_dtEditedOn = Helper.GetDefaultDateTime();

        private Object m_chrPartNo = null;

        private Object m_nOnsiteMediaTypeId = null;
        private Object m_nOffsiteMediaTypeId = null;

        private Object m_nDepartmentId = null;

        private Object m_nPhotographerId = null;

	#endregion 

        #region  Constructors

        public ArchiveDB()
        {

        }

        #endregion

        #region  Properties 

		public Int64 ArchiveID
		{
			get
			{
				return m_lArchiveID;

			}
			set
			{
				m_lArchiveID = value;

			}
		}

		public Int64 MediaNo
		{
			get
			{
				return m_lMediaNo;

			}
			set
			{
				m_lMediaNo = value;

			}
		}

		public Int32 ChannelID
		{
			get
			{
				return m_shChannelID;

			}
			set
			{
				m_shChannelID = value;

			}
		}

		public Int32 CategoryID
		{
			get
			{
				return m_shCategoryID;

			}
			set
			{
				m_shCategoryID = value;

			}
		}

		public Int32 MediaTypeID
		{
			get
			{
				return m_nMediaTypeID;

			}
			set
			{
				m_nMediaTypeID = value;

			}
		}

		public Int32 ClassificationID
		{
			get
			{
				return m_nClassificationID;

			}
			set
			{
				m_nClassificationID = value;

			}
		}

		public DateTime EntryDate
		{
			get
			{
				return m_dtEntryDate;

			}
			set
			{
				m_dtEntryDate = value;

			}
		}

		public DateTime ShootDate
		{
			get
			{
				return m_dtShootDate;

			}
			set
			{
				m_dtShootDate = value;

			}
		}

		public Int32 SourceID
		{
			get
			{
				return m_nSourceID;

			}
			set
			{
				m_nSourceID = value;

			}
		}

		public Int32 FootageTypeID
		{
			get
			{
				return m_nFootageTypeID;

			}
			set
			{
				m_nFootageTypeID = value;

			}
		}

		public String FileName
		{
			get
			{
				return m_strFileName;

			}
			set
			{
				m_strFileName = value;

			}
		}

		public String JTSFileName
		{
			get
			{
				return m_strJTSFileName;

			}
			set
			{
				m_strJTSFileName = value;

			}
		}

		public String JTSTicketNo
		{
			get
			{
				return m_strJTSTicketNo;

			}
			set
			{
				m_strJTSTicketNo = value;

			}
		}

		public Int32 JTSReporterID
		{
			get
			{
				return m_nJTSReporterID;

			}
			set
			{
				m_nJTSReporterID = value;

			}
		}

		public Int32 JTSCameramanID
		{
			get
			{
				return m_nJTSCameramanID;

			}
			set
			{
				m_nJTSCameramanID = value;

			}
		}

		public Int32 JTSBureau
		{
			get
			{
				return m_nJTSBureau;

			}
			set
			{
				m_nJTSBureau = value;

			}
		}

		public String JTSJobType
		{
			get
			{
				return m_strJTSJobType;

			}
			set
			{
				m_strJTSJobType = value;

			}
		}

		public String JTSCreatedBy
		{
			get
			{
				return m_strJTSCreatedBy;

			}
			set
			{
				m_strJTSCreatedBy = value;

			}
		}

		public String JTSSlug
		{
			get
			{
				return m_strJTSSlug;

			}
			set
			{
				m_strJTSSlug = value;

			}
		}

		public String JTSDuration
		{
			get
			{
				return m_strJTSDuration;

			}
			set
			{
				m_strJTSDuration = value;

			}
		}

		public String RackNo
		{
			get
			{
				return m_strRackNo;

			}
			set
			{
				m_strRackNo = value;

			}
		}

		public String ShelfNo
		{
			get
			{
				return m_strShelfNo;

			}
			set
			{
				m_strShelfNo = value;

			}
		}

		public Int32 ProducerID
		{
			get
			{
				return m_shProducerID;

			}
			set
			{
				m_shProducerID = value;

			}
		}

		public String Director
		{
			get
			{
				return m_strDirector;

			}
			set
			{
				m_strDirector = value;

			}
		}

		public String Singer_Band
		{
			get
			{
				return m_strSinger_Band;

			}
			set
			{
				m_strSinger_Band = value;

			}
		}

		public String Duration
		{
			get
			{
				return m_strDuration;

			}
			set
			{
				m_strDuration = value;

			}
		}

		public String ProgrammeTitleOrName
		{
			get
			{
				return m_strProgrammeTitleOrName;

			}
			set
			{
				m_strProgrammeTitleOrName = value;

			}
		}

		public String ProgramGenre_Lyrics
		{
			get
			{
				return m_strProgramGenre_Lyrics;

			}
			set
			{
				m_strProgramGenre_Lyrics = value;

			}
		}

		public String SubTitleOrCaption_Cast
		{
			get
			{
				return m_strSubTitleOrCaption_Cast;

			}
			set
			{
				m_strSubTitleOrCaption_Cast = value;

			}
		}

		public DateTime TelecastDate
		{
			get
			{
				return m_dtTelecastDate;

			}
			set
			{
				m_dtTelecastDate = value;

			}
		}

		public Int32 Midbreaks
		{
			get
			{
				return m_nMidbreaks;

			}
			set
			{
				m_nMidbreaks = value;

			}
		}

		public String EpisodeNo
		{
			get
			{
				return m_strEpisodeNo;

			}
			set
			{
				m_strEpisodeNo = value;

			}
		}

		public String Writer
		{
			get
			{
				return m_strWriter;

			}
			set
			{
				m_strWriter = value;

			}
		}

		public String NOCNo
		{
			get
			{
				return m_strNOCNo;

			}
			set
			{
				m_strNOCNo = value;

			}
		}

		public Int32 LanguageID
		{
			get
			{
				return m_shLanguageID;

			}
			set
			{
				m_shLanguageID = value;

			}
		}

		public String Theme
		{
			get
			{
				return m_strTheme;

			}
			set
			{
				m_strTheme = value;

			}
		}

		public String Remarks
		{
			get
			{
				return m_strRemarks;

			}
			set
			{
				m_strRemarks = value;

			}
		}

		public String Album
		{
			get
			{
				return m_strAlbum;

			}
			set
			{
				m_strAlbum = value;

			}
		}

		public String Origin
		{
			get
			{
				return m_strOrigin;

			}
			set
			{
				m_strOrigin = value;

			}
		}

		public String FootageDetail
		{
			get
			{
				return m_strFootageDetail;

			}
			set
			{
				m_strFootageDetail = value;

			}
		}

		public String Client
		{
			get
			{
				return m_strClient;

			}
			set
			{
				m_strClient = value;

			}
		}

		public DateTime Cdate
		{
			get
			{
				return m_dtCdate;

			}
			set
			{
				m_dtCdate = value;

			}
		}

		public DateTime CTime
		{
			get
			{
				return m_dtCTime;

			}
			set
			{
				m_dtCTime = value;

			}
		}

		public String SeaChangeCode
		{
			get
			{
				return m_strSeaChangeCode;

			}
			set
			{
				m_strSeaChangeCode = value;

			}
		}

		public Int32 MediaStatusID
		{
			get
			{
				return m_shMediaStatusID;

			}
			set
			{
				m_shMediaStatusID = value;

			}
		}

		public Int32 InputTypeID
		{
			get
			{
				return m_nInputTypeID;

			}
			set
			{
				m_nInputTypeID = value;

			}
		}

		public Decimal TotalCapacity
		{
			get
			{
				return m_dTotalCapacity;

			}
			set
			{
				m_dTotalCapacity = value;

			}
		}

		public Decimal CapacityUtilized
		{
			get
			{
				return m_dCapacityUtilized;

			}
			set
			{
				m_dCapacityUtilized = value;

			}
		}

		public Decimal CapacityAvailable
		{
			get
			{
				return m_dCapacityAvailable;

			}
			set
			{
				m_dCapacityAvailable = value;

			}
		}

		public String DVDTitle
		{
			get
			{
				return m_strDVDTitle;

			}
			set
			{
				m_strDVDTitle = value;

			}
		}

		public Boolean IsHighClip
		{
			get
			{
				return m_bIsHighClip;

			}
			set
			{
				m_bIsHighClip = value;

			}
		}

		public Boolean IsLowClip
		{
			get
			{
				return m_bIsLowClip;

			}
			set
			{
				m_bIsLowClip = value;

			}
		}

		public Int64 DVDNo
		{
			get
			{
				return m_lDVDNo;

			}
			set
			{
				m_lDVDNo = value;

			}
		}

		public Boolean IsIssued
		{
			get
			{
				return m_bIsIssued;

			}
			set
			{
				m_bIsIssued = value;

			}
		}

		public Int64 OnsiteBackup
		{
			get
			{
				return m_lOnsiteBackup;

			}
			set
			{
				m_lOnsiteBackup = value;

			}
		}

		public Int64 OffsiteBackup
		{
			get
			{
				return m_lOffsiteBackup;

			}
			set
			{
				m_lOffsiteBackup = value;

			}
		}

		public String TitleOfDVD
		{
			get
			{
				return m_strTitleOfDVD;

			}
			set
			{
				m_strTitleOfDVD = value;

			}
		}

		public Int32 CreatedBy
		{
			get
			{
				return m_nCreatedBy;

			}
			set
			{
				m_nCreatedBy = value;

			}
		}

		public DateTime CreatedOn
		{
			get
			{
				return m_dtCreatedOn;

			}
			set
			{
				m_dtCreatedOn = value;

			}
		}

		public Int32 EditedBy
		{
			get
			{
				return m_nEditedBy;

			}
			set
			{
				m_nEditedBy = value;

			}
		}

		public DateTime EditedOn
		{
			get
			{
				return m_dtEditedOn;

			}
			set
			{
				m_dtEditedOn = value;

			}
		}

        public Object PartNo
        {
            get
            {
                return m_chrPartNo;
            }
            set
            {
                m_chrPartNo = value; 
            }
        }

        public Object onSiteMediaType
        {
            get
            {
                return m_nOnsiteMediaTypeId;

            }
            set
            {
                m_nOnsiteMediaTypeId = value;

            }
        }
        public Object offSiteMediaType
        {
            get
            {
                return m_nOffsiteMediaTypeId;

            }
            set
            {
                m_nOffsiteMediaTypeId = value;

            }
        }

        public Object DepartmentID
        {
            get
            {
                return m_nDepartmentId;

            }
            set
            {
                m_nDepartmentId = value;

            }
        }

        public Object PhotographerID
        {
            get
            {
                return m_nPhotographerId;

            }
            set
            {
                m_nPhotographerId = value;

            }
        }

	#endregion 

        #region  Methods 
        public DataTable GetPrintLablePictureByParam
            (
              Object MediaNo,
              Object OnSiteBackup,
              Object OffSiteBackup
            )
        {
            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetPrintLablePictureByParam", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dbAdapter.SelectCommand.CommandTimeout = 0;

            if (MediaNo != null && MediaNo != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@MediaNo", MediaNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@MediaNo", System.DBNull.Value);
            }

            if (OnSiteBackup != null && OnSiteBackup != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@OnSiteBackup", OnSiteBackup);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@OnSiteBackup", System.DBNull.Value);
            }

            if (OffSiteBackup != null && OffSiteBackup != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@OffSiteBackup", OffSiteBackup);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@OffSiteBackup", System.DBNull.Value);
            }

            DataTable dtArchive = new DataTable("ArchiveDB");
            dbAdapter.Fill(dtArchive);
            return dtArchive;
 
        }
        public DataTable GetPrintLableProgramSearchByParam
                 (
                   Object MediaNo,
                   Object OnSiteMediaNo,
                   Object OffSiteMediaNo
                 )
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetPrintLableProgramSearchByParam", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dbAdapter.SelectCommand.CommandTimeout = 0;

            if (MediaNo != null && MediaNo != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@MediaNo", MediaNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@MediaNo", System.DBNull.Value);
            }

            if (OnSiteMediaNo != null && OnSiteMediaNo != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@OnSiteMediaNo", OnSiteMediaNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@OnSiteMediaNo", System.DBNull.Value);
            }

            if (OffSiteMediaNo != null && OffSiteMediaNo != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@OffSiteMediaNo", OffSiteMediaNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@OffSiteMediaNo", System.DBNull.Value);
            }

            DataTable dtArchive = new DataTable("ArchiveDB");
            dbAdapter.Fill(dtArchive);
            return dtArchive;

        }
        public DataTable GetArchiveGuestList(Int32 Arcid)
        {
            DataSet dt = null;
            DBManager dbCom = new DBManager();
            dbCom.Open();
            dbCom.Command.Parameters.Clear();
            dbCom.CreateParameters(1);
            dbCom.AddParameters(0, "@ArchiveId", Arcid, 0);
            try
            {
                dt = dbCom.ExecuteDataSet(CommandType.StoredProcedure, "[usp_GetArchiveGuestDetail]");
            }
            catch
            {

            }
            return dt.Tables[0];
        }
        public DataTable GetPrintLableCommercialByParam
            (
               Object MediaNo,
               Object OnSiteBackup,
               Object OffSiteBackup
            )
        {
            
            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetPrintLableCommercialByParam", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dbAdapter.SelectCommand.CommandTimeout = 0;

            if (MediaNo != null && MediaNo != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@MediaNo", MediaNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@MediaNo", System.DBNull.Value);
            }
            if (OnSiteBackup != null && OnSiteBackup != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@OnSiteBackup", OnSiteBackup);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@OnSiteBackup", System.DBNull.Value);
            }
            if (OffSiteBackup != null && OffSiteBackup != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@OffSiteBackup", OffSiteBackup);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@OffSiteBackup", System.DBNull.Value);
            }

            DataTable dtArchive = new DataTable("ArchiveDB");
            dbAdapter.Fill(dtArchive);
            return dtArchive;
        }    

        public DataTable GetAllQuickSearch
            (
             object archiveID,
             object mediaNo,
             object channelID,
             object categoryID,
             object mediaTypeID,
             object classificationID,
             object entryDate,
             object shootDate,
             object sourceID,
             object footageTypeID,
             object fileName,
             object jTSFileName,
             object jTSTicketNo,
             object jTSReporterID,
             object jTSCameramanID,
             object jTSBureau,
             object jTSJobType,
             object jTSCreatedBy,
             object jTSSlug,
             object jTSDuration,
             object rackNo,
             object shelfNo,
             object producerID,
             object director,
             object singer_Band,
             object duration,
             object programmeTitleOrName,
             object programGenre_Lyrics,
             object subTitleOrCaption_Cast,
             object telecastDate,
             object midbreaks,
             object episodeNo,
             object writer,
             object nOCNo,
             object languageID,
             object theme,
             object remarks,
             object album,
             object origin,
             object footageDetail,
             object client,
             object cdate,
             object cTime,
             object seaChangeCode,
             object mediaStatusID,
             object inputTypeID,
             object totalCapacity,
             object capacityUtilized,
             object capacityAvailable,
             object dVDTitle,
             object isHighClip,
             object isLowClip,
             object dVDNo,
             object isIssued,
             object onsiteBackup,
             object offsiteBackup,
             object titleOfDVD,
             object createdBy,
             object createdOn,
             object editedBy,
             object editedOn,
             object keywordType,
             object keyword,
             object exactwordsearch,
             object fromshootdate,
             object toshootdate ////,  
            ////object isDeepSearch
            )
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllQuickSearch", dbConn);//usp_GetAllQuickSearch //sp_GetAllQuickSearch
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dbAdapter.SelectCommand.CommandTimeout = 0;

            if (archiveID != null && !archiveID.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveID", archiveID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveID", System.DBNull.Value);
            }
            if (mediaNo != null && !mediaNo.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nMediaNo", mediaNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nMediaNo", System.DBNull.Value);
            }
            if (channelID != null && Convert.ToInt64(channelID) != 0)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nChannelID", channelID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nChannelID", System.DBNull.Value);
            }
            if (categoryID != null && !categoryID.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nCategoryID", categoryID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nCategoryID", System.DBNull.Value);
            }
            if (mediaTypeID != null && Convert.ToInt64(mediaTypeID) != 0)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nMediaTypeID", mediaTypeID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nMediaTypeID", System.DBNull.Value);
            }
            if (classificationID != null && Convert.ToInt64(classificationID) != 0)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nClassificationID", classificationID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nClassificationID", System.DBNull.Value);
            }
            if (entryDate != null && !entryDate.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtEntryDate", entryDate);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtEntryDate", System.DBNull.Value);
            }
            if (shootDate != null && !shootDate.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtShootDate", shootDate);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtShootDate", System.DBNull.Value);
            }
            if (sourceID != null && Convert.ToInt64(sourceID) != 0)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nSourceID", sourceID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nSourceID", System.DBNull.Value);
            }
            if (footageTypeID != null && Convert.ToInt64(footageTypeID) != 0)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nFootageTypeID", footageTypeID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nFootageTypeID", System.DBNull.Value);
            }
            if (fileName != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strFileName", fileName);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strFileName", System.DBNull.Value);
            }
            if (jTSFileName != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSFileName", jTSFileName);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSFileName", System.DBNull.Value);
            }
            if (jTSTicketNo != null && jTSTicketNo != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSTicketNo", jTSTicketNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSTicketNo", System.DBNull.Value);
            }
            if (jTSReporterID != null && Convert.ToInt64(jTSReporterID) != 0)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nJTSReporterID", jTSReporterID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nJTSReporterID", System.DBNull.Value);
            }
            if (jTSCameramanID != null && !jTSCameramanID.ToString().Equals("0"))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nJTSCameramanID", jTSCameramanID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nJTSCameramanID", System.DBNull.Value);
            }
            if (jTSBureau != null && Convert.ToInt64(jTSBureau) != 0)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nJTSBureau", jTSBureau);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nJTSBureau", System.DBNull.Value);
            }
            if (jTSJobType != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSJobType", jTSJobType);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSJobType", System.DBNull.Value);
            }
            if (jTSCreatedBy != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSCreatedBy", jTSCreatedBy);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSCreatedBy", System.DBNull.Value);
            }
            if (jTSSlug != null && !jTSSlug.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSSlug", jTSSlug);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSSlug", System.DBNull.Value);
            }
            if (jTSDuration != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSDuration", jTSDuration);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSDuration", System.DBNull.Value);
            }
            if (rackNo != null && !rackNo.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strRackNo", rackNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strRackNo", System.DBNull.Value);
            }
            if (shelfNo != null && !shelfNo.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strShelfNo", shelfNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strShelfNo", System.DBNull.Value);
            }
            if (producerID != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nProducerID", producerID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nProducerID", System.DBNull.Value);
            }
            if (director != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strDirector", director);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strDirector", System.DBNull.Value);
            }
            if (singer_Band != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strSinger_Band", singer_Band);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strSinger_Band", System.DBNull.Value);
            }
            if (duration != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strDuration", duration);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strDuration", System.DBNull.Value);
            }
            if (programmeTitleOrName != null && !programmeTitleOrName.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strProgrammeTitleOrName", programmeTitleOrName);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strProgrammeTitleOrName", System.DBNull.Value);
            }
            if (programGenre_Lyrics != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strProgramGenre_Lyrics", programGenre_Lyrics);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strProgramGenre_Lyrics", System.DBNull.Value);
            }
            if (subTitleOrCaption_Cast != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strSubTitleOrCaption_Cast", subTitleOrCaption_Cast);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strSubTitleOrCaption_Cast", System.DBNull.Value);
            }
            if (telecastDate != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtTelecastDate", telecastDate);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtTelecastDate", System.DBNull.Value);
            }
            if (midbreaks != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nMidbreaks", midbreaks);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nMidbreaks", System.DBNull.Value);
            }
            if (episodeNo != null && !episodeNo.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strEpisodeNo", episodeNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strEpisodeNo", System.DBNull.Value);
            }
            if (writer != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strWriter", writer);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strWriter", System.DBNull.Value);
            }
            if (nOCNo != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strNOCNo", nOCNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strNOCNo", System.DBNull.Value);
            }
            if (languageID != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nLanguageID", languageID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nLanguageID", System.DBNull.Value);
            }
            if (theme != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strTheme", theme);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strTheme", System.DBNull.Value);
            }
            if (remarks != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strRemarks", remarks);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strRemarks", System.DBNull.Value);
            }
            if (album != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strAlbum", album);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strAlbum", System.DBNull.Value);
            }
            if (origin != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strOrigin", origin);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strOrigin", System.DBNull.Value);
            }
            if (footageDetail != null && !footageDetail.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strFootageDetail", footageDetail);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strFootageDetail", System.DBNull.Value);

            }
            if (client != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strClient", client);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strClient", System.DBNull.Value);
            }
            if (cdate != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtCdate", cdate);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtCdate", System.DBNull.Value);
            }
            if (cTime != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtCTime", cTime);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtCTime", System.DBNull.Value);
            }
            if (seaChangeCode != null && !seaChangeCode.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strSeaChangeCode", seaChangeCode);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strSeaChangeCode", System.DBNull.Value);
            }
            if (mediaStatusID != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nMediaStatusID", mediaStatusID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nMediaStatusID", System.DBNull.Value);
            }
            if (inputTypeID != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nInputTypeID", inputTypeID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nInputTypeID", System.DBNull.Value);
            }
            if (totalCapacity != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dTotalCapacity", totalCapacity);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dTotalCapacity", System.DBNull.Value);
            }
            if (capacityUtilized != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dCapacityUtilized", capacityUtilized);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dCapacityUtilized", System.DBNull.Value);
            }
            if (capacityAvailable != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dCapacityAvailable", capacityAvailable);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dCapacityAvailable", System.DBNull.Value);
            }
            if (dVDTitle != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strDVDTitle", dVDTitle);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strDVDTitle", System.DBNull.Value);
            }
            if (isHighClip != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_bIsHighClip", isHighClip);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_bIsHighClip", System.DBNull.Value);
            }
            if (isLowClip != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_bIsLowClip", isLowClip);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_bIsLowClip", System.DBNull.Value);
            }
            if (dVDNo != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nDVDNo", dVDNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nDVDNo", System.DBNull.Value);
            }
            if (isIssued != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_bIsIssued", isIssued);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_bIsIssued", System.DBNull.Value);
            }
            if (onsiteBackup != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nOnsiteBackup", onsiteBackup);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nOnsiteBackup", System.DBNull.Value);
            }
            if (offsiteBackup != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nOffsiteBackup", offsiteBackup);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nOffsiteBackup", System.DBNull.Value);
            }

            if (createdBy != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nCreatedBy", createdBy);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nCreatedBy", System.DBNull.Value);
            }
            if (createdOn != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtCreatedOn", createdOn);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtCreatedOn", System.DBNull.Value);
            }
            if (editedBy != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nEditedBy", editedBy);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nEditedBy", System.DBNull.Value);
            }
            if (editedOn != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtEditedOn", editedOn);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtEditedOn", System.DBNull.Value);
            }



            if (keywordType != null && Convert.ToInt64(keywordType) != 0)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nKeywordType", keywordType);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nKeywordType", System.DBNull.Value);
            }
            if (keyword != null && !keyword.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strkeyword", keyword);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strkeyword", System.DBNull.Value);
            }


            if (exactwordsearch != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_bexactwordsearch", exactwordsearch);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_bexactwordsearch", System.DBNull.Value);
            }


            if (fromshootdate != null && !fromshootdate.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtShootDateFrom", fromshootdate);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtShootDateFrom", System.DBNull.Value);
            }
            if (toshootdate != null && !toshootdate.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtShootDateTo", toshootdate);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtShootDateTo", System.DBNull.Value);
            }

            ////if (isDeepSearch != null)
            ////{
            ////    dbAdapter.SelectCommand.Parameters.Add("@p_bIsDeepSearch", isDeepSearch);
            ////}
            ////else
            ////{
            ////    dbAdapter.SelectCommand.Parameters.Add("@p_bIsDeepSearch", System.DBNull.Value);
            ////}

            DataTable dtArchive = new DataTable("ArchiveDB");

            dbAdapter.Fill(dtArchive);

            return dtArchive;
        }
        public DataTable GetAllQuickSearchPic
        (
         object archiveID,
         object mediaNo,
         object channelID,
         object categoryID,
         object mediaTypeID,
         object classificationID,
         object entryDate,
         object shootDate,
         object sourceID,
         object footageTypeID,
         object fileName,
         object jTSFileName,
         object jTSTicketNo,
         object jTSReporterID,
         object jTSCameramanID,
         object jTSBureau,
         object jTSJobType,
         object jTSCreatedBy,
         object jTSSlug,
         object jTSDuration,
         object rackNo,
         object shelfNo,
         object producerID,
         object director,
         object singer_Band,
         object duration,
         object programmeTitleOrName,
         object programGenre_Lyrics,
         object subTitleOrCaption_Cast,
         object telecastDate,
         object midbreaks,
         object episodeNo,
         object writer,
         object nOCNo,
         object languageID,
         object theme,
         object remarks,
         object album,
         object origin,
         object footageDetail,
         object client,
         object cdate,
         object cTime,
         object seaChangeCode,
         object mediaStatusID,
         object inputTypeID,
         object totalCapacity,
         object capacityUtilized,
         object capacityAvailable,
         object dVDTitle,
         object isHighClip,
         object isLowClip,
         object dVDNo,
         object isIssued,
         object onsiteBackup,
         object offsiteBackup,
         object titleOfDVD,
         object createdBy,
         object createdOn,
         object editedBy,
         object editedOn,
         object keywordType,
         object keyword,
         object exactwordsearch,
         object fromshootdate,
         object toshootdate,
         object PhotographerID
            ////,  
            ////object isDeepSearch
        )
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllQuickSearchPic", dbConn);//usp_GetAllQuickSearch //sp_GetAllQuickSearch
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dbAdapter.SelectCommand.CommandTimeout = 0;

            if (archiveID != null && !archiveID.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveID", archiveID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveID", System.DBNull.Value);
            }
            if (mediaNo != null && !mediaNo.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nMediaNo", mediaNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nMediaNo", System.DBNull.Value);
            }
            if (channelID != null && Convert.ToInt64(channelID) != 0)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nChannelID", channelID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nChannelID", System.DBNull.Value);
            }
            if (categoryID != null && !categoryID.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nCategoryID", categoryID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nCategoryID", System.DBNull.Value);
            }
            if (mediaTypeID != null && Convert.ToInt64(mediaTypeID) != 0)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nMediaTypeID", mediaTypeID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nMediaTypeID", System.DBNull.Value);
            }
            if (classificationID != null && Convert.ToInt64(classificationID) != 0)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nClassificationID", classificationID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nClassificationID", System.DBNull.Value);
            }
            if (entryDate != null && !entryDate.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtEntryDate", entryDate);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtEntryDate", System.DBNull.Value);
            }
            if (shootDate != null && !shootDate.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtShootDate", shootDate);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtShootDate", System.DBNull.Value);
            }
            if (sourceID != null && Convert.ToInt64(sourceID) != 0)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nSourceID", sourceID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nSourceID", System.DBNull.Value);
            }
            if (footageTypeID != null && Convert.ToInt64(footageTypeID) != 0)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nFootageTypeID", footageTypeID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nFootageTypeID", System.DBNull.Value);
            }
            if (fileName != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strFileName", fileName);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strFileName", System.DBNull.Value);
            }
            if (jTSFileName != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSFileName", jTSFileName);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSFileName", System.DBNull.Value);
            }
            if (jTSTicketNo != null && jTSTicketNo != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSTicketNo", jTSTicketNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSTicketNo", System.DBNull.Value);
            }
            if (jTSReporterID != null && Convert.ToInt64(jTSReporterID) != 0)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nJTSReporterID", jTSReporterID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nJTSReporterID", System.DBNull.Value);
            }
            if (jTSCameramanID != null && !jTSCameramanID.ToString().Equals("0"))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nJTSCameramanID", jTSCameramanID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nJTSCameramanID", System.DBNull.Value);
            }
            if (jTSBureau != null && Convert.ToInt64(jTSBureau) != 0)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nJTSBureau", jTSBureau);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nJTSBureau", System.DBNull.Value);
            }
            if (jTSJobType != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSJobType", jTSJobType);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSJobType", System.DBNull.Value);
            }
            if (jTSCreatedBy != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSCreatedBy", jTSCreatedBy);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSCreatedBy", System.DBNull.Value);
            }
            if (jTSSlug != null && !jTSSlug.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSSlug", jTSSlug);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSSlug", System.DBNull.Value);
            }
            if (jTSDuration != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSDuration", jTSDuration);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSDuration", System.DBNull.Value);
            }
            if (rackNo != null && !rackNo.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strRackNo", rackNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strRackNo", System.DBNull.Value);
            }
            if (shelfNo != null && !shelfNo.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strShelfNo", shelfNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strShelfNo", System.DBNull.Value);
            }
            if (producerID != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nProducerID", producerID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nProducerID", System.DBNull.Value);
            }
            if (director != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strDirector", director);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strDirector", System.DBNull.Value);
            }
            if (singer_Band != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strSinger_Band", singer_Band);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strSinger_Band", System.DBNull.Value);
            }
            if (duration != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strDuration", duration);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strDuration", System.DBNull.Value);
            }
            if (programmeTitleOrName != null && !programmeTitleOrName.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strProgrammeTitleOrName", programmeTitleOrName);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strProgrammeTitleOrName", System.DBNull.Value);
            }
            if (programGenre_Lyrics != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strProgramGenre_Lyrics", programGenre_Lyrics);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strProgramGenre_Lyrics", System.DBNull.Value);
            }
            if (subTitleOrCaption_Cast != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strSubTitleOrCaption_Cast", subTitleOrCaption_Cast);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strSubTitleOrCaption_Cast", System.DBNull.Value);
            }
            if (telecastDate != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtTelecastDate", telecastDate);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtTelecastDate", System.DBNull.Value);
            }
            if (midbreaks != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nMidbreaks", midbreaks);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nMidbreaks", System.DBNull.Value);
            }
            if (episodeNo != null && !episodeNo.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strEpisodeNo", episodeNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strEpisodeNo", System.DBNull.Value);
            }
            if (writer != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strWriter", writer);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strWriter", System.DBNull.Value);
            }
            if (nOCNo != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strNOCNo", nOCNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strNOCNo", System.DBNull.Value);
            }
            if (languageID != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nLanguageID", languageID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nLanguageID", System.DBNull.Value);
            }
            if (theme != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strTheme", theme);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strTheme", System.DBNull.Value);
            }
            if (remarks != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strRemarks", remarks);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strRemarks", System.DBNull.Value);
            }
            if (album != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strAlbum", album);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strAlbum", System.DBNull.Value);
            }
            if (origin != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strOrigin", origin);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strOrigin", System.DBNull.Value);
            }
            if (footageDetail != null && !footageDetail.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strFootageDetail", footageDetail);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strFootageDetail", System.DBNull.Value);

            }
            if (client != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strClient", client);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strClient", System.DBNull.Value);
            }
            if (cdate != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtCdate", cdate);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtCdate", System.DBNull.Value);
            }
            if (cTime != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtCTime", cTime);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtCTime", System.DBNull.Value);
            }
            if (seaChangeCode != null && !seaChangeCode.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strSeaChangeCode", seaChangeCode);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strSeaChangeCode", System.DBNull.Value);
            }
            if (mediaStatusID != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nMediaStatusID", mediaStatusID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nMediaStatusID", System.DBNull.Value);
            }
            if (inputTypeID != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nInputTypeID", inputTypeID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nInputTypeID", System.DBNull.Value);
            }
            if (totalCapacity != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dTotalCapacity", totalCapacity);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dTotalCapacity", System.DBNull.Value);
            }
            if (capacityUtilized != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dCapacityUtilized", capacityUtilized);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dCapacityUtilized", System.DBNull.Value);
            }
            if (capacityAvailable != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dCapacityAvailable", capacityAvailable);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dCapacityAvailable", System.DBNull.Value);
            }
            if (dVDTitle != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strDVDTitle", dVDTitle);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strDVDTitle", System.DBNull.Value);
            }
            if (isHighClip != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_bIsHighClip", isHighClip);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_bIsHighClip", System.DBNull.Value);
            }
            if (isLowClip != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_bIsLowClip", isLowClip);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_bIsLowClip", System.DBNull.Value);
            }
            if (dVDNo != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nDVDNo", dVDNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nDVDNo", System.DBNull.Value);
            }
            if (isIssued != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_bIsIssued", isIssued);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_bIsIssued", System.DBNull.Value);
            }
            if (onsiteBackup != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nOnsiteBackup", onsiteBackup);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nOnsiteBackup", System.DBNull.Value);
            }
            if (offsiteBackup != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nOffsiteBackup", offsiteBackup);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nOffsiteBackup", System.DBNull.Value);
            }

            if (createdBy != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nCreatedBy", createdBy);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nCreatedBy", System.DBNull.Value);
            }
            if (createdOn != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtCreatedOn", createdOn);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtCreatedOn", System.DBNull.Value);
            }
            if (editedBy != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nEditedBy", editedBy);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nEditedBy", System.DBNull.Value);
            }
            if (editedOn != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtEditedOn", editedOn);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtEditedOn", System.DBNull.Value);
            }



            if (keywordType != null && Convert.ToInt64(keywordType) != 0)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nKeywordType", keywordType);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nKeywordType", System.DBNull.Value);
            }
            if (keyword != null && !keyword.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strkeyword", keyword);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strkeyword", System.DBNull.Value);
            }


            if (exactwordsearch != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_bexactwordsearch", exactwordsearch);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_bexactwordsearch", System.DBNull.Value);
            }


            if (fromshootdate != null && !fromshootdate.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtShootDateFrom", fromshootdate);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtShootDateFrom", System.DBNull.Value);
            }
            if (toshootdate != null && !toshootdate.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtShootDateTo", toshootdate);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtShootDateTo", System.DBNull.Value);
            }
            if (PhotographerID != null && Convert.ToInt64(PhotographerID) != 0)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nPhotgraphId", PhotographerID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nPhotgraphId", System.DBNull.Value);
            }
            ////if (isDeepSearch != null)
            ////{
            ////    dbAdapter.SelectCommand.Parameters.Add("@p_bIsDeepSearch", isDeepSearch);
            ////}
            ////else
            ////{
            ////    dbAdapter.SelectCommand.Parameters.Add("@p_bIsDeepSearch", System.DBNull.Value);
            ////}

            DataTable dtArchive = new DataTable("ArchiveDB");

            dbAdapter.Fill(dtArchive);

            return dtArchive;
        }
        public DataTable GetAllQuickSearch_Comm
           (
         
            object channelID,
            object jTSBureau,
            object programmeTitleOrName,
            object subTitleOrCaption_Cast,
            object client,
            object seaChangeCode,
            object isHighClip,
            object keyword,
            object exactwordsearch,
            object fromshootdate,
            object toshootdate, ////,  
            object strfootage
            ////object isDeepSearch
           )
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllQuickSearch_Comm", dbConn);//usp_GetAllQuickSearch //sp_GetAllQuickSearch
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dbAdapter.SelectCommand.CommandTimeout = 0;

            if (channelID != null && Convert.ToInt64(channelID) != 0)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nChannelID", channelID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nChannelID", System.DBNull.Value);
            }
            if (jTSBureau != null && Convert.ToInt64(jTSBureau) != 0)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nJTSBureau", jTSBureau);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nJTSBureau", System.DBNull.Value);
            }
           
            if (programmeTitleOrName != null && !programmeTitleOrName.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strProgrammeTitleOrName", programmeTitleOrName);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strProgrammeTitleOrName", System.DBNull.Value);
            }

            if (subTitleOrCaption_Cast != null && !subTitleOrCaption_Cast.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strSubTitleOrCaption_Cast", subTitleOrCaption_Cast);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strSubTitleOrCaption_Cast", System.DBNull.Value);
            }



            if (client != null && !client.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strClient", client);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strClient", System.DBNull.Value);
            }
           
            if (seaChangeCode != null && !seaChangeCode.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strSeaChangeCode", seaChangeCode);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strSeaChangeCode", System.DBNull.Value);
            }
           
           
            if (isHighClip != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_bIsHighClip", isHighClip);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_bIsHighClip", System.DBNull.Value);
            }
           
            if (keyword != null && !keyword.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strkeyword", keyword);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strkeyword", System.DBNull.Value);
            }


            if (exactwordsearch != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_bexactwordsearch", exactwordsearch);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_bexactwordsearch", System.DBNull.Value);
            }


            if (fromshootdate != null && !fromshootdate.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtShootDateFrom", fromshootdate);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtShootDateFrom", System.DBNull.Value);
            }
            if (toshootdate != null && !toshootdate.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtShootDateTo", toshootdate);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtShootDateTo", System.DBNull.Value);
            }
            if (strfootage != null && !strfootage.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strFootageDetail", strfootage);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strFootageDetail", System.DBNull.Value);

            }
            DataTable dtArchive = new DataTable("ArchiveDB");

            dbAdapter.Fill(dtArchive);

            return dtArchive;
        }
        public DataTable GetQuickSearch(String Sql)
        {
            SqlConnection dbConn = new SqlConnection(Helper.GetEXPARCSDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter(Sql, dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.Text;
            dbAdapter.SelectCommand.CommandTimeout = 0;

            DataTable dtArchive = new DataTable("ArchiveDB");

            dbAdapter.Fill(dtArchive);

            return dtArchive;
        }

        public DataTable GetAllQuickSearch
          (
          
           object mediaNo,          
           object onsiteBackup,
           object offsiteBackup
          
          )
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllQuickSearchForDesktop", dbConn);//usp_GetAllQuickSearch //sp_GetAllQuickSearch
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dbAdapter.SelectCommand.CommandTimeout = 0;

           
            if (mediaNo != null && !mediaNo.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nMediaNo", mediaNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nMediaNo", System.DBNull.Value);
            }
            if (onsiteBackup != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nOnsiteBackup", onsiteBackup);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nOnsiteBackup", System.DBNull.Value);
            }
            if (offsiteBackup != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nOffsiteBackup", offsiteBackup);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nOffsiteBackup", System.DBNull.Value);
            }

            DataTable dtArchive = new DataTable("ArchiveDB");

            dbAdapter.Fill(dtArchive);

            return dtArchive;
        }
        public String GetNextID()
        {
            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString ());
            SqlCommand dbCom = new SqlCommand("usp_GetMaxIDArchive", dbConn);
            //SqlCommand dbCom = new SqlCommand("dbo].[usp_GetMaxMediaNo", dbConn);
            dbCom.CommandType = CommandType.StoredProcedure;

            /*Output Parameters*/
            SqlParameter pID = new SqlParameter();
            pID.ParameterName = "@p_nmaxID";
            pID.SqlDbType = SqlDbType.Int;
            pID.Direction = ParameterDirection.Output;
            dbCom.Parameters.Add(pID);

            dbConn.Open();
            dbCom.ExecuteNonQuery();
            dbConn.Close();

            return Convert.ToString(Int32.Parse(pID.Value.ToString()) + 1);
        }
        public DataTable GetProgramSearch(
                                          Object ArchiveId,
                                          Object ChannelID,
                                          Object MediaTypeID,
                                          Object ProducerID,
                                          Object IsHighClip,
                                          Object ProgrammeTitleOrName,
                                          Object SubTitleOrCaption_Cast,
                                          Object Remarks,
                                          Object MediaNo,
                                          Object OnSiteMediaNo,
                                          Object SeaChangeCode,
                                          Object DepartmentId,
                                          Object Detail,
                                          Object EpisodeNo,
                                          Object PartNo,
                                          Object KeywordTypeId,
                                          Object Keyword,
                                          Object ShootDateFrom,
                                          Object ShootDateTo,
                                          Object TDateFrom,
                                          Object TDateTo,
                                          Object OffSiteMediaNo
                                         
                                         )
        {
            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetProgramSearch_Dsk", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dbAdapter.SelectCommand.CommandTimeout = 0;

            if (ArchiveId != null && ArchiveId.ToString () != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@ArchiveID", ArchiveId);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@ArchiveID", System.DBNull.Value);
            }

            if (ChannelID != null && !ChannelID.ToString().Equals("0"))
            {
                dbAdapter.SelectCommand.Parameters.Add("@ChannelID", ChannelID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@ChannelID", System.DBNull.Value);
            }

            if (MediaTypeID != null && !MediaTypeID.ToString().Equals("0"))
            {
                dbAdapter.SelectCommand.Parameters.Add("@MediaTypeID", MediaTypeID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@MediaTypeID", System.DBNull.Value);
            }

            if (ProducerID != null && !ProducerID.ToString().Equals("0"))
            {
                dbAdapter.SelectCommand.Parameters.Add("@ProducerID", ProducerID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@ProducerID", System.DBNull.Value);
            }

            if (IsHighClip != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@IsHighClip", IsHighClip);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@IsHighClip", System.DBNull.Value);
            }           

            if (ProgrammeTitleOrName != null && ProgrammeTitleOrName != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@ProgrammeTitleOrName", ProgrammeTitleOrName);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@ProgrammeTitleOrName", System.DBNull.Value);
            }

            if (SubTitleOrCaption_Cast != null && SubTitleOrCaption_Cast != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@SubTitleOrCaption_Cast", SubTitleOrCaption_Cast);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@SubTitleOrCaption_Cast", System.DBNull.Value);
            }

            if (Remarks != null && Remarks != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@Remarks", Remarks);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@Remarks", System.DBNull.Value);
            }
            if (MediaNo != null && MediaNo != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@MediaNo", MediaNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@MediaNo", System.DBNull.Value);
            }

            if (OnSiteMediaNo != null && OnSiteMediaNo != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@OnSiteMediaNo", OnSiteMediaNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@OnSiteMediaNo", System.DBNull.Value);
            }
            if (SeaChangeCode != null && SeaChangeCode != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@SeaChangeCode", SeaChangeCode);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@SeaChangeCode", System.DBNull.Value);
            }

            if (DepartmentId != null && !DepartmentId.ToString().Equals("0"))
            {
                dbAdapter.SelectCommand.Parameters.Add("@DepartmentId", DepartmentId);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@DepartmentId", System.DBNull.Value);
            }
            if (Detail != null && Detail != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@Detail", Detail);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@Detail", System.DBNull.Value);
            }

            if (EpisodeNo != null && EpisodeNo != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@EpisodeNo", EpisodeNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@EpisodeNo", System.DBNull.Value);
            }
            if (PartNo != null && PartNo != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@PartNo", PartNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@PartNo", System.DBNull.Value);
            }
            if (KeywordTypeId != null && !KeywordTypeId.ToString().Equals("0"))
            {
                dbAdapter.SelectCommand.Parameters.Add("@KeywordTypeId", KeywordTypeId);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@KeywordTypeId", System.DBNull.Value);
            }
            if (Keyword != null && Keyword != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@Keyword", Keyword);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@Keyword", System.DBNull.Value);
            }
            if (ShootDateFrom != null && ShootDateFrom != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@ShootDateFrom", ShootDateFrom);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@ShootDateFrom", System.DBNull.Value);
            }
            if (ShootDateTo != null && ShootDateTo != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@ShootDateTo", ShootDateTo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@ShootDateTo", System.DBNull.Value);
            }
            if (TDateFrom != null && TDateFrom != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@TDateFrom", TDateFrom);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@TDateFrom", System.DBNull.Value);
            }
            if (TDateTo != null && TDateTo != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@TDateTo", TDateTo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@TDateTo", System.DBNull.Value);
            }

            if (OffSiteMediaNo != null && OffSiteMediaNo != String.Empty)
            {
                dbAdapter.SelectCommand.Parameters.Add("@OffSiteMediaNo", OffSiteMediaNo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@OffSiteMediaNo", System.DBNull.Value);
            }

            DataTable dtArchive = new DataTable("ArchiveDB");
            dbAdapter.Fill(dtArchive);
            return dtArchive;
        }
		public Int64 InsertArchive(ArchiveDB objArchive)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_InsertArchive", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_nMediaNo",objArchive.MediaNo);
			dbCom.Parameters.Add("@p_nChannelID",objArchive.ChannelID);
			dbCom.Parameters.Add("@p_nCategoryID",objArchive.CategoryID);
			dbCom.Parameters.Add("@p_nMediaTypeID",objArchive.MediaTypeID);
			dbCom.Parameters.Add("@p_nClassificationID",objArchive.ClassificationID);
			dbCom.Parameters.Add("@p_dtEntryDate",objArchive.EntryDate);
			dbCom.Parameters.Add("@p_dtShootDate",objArchive.ShootDate);
			dbCom.Parameters.Add("@p_nSourceID",objArchive.SourceID);
			dbCom.Parameters.Add("@p_nFootageTypeID",objArchive.FootageTypeID);
			dbCom.Parameters.Add("@p_strFileName",objArchive.FileName);
			dbCom.Parameters.Add("@p_strJTSFileName",objArchive.JTSFileName);
			dbCom.Parameters.Add("@p_strJTSTicketNo",objArchive.JTSTicketNo);
			dbCom.Parameters.Add("@p_nJTSReporterID",objArchive.JTSReporterID);
			dbCom.Parameters.Add("@p_nJTSCameramanID",objArchive.JTSCameramanID);
			dbCom.Parameters.Add("@p_nJTSBureau",objArchive.JTSBureau);
			dbCom.Parameters.Add("@p_strJTSJobType",objArchive.JTSJobType);
			dbCom.Parameters.Add("@p_strJTSCreatedBy",objArchive.JTSCreatedBy);
			dbCom.Parameters.Add("@p_strJTSSlug",objArchive.JTSSlug);
			dbCom.Parameters.Add("@p_strJTSDuration",objArchive.JTSDuration);
			dbCom.Parameters.Add("@p_strRackNo",objArchive.RackNo);
			dbCom.Parameters.Add("@p_strShelfNo",objArchive.ShelfNo);
			dbCom.Parameters.Add("@p_nProducerID",objArchive.ProducerID);
			dbCom.Parameters.Add("@p_strDirector",objArchive.Director);
			dbCom.Parameters.Add("@p_strSinger_Band",objArchive.Singer_Band);
			dbCom.Parameters.Add("@p_strDuration",objArchive.Duration);
			dbCom.Parameters.Add("@p_strProgrammeTitleOrName",objArchive.ProgrammeTitleOrName);
			dbCom.Parameters.Add("@p_strProgramGenre_Lyrics",objArchive.ProgramGenre_Lyrics);
			dbCom.Parameters.Add("@p_strSubTitleOrCaption_Cast",objArchive.SubTitleOrCaption_Cast);
			dbCom.Parameters.Add("@p_dtTelecastDate",objArchive.TelecastDate);
			dbCom.Parameters.Add("@p_nMidbreaks",objArchive.Midbreaks);
			dbCom.Parameters.Add("@p_strEpisodeNo",objArchive.EpisodeNo);
			dbCom.Parameters.Add("@p_strWriter",objArchive.Writer);
			dbCom.Parameters.Add("@p_strNOCNo",objArchive.NOCNo);
			dbCom.Parameters.Add("@p_nLanguageID",objArchive.LanguageID);
			dbCom.Parameters.Add("@p_strTheme",objArchive.Theme);
			dbCom.Parameters.Add("@p_strRemarks",objArchive.Remarks);
			dbCom.Parameters.Add("@p_strAlbum",objArchive.Album);
			dbCom.Parameters.Add("@p_strOrigin",objArchive.Origin);
			dbCom.Parameters.Add("@p_strFootageDetail",objArchive.FootageDetail);
			dbCom.Parameters.Add("@p_strClient",objArchive.Client);
			dbCom.Parameters.Add("@p_dtCdate",objArchive.Cdate);
			dbCom.Parameters.Add("@p_dtCTime",objArchive.CTime);
			dbCom.Parameters.Add("@p_strSeaChangeCode",objArchive.SeaChangeCode);
			dbCom.Parameters.Add("@p_nMediaStatusID",objArchive.MediaStatusID);
			dbCom.Parameters.Add("@p_nInputTypeID",objArchive.InputTypeID);
			dbCom.Parameters.Add("@p_dTotalCapacity",objArchive.TotalCapacity);
			dbCom.Parameters.Add("@p_dCapacityUtilized",objArchive.CapacityUtilized);
			dbCom.Parameters.Add("@p_dCapacityAvailable",objArchive.CapacityAvailable);
			dbCom.Parameters.Add("@p_strDVDTitle",objArchive.DVDTitle);
			dbCom.Parameters.Add("@p_bIsHighClip",objArchive.IsHighClip);
			dbCom.Parameters.Add("@p_bIsLowClip",objArchive.IsLowClip);
			dbCom.Parameters.Add("@p_nDVDNo",objArchive.DVDNo);
			dbCom.Parameters.Add("@p_bIsIssued",objArchive.IsIssued);
			dbCom.Parameters.Add("@p_nOnsiteBackup",objArchive.OnsiteBackup);
			dbCom.Parameters.Add("@p_nOffsiteBackup",objArchive.OffsiteBackup);
			dbCom.Parameters.Add("@p_strTitleOfDVD",objArchive.TitleOfDVD);
			dbCom.Parameters.Add("@p_nCreatedBy",objArchive.CreatedBy);
			dbCom.Parameters.Add("@p_dtCreatedOn",objArchive.CreatedOn);
			dbCom.Parameters.Add("@p_nEditedBy",objArchive.EditedBy);
			dbCom.Parameters.Add("@p_dtEditedOn",objArchive.EditedOn);

            dbCom.Parameters.Add("@p_strPartNo", objArchive.PartNo);

            if (objArchive.onSiteMediaType.ToString()  != "0")
            {
                dbCom.Parameters.Add("@p_nOnsiteMediaTypeID", objArchive.onSiteMediaType);
            }
            else
            {
                dbCom.Parameters.Add("@p_nOnsiteMediaTypeID", System.DBNull.Value);
            }

            if (objArchive.offSiteMediaType.ToString()  != "0")
            {
                dbCom.Parameters.Add("@p_nOffsiteMediaTypeID", objArchive.offSiteMediaType);
            }
            else
            {
                dbCom.Parameters.Add("@p_nOffsiteMediaTypeID", System.DBNull.Value);
            }

            if (objArchive.DepartmentID.ToString() != "0")
            {
                dbCom.Parameters.Add("@p_nDepartmentID", objArchive.DepartmentID);
            }
            else
            {
                dbCom.Parameters.Add("@p_nDepartmentID", System.DBNull.Value);
            }

            if (objArchive.PhotographerID.ToString() != "0")
            {
                dbCom.Parameters.Add("@p_nPhotographerID", objArchive.PhotographerID);
            }
            else
            {
                dbCom.Parameters.Add("@p_nPhotographerID", System.DBNull.Value);
            }
 /*Output Parameters*/
			SqlParameter pArchiveID=new SqlParameter();
			pArchiveID.ParameterName="@p_nArchiveID";
			pArchiveID.SqlDbType = SqlDbType.BigInt;
			pArchiveID.Direction=ParameterDirection.Output;
			dbCom.Parameters.Add(pArchiveID);

				dbConn.Open();
				dbCom.ExecuteNonQuery();
				dbConn.Close();

                return Int64.Parse(pArchiveID.Value.ToString());
		}

 //       public ArchiveDB GetArchive(Int64 archiveID)
 //       {

 //           SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
 //           SqlCommand dbCom=new SqlCommand("usp_GetArchive", dbConn);
 //           dbCom.CommandType=CommandType.StoredProcedure;


 ///*Input Parameters*/
 //           dbCom.Parameters.Add("@p_nArchiveID",archiveID);

 ///*Output Parameters*/
 //           SqlParameter pMediaNo=new SqlParameter();
 //           pMediaNo.ParameterName="@p_nMediaNo";
 //           pMediaNo.SqlDbType = SqlDbType.BigInt;
 //           pMediaNo.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pMediaNo);

 //           SqlParameter pChannelID=new SqlParameter();
 //           pChannelID.ParameterName="@p_nChannelID";
 //           pChannelID.SqlDbType = SqlDbType.SmallInt;
 //           pChannelID.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pChannelID);

 //           SqlParameter pCategoryID=new SqlParameter();
 //           pCategoryID.ParameterName="@p_nCategoryID";
 //           pCategoryID.SqlDbType = SqlDbType.SmallInt;
 //           pCategoryID.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pCategoryID);

 //           SqlParameter pMediaTypeID=new SqlParameter();
 //           pMediaTypeID.ParameterName="@p_nMediaTypeID";
 //           pMediaTypeID.SqlDbType = SqlDbType.Int;
 //           pMediaTypeID.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pMediaTypeID);

 //           SqlParameter pClassificationID=new SqlParameter();
 //           pClassificationID.ParameterName="@p_nClassificationID";
 //           pClassificationID.SqlDbType = SqlDbType.Int;
 //           pClassificationID.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pClassificationID);

 //           SqlParameter pEntryDate=new SqlParameter();
 //           pEntryDate.ParameterName="@p_dtEntryDate";
 //           pEntryDate.SqlDbType = SqlDbType.DateTime;
 //           pEntryDate.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pEntryDate);

 //           SqlParameter pShootDate=new SqlParameter();
 //           pShootDate.ParameterName="@p_dtShootDate";
 //           pShootDate.SqlDbType = SqlDbType.DateTime;
 //           pShootDate.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pShootDate);

 //           SqlParameter pSourceID=new SqlParameter();
 //           pSourceID.ParameterName="@p_nSourceID";
 //           pSourceID.SqlDbType = SqlDbType.Int;
 //           pSourceID.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pSourceID);

 //           SqlParameter pFootageTypeID=new SqlParameter();
 //           pFootageTypeID.ParameterName="@p_nFootageTypeID";
 //           pFootageTypeID.SqlDbType = SqlDbType.Int;
 //           pFootageTypeID.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pFootageTypeID);

 //           SqlParameter pFileName=new SqlParameter();
 //           pFileName.ParameterName="@p_strFileName";
 //           pFileName.SqlDbType = SqlDbType.VarChar;
 //           pFileName.Size = 200;
 //           pFileName.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pFileName);

 //           SqlParameter pJTSFileName=new SqlParameter();
 //           pJTSFileName.ParameterName="@p_strJTSFileName";
 //           pJTSFileName.SqlDbType = SqlDbType.VarChar;
 //           pJTSFileName.Size = 200;
 //           pJTSFileName.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pJTSFileName);

 //           SqlParameter pJTSTicketNo=new SqlParameter();
 //           pJTSTicketNo.ParameterName="@p_strJTSTicketNo";
 //           pJTSTicketNo.SqlDbType = SqlDbType.VarChar;
 //           pJTSTicketNo.Size = 50;
 //           pJTSTicketNo.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pJTSTicketNo);

 //           SqlParameter pJTSReporterID=new SqlParameter();
 //           pJTSReporterID.ParameterName="@p_nJTSReporterID";
 //           pJTSReporterID.SqlDbType = SqlDbType.Int;
 //           pJTSReporterID.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pJTSReporterID);

 //           SqlParameter pJTSCameramanID=new SqlParameter();
 //           pJTSCameramanID.ParameterName="@p_nJTSCameramanID";
 //           pJTSCameramanID.SqlDbType = SqlDbType.Int;
 //           pJTSCameramanID.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pJTSCameramanID);

 //           SqlParameter pJTSBureau=new SqlParameter();
 //           pJTSBureau.ParameterName="@p_nJTSBureau";
 //           pJTSBureau.SqlDbType = SqlDbType.Int;
 //           pJTSBureau.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pJTSBureau);

 //           SqlParameter pJTSJobType=new SqlParameter();
 //           pJTSJobType.ParameterName="@p_strJTSJobType";
 //           pJTSJobType.SqlDbType = SqlDbType.VarChar;
 //           pJTSJobType.Size = 30;
 //           pJTSJobType.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pJTSJobType);

 //           SqlParameter pJTSCreatedBy=new SqlParameter();
 //           pJTSCreatedBy.ParameterName="@p_strJTSCreatedBy";
 //           pJTSCreatedBy.SqlDbType = SqlDbType.VarChar;
 //           pJTSCreatedBy.Size = 30;
 //           pJTSCreatedBy.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pJTSCreatedBy);

 //           SqlParameter pJTSSlug=new SqlParameter();
 //           pJTSSlug.ParameterName="@p_strJTSSlug";
 //           pJTSSlug.SqlDbType = SqlDbType.VarChar;
 //           pJTSSlug.Size = 2000;
 //           pJTSSlug.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pJTSSlug);

 //           SqlParameter pJTSDuration=new SqlParameter();
 //           pJTSDuration.ParameterName="@p_strJTSDuration";
 //           pJTSDuration.SqlDbType = SqlDbType.Char;
 //           pJTSDuration.Size = 10;
 //           pJTSDuration.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pJTSDuration);

 //           SqlParameter pRackNo=new SqlParameter();
 //           pRackNo.ParameterName="@p_strRackNo";
 //           pRackNo.SqlDbType = SqlDbType.VarChar;
 //           pRackNo.Size = 10;
 //           pRackNo.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pRackNo);

 //           SqlParameter pShelfNo=new SqlParameter();
 //           pShelfNo.ParameterName="@p_strShelfNo";
 //           pShelfNo.SqlDbType = SqlDbType.VarChar;
 //           pShelfNo.Size = 10;
 //           pShelfNo.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pShelfNo);

 //           SqlParameter pProducerID=new SqlParameter();
 //           pProducerID.ParameterName="@p_nProducerID";
 //           pProducerID.SqlDbType = SqlDbType.SmallInt;
 //           pProducerID.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pProducerID);

 //           SqlParameter pDirector=new SqlParameter();
 //           pDirector.ParameterName="@p_strDirector";
 //           pDirector.SqlDbType = SqlDbType.VarChar;
 //           pDirector.Size = 50;
 //           pDirector.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pDirector);

 //           SqlParameter pSinger_Band=new SqlParameter();
 //           pSinger_Band.ParameterName="@p_strSinger_Band";
 //           pSinger_Band.SqlDbType = SqlDbType.VarChar;
 //           pSinger_Band.Size = 50;
 //           pSinger_Band.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pSinger_Band);

 //           SqlParameter pDuration=new SqlParameter();
 //           pDuration.ParameterName="@p_strDuration";
 //           pDuration.SqlDbType = SqlDbType.VarChar;
 //           pDuration.Size = 10;
 //           pDuration.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pDuration);

 //           SqlParameter pProgrammeTitleOrName=new SqlParameter();
 //           pProgrammeTitleOrName.ParameterName="@p_strProgrammeTitleOrName";
 //           pProgrammeTitleOrName.SqlDbType = SqlDbType.VarChar;
 //           pProgrammeTitleOrName.Size = 100;
 //           pProgrammeTitleOrName.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pProgrammeTitleOrName);

 //           SqlParameter pProgramGenre_Lyrics=new SqlParameter();
 //           pProgramGenre_Lyrics.ParameterName="@p_strProgramGenre_Lyrics";
 //           pProgramGenre_Lyrics.SqlDbType = SqlDbType.VarChar;
 //           pProgramGenre_Lyrics.Size = 100;
 //           pProgramGenre_Lyrics.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pProgramGenre_Lyrics);

 //           SqlParameter pSubTitleOrCaption_Cast=new SqlParameter();
 //           pSubTitleOrCaption_Cast.ParameterName="@p_strSubTitleOrCaption_Cast";
 //           pSubTitleOrCaption_Cast.SqlDbType = SqlDbType.VarChar;
 //           pSubTitleOrCaption_Cast.Size = 200;
 //           pSubTitleOrCaption_Cast.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pSubTitleOrCaption_Cast);

 //           SqlParameter pTelecastDate=new SqlParameter();
 //           pTelecastDate.ParameterName="@p_dtTelecastDate";
 //           pTelecastDate.SqlDbType = SqlDbType.DateTime;
 //           pTelecastDate.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pTelecastDate);

 //           SqlParameter pMidbreaks=new SqlParameter();
 //           pMidbreaks.ParameterName="@p_nMidbreaks";
 //           pMidbreaks.SqlDbType = SqlDbType.Int;
 //           pMidbreaks.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pMidbreaks);

 //           SqlParameter pEpisodeNo=new SqlParameter();
 //           pEpisodeNo.ParameterName="@p_strEpisodeNo";
 //           pEpisodeNo.SqlDbType = SqlDbType.VarChar;
 //           pEpisodeNo.Size = 5;
 //           pEpisodeNo.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pEpisodeNo);

 //           SqlParameter pWriter=new SqlParameter();
 //           pWriter.ParameterName="@p_strWriter";
 //           pWriter.SqlDbType = SqlDbType.VarChar;
 //           pWriter.Size = 50;
 //           pWriter.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pWriter);

 //           SqlParameter pNOCNo=new SqlParameter();
 //           pNOCNo.ParameterName="@p_strNOCNo";
 //           pNOCNo.SqlDbType = SqlDbType.VarChar;
 //           pNOCNo.Size = 5;
 //           pNOCNo.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pNOCNo);

 //           SqlParameter pLanguageID=new SqlParameter();
 //           pLanguageID.ParameterName="@p_nLanguageID";
 //           pLanguageID.SqlDbType = SqlDbType.SmallInt;
 //           pLanguageID.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pLanguageID);

 //           SqlParameter pTheme=new SqlParameter();
 //           pTheme.ParameterName="@p_strTheme";
 //           pTheme.SqlDbType = SqlDbType.VarChar;
 //           pTheme.Size = 30;
 //           pTheme.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pTheme);

 //           SqlParameter pRemarks=new SqlParameter();
 //           pRemarks.ParameterName="@p_strRemarks";
 //           pRemarks.SqlDbType = SqlDbType.VarChar;
 //           pRemarks.Size = 100;
 //           pRemarks.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pRemarks);

 //           SqlParameter pAlbum=new SqlParameter();
 //           pAlbum.ParameterName="@p_strAlbum";
 //           pAlbum.SqlDbType = SqlDbType.VarChar;
 //           pAlbum.Size = 50;
 //           pAlbum.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pAlbum);

 //           SqlParameter pOrigin=new SqlParameter();
 //           pOrigin.ParameterName="@p_strOrigin";
 //           pOrigin.SqlDbType = SqlDbType.VarChar;
 //           pOrigin.Size = 30;
 //           pOrigin.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pOrigin);

 //           SqlParameter pFootageDetail=new SqlParameter();
 //           pFootageDetail.ParameterName="@p_strFootageDetail";
 //           pFootageDetail.SqlDbType = SqlDbType.VarChar;
 //           pFootageDetail.Size = 200;
 //           pFootageDetail.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pFootageDetail);

 //           SqlParameter pClient=new SqlParameter();
 //           pClient.ParameterName="@p_strClient";
 //           pClient.SqlDbType = SqlDbType.VarChar;
 //           pClient.Size = 100;
 //           pClient.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pClient);

 //           SqlParameter pCdate=new SqlParameter();
 //           pCdate.ParameterName="@p_dtCdate";
 //           pCdate.SqlDbType = SqlDbType.DateTime;
 //           pCdate.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pCdate);

 //           SqlParameter pCTime=new SqlParameter();
 //           pCTime.ParameterName="@p_dtCTime";
 //           pCTime.SqlDbType = SqlDbType.DateTime;
 //           pCTime.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pCTime);

 //           SqlParameter pSeaChangeCode=new SqlParameter();
 //           pSeaChangeCode.ParameterName="@p_strSeaChangeCode";
 //           pSeaChangeCode.SqlDbType = SqlDbType.VarChar;
 //           pSeaChangeCode.Size = 20;
 //           pSeaChangeCode.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pSeaChangeCode);

 //           SqlParameter pMediaStatusID=new SqlParameter();
 //           pMediaStatusID.ParameterName="@p_nMediaStatusID";
 //           pMediaStatusID.SqlDbType = SqlDbType.SmallInt;
 //           pMediaStatusID.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pMediaStatusID);

 //           SqlParameter pInputTypeID=new SqlParameter();
 //           pInputTypeID.ParameterName="@p_nInputTypeID";
 //           pInputTypeID.SqlDbType = SqlDbType.Int;
 //           pInputTypeID.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pInputTypeID);

 //           SqlParameter pTotalCapacity=new SqlParameter();
 //           pTotalCapacity.ParameterName="@p_dTotalCapacity";
 //           pTotalCapacity.SqlDbType = SqlDbType.Decimal;
 //           pTotalCapacity.Precision = 18;
 //           pTotalCapacity.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pTotalCapacity);

 //           SqlParameter pCapacityUtilized=new SqlParameter();
 //           pCapacityUtilized.ParameterName="@p_dCapacityUtilized";
 //           pCapacityUtilized.SqlDbType = SqlDbType.Decimal;
 //           pCapacityUtilized.Precision = 18;
 //           pCapacityUtilized.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pCapacityUtilized);

 //           SqlParameter pCapacityAvailable=new SqlParameter();
 //           pCapacityAvailable.ParameterName="@p_dCapacityAvailable";
 //           pCapacityAvailable.SqlDbType = SqlDbType.Decimal;
 //           pCapacityAvailable.Precision = 18;
 //           pCapacityAvailable.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pCapacityAvailable);

 //           SqlParameter pDVDTitle=new SqlParameter();
 //           pDVDTitle.ParameterName="@p_strDVDTitle";
 //           pDVDTitle.SqlDbType = SqlDbType.VarChar;
 //           pDVDTitle.Size = 50;
 //           pDVDTitle.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pDVDTitle);

 //           SqlParameter pIsHighClip=new SqlParameter();
 //           pIsHighClip.ParameterName="@p_bIsHighClip";
 //           pIsHighClip.SqlDbType = SqlDbType.Bit;
 //           pIsHighClip.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pIsHighClip);

 //           SqlParameter pIsLowClip=new SqlParameter();
 //           pIsLowClip.ParameterName="@p_bIsLowClip";
 //           pIsLowClip.SqlDbType = SqlDbType.Bit;
 //           pIsLowClip.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pIsLowClip);

 //           SqlParameter pDVDNo=new SqlParameter();
 //           pDVDNo.ParameterName="@p_nDVDNo";
 //           pDVDNo.SqlDbType = SqlDbType.BigInt;
 //           pDVDNo.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pDVDNo);

 //           SqlParameter pIsIssued=new SqlParameter();
 //           pIsIssued.ParameterName="@p_bIsIssued";
 //           pIsIssued.SqlDbType = SqlDbType.Bit;
 //           pIsIssued.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pIsIssued);

 //           SqlParameter pOnsiteBackup=new SqlParameter();
 //           pOnsiteBackup.ParameterName="@p_nOnsiteBackup";
 //           pOnsiteBackup.SqlDbType = SqlDbType.BigInt;
 //           pOnsiteBackup.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pOnsiteBackup);

 //           SqlParameter pOffsiteBackup=new SqlParameter();
 //           pOffsiteBackup.ParameterName="@p_nOffsiteBackup";
 //           pOffsiteBackup.SqlDbType = SqlDbType.BigInt;
 //           pOffsiteBackup.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pOffsiteBackup);

 //           SqlParameter pTitleOfDVD=new SqlParameter();
 //           pTitleOfDVD.ParameterName="@p_strTitleOfDVD";
 //           pTitleOfDVD.SqlDbType = SqlDbType.VarChar;
 //           pTitleOfDVD.Size = 100;
 //           pTitleOfDVD.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pTitleOfDVD);

 //           SqlParameter pCreatedBy=new SqlParameter();
 //           pCreatedBy.ParameterName="@p_nCreatedBy";
 //           pCreatedBy.SqlDbType = SqlDbType.Int;
 //           pCreatedBy.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pCreatedBy);

 //           SqlParameter pCreatedOn=new SqlParameter();
 //           pCreatedOn.ParameterName="@p_dtCreatedOn";
 //           pCreatedOn.SqlDbType = SqlDbType.DateTime;
 //           pCreatedOn.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pCreatedOn);

 //           SqlParameter pEditedBy=new SqlParameter();
 //           pEditedBy.ParameterName="@p_nEditedBy";
 //           pEditedBy.SqlDbType = SqlDbType.Int;
 //           pEditedBy.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pEditedBy);

 //           SqlParameter pEditedOn=new SqlParameter();
 //           pEditedOn.ParameterName="@p_dtEditedOn";
 //           pEditedOn.SqlDbType = SqlDbType.DateTime;
 //           pEditedOn.Direction=ParameterDirection.Output;
 //           dbCom.Parameters.Add(pEditedOn);

 //               dbConn.Open();
 //               dbCom.ExecuteNonQuery();
 //               dbConn.Close();

 //           ArchiveDB objArchive = new ArchiveDB();

 //           objArchive.MediaNo = Int64.Parse(pMediaNo.Value.ToString());
 //           objArchive.ChannelID = Int32.Parse(pChannelID.Value.ToString());
 //           objArchive.CategoryID = Int32.Parse(pCategoryID.Value.ToString());
 //           objArchive.MediaTypeID = Int32.Parse(pMediaTypeID.Value.ToString());
 //           objArchive.ClassificationID = Int32.Parse(pClassificationID.Value.ToString());
 //           objArchive.EntryDate = DateTime.Parse(pEntryDate.Value.ToString());
 //           objArchive.ShootDate = DateTime.Parse(pShootDate.Value.ToString());
 //           objArchive.SourceID = Int32.Parse(pSourceID.Value.ToString());
 //           objArchive.FootageTypeID = Int32.Parse(pFootageTypeID.Value.ToString());
 //           objArchive.FileName = pFileName.Value.ToString();
 //           objArchive.JTSFileName = pJTSFileName.Value.ToString();
 //           objArchive.JTSTicketNo = pJTSTicketNo.Value.ToString();
 //           objArchive.JTSReporterID = Int32.Parse(pJTSReporterID.Value.ToString());
 //           objArchive.JTSCameramanID = Int32.Parse(pJTSCameramanID.Value.ToString());
 //           objArchive.JTSBureau = Int32.Parse(pJTSBureau.Value.ToString());
 //           objArchive.JTSJobType = pJTSJobType.Value.ToString();
 //           objArchive.JTSCreatedBy = pJTSCreatedBy.Value.ToString();
 //           objArchive.JTSSlug = pJTSSlug.Value.ToString();
 //           objArchive.JTSDuration = pJTSDuration.Value.ToString();
 //           objArchive.RackNo = pRackNo.Value.ToString();
 //           objArchive.ShelfNo = pShelfNo.Value.ToString();
 //           objArchive.ProducerID = Int32.Parse(pProducerID.Value.ToString());
 //           objArchive.Director = pDirector.Value.ToString();
 //           objArchive.Singer_Band = pSinger_Band.Value.ToString();
 //           objArchive.Duration = pDuration.Value.ToString();
 //           objArchive.ProgrammeTitleOrName = pProgrammeTitleOrName.Value.ToString();
 //           objArchive.ProgramGenre_Lyrics = pProgramGenre_Lyrics.Value.ToString();
 //           objArchive.SubTitleOrCaption_Cast = pSubTitleOrCaption_Cast.Value.ToString();
 //           objArchive.TelecastDate = DateTime.Parse(pTelecastDate.Value.ToString());
 //           objArchive.Midbreaks = Int32.Parse(pMidbreaks.Value.ToString());
 //           objArchive.EpisodeNo = pEpisodeNo.Value.ToString();
 //           objArchive.Writer = pWriter.Value.ToString();
 //           objArchive.NOCNo = pNOCNo.Value.ToString();
 //           objArchive.LanguageID = Int32.Parse(pLanguageID.Value.ToString());
 //           objArchive.Theme = pTheme.Value.ToString();
 //           objArchive.Remarks = pRemarks.Value.ToString();
 //           objArchive.Album = pAlbum.Value.ToString();
 //           objArchive.Origin = pOrigin.Value.ToString();
 //           objArchive.FootageDetail = pFootageDetail.Value.ToString();
 //           objArchive.Client = pClient.Value.ToString();
 //           objArchive.Cdate = DateTime.Parse(pCdate.Value.ToString());
 //           objArchive.CTime = DateTime.Parse(pCTime.Value.ToString());
 //           objArchive.SeaChangeCode = pSeaChangeCode.Value.ToString();
 //           objArchive.MediaStatusID = Int32.Parse(pMediaStatusID.Value.ToString());
 //           objArchive.InputTypeID = Int32.Parse(pInputTypeID.Value.ToString());
 //           objArchive.TotalCapacity = Decimal.Parse(pTotalCapacity.Value.ToString());
 //           objArchive.CapacityUtilized = Decimal.Parse(pCapacityUtilized.Value.ToString());
 //           objArchive.CapacityAvailable = Decimal.Parse(pCapacityAvailable.Value.ToString());
 //           objArchive.DVDTitle = pDVDTitle.Value.ToString();
 //           objArchive.IsHighClip = Boolean.Parse(pIsHighClip.Value.ToString());
 //           objArchive.IsLowClip = Boolean.Parse(pIsLowClip.Value.ToString());
 //           objArchive.DVDNo = Int64.Parse(pDVDNo.Value.ToString());
 //           objArchive.IsIssued = Boolean.Parse(pIsIssued.Value.ToString());
 //           objArchive.OnsiteBackup = Int64.Parse(pOnsiteBackup.Value.ToString());
 //           objArchive.OffsiteBackup = Int64.Parse(pOffsiteBackup.Value.ToString());
 //           objArchive.TitleOfDVD = pTitleOfDVD.Value.ToString();
 //           objArchive.CreatedBy = Int32.Parse(pCreatedBy.Value.ToString());
 //           objArchive.CreatedOn = DateTime.Parse(pCreatedOn.Value.ToString());
 //           objArchive.EditedBy = Int32.Parse(pEditedBy.Value.ToString());
 //           objArchive.EditedOn = DateTime.Parse(pEditedOn.Value.ToString());
 //           objArchive.ArchiveID = archiveID;

 //           return objArchive;
 //       }
        public DataTable GetAllArchive(int archiveID, object  categoryID)
        { 
            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			//SqlDataAdapter dbAdapter=new SqlDataAdapter("[usp_GetAllArchiveForDeskTop]", dbConn);
            SqlDataAdapter dbAdapter = new SqlDataAdapter("[usp_GetAllArchiveForDeskTop_bk]", dbConn);
			dbAdapter.SelectCommand.CommandType=CommandType.StoredProcedure;

			if(archiveID!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveID",archiveID);
			}
			else
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveID",System.DBNull.Value);
			}			
			
			if(categoryID!=null)
			{
				dbAdapter.SelectCommand.Parameters.Add("@p_nCategoryID",categoryID);
            }

            DataTable dtArchive = new DataTable("ArchiveDB");

            dbAdapter.Fill(dtArchive);

            return dtArchive;
        }
     
        //public DataTable GetAllArchive(object archiveID, object mediaNo, object channelID, object categoryID, object mediaTypeID, 
        //    object classificationID, object entryDate, object shootDate, object sourceID, object footageTypeID, object fileName, 
        //    object jTSFileName, object jTSTicketNo, object jTSReporterID, object jTSCameramanID, object jTSBureau, 
        //    object jTSJobType, object jTSCreatedBy, object jTSSlug, object jTSDuration, object rackNo, object shelfNo, object producerID, 
        //    object director, object singer_Band, object duration, object programmeTitleOrName, object programGenre_Lyrics, 
        //    object subTitleOrCaption_Cast, object telecastDate, object midbreaks, object episodeNo, object writer, object nOCNo, 
        //    object languageID, object theme, object remarks, object album, object origin, object footageDetail, object client, object cdate, 
        //    object cTime, object seaChangeCode, object mediaStatusID, object inputTypeID, object totalCapacity, object capacityUtilized, 
        //    object capacityAvailable, object dVDTitle, object isHighClip, object isLowClip, object dVDNo, object isIssued, object onsiteBackup, 
        //    object offsiteBackup, object titleOfDVD, object createdBy, object createdOn, object editedBy, object editedOn, object partNo,
        //    object OnsiteMediaTypeId, object OffsiteMediaTypeId, object DepartmentId, object PhotographerId)
        //{

        //    SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
        //    SqlDataAdapter dbAdapter=new SqlDataAdapter("usp_GetAllArchive", dbConn);
        //    dbAdapter.SelectCommand.CommandType=CommandType.StoredProcedure;

        //    if(archiveID!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveID",archiveID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveID",System.DBNull.Value);
        //    }
        //    if(mediaNo!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nMediaNo",mediaNo);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nMediaNo",System.DBNull.Value);
        //    }
        //    if(channelID!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nChannelID",channelID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nChannelID",System.DBNull.Value);
        //    }
        //    if(categoryID!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nCategoryID",categoryID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nCategoryID",System.DBNull.Value);
        //    }
        //    if(mediaTypeID!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nMediaTypeID",mediaTypeID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nMediaTypeID",System.DBNull.Value);
        //    }
        //    if(classificationID!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nClassificationID",classificationID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nClassificationID",System.DBNull.Value);
        //    }
        //    if(entryDate!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtEntryDate",entryDate);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtEntryDate",System.DBNull.Value);
        //    }
        //    if(shootDate!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtShootDate",shootDate);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtShootDate",System.DBNull.Value);
        //    }
        //    if(sourceID!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nSourceID",sourceID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nSourceID",System.DBNull.Value);
        //    }
        //    if(footageTypeID!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nFootageTypeID",footageTypeID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nFootageTypeID",System.DBNull.Value);
        //    }
        //    if(fileName!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strFileName",fileName);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strFileName",System.DBNull.Value);
        //    }
        //    if(jTSFileName!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSFileName",jTSFileName);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSFileName",System.DBNull.Value);
        //    }
        //    if(jTSTicketNo!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSTicketNo",jTSTicketNo);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSTicketNo",System.DBNull.Value);
        //    }
        //    if(jTSReporterID!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nJTSReporterID",jTSReporterID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nJTSReporterID",System.DBNull.Value);
        //    }
        //    if(jTSCameramanID!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nJTSCameramanID",jTSCameramanID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nJTSCameramanID",System.DBNull.Value);
        //    }
        //    if(jTSBureau!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nJTSBureau",jTSBureau);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nJTSBureau",System.DBNull.Value);
        //    }
        //    if(jTSJobType!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSJobType",jTSJobType);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSJobType",System.DBNull.Value);
        //    }
        //    if(jTSCreatedBy!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSCreatedBy",jTSCreatedBy);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSCreatedBy",System.DBNull.Value);
        //    }
        //    if(jTSSlug!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSSlug",jTSSlug);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSSlug",System.DBNull.Value);
        //    }
        //    if(jTSDuration!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSDuration",jTSDuration);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSDuration",System.DBNull.Value);
        //    }
        //    if(rackNo!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strRackNo",rackNo);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strRackNo",System.DBNull.Value);
        //    }
        //    if(shelfNo!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strShelfNo",shelfNo);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strShelfNo",System.DBNull.Value);
        //    }
        //    if(producerID!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nProducerID",producerID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nProducerID",System.DBNull.Value);
        //    }
        //    if(director!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strDirector",director);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strDirector",System.DBNull.Value);
        //    }
        //    if(singer_Band!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strSinger_Band",singer_Band);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strSinger_Band",System.DBNull.Value);
        //    }
        //    if(duration!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strDuration",duration);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strDuration",System.DBNull.Value);
        //    }
        //    if(programmeTitleOrName!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strProgrammeTitleOrName",programmeTitleOrName);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strProgrammeTitleOrName",System.DBNull.Value);
        //    }
        //    if(programGenre_Lyrics!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strProgramGenre_Lyrics",programGenre_Lyrics);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strProgramGenre_Lyrics",System.DBNull.Value);
        //    }
        //    if(subTitleOrCaption_Cast!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strSubTitleOrCaption_Cast",subTitleOrCaption_Cast);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strSubTitleOrCaption_Cast",System.DBNull.Value);
        //    }
        //    if(telecastDate!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtTelecastDate",telecastDate);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtTelecastDate",System.DBNull.Value);
        //    }
        //    if(midbreaks!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nMidbreaks",midbreaks);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nMidbreaks",System.DBNull.Value);
        //    }
        //    if(episodeNo!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strEpisodeNo",episodeNo);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strEpisodeNo",System.DBNull.Value);
        //    }
        //    if(writer!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strWriter",writer);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strWriter",System.DBNull.Value);
        //    }
        //    if(nOCNo!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strNOCNo",nOCNo);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strNOCNo",System.DBNull.Value);
        //    }
        //    if(languageID!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nLanguageID",languageID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nLanguageID",System.DBNull.Value);
        //    }
        //    if(theme!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strTheme",theme);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strTheme",System.DBNull.Value);
        //    }
        //    if(remarks!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strRemarks",remarks);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strRemarks",System.DBNull.Value);
        //    }
        //    if(album!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strAlbum",album);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strAlbum",System.DBNull.Value);
        //    }
        //    if(origin!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strOrigin",origin);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strOrigin",System.DBNull.Value);
        //    }
        //    if(footageDetail!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strFootageDetail",footageDetail);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strFootageDetail",System.DBNull.Value);
        //    }
        //    if(client!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strClient",client);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strClient",System.DBNull.Value);
        //    }
        //    if(cdate!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtCdate",cdate);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtCdate",System.DBNull.Value);
        //    }
        //    if(cTime!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtCTime",cTime);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtCTime",System.DBNull.Value);
        //    }
        //    if(seaChangeCode!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strSeaChangeCode",seaChangeCode);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strSeaChangeCode",System.DBNull.Value);
        //    }
        //    if(mediaStatusID!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nMediaStatusID",mediaStatusID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nMediaStatusID",System.DBNull.Value);
        //    }
        //    if(inputTypeID!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nInputTypeID",inputTypeID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nInputTypeID",System.DBNull.Value);
        //    }
        //    if(totalCapacity!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dTotalCapacity",totalCapacity);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dTotalCapacity",System.DBNull.Value);
        //    }
        //    if(capacityUtilized!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dCapacityUtilized",capacityUtilized);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dCapacityUtilized",System.DBNull.Value);
        //    }
        //    if(capacityAvailable!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dCapacityAvailable",capacityAvailable);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dCapacityAvailable",System.DBNull.Value);
        //    }
        //    if(dVDTitle!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strDVDTitle",dVDTitle);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strDVDTitle",System.DBNull.Value);
        //    }
        //    if(isHighClip!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_bIsHighClip",isHighClip);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_bIsHighClip",System.DBNull.Value);
        //    }
        //    if(isLowClip!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_bIsLowClip",isLowClip);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_bIsLowClip",System.DBNull.Value);
        //    }
        //    if(dVDNo!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nDVDNo",dVDNo);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nDVDNo",System.DBNull.Value);
        //    }
        //    if(isIssued!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_bIsIssued",isIssued);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_bIsIssued",System.DBNull.Value);
        //    }
        //    if(onsiteBackup!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nOnsiteBackup",onsiteBackup);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nOnsiteBackup",System.DBNull.Value);
        //    }
        //    if(offsiteBackup!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nOffsiteBackup",offsiteBackup);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nOffsiteBackup",System.DBNull.Value);
        //    }
        //    if(titleOfDVD!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strTitleOfDVD",titleOfDVD);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strTitleOfDVD",System.DBNull.Value);
        //    }
        //    if(createdBy!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nCreatedBy",createdBy);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nCreatedBy",System.DBNull.Value);
        //    }
        //    if(createdOn!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtCreatedOn",createdOn);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtCreatedOn",System.DBNull.Value);
        //    }
        //    if(editedBy!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nEditedBy",editedBy);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nEditedBy",System.DBNull.Value);
        //    }
        //    if(editedOn!=null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtEditedOn",editedOn);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtEditedOn",System.DBNull.Value);
        //    }

        //    if (partNo != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strPartNo", partNo);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strPartNo", System.DBNull.Value);
        //    }

        //    if (OnsiteMediaTypeId != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nOnsiteMediaTypeID", OnsiteMediaTypeId);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nOnsiteMediaTypeID", System.DBNull.Value);
        //    }

        //    if (OffsiteMediaTypeId != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nOffsiteMediaTypeID", OffsiteMediaTypeId);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nOffsiteMediaTypeID", System.DBNull.Value);
        //    }

        //    if (DepartmentId  != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nDepartmentID", DepartmentId);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nDepartmentID", System.DBNull.Value);
        //    }            

        //    if (PhotographerId  != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nPhotographerID", PhotographerId);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nPhotographerID", System.DBNull.Value);
        //    }



        //    DataTable dtArchive=new DataTable("ArchiveDB");

        //    dbAdapter.Fill(dtArchive);

        //    return dtArchive;
        //}

		public void DeleteArchive(Int64 archiveID)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_DeleteArchive", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_nArchiveID",archiveID);

				dbConn.Open();
				dbCom.ExecuteNonQuery();
				dbConn.Close();
		}

		public void UpdateArchive(ArchiveDB objArchive)
		{

			SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
			SqlCommand dbCom=new SqlCommand("usp_UpdateArchive", dbConn);
			dbCom.CommandType=CommandType.StoredProcedure;


 /*Input Parameters*/
			dbCom.Parameters.Add("@p_nArchiveID",objArchive.ArchiveID);
			dbCom.Parameters.Add("@p_nMediaNo",objArchive.MediaNo);
			dbCom.Parameters.Add("@p_nChannelID",objArchive.ChannelID);
			dbCom.Parameters.Add("@p_nCategoryID",objArchive.CategoryID);
			dbCom.Parameters.Add("@p_nMediaTypeID",objArchive.MediaTypeID);
			dbCom.Parameters.Add("@p_nClassificationID",objArchive.ClassificationID);
			dbCom.Parameters.Add("@p_dtEntryDate",objArchive.EntryDate);
			dbCom.Parameters.Add("@p_dtShootDate",objArchive.ShootDate);
			dbCom.Parameters.Add("@p_nSourceID",objArchive.SourceID);
			dbCom.Parameters.Add("@p_nFootageTypeID",objArchive.FootageTypeID);
			dbCom.Parameters.Add("@p_strFileName",objArchive.FileName);
			dbCom.Parameters.Add("@p_strJTSFileName",objArchive.JTSFileName);
			dbCom.Parameters.Add("@p_strJTSTicketNo",objArchive.JTSTicketNo);
			dbCom.Parameters.Add("@p_nJTSReporterID",objArchive.JTSReporterID);
			dbCom.Parameters.Add("@p_nJTSCameramanID",objArchive.JTSCameramanID);
			dbCom.Parameters.Add("@p_nJTSBureau",objArchive.JTSBureau);
			dbCom.Parameters.Add("@p_strJTSJobType",objArchive.JTSJobType);
			dbCom.Parameters.Add("@p_strJTSCreatedBy",objArchive.JTSCreatedBy);
			dbCom.Parameters.Add("@p_strJTSSlug",objArchive.JTSSlug);
			dbCom.Parameters.Add("@p_strJTSDuration",objArchive.JTSDuration);
			dbCom.Parameters.Add("@p_strRackNo",objArchive.RackNo);
			dbCom.Parameters.Add("@p_strShelfNo",objArchive.ShelfNo);
			dbCom.Parameters.Add("@p_nProducerID",objArchive.ProducerID);
			dbCom.Parameters.Add("@p_strDirector",objArchive.Director);
			dbCom.Parameters.Add("@p_strSinger_Band",objArchive.Singer_Band);
			dbCom.Parameters.Add("@p_strDuration",objArchive.Duration);
			dbCom.Parameters.Add("@p_strProgrammeTitleOrName",objArchive.ProgrammeTitleOrName);
			dbCom.Parameters.Add("@p_strProgramGenre_Lyrics",objArchive.ProgramGenre_Lyrics);
			dbCom.Parameters.Add("@p_strSubTitleOrCaption_Cast",objArchive.SubTitleOrCaption_Cast);
			dbCom.Parameters.Add("@p_dtTelecastDate",objArchive.TelecastDate);
			dbCom.Parameters.Add("@p_nMidbreaks",objArchive.Midbreaks);
			dbCom.Parameters.Add("@p_strEpisodeNo",objArchive.EpisodeNo);
			dbCom.Parameters.Add("@p_strWriter",objArchive.Writer);
			dbCom.Parameters.Add("@p_strNOCNo",objArchive.NOCNo);
			dbCom.Parameters.Add("@p_nLanguageID",objArchive.LanguageID);
			dbCom.Parameters.Add("@p_strTheme",objArchive.Theme);
			dbCom.Parameters.Add("@p_strRemarks",objArchive.Remarks);
			dbCom.Parameters.Add("@p_strAlbum",objArchive.Album);
			dbCom.Parameters.Add("@p_strOrigin",objArchive.Origin);
			dbCom.Parameters.Add("@p_strFootageDetail",objArchive.FootageDetail);
			dbCom.Parameters.Add("@p_strClient",objArchive.Client);
			dbCom.Parameters.Add("@p_dtCdate",objArchive.Cdate);
			dbCom.Parameters.Add("@p_dtCTime",objArchive.CTime);
			dbCom.Parameters.Add("@p_strSeaChangeCode",objArchive.SeaChangeCode);
			dbCom.Parameters.Add("@p_nMediaStatusID",objArchive.MediaStatusID);
			dbCom.Parameters.Add("@p_nInputTypeID",objArchive.InputTypeID);
			dbCom.Parameters.Add("@p_dTotalCapacity",objArchive.TotalCapacity);
			dbCom.Parameters.Add("@p_dCapacityUtilized",objArchive.CapacityUtilized);
			dbCom.Parameters.Add("@p_dCapacityAvailable",objArchive.CapacityAvailable);
			dbCom.Parameters.Add("@p_strDVDTitle",objArchive.DVDTitle);
			dbCom.Parameters.Add("@p_bIsHighClip",objArchive.IsHighClip);
			dbCom.Parameters.Add("@p_bIsLowClip",objArchive.IsLowClip);
			dbCom.Parameters.Add("@p_nDVDNo",objArchive.DVDNo);
			dbCom.Parameters.Add("@p_bIsIssued",objArchive.IsIssued);
			dbCom.Parameters.Add("@p_nOnsiteBackup",objArchive.OnsiteBackup);
			dbCom.Parameters.Add("@p_nOffsiteBackup",objArchive.OffsiteBackup);
			dbCom.Parameters.Add("@p_strTitleOfDVD",objArchive.TitleOfDVD);
			dbCom.Parameters.Add("@p_nCreatedBy",objArchive.CreatedBy);
			dbCom.Parameters.Add("@p_dtCreatedOn",objArchive.CreatedOn);
			dbCom.Parameters.Add("@p_nEditedBy",objArchive.EditedBy);
			dbCom.Parameters.Add("@p_dtEditedOn",objArchive.EditedOn);
            dbCom.Parameters.Add("@p_strPartNo", objArchive.PartNo);


            if (objArchive.onSiteMediaType.ToString() != "0")
            {
                dbCom.Parameters.Add("@p_nOnsiteMediaTypeID", objArchive.onSiteMediaType);
            }
            else
            {
                dbCom.Parameters.Add("@p_nOnsiteMediaTypeID", System.DBNull.Value);
            }

            if (objArchive.offSiteMediaType.ToString() != "0")
            {
                dbCom.Parameters.Add("@p_nOffsiteMediaTypeID", objArchive.offSiteMediaType);
            }
            else
            {
                dbCom.Parameters.Add("@p_nOffsiteMediaTypeID", System.DBNull.Value);
            }

            if (objArchive.DepartmentID.ToString() != "0")
            {
                dbCom.Parameters.Add("@p_nDepartmentID", objArchive.DepartmentID);
            }
            else
            {
                dbCom.Parameters.Add("@p_nDepartmentID", System.DBNull.Value);
            }


            if (objArchive.PhotographerID.ToString() != "0")
            {
                dbCom.Parameters.Add("@p_nPhotographerID", objArchive.PhotographerID);
            }
            else
            {
                dbCom.Parameters.Add("@p_nPhotographerID", System.DBNull.Value);
            }

				dbConn.Open();
				dbCom.ExecuteNonQuery();
				dbConn.Close();
		}

        public void UpdateArchiveForMediaNo(Int64 ArchiveID, Int64 MediaNo, String RackNo, String ShelfNo, int writeMode)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlCommand dbCom = new SqlCommand("usp_UpdateArchiveForMediaNo", dbConn);
            dbCom.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbCom.Parameters.Add("@p_nArchiveID", ArchiveID);
            dbCom.Parameters.Add("@p_nMediaNo", MediaNo);
            dbCom.Parameters.Add("@p_strRackNo", RackNo);
            dbCom.Parameters.Add("@p_strShelfNo", ShelfNo);
            dbCom.Parameters.Add("@p_nWriteMode", writeMode);
            
            
            dbConn.Open();
            dbCom.ExecuteNonQuery();
            dbConn.Close();
        }
        public DataTable GetAllArchiveNewsForSearch(
           object archiveIDFrom,
           object archiveIDTo,
           object entryDateFrom,
           object entryDateTo,
           object jTSSlug,
           object CategoryID )
        {
            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllArchiveNewsForSearch", dbConn);//usp_GetAllQuickSearch //sp_GetAllQuickSearch
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dbAdapter.SelectCommand.CommandTimeout = 0;

            if (archiveIDFrom != null && !archiveIDFrom.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveIDFrom", archiveIDFrom);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveIDFrom", System.DBNull.Value);
            }

            if (archiveIDTo != null && !archiveIDTo.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveIDTo", archiveIDTo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveIDTo", System.DBNull.Value);
            }

            if (entryDateFrom != null && !entryDateFrom.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtEntryDateFrom", entryDateFrom);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtEntryDateFrom", System.DBNull.Value);
            }

            if (entryDateTo != null && !entryDateTo.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtEntryDateTo", entryDateTo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtEntryDateTo", System.DBNull.Value);
            }

            if (jTSSlug != null && !jTSSlug.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSSlug", jTSSlug);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSSlug", System.DBNull.Value);
            }
            if (CategoryID != null && !CategoryID.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nCategoryId", CategoryID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nCategoryId", System.DBNull.Value);
            }


            DataTable dtArchive = new DataTable("ArchiveDB");

            dbAdapter.Fill(dtArchive);

            return dtArchive;

        }
        public DataTable GetAllArchiveEntertainment(
          object archiveIDFrom,
          object archiveIDTo,
          object entryDateFrom,
          object entryDateTo,
          object jTSSlug,
          object CategoryID)
        {
            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllArchiveEntertainment", dbConn);//usp_GetAllQuickSearch //sp_GetAllQuickSearch
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dbAdapter.SelectCommand.CommandTimeout = 0;

            if (archiveIDFrom != null && !archiveIDFrom.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveIDFrom", archiveIDFrom);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveIDFrom", System.DBNull.Value);
            }

            if (archiveIDTo != null && !archiveIDTo.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveIDTo", archiveIDTo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveIDTo", System.DBNull.Value);
            }

            if (entryDateFrom != null && !entryDateFrom.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtEntryDateFrom", entryDateFrom);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtEntryDateFrom", System.DBNull.Value);
            }

            if (entryDateTo != null && !entryDateTo.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtEntryDateTo", entryDateTo);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_dtEntryDateTo", System.DBNull.Value);
            }

            if (jTSSlug != null && !jTSSlug.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSSlug", jTSSlug);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_strJTSSlug", System.DBNull.Value);
            }
            if (CategoryID != null && !CategoryID.ToString().Equals(""))
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nCategoryId", CategoryID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nCategoryId", System.DBNull.Value);
            }


            DataTable dtArchive = new DataTable("ArchiveDB");

            dbAdapter.Fill(dtArchive);

            return dtArchive;

        }
        public void UpdateArchiveForFileName(Int64 ArchiveID, String fileName)
        {

            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlCommand dbCom = new SqlCommand("usp_UpdateArchiveForFileName", dbConn);
            dbCom.CommandType = CommandType.StoredProcedure;


            /*Input Parameters*/
            dbCom.Parameters.Add("@p_nArchiveID", ArchiveID);
            dbCom.Parameters.Add("@p_strFileName", fileName);
            
            dbConn.Open();
            dbCom.ExecuteNonQuery();
            dbConn.Close();
        }

        public bool checkMediaExistance(int  mediaNo)
        {
            DataTable dt = new DataTable();
            dt = GetAllArchive(mediaNo, null);
            if (dt.Rows.Count > 0)
                return false;
            else
                return true;

        }


	#endregion 

    }
}
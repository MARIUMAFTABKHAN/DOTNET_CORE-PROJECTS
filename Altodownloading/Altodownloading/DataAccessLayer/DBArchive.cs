using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Altodownloading.DataAccessLayer
{
    public class DBArchive
    {

        #region  Attributes

        private string m_txtdetails;

        private int m_MediaSource;

        private string m_ArchiveScript;

        private Int64 m_lArchiveID = 0;

        private Int64 m_lMediaNo = 0;
        private Int32 m_nAnchorlD = 0;

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

        private string m_chrPartNo = null;

        private Int32 m_nOnsiteMediaTypeId = 0;
        private Int32 m_nOffsiteMediaTypeId = 0;

        private Int32 m_nDepartmentId = 0;

        private Int32 m_nPhotographerId = 0;
        private Int32 m_ProgramTitleID = 0;

        private Int32 m_nBureauTape = 0;

        private bool m_IsMasterCopy = false;
        private bool m_IsInternational = false;
        private string m_BaseStationID = "";

        public int contenttypeID { get; set; }

        public int ChannelCategoryID { get; set; }

        #endregion
        #region  Properties
        public string BaseStationID
        {
            get { return m_BaseStationID; }
            set { m_BaseStationID = value.Trim(); }
        }

        public Int32 MediaSource
        {
            get { return m_MediaSource; }
            set { m_MediaSource = value; }
        }
        public Int32 ProgramTitleID
        {
            get { return m_ProgramTitleID; }
            set { m_ProgramTitleID = value; }
        }


        public Boolean MarkDeleted { get; set; }

        public Boolean IsMasterCopy
        {
            get
            {
                return m_IsMasterCopy;

            }
            set
            {
                m_IsMasterCopy = value;

            }
        }

        public Boolean IsInternational
        {
            get
            {
                return m_IsInternational;

            }
            set
            {
                m_IsInternational = value;

            }
        }
        public string TxtDetails
        {
            get { return m_txtdetails; }
            set { m_txtdetails = value.Trim(); }
        }
        public string ArchiveScript
        {
            get { return m_ArchiveScript; }
            set { m_ArchiveScript = value.Trim(); }
        }
        public Int32 AnchorID
        {
            get
            {
                return m_nAnchorlD;

            }
            set
            {
                m_nAnchorlD = value;

            }
        }
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
        public Int32 BureauTape
        {
            get
            {
                return m_nBureauTape;

            }
            set
            {
                m_nBureauTape = value;

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

        public string PartNo
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

        public int onSiteMediaType
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
        public int offSiteMediaType
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

        public int DepartmentID
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

        public int PhotographerID
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

        #region Methods
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
        //    SqlDataAdapter dbAdapter = new SqlDataAdapter("usp_GetAllArchive", dbConn);
        //    dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        //    if (archiveID != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveID", archiveID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveID", System.DBNull.Value);
        //    }
        //    if (mediaNo != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nMediaNo", mediaNo);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nMediaNo", System.DBNull.Value);
        //    }
        //    if (channelID != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nChannelID", channelID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nChannelID", System.DBNull.Value);
        //    }
        //    if (categoryID != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nCategoryID", categoryID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nCategoryID", System.DBNull.Value);
        //    }
        //    if (mediaTypeID != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nMediaTypeID", mediaTypeID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nMediaTypeID", System.DBNull.Value);
        //    }
        //    if (classificationID != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nClassificationID", classificationID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nClassificationID", System.DBNull.Value);
        //    }
        //    if (entryDate != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtEntryDate", entryDate);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtEntryDate", System.DBNull.Value);
        //    }
        //    if (shootDate != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtShootDate", shootDate);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtShootDate", System.DBNull.Value);
        //    }
        //    if (sourceID != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nSourceID", sourceID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nSourceID", System.DBNull.Value);
        //    }
        //    if (footageTypeID != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nFootageTypeID", footageTypeID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nFootageTypeID", System.DBNull.Value);
        //    }
        //    if (fileName != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strFileName", fileName);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strFileName", System.DBNull.Value);
        //    }
        //    if (jTSFileName != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSFileName", jTSFileName);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSFileName", System.DBNull.Value);
        //    }
        //    if (jTSTicketNo != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSTicketNo", jTSTicketNo);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSTicketNo", System.DBNull.Value);
        //    }
        //    if (jTSReporterID != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nJTSReporterID", jTSReporterID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nJTSReporterID", System.DBNull.Value);
        //    }
        //    if (jTSCameramanID != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nJTSCameramanID", jTSCameramanID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nJTSCameramanID", System.DBNull.Value);
        //    }
        //    if (jTSBureau != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nJTSBureau", jTSBureau);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nJTSBureau", System.DBNull.Value);
        //    }
        //    if (jTSJobType != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSJobType", jTSJobType);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSJobType", System.DBNull.Value);
        //    }
        //    if (jTSCreatedBy != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSCreatedBy", jTSCreatedBy);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSCreatedBy", System.DBNull.Value);
        //    }
        //    if (jTSSlug != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSSlug", jTSSlug);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSSlug", System.DBNull.Value);
        //    }
        //    if (jTSDuration != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSDuration", jTSDuration);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strJTSDuration", System.DBNull.Value);
        //    }
        //    if (rackNo != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strRackNo", rackNo);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strRackNo", System.DBNull.Value);
        //    }
        //    if (shelfNo != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strShelfNo", shelfNo);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strShelfNo", System.DBNull.Value);
        //    }
        //    if (producerID != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nProducerID", producerID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nProducerID", System.DBNull.Value);
        //    }
        //    if (director != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strDirector", director);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strDirector", System.DBNull.Value);
        //    }
        //    if (singer_Band != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strSinger_Band", singer_Band);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strSinger_Band", System.DBNull.Value);
        //    }
        //    if (duration != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strDuration", duration);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strDuration", System.DBNull.Value);
        //    }
        //    if (programmeTitleOrName != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strProgrammeTitleOrName", programmeTitleOrName);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strProgrammeTitleOrName", System.DBNull.Value);
        //    }
        //    if (programGenre_Lyrics != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strProgramGenre_Lyrics", programGenre_Lyrics);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strProgramGenre_Lyrics", System.DBNull.Value);
        //    }
        //    if (subTitleOrCaption_Cast != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strSubTitleOrCaption_Cast", subTitleOrCaption_Cast);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strSubTitleOrCaption_Cast", System.DBNull.Value);
        //    }
        //    if (telecastDate != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtTelecastDate", telecastDate);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtTelecastDate", System.DBNull.Value);
        //    }
        //    if (midbreaks != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nMidbreaks", midbreaks);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nMidbreaks", System.DBNull.Value);
        //    }
        //    if (episodeNo != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strEpisodeNo", episodeNo);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strEpisodeNo", System.DBNull.Value);
        //    }
        //    if (writer != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strWriter", writer);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strWriter", System.DBNull.Value);
        //    }
        //    if (nOCNo != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strNOCNo", nOCNo);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strNOCNo", System.DBNull.Value);
        //    }
        //    if (languageID != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nLanguageID", languageID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nLanguageID", System.DBNull.Value);
        //    }
        //    if (theme != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strTheme", theme);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strTheme", System.DBNull.Value);
        //    }
        //    if (remarks != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strRemarks", remarks);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strRemarks", System.DBNull.Value);
        //    }
        //    if (album != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strAlbum", album);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strAlbum", System.DBNull.Value);
        //    }
        //    if (origin != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strOrigin", origin);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strOrigin", System.DBNull.Value);
        //    }
        //    if (footageDetail != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strFootageDetail", footageDetail);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strFootageDetail", System.DBNull.Value);
        //    }
        //    if (client != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strClient", client);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strClient", System.DBNull.Value);
        //    }
        //    if (cdate != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtCdate", cdate);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtCdate", System.DBNull.Value);
        //    }
        //    if (cTime != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtCTime", cTime);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtCTime", System.DBNull.Value);
        //    }
        //    if (seaChangeCode != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strSeaChangeCode", seaChangeCode);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strSeaChangeCode", System.DBNull.Value);
        //    }
        //    if (mediaStatusID != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nMediaStatusID", mediaStatusID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nMediaStatusID", System.DBNull.Value);
        //    }
        //    if (inputTypeID != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nInputTypeID", inputTypeID);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nInputTypeID", System.DBNull.Value);
        //    }
        //    if (totalCapacity != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dTotalCapacity", totalCapacity);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dTotalCapacity", System.DBNull.Value);
        //    }
        //    if (capacityUtilized != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dCapacityUtilized", capacityUtilized);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dCapacityUtilized", System.DBNull.Value);
        //    }
        //    if (capacityAvailable != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dCapacityAvailable", capacityAvailable);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dCapacityAvailable", System.DBNull.Value);
        //    }
        //    if (dVDTitle != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strDVDTitle", dVDTitle);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strDVDTitle", System.DBNull.Value);
        //    }
        //    if (isHighClip != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_bIsHighClip", isHighClip);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_bIsHighClip", System.DBNull.Value);
        //    }
        //    if (isLowClip != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_bIsLowClip", isLowClip);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_bIsLowClip", System.DBNull.Value);
        //    }
        //    if (dVDNo != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nDVDNo", dVDNo);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nDVDNo", System.DBNull.Value);
        //    }
        //    if (isIssued != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_bIsIssued", isIssued);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_bIsIssued", System.DBNull.Value);
        //    }
        //    if (onsiteBackup != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nOnsiteBackup", onsiteBackup);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nOnsiteBackup", System.DBNull.Value);
        //    }
        //    if (offsiteBackup != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nOffsiteBackup", offsiteBackup);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nOffsiteBackup", System.DBNull.Value);
        //    }
        //    if (titleOfDVD != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strTitleOfDVD", titleOfDVD);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_strTitleOfDVD", System.DBNull.Value);
        //    }
        //    if (createdBy != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nCreatedBy", createdBy);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nCreatedBy", System.DBNull.Value);
        //    }
        //    if (createdOn != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtCreatedOn", createdOn);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtCreatedOn", System.DBNull.Value);
        //    }
        //    if (editedBy != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nEditedBy", editedBy);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nEditedBy", System.DBNull.Value);
        //    }
        //    if (editedOn != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtEditedOn", editedOn);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_dtEditedOn", System.DBNull.Value);
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

        //    if (DepartmentId != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nDepartmentID", DepartmentId);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nDepartmentID", System.DBNull.Value);
        //    }

        //    if (PhotographerId != null)
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nPhotographerID", PhotographerId);
        //    }
        //    else
        //    {
        //        dbAdapter.SelectCommand.Parameters.Add("@p_nPhotographerID", System.DBNull.Value);
        //    }



        //    DataTable dtArchive = new DataTable("ArchiveDB");

        //    dbAdapter.Fill(dtArchive);

        //    return dtArchive;
        //}

        public bool SaveDetails(DBManager db, Int64 aID, DataGridView dtKeywordDetail)
        {
            bool result = false;
            DataAccessLayer.DBArchiveKeyDetailDb objKeyDetail = new DBArchiveKeyDetailDb();
            KeywordDetailDB objKeywordDetail = new KeywordDetailDB();
            DataAccessLayer.DBArchiveKeyDetailDb KeywordDetailDB = new DBArchiveKeyDetailDb();
            int gvCounter = dtKeywordDetail.RowCount;
            int recCoutner = 0;
            foreach (DataGridViewRow dr in dtKeywordDetail.Rows)
            {
                // objKeyDetail.DeleteArchiveKeyDetail(aID);
                objKeyDetail.ArchiveID = aID;
                objKeyDetail.Detail = Convert.ToString(dr.Cells["Details"].Value);
                objKeyDetail.StartTime = Convert.ToString(dr.Cells["StartTime"].Value);
                objKeyDetail.EndTime = Convert.ToString(dr.Cells["EndTime"].Value);
                objKeyDetail.CreatedBy = BAL.GlobalClass.CurrentUserId;
                long keyID = objKeyDetail.InsertArchiveKeyDetail(db, objKeyDetail);
                objKeywordDetail.KeywordTypeID = Convert.ToInt32(dr.Cells["KeyId"].Value);
                objKeywordDetail.Keyword = Convert.ToString(dr.Cells["Keywords"].Value);
                objKeywordDetail.ArchiveKeyDetailID = keyID;
                objKeywordDetail.CreatedBy = BAL.GlobalClass.CurrentUserId;


                if ((keyID > 0) && (objKeywordDetail.InsertKeywordDetail(db, objKeywordDetail) > 0))
                {
                    recCoutner++;
                }

                //Char[] spliter = { ',' };
                //String[] IDs = Convert.ToString(dr["KeywordTypeIDs"]).Split(spliter, StringSplitOptions.None);
                //String[] keywords = Convert.ToString(dr["KeywordTypes"]).Split(spliter, StringSplitOptions.None);
                //int i = 0;
                //foreach (String str in IDs)
                //{
                //    objKeywordDetail.ArchiveKeyDetailID = keyID;
                //    objKeywordDetail.KeywordTypeID = Convert.ToInt32(str);
                //    objKeywordDetail.Keyword = keywords[i++];
                //objKeywordDetail.InsertKeywordDetail(db ,objKeywordDetail);
                //}
            }

            if (gvCounter == recCoutner)
            {
                result = true;
            }
            return result;
        }
        public DataTable GetAllArchive(int archiveID, object categoryID)
        {
            SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
            SqlDataAdapter dbAdapter = new SqlDataAdapter("[usp_GetAllArchiveForDeskTop]", dbConn);
            dbAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (archiveID != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveID", archiveID);
            }
            else
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nArchiveID", System.DBNull.Value);
            }

            if (categoryID != null)
            {
                dbAdapter.SelectCommand.Parameters.Add("@p_nCategoryID", categoryID);
            }

            DataTable dtArchive = new DataTable("ArchiveDB");

            dbAdapter.Fill(dtArchive);

            return dtArchive;
        }
        public bool SaveDetails(DBManager db, Int64 aID, ListViewEx dtKeywordDetail)
        {
            bool result = false;
            DataAccessLayer.DBArchiveKeyDetailDb objKeyDetail = new DBArchiveKeyDetailDb();
            KeywordDetailDB objKeywordDetail = new KeywordDetailDB();
            DataAccessLayer.DBArchiveKeyDetailDb KeywordDetailDB = new DBArchiveKeyDetailDb();
            int gvCounter = dtKeywordDetail.Items.Count;
            int recCoutner = 0;
            for (int i = 0; i < gvCounter; i++)
            {
                // objKeyDetail.DeleteArchiveKeyDetail(aID);
                objKeyDetail.ArchiveID = aID;
                objKeyDetail.StartTime = Convert.ToString(dtKeywordDetail.Items[i].SubItems[1].Text);
                objKeyDetail.EndTime = Convert.ToString(dtKeywordDetail.Items[i].SubItems[2].Text);
                objKeyDetail.Detail = Convert.ToString(dtKeywordDetail.Items[i].SubItems[4].Text);
                objKeyDetail.CreatedBy = BAL.GlobalClass.CurrentUserId;
                long keyID = objKeyDetail.InsertArchiveKeyDetail(db, objKeyDetail);
                objKeywordDetail.KeywordTypeID = Convert.ToInt32(dtKeywordDetail.Items[i].Tag);
                objKeywordDetail.Keyword = Convert.ToString(dtKeywordDetail.Items[i].SubItems[3].Text);
                objKeywordDetail.ArchiveKeyDetailID = keyID;
                objKeywordDetail.CreatedBy = BAL.GlobalClass.CurrentUserId;


                if ((keyID > 0) && (objKeywordDetail.InsertKeywordDetail(db, objKeywordDetail) > 0))
                {
                    recCoutner++;
                }

                //Char[] spliter = { ',' };
                //String[] IDs = Convert.ToString(dr["KeywordTypeIDs"]).Split(spliter, StringSplitOptions.None);
                //String[] keywords = Convert.ToString(dr["KeywordTypes"]).Split(spliter, StringSplitOptions.None);
                //int i = 0;
                //foreach (String str in IDs)
                //{
                //    objKeywordDetail.ArchiveKeyDetailID = keyID;
                //    objKeywordDetail.KeywordTypeID = Convert.ToInt32(str);
                //    objKeywordDetail.Keyword = keywords[i++];
                //objKeywordDetail.InsertKeywordDetail(db ,objKeywordDetail);
                //}
            }

            if (gvCounter == recCoutner)
            {
                result = true;
            }
            return result;
        }

        private string GetAddressedPath(string mpath)
        {
            string str = "";

            try
            {
                str = mpath.Replace('&', '_').Replace(')', '_').Replace('(', '_').Replace('#', '_').Replace('!', '_').Replace('%', '_').Replace('+', '_').Replace('=', '_').Replace(' ', '_').Trim();
            }
            catch (Exception)
            {
                str = mpath;
            }
            

            return str;
        }
        public bool SaveAltoFiles(long Arcid, DBManager dbCom, System.Collections.ArrayList Lst, Int16 PriorityOrder)
        {
            bool result = true;
            int LstRec = Lst.Count;
            int RecCounter = 0;
            string basestation = System.Configuration.ConfigurationManager.AppSettings ["BaseStation"]; 
            for (int i = 0; i < LstRec; i++)
            {
                string[] AltoFlag = Lst[i].ToString().Split(';');
                //if (AltoFlag[2].Contains("Thumbs.db"))
                //{
                //    RecCounter++;
                //}
                //else
                //{
                    dbCom.Command.Parameters.Clear();
                    dbCom.CreateParameters(11);
                    dbCom.AddParameters(0, "@ArchiveID", Arcid, 0);
                    dbCom.AddParameters(1, "@FileTag", AltoFlag[1], 0);
                    dbCom.AddParameters(2, "@AltoFileName", GetAddressedPath( AltoFlag[0]), 0);
                    dbCom.AddParameters(3, "@mDuration", AltoFlag[2], 0);
                    dbCom.AddParameters(4, "@mFileSize", AltoFlag[3], 0);
                    dbCom.AddParameters(5, "@mIsCopied", false, 0);
                    dbCom.AddParameters(6, "@mPriorityOrder", PriorityOrder, 0);
                    dbCom.AddParameters(7, "@mCreatedBy", Helper.UserFullName, 0);
                    dbCom.AddParameters(8, "@mCreatedOn", DateTime.Now, 0);
                    dbCom.AddParameters(9, "@msourcePath", AltoFlag[4], 0);
                    dbCom.AddParameters(10, "@BaseStation", basestation, 0);
                
                    // if (dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "usp_InsertAltoFileWithDuration") > 0)
                    if (dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "usp_InsertAltoFileWithDuration_CopyFlag") > 0)                       
                    {
                        RecCounter++;
                    }
               // }
            }
            if (RecCounter != LstRec)
            {
                result = false;
            }

            return result;
        }
        public bool SaveArchiveGuestDetail(long Arcid, DBManager dbCom, ListViewEx Lst)
        {
            bool result = true;
            int LstRec = Lst.Items.Count;
            int RecCounter = 0;
            for (int i = 0; i < LstRec; i++)
            {
                dbCom.Command.Parameters.Clear();
                dbCom.CreateParameters(7);
                dbCom.AddParameters(0, "@GuestDtailID", 0, 0);
                dbCom.AddParameters(1, "@GuestID", Convert.ToInt32(Lst.Items[i].SubItems[2].Text), 0);
                dbCom.AddParameters(2, "@ArchiveID", Arcid, 0);
                dbCom.AddParameters(3, "@CreatedBy", CreatedBy);
                dbCom.AddParameters(4, "@CreatedOn", CreatedOn);
                dbCom.AddParameters(5, "@EditedBy", EditedBy);
                dbCom.AddParameters(6, "@EditedOn", EditedOn);

                if (dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "usp_InsertArchiveGuestDetail") > 0)
                {
                    RecCounter++;
                }
            }
            if (RecCounter != LstRec)
            {
                result = false;
            }

            return result;
        }

        public bool DeleteArchiveGuestDetail(DBManager dbCom, long ArcId)
        {
            bool result = true;
            dbCom.Command.Parameters.Clear();
            dbCom.CreateParameters(1);
            dbCom.AddParameters(0, "@ArchiveId", ArcId, 0);
            try
            {
                dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "[usp_DeleteArchiveGuestDetail]");
            }

            catch (Exception ex)
            {
                result = false;
            }

            return result;

        }


        private bool SaveArchiveScript(DBManager dbCom, Int64 id)
        {
            bool result = true;
            if (ArchiveScript.Trim().Length > 0)
            {

                dbCom.Command.Parameters.Clear();
                dbCom.CreateParameters(3);
                dbCom.AddParameters(0, "@ScriptId", 0, 1);
                dbCom.AddParameters(1, "@ArchiveId", id, 0);
                dbCom.AddParameters(2, "@script", ArchiveScript, 0);
                try
                {
                    dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "[usp_InsertArchiveScript]");

                }

                catch (Exception ex)
                {
                    result = false;
                }
            }
            return result;

        }
        public bool updateArchiveScript(DBManager dbCom, Int64 id)
        {
            bool result = true;
            int i = -1;
            dbCom.Command.Parameters.Clear();
            dbCom.CreateParameters(2);
            dbCom.AddParameters(0, "@ArchiveId", id, 0);
            dbCom.AddParameters(1, "@script", ArchiveScript, 0);
            try
            {
                i = dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "[usp_UpdateArchiveScript]");
                if (i == 0)
                {
                    if (SaveArchiveScript(dbCom, id) == false)
                    {
                        result = false;
                    }
                }

            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;

        }
        public bool SaveArchive(DBManager dbCom, DBArchive objArchive, string ArchiveFileName, DataGridView dtKeywordDetail)
        {
            Int64 id = 0;
            bool result = true;
            try
            {

                dbCom.Command.Parameters.Clear();
                dbCom.CreateParameters(69);
                id = InsertArchive(dbCom, objArchive);
                if ((id > 0) && (UpdateArchiveForFileName(dbCom, id, ArchiveFileName) == true) && (DeleteArchiveKeyDetail(dbCom, id) == true) && (SaveDetails(dbCom, id, dtKeywordDetail) == true) && (SaveArchiveScript(dbCom, id) == true))
                {
                    result = true;
                    ArchiveID = id;
                }
                else
                {

                    result = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Support Department for rror", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //dbCom.Close();
                result = false;
            }
            finally
            {
                //dbCom.Close();
            }

            return result;
        }

        //private bool SaveArchiveFiles(DBManager dbCom ,ListViewEx lst, string noc, string script, long id)
        //{

        //    bool result = true;
        //    ArchiveFileDetail obj = new ArchiveFileDetail();
        //    using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
        //    {
        //        foreach (ListViewItem item in lst.Items)
        //        {

        //        }
        //    }
        //    return result;
        //}

        public bool SaveArchive(DBManager dbCom, DBArchive objArchive, string ArchiveFileName, ListViewEx dtKeywordDetail, ListViewEx LstGuest, bool IsGuest)
        {
            Int64 id = 0;
            bool result = true;
            bool bGuest = true;

            try
            {

                dbCom.Command.Parameters.Clear();
                //dbCom.CreateParameters(69);
                dbCom.CreateParameters(77);
                id = InsertArchive(dbCom, objArchive);


                if (IsGuest == true)
                {
                    bGuest = SaveArchiveGuestDetail(id, dbCom, LstGuest);
                    if ((id > 0) && (UpdateArchiveForFileName(dbCom, id, ArchiveFileName) == true) && (DeleteArchiveKeyDetail(dbCom, id) == true) && (SaveDetails(dbCom, id, dtKeywordDetail) == true) && (SaveArchiveScript(dbCom, id) == true) && (bGuest == true))
                    {
                        result = true;
                        ArchiveID = id;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    if ((id > 0) && (UpdateArchiveForFileName(dbCom, id, ArchiveFileName) == true) && (DeleteArchiveKeyDetail(dbCom, id) == true) && (SaveDetails(dbCom, id, dtKeywordDetail) == true) && (SaveArchiveScript(dbCom, id) == true))
                    {
                        result = true;
                        ArchiveID = id;
                    }
                    else
                    {

                        result = false;
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Support Department for Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //dbCom.Close();
                result = false;
            }
            finally
            {
                //dbCom.Close();
            }

            return result;
        }

        public bool SaveArchive(DBManager dbCom, DBArchive objArchive, string ArchiveFileName, ListViewEx dtKeywordDetail, ListViewEx LstGuest, bool IsGuest, ListViewEx lstMulti, string txtNOC, string txtScript)
        {
            Int64 id = 0;
            bool result = true;
            bool bGuest = true;

            try
            {

                dbCom.Command.Parameters.Clear();
                dbCom.CreateParameters(77);
                id = InsertArchive(dbCom, objArchive);


                if (IsGuest == true)
                {
                    bGuest = SaveArchiveGuestDetail(id, dbCom, LstGuest);
                    if ((id > 0) && (UpdateArchiveForFileName(dbCom, id, ArchiveFileName) == true) && (DeleteArchiveKeyDetail(dbCom, id) == true) && (SaveDetails(dbCom, id, dtKeywordDetail) == true) && (SaveArchiveScript(dbCom, id) == true) && (bGuest == true))
                    {
                        result = true;
                        ArchiveID = id;
                    }
                    else
                    {

                        result = false;
                    }
                }
                else
                {
                    if ((id > 0) && (UpdateArchiveForFileName(dbCom, id, ArchiveFileName) == true) && (DeleteArchiveKeyDetail(dbCom, id) == true) && (SaveDetails(dbCom, id, dtKeywordDetail) == true) && (SaveArchiveScript(dbCom, id) == true))
                    {
                        result = true;
                        ArchiveID = id;
                    }
                    else
                    {

                        result = false;
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Contact Support Department for Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //dbCom.Close();
                result = false;
            }
            finally
            {
                //dbCom.Close();
            }

            return result;
        }
        public bool UpdateArchive(DBManager dbCom, DBArchive objArchive)
        {
            bool result = true;
            dbCom.Command.Parameters.Clear();
            dbCom.CreateParameters(75);
            dbCom.AddParameters(0, "@p_nArchiveID", objArchive.ArchiveID);
            dbCom.AddParameters(1, "@p_nMediaNo", objArchive.MediaNo);
            dbCom.AddParameters(2, "@p_nChannelID", objArchive.ChannelID);
            dbCom.AddParameters(3, "@p_nCategoryID", objArchive.CategoryID);
            dbCom.AddParameters(4, "@p_nMediaTypeID", objArchive.MediaTypeID);
            dbCom.AddParameters(5, "@p_nClassificationID", objArchive.ClassificationID);
            dbCom.AddParameters(6, "@p_dtEntryDate", objArchive.EntryDate);
            dbCom.AddParameters(7, "@p_dtShootDate", objArchive.ShootDate);
            dbCom.AddParameters(8, "@p_nSourceID", objArchive.SourceID);
            dbCom.AddParameters(9, "@p_nFootageTypeID", objArchive.FootageTypeID);
            dbCom.AddParameters(10, "@p_strFileName", objArchive.FileName);
            dbCom.AddParameters(11, "@p_strJTSFileName", objArchive.JTSFileName);
            dbCom.AddParameters(12, "@p_strJTSTicketNo", objArchive.JTSTicketNo);
            dbCom.AddParameters(13, "@p_nJTSReporterID", objArchive.JTSReporterID);
            dbCom.AddParameters(14, "@p_nJTSCameramanID", objArchive.JTSCameramanID);
            dbCom.AddParameters(15, "@p_nJTSBureau", objArchive.JTSBureau);
            dbCom.AddParameters(16, "@p_strJTSJobType", objArchive.JTSJobType);
            dbCom.AddParameters(17, "@p_strJTSCreatedBy", objArchive.JTSCreatedBy);
            dbCom.AddParameters(18, "@p_strJTSSlug", objArchive.JTSSlug);
            dbCom.AddParameters(19, "@p_strJTSDuration", objArchive.JTSDuration);
            dbCom.AddParameters(20, "@p_strRackNo", objArchive.RackNo);
            dbCom.AddParameters(21, "@p_strShelfNo", objArchive.ShelfNo);
            dbCom.AddParameters(22, "@p_nProducerID", objArchive.ProducerID);
            dbCom.AddParameters(23, "@p_strDirector", objArchive.Director);
            dbCom.AddParameters(24, "@p_strSinger_Band", objArchive.Singer_Band);
            dbCom.AddParameters(25, "@p_strDuration", objArchive.Duration);
            dbCom.AddParameters(26, "@p_strProgrammeTitleOrName", objArchive.ProgrammeTitleOrName);
            dbCom.AddParameters(27, "@p_strProgramGenre_Lyrics", objArchive.ProgramGenre_Lyrics);
            dbCom.AddParameters(28, "@p_strSubTitleOrCaption_Cast", objArchive.SubTitleOrCaption_Cast);
            dbCom.AddParameters(29, "@p_dtTelecastDate", objArchive.TelecastDate);
            dbCom.AddParameters(30, "@p_nMidbreaks", objArchive.Midbreaks);
            dbCom.AddParameters(31, "@p_strEpisodeNo", objArchive.EpisodeNo);
            dbCom.AddParameters(32, "@p_strWriter", objArchive.Writer);
            dbCom.AddParameters(33, "@p_strNOCNo", objArchive.NOCNo);
            dbCom.AddParameters(34, "@p_nLanguageID", objArchive.LanguageID);
            dbCom.AddParameters(35, "@p_strTheme", objArchive.Theme);
            dbCom.AddParameters(36, "@p_strRemarks", objArchive.Remarks);
            dbCom.AddParameters(37, "@p_strAlbum", objArchive.Album);
            dbCom.AddParameters(38, "@p_strOrigin", objArchive.Origin);
            dbCom.AddParameters(39, "@p_strFootageDetail", objArchive.FootageDetail);
            dbCom.AddParameters(40, "@p_strClient", objArchive.Client);
            dbCom.AddParameters(41, "@p_dtCdate", objArchive.Cdate);
            dbCom.AddParameters(42, "@p_dtCTime", objArchive.CTime);
            dbCom.AddParameters(43, "@p_strSeaChangeCode", objArchive.SeaChangeCode);
            dbCom.AddParameters(44, "@p_nMediaStatusID", objArchive.MediaStatusID);
            dbCom.AddParameters(45, "@p_nInputTypeID", objArchive.InputTypeID);
            dbCom.AddParameters(46, "@p_dTotalCapacity", objArchive.TotalCapacity);
            dbCom.AddParameters(47, "@p_dCapacityUtilized", objArchive.CapacityUtilized);
            dbCom.AddParameters(48, "@p_dCapacityAvailable", objArchive.CapacityAvailable);
            dbCom.AddParameters(49, "@p_strDVDTitle", objArchive.DVDTitle);
            dbCom.AddParameters(50, "@p_bIsHighClip", objArchive.IsHighClip);
            dbCom.AddParameters(51, "@p_bIsLowClip", objArchive.IsLowClip);
            dbCom.AddParameters(52, "@p_nDVDNo", objArchive.DVDNo);
            dbCom.AddParameters(53, "@p_bIsIssued", objArchive.IsIssued);
            dbCom.AddParameters(54, "@p_nOnsiteBackup", objArchive.OnsiteBackup);
            dbCom.AddParameters(55, "@p_nOffsiteBackup", objArchive.OffsiteBackup);
            dbCom.AddParameters(56, "@p_strTitleOfDVD", objArchive.TitleOfDVD);
            dbCom.AddParameters(57, "@p_nCreatedBy", objArchive.CreatedBy);
            dbCom.AddParameters(58, "@p_dtCreatedOn", objArchive.CreatedOn);
            dbCom.AddParameters(59, "@p_nEditedBy", objArchive.EditedBy);
            dbCom.AddParameters(60, "@p_dtEditedOn", objArchive.EditedOn);
            dbCom.AddParameters(61, "@p_strPartNo", objArchive.PartNo);


            if (objArchive.onSiteMediaType.ToString() != "0")
            {
                dbCom.AddParameters(62, "@p_nOnsiteMediaTypeID", objArchive.onSiteMediaType);
            }
            else
            {
                dbCom.AddParameters(62, "@p_nOnsiteMediaTypeID", System.DBNull.Value);
            }

            if (objArchive.offSiteMediaType.ToString() != "0")
            {
                dbCom.AddParameters(63, "@p_nOffsiteMediaTypeID", objArchive.offSiteMediaType);
            }
            else
            {
                dbCom.AddParameters(63, "@p_nOffsiteMediaTypeID", System.DBNull.Value);
            }

            if (objArchive.DepartmentID.ToString() != "0")
            {
                dbCom.AddParameters(64, "@p_nDepartmentID", objArchive.DepartmentID);
            }
            else
            {
                dbCom.AddParameters(64, "@p_nDepartmentID", System.DBNull.Value);
            }


            if (objArchive.PhotographerID.ToString() != "0")
            {
                dbCom.AddParameters(65, "@p_nPhotographerID", objArchive.PhotographerID);
            }
            else
            {
                dbCom.AddParameters(65, "@p_nPhotographerID", System.DBNull.Value);
            }


            if (objArchive.BureauTape.ToString() != "0")
            {
                dbCom.AddParameters(66, "@p_nbureauTape", objArchive.BureauTape, 0);
            }
            else
            {
                dbCom.AddParameters(66, "@p_nbureauTape", System.DBNull.Value, 0);
            }

            if (objArchive.AnchorID.ToString() != "0")
            {
                dbCom.AddParameters(67, "@p_nAnchorID", objArchive.AnchorID, 0);
            }
            else
            {
                dbCom.AddParameters(67, "@p_nAnchorID", System.DBNull.Value, 0);
            }

            //dbCom.AddParameters(68, "@p_nAnchorID", objArchive.AnchorID, 0);
            dbCom.AddParameters(68, "@p_IsInternational", objArchive.IsInternational, 0);
            dbCom.AddParameters(69, "@p_IsMasterCopy", objArchive.IsMasterCopy, 0);
            dbCom.AddParameters(70, "@p_MediaSource", objArchive.MediaSource, 0);
            dbCom.AddParameters(71, "@p_ProgramTitleID", objArchive.ProgramTitleID, 0);
            dbCom.AddParameters(72, "@p_MarkDeleted", objArchive.MarkDeleted , 0);


            if (objArchive.contenttypeID.ToString() != "0")
            {
                dbCom.AddParameters(73, "@p_ContentTypeId", objArchive.contenttypeID, 0);
            }
            else
            {
                dbCom.AddParameters(73, "@p_ContentTypeId", System.DBNull.Value, 0);
            }
            dbCom.AddParameters(74, "@p_ChannelCategoryID", objArchive.ChannelCategoryID );
            try
            {
                //dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "usp_UpdateArchiveForDesktop");
                dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "usp_UpdateArchiveForDesktop_bk2");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                result = false;

            }

            return result;
        }
        private Int64 InsertArchive(DBManager dbCom, DBArchive objArchive)
        {
            //DBManager dbCom = new DBManager();

            ////dbCom.BeginTransaction();
            //dbCom.CreateParameters(67);
            //dbCom.Open();
            dbCom.AddParameters(0, "@p_nMediaNo", objArchive.MediaNo, 0);
            dbCom.AddParameters(1, "@p_nChannelID", objArchive.ChannelID, 0);
            dbCom.AddParameters(2, "@p_nCategoryID", objArchive.CategoryID, 0);
            dbCom.AddParameters(3, "@p_nMediaTypeID", objArchive.MediaTypeID, 0);
            dbCom.AddParameters(4, "@p_nClassificationID", objArchive.ClassificationID, 0);
            dbCom.AddParameters(5, "@p_dtEntryDate", objArchive.EntryDate, 0);
            dbCom.AddParameters(6, "@p_dtShootDate", objArchive.ShootDate, 0);
            dbCom.AddParameters(7, "@p_nSourceID", objArchive.SourceID, 0);
            dbCom.AddParameters(8, "@p_nFootageTypeID", objArchive.FootageTypeID, 0);
            dbCom.AddParameters(9, "@p_strFileName", objArchive.FileName);
            dbCom.AddParameters(10, "@p_strJTSFileName", objArchive.JTSFileName, 0);
            dbCom.AddParameters(11, "@p_strJTSTicketNo", objArchive.JTSTicketNo, 0);
            dbCom.AddParameters(12, "@p_nJTSReporterID", objArchive.JTSReporterID, 0);
            dbCom.AddParameters(13, "@p_nJTSCameramanID", objArchive.JTSCameramanID, 0);
            dbCom.AddParameters(14, "@p_nJTSBureau", objArchive.JTSBureau, 0);
            dbCom.AddParameters(15, "@p_strJTSJobType", objArchive.JTSJobType, 0);
            dbCom.AddParameters(16, "@p_strJTSCreatedBy", objArchive.JTSCreatedBy, 0);
            dbCom.AddParameters(17, "@p_strJTSSlug", objArchive.JTSSlug, 0);
            dbCom.AddParameters(18, "@p_strJTSDuration", objArchive.JTSDuration, 0);
            dbCom.AddParameters(19, "@p_strRackNo", objArchive.RackNo, 0);
            dbCom.AddParameters(20, "@p_strShelfNo", objArchive.ShelfNo, 0);
            dbCom.AddParameters(21, "@p_nProducerID", objArchive.ProducerID, 0);
            dbCom.AddParameters(22, "@p_strDirector", objArchive.Director, 0);
            dbCom.AddParameters(23, "@p_strSinger_Band", objArchive.Singer_Band, 0);
            dbCom.AddParameters(24, "@p_strDuration", objArchive.Duration, 0);
            dbCom.AddParameters(25, "@p_strProgrammeTitleOrName", objArchive.ProgrammeTitleOrName, 0);
            dbCom.AddParameters(26, "@p_strProgramGenre_Lyrics", objArchive.ProgramGenre_Lyrics, 0);
            dbCom.AddParameters(27, "@p_strSubTitleOrCaption_Cast", objArchive.SubTitleOrCaption_Cast, 0);
            dbCom.AddParameters(28, "@p_dtTelecastDate", objArchive.TelecastDate, 0);
            dbCom.AddParameters(29, "@p_nMidbreaks", objArchive.Midbreaks, 0);
            dbCom.AddParameters(30, "@p_strEpisodeNo", objArchive.EpisodeNo, 0);
            dbCom.AddParameters(31, "@p_strWriter", objArchive.Writer, 0);
            dbCom.AddParameters(32, "@p_strNOCNo", objArchive.NOCNo, 0);
            dbCom.AddParameters(33, "@p_nLanguageID", objArchive.LanguageID, 0);
            dbCom.AddParameters(34, "@p_strTheme", objArchive.Theme, 0);
            dbCom.AddParameters(35, "@p_strRemarks", objArchive.Remarks, 0);
            dbCom.AddParameters(36, "@p_strAlbum", objArchive.Album, 0);
            dbCom.AddParameters(37, "@p_strOrigin", objArchive.Origin, 0);
            dbCom.AddParameters(38, "@p_strFootageDetail", objArchive.FootageDetail, 0);
            dbCom.AddParameters(39, "@p_strClient", objArchive.Client, 0);
            dbCom.AddParameters(40, "@p_dtCdate", objArchive.Cdate, 0);
            dbCom.AddParameters(41, "@p_dtCTime", objArchive.CTime, 0);
            dbCom.AddParameters(42, "@p_strSeaChangeCode", objArchive.SeaChangeCode, 0);
            dbCom.AddParameters(43, "@p_nMediaStatusID", objArchive.MediaStatusID, 0);
            dbCom.AddParameters(44, "@p_nInputTypeID", objArchive.InputTypeID, 0);
            dbCom.AddParameters(45, "@p_dTotalCapacity", objArchive.TotalCapacity, 0);
            dbCom.AddParameters(46, "@p_dCapacityUtilized", objArchive.CapacityUtilized, 0);
            dbCom.AddParameters(47, "@p_dCapacityAvailable", objArchive.CapacityAvailable, 0);
            dbCom.AddParameters(48, "@p_strDVDTitle", objArchive.DVDTitle, 0);
            dbCom.AddParameters(49, "@p_bIsHighClip", objArchive.IsHighClip, 0);
            dbCom.AddParameters(50, "@p_bIsLowClip", objArchive.IsLowClip, 0);
            dbCom.AddParameters(51, "@p_nDVDNo", objArchive.DVDNo, 0);
            dbCom.AddParameters(52, "@p_bIsIssued", objArchive.IsIssued, 0);
            dbCom.AddParameters(53, "@p_nOnsiteBackup", objArchive.OnsiteBackup, 0);
            dbCom.AddParameters(54, "@p_nOffsiteBackup", objArchive.OffsiteBackup, 0);
            dbCom.AddParameters(55, "@p_strTitleOfDVD", objArchive.TitleOfDVD, 0);
            dbCom.AddParameters(56, "@p_nCreatedBy", objArchive.CreatedBy, 0);
            dbCom.AddParameters(57, "@p_dtCreatedOn", objArchive.CreatedOn, 0);
            dbCom.AddParameters(58, "@p_nEditedBy", objArchive.EditedBy, 0);
            dbCom.AddParameters(59, "@p_dtEditedOn", objArchive.EditedOn, 0);
            dbCom.AddParameters(60, "@p_strPartNo", objArchive.PartNo, 0);


            if (objArchive.onSiteMediaType.ToString() != "0")
            {
                dbCom.AddParameters(61, "@p_nOnsiteMediaTypeID", objArchive.onSiteMediaType, 0);
            }
            else
            {
                dbCom.AddParameters(61, "@p_nOnsiteMediaTypeID", System.DBNull.Value, 0);
            }

            if (objArchive.offSiteMediaType.ToString() != "0")
            {
                dbCom.AddParameters(62, "@p_nOffsiteMediaTypeID", objArchive.offSiteMediaType, 0);
            }
            else
            {
                dbCom.AddParameters(62, "@p_nOffsiteMediaTypeID", System.DBNull.Value, 0);
            }

            if (objArchive.DepartmentID.ToString() != "0")
            {
                dbCom.AddParameters(63, "@p_nDepartmentID", objArchive.DepartmentID, 0);
            }
            else
            {
                dbCom.AddParameters(63, "@p_nDepartmentID", System.DBNull.Value, 0);
            }

            if (objArchive.PhotographerID.ToString() != "0")
            {
                dbCom.AddParameters(64, "@p_nPhotographerID", objArchive.PhotographerID, 0);
            }
            else
            {
                dbCom.AddParameters(64, "@p_nPhotographerID", System.DBNull.Value, 0);
            }

            /*Output Parameters*/

            Int64 pArchiveID = 0;
            bool pStatus = false;
            dbCom.AddParameters(65, "@p_nArchiveID", pArchiveID, 1);
            dbCom.AddParameters(66, "@p_Status", pStatus, 1);

            //if (objArchive.BureauTape.ToString() != "0")
            {
                dbCom.AddParameters(67, "@p_nbureauTape", objArchive.BureauTape, 0);
            }
            //else
            //{
            //    dbCom.AddParameters(67, "@p_nbureauTape", System.DBNull.Value, 0);
            //}

            //if (objArchive.AnchorID.ToString() != "0")
            {
                dbCom.AddParameters(68, "@p_nAnchorID", objArchive.AnchorID, 0);
                dbCom.AddParameters(69, "@p_IsInternational", objArchive.IsInternational, 0);
                dbCom.AddParameters(70, "@p_BaseStationID", objArchive.BaseStationID, 0);
                dbCom.AddParameters(71, "@p_IsMasterCopy", objArchive.IsMasterCopy, 0);
                dbCom.AddParameters(72, "@p_MediaSource", objArchive.MediaSource, 0);
                dbCom.AddParameters(73, "@p_ProgramTitleID", objArchive.ProgramTitleID, 0);
                dbCom.AddParameters(74, "@p_MarkDeleted", objArchive.MarkDeleted, 0);

                if (objArchive.contenttypeID.ToString() != "0")
                {
                    dbCom.AddParameters(75, "@p_ContentTypeId", objArchive.contenttypeID, 0);
                }
                else
                {
                    dbCom.AddParameters(75, "@p_ContentTypeId", System.DBNull.Value, 0);
                }
                dbCom.AddParameters(76, "@p_ChannelCategoryID", objArchive.ChannelCategoryID , 0);
               
                


            }
            //else
            //{
            //    dbCom.AddParameters(68, "@p_nAnchorID", System.DBNull.Value, 0);
            //}


            //dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "[dbo].[usp_InsertArchiveForDesktop]");
            dbCom.ExecuteNonQuery(CommandType.StoredProcedure, "[dbo].[usp_InsertArchiveForDesktop_bk2]");

            pArchiveID = Convert.ToInt32(dbCom.Parameters[65].Value);
            pStatus = Convert.ToBoolean(dbCom.Parameters[66].Value);
            return pArchiveID;
        }
        public bool UpdateArchiveForFileName(DBManager DB, Int64 ArchiveID, String fileName)
        {
            bool result = true;
            DB.Command.Parameters.Clear();
            DB.CreateParameters(2);
            DB.AddParameters(0, "@p_nArchiveID", ArchiveID);
            DB.AddParameters(1, "@p_strFileName", ArchiveID.ToString() + fileName);
            try
            {
                DB.ExecuteNonQuery(CommandType.StoredProcedure, "usp_UpdateArchiveForFileName");
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;


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
        public bool DeleteArchiveKeyDetail(DBManager dbcom, Int64 archiveID)
        {
            bool result = true;

            //SqlCommand dbCom = new SqlCommand("usp_DeleteArchiveKeyDetail", dbConn);
            dbcom.Command.Parameters.Clear();
            dbcom.CreateParameters(1);

            dbcom.AddParameters(0, "@p_nArchiveID", archiveID, 0);
            try
            {
                dbcom.ExecuteNonQuery(CommandType.StoredProcedure, "usp_DeleteArchiveKeyDetail");
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }
        //public void UpdateArchive(ArchiveDB objArchive)
        //{

        //    SqlConnection dbConn = new SqlConnection(Helper.GetDBConnectionString());
        //    SqlCommand dbCom = new SqlCommand("usp_UpdateArchive", dbConn);
        //    dbCom.CommandType = CommandType.StoredProcedure;


        //    /*Input Parameters*/
        //    dbCom.Parameters.Add("@p_nArchiveID", objArchive.ArchiveID);
        //    dbCom.Parameters.Add("@p_nMediaNo", objArchive.MediaNo);
        //    dbCom.Parameters.Add("@p_nChannelID", objArchive.ChannelID);
        //    dbCom.Parameters.Add("@p_nCategoryID", objArchive.CategoryID);
        //    dbCom.Parameters.Add("@p_nMediaTypeID", objArchive.MediaTypeID);
        //    dbCom.Parameters.Add("@p_nClassificationID", objArchive.ClassificationID);
        //    dbCom.Parameters.Add("@p_dtEntryDate", objArchive.EntryDate);
        //    dbCom.Parameters.Add("@p_dtShootDate", objArchive.ShootDate);
        //    dbCom.Parameters.Add("@p_nSourceID", objArchive.SourceID);
        //    dbCom.Parameters.Add("@p_nFootageTypeID", objArchive.FootageTypeID);
        //    dbCom.Parameters.Add("@p_strFileName", objArchive.FileName);
        //    dbCom.Parameters.Add("@p_strJTSFileName", objArchive.JTSFileName);
        //    dbCom.Parameters.Add("@p_strJTSTicketNo", objArchive.JTSTicketNo);
        //    dbCom.Parameters.Add("@p_nJTSReporterID", objArchive.JTSReporterID);
        //    dbCom.Parameters.Add("@p_nJTSCameramanID", objArchive.JTSCameramanID);
        //    dbCom.Parameters.Add("@p_nJTSBureau", objArchive.JTSBureau);
        //    dbCom.Parameters.Add("@p_strJTSJobType", objArchive.JTSJobType);
        //    dbCom.Parameters.Add("@p_strJTSCreatedBy", objArchive.JTSCreatedBy);
        //    dbCom.Parameters.Add("@p_strJTSSlug", objArchive.JTSSlug);
        //    dbCom.Parameters.Add("@p_strJTSDuration", objArchive.JTSDuration);
        //    dbCom.Parameters.Add("@p_strRackNo", objArchive.RackNo);
        //    dbCom.Parameters.Add("@p_strShelfNo", objArchive.ShelfNo);
        //    dbCom.Parameters.Add("@p_nProducerID", objArchive.ProducerID);
        //    dbCom.Parameters.Add("@p_strDirector", objArchive.Director);
        //    dbCom.Parameters.Add("@p_strSinger_Band", objArchive.Singer_Band);
        //    dbCom.Parameters.Add("@p_strDuration", objArchive.Duration);
        //    dbCom.Parameters.Add("@p_strProgrammeTitleOrName", objArchive.ProgrammeTitleOrName);
        //    dbCom.Parameters.Add("@p_strProgramGenre_Lyrics", objArchive.ProgramGenre_Lyrics);
        //    dbCom.Parameters.Add("@p_strSubTitleOrCaption_Cast", objArchive.SubTitleOrCaption_Cast);
        //    dbCom.Parameters.Add("@p_dtTelecastDate", objArchive.TelecastDate);
        //    dbCom.Parameters.Add("@p_nMidbreaks", objArchive.Midbreaks);
        //    dbCom.Parameters.Add("@p_strEpisodeNo", objArchive.EpisodeNo);
        //    dbCom.Parameters.Add("@p_strWriter", objArchive.Writer);
        //    dbCom.Parameters.Add("@p_strNOCNo", objArchive.NOCNo);
        //    dbCom.Parameters.Add("@p_nLanguageID", objArchive.LanguageID);
        //    dbCom.Parameters.Add("@p_strTheme", objArchive.Theme);
        //    dbCom.Parameters.Add("@p_strRemarks", objArchive.Remarks);
        //    dbCom.Parameters.Add("@p_strAlbum", objArchive.Album);
        //    dbCom.Parameters.Add("@p_strOrigin", objArchive.Origin);
        //    dbCom.Parameters.Add("@p_strFootageDetail", objArchive.FootageDetail);
        //    dbCom.Parameters.Add("@p_strClient", objArchive.Client);
        //    dbCom.Parameters.Add("@p_dtCdate", objArchive.Cdate);
        //    dbCom.Parameters.Add("@p_dtCTime", objArchive.CTime);
        //    dbCom.Parameters.Add("@p_strSeaChangeCode", objArchive.SeaChangeCode);
        //    dbCom.Parameters.Add("@p_nMediaStatusID", objArchive.MediaStatusID);
        //    dbCom.Parameters.Add("@p_nInputTypeID", objArchive.InputTypeID);
        //    dbCom.Parameters.Add("@p_dTotalCapacity", objArchive.TotalCapacity);
        //    dbCom.Parameters.Add("@p_dCapacityUtilized", objArchive.CapacityUtilized);
        //    dbCom.Parameters.Add("@p_dCapacityAvailable", objArchive.CapacityAvailable);
        //    dbCom.Parameters.Add("@p_strDVDTitle", objArchive.DVDTitle);
        //    dbCom.Parameters.Add("@p_bIsHighClip", objArchive.IsHighClip);
        //    dbCom.Parameters.Add("@p_bIsLowClip", objArchive.IsLowClip);
        //    dbCom.Parameters.Add("@p_nDVDNo", objArchive.DVDNo);
        //    dbCom.Parameters.Add("@p_bIsIssued", objArchive.IsIssued);
        //    dbCom.Parameters.Add("@p_nOnsiteBackup", objArchive.OnsiteBackup);
        //    dbCom.Parameters.Add("@p_nOffsiteBackup", objArchive.OffsiteBackup);
        //    dbCom.Parameters.Add("@p_strTitleOfDVD", objArchive.TitleOfDVD);
        //    dbCom.Parameters.Add("@p_nCreatedBy", objArchive.CreatedBy);
        //    dbCom.Parameters.Add("@p_dtCreatedOn", objArchive.CreatedOn);
        //    dbCom.Parameters.Add("@p_nEditedBy", objArchive.EditedBy);
        //    dbCom.Parameters.Add("@p_dtEditedOn", objArchive.EditedOn);
        //    dbCom.Parameters.Add("@p_strPartNo", objArchive.PartNo);


        //    if (objArchive.onSiteMediaType.ToString() != "0")
        //    {
        //        dbCom.Parameters.Add("@p_nOnsiteMediaTypeID", objArchive.onSiteMediaType);
        //    }
        //    else
        //    {
        //        dbCom.Parameters.Add("@p_nOnsiteMediaTypeID", System.DBNull.Value);
        //    }

        //    if (objArchive.offSiteMediaType.ToString() != "0")
        //    {
        //        dbCom.Parameters.Add("@p_nOffsiteMediaTypeID", objArchive.offSiteMediaType);
        //    }
        //    else
        //    {
        //        dbCom.Parameters.Add("@p_nOffsiteMediaTypeID", System.DBNull.Value);
        //    }

        //    if (objArchive.DepartmentID.ToString() != "0")
        //    {
        //        dbCom.Parameters.Add("@p_nDepartmentID", objArchive.DepartmentID);
        //    }
        //    else
        //    {
        //        dbCom.Parameters.Add("@p_nDepartmentID", System.DBNull.Value);
        //    }


        //    if (objArchive.PhotographerID.ToString() != "0")
        //    {
        //        dbCom.Parameters.Add("@p_nPhotographerID", objArchive.PhotographerID);
        //    }
        //    else
        //    {
        //        dbCom.Parameters.Add("@p_nPhotographerID", System.DBNull.Value);
        //    }


        //    dbConn.Open();
        //    dbCom.ExecuteNonQuery();
        //    dbConn.Close();
        //}
        #endregion


    }
}

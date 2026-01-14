using System;
using System.Collections.Generic;
using System.Text;

namespace Altodownloading.BAL
{
    public static class GlobalClass
    {
        #region Declaration

        #region Strings
        private static string m_CurrentUser, m_CurrentUGroup, m_CurrentLevel, m_LastLoginDateTime, m_RptPath, m_WindowsLoginId, m_CurrentGLHeadAccount, m_CurrentVoucherType;
        private static string m_CurrentCompanyName, m_CompanyAbbrev, m_CompanyChar, m_CompanyCode, m_CurrentStation, m_CurrentStationChar, m_CurrentStationAbbrev, m_CurrentFiscalYear, m_IconPath, m_CurrentPublicationAbbrev, m_GLAccountTitle, m_StartingFiscalYear, m_EndingFiscalYear;
        private static string m_CurrentMachine, m_CurrentPublication, m_SubsidiaryParam, m_SubsidiaryAccTitle, m_SubsidiaryId, m_SubsidiaryAcType, m_mdesc, m_mcode, m_CurrentLoadedForm;
        private static string m_GLReportPath, m_BillingReportPath, m_CirculationReportPath, m_HRReportPath, m_CurrentDepartment, m_PreviousFiscalYear, m_CurrentCityCode, m_GLAccountCode;
        private static string m_ConnString, m_CurrentUserPWD, m_CurrentWorkingUserName, mFileName,mFileVersion;
        #endregion

        # region Numbers
        private static Int32 m_CurrentFiscalYearId, m_CurrentUserId, m_CurrentWorkingUserId, m_CurrentCompanyId, m_CurrentStationId, m_CurrentPublicationId, m_CurrentDepartmentId, m_CurrentUserGroupId, m_CurrentEmployeeId;
        #endregion

        # region boolean
        private static bool m_CurrentFiscalYearStatus, m_IsActiveForm, m_IsQuit, m_IsLogin, m_FromTree, m_ButtonReturn, m_FiscalLock, m_IsMonthClosed, m_IsCancel, m_IsValidUser;

        #endregion

        #region DateTimes
        private static DateTime m_SQLDateTime, m_LoginTime;
        #endregion

        #endregion

        #region Properties

        #region StringProperties

        public static string  FileVersion{get;set;}        
        public static string FileName
        {
            get { return mFileName; }
            set { mFileName = value; }
        }
        public static string ConnString
        {
            get { return m_ConnString; }
            set { m_ConnString = value; }
        }
        public static string CurrentWorkingUserName
        {
            get { return m_CurrentWorkingUserName; }
            set { m_CurrentWorkingUserName = value; }
        }

        public static string CurrentUserPWD
        {
            get { return m_CurrentUserPWD; }
            set { m_CurrentUserPWD = value; }
        }
        public static string CurrentVoucherType
        {
            get { return m_CurrentVoucherType; }
            set { m_CurrentVoucherType = value; }
        }

        public static string CurrentGLHeadAccount
        {
            get { return m_CurrentGLHeadAccount; }
            set { m_CurrentGLHeadAccount = value; }
        }

        public static string GLAccountCode
        {
            get { return m_GLAccountCode; }
            set { m_GLAccountCode = value; }
        }

        public static string GLAccountTitle
        {
            get { return m_GLAccountTitle; }
            set { m_GLAccountTitle = value; }
        }

        public static string CurrentCityCode
        {
            get { return m_CurrentCityCode; }
            set { m_CurrentCityCode = value; }
        }

        public static string mcode
        {
            get { return m_mcode; }
            set { m_mcode = value; }
        }

        public static string SubsidiaryAccTitle
        {
            get { return m_SubsidiaryAccTitle; }
            set { m_SubsidiaryAccTitle = value; }
        }

        public static string SubsidiaryId
        {
            get { return m_SubsidiaryId; }
            set { m_SubsidiaryId = value; }
        }

        public static string CurrentLoadedForm
        {
            get { return m_CurrentLoadedForm; }
            set { m_CurrentLoadedForm = value; }
        }

        public static string mdesc
        {
            get { return m_mdesc; }
            set { m_mdesc = value; }
        }

        public static string StartingFiscalYear
        {
            get { return m_StartingFiscalYear; }
            set { m_StartingFiscalYear = value; }
        }
        public static string EndingFiscalYear
        {
            get { return m_EndingFiscalYear; }
            set { m_EndingFiscalYear = value; }
        }

        public static string SubsidiaryParam
        {
            get { return m_SubsidiaryParam; }
            set { m_SubsidiaryParam = value; }
        }

        public static string SubsidiaryAcType
        {
            get { return m_SubsidiaryAcType; }
            set { m_SubsidiaryAcType = value; }
        }

        public static string RptPath
        {
            get { return m_RptPath; }
            set { m_RptPath = value; }
        }

        public static string IconPath
        {
            get { return m_IconPath; }
            set { m_IconPath = value; }
        }

        public static string LastLoginDateTime
        {
            get { return m_LastLoginDateTime; }
            set { m_LastLoginDateTime = value; }
        }

        public static string CurrentPublication
        {
            get { return m_CurrentPublication; }
            set { m_CurrentPublication = value; }
        }

        public static string GLReportPath
        {
            get { return m_GLReportPath; }
            set { m_GLReportPath = value; }
        }

        public static string CurrentStationAbbrev
        {
            get { return m_CurrentStationAbbrev; }
            set { m_CurrentStationAbbrev = value; }
        }

        public static string CompanyAbbrev
        {
            get { return m_CompanyAbbrev; }
            set { m_CompanyAbbrev = value; }
        }

        public static string CurrentPublicationAbbrev
        {
            get { return m_CurrentPublicationAbbrev; }
            set { m_CurrentPublicationAbbrev = value; }
        }

        public static string BillingReportPath
        {
            get { return m_BillingReportPath; }
            set { m_BillingReportPath = value; }
        }

        public static string CirculationReportPath
        {
            get { return m_CirculationReportPath; }
            set { m_CirculationReportPath = value; }
        }

        public static string HRReportPath
        {
            get { return m_HRReportPath; }
            set { m_HRReportPath = value; }
        }

        public static string CurrentMachine
        {
            get { return m_CurrentMachine; }
            set { m_CurrentMachine = value; }
        }

        public static string WindowsLoginId
        {
            get { return m_WindowsLoginId; }
            set { m_WindowsLoginId = value; }
        }

        public static string CompanyCode
        {
            get { return m_CompanyCode; }
            set { m_CompanyCode = value; }
        }



        public static string CurrentUser
        {
            get { return m_CurrentUser; }
            set { m_CurrentUser = value; }
        }

        public static string CurrentUGroup
        {
            get { return m_CurrentUGroup; }
            set { m_CurrentUGroup = value; }
        }

        public static string CurrentLevel
        {
            get { return m_CurrentLevel; }
            set { m_CurrentLevel = value; }
        }

        public static string CurrentDepartment
        {
            get { return m_CurrentDepartment; }
            set { m_CurrentDepartment = value; }
        }

        public static string CurrentCompanyName
        {
            get { return m_CurrentCompanyName; }
            set { m_CurrentCompanyName = value; }
        }

        public static string CompanyChar
        {
            get { return m_CompanyChar; }
            set { m_CompanyChar = value; }
        }

        public static string CurrentStation
        {
            get { return m_CurrentStation; }
            set { m_CurrentStation = value; }
        }

        public static string CurrentStationChar
        {
            get { return m_CurrentStationChar; }
            set { m_CurrentStationChar = value; }
        }

        public static string CurrentFiscalYear
        {
            get { return m_CurrentFiscalYear; }
            set { m_CurrentFiscalYear = value; }
        }

        public static string PreviousFiscalYear
        {
            get { return m_PreviousFiscalYear; }
            set { m_PreviousFiscalYear = value; }
        }
        #endregion

        #region NumericProperties

        public static Int32 CurrentCompanyId
        {
            get { return m_CurrentCompanyId; }
            set { m_CurrentCompanyId = value; }
        }

        public static Int32 CurrentStationId
        {
            get { return m_CurrentStationId; }
            set { m_CurrentStationId = value; }
        }

        public static Int32 CurrentDepartmentId
        {
            get { return m_CurrentDepartmentId; }
            set { m_CurrentDepartmentId = value; }
        }

        public static Int32 CurrentFiscalYearId
        {
            get { return m_CurrentFiscalYearId; }
            set { m_CurrentFiscalYearId = value; }
        }

        public static Int32 CurrentUserId
        {
            get { return m_CurrentUserId; }
            set { m_CurrentUserId = value; }
        }
        public static Int32 CurrentEmployeeId
        {
            get { return m_CurrentEmployeeId; }
            set { m_CurrentEmployeeId = value; }
        }

        public static Int32 CurrentPublicationId
        {
            get { return m_CurrentPublicationId; }
            set { m_CurrentPublicationId = value; }
        }

        public static Int32 CurrentWorkingUserId
        {
            get { return m_CurrentWorkingUserId; }
            set { m_CurrentWorkingUserId = value; }
        }
        public static Int32 CurrentUserGroupId
        {
            get { return m_CurrentUserGroupId; }
            set { m_CurrentUserGroupId = value; }
        }

        #endregion

        #region DateTimeProperties

        public static DateTime LoginTime
        {
            get { return m_LoginTime; }
            set { m_LoginTime = value; }
        }
        public static DateTime SQLDateTime
        {
            get { return m_SQLDateTime; }
            set { m_SQLDateTime = value; }
        }

        #endregion

        #region BooleanProperties

        public static bool IsCancel
        {
            get { return m_IsCancel; }
            set { m_IsCancel = value; }
        }

        public static bool FiscalLock
        {
            get { return m_FiscalLock; }
            set { m_FiscalLock = value; }
        }

        public static bool ButtonReturn
        {
            get { return m_ButtonReturn; }
            set { m_ButtonReturn = value; }
        }

        public static bool FromTree
        {
            get { return m_FromTree; }
            set { m_FromTree = value; }
        }
        public static bool IsValidUser
        {
            get { return m_IsValidUser; }
            set { m_IsValidUser = value; }
        }
        public static bool CurrentFiscalYearStatus
        {
            get { return m_CurrentFiscalYearStatus; }
            set { m_CurrentFiscalYearStatus = value; }
        }

        public static bool IsActiveForm
        {
            get { return m_IsActiveForm; }
            set { m_IsActiveForm = value; }
        }

        public static bool IsQuit
        {
            get { return m_IsQuit; }
            set { m_IsQuit = value; }
        }

        public static bool IsLogin
        {
            get { return m_IsLogin; }
            set { m_IsLogin = value; }
        }

        public static bool IsMothClosed
        {
            get { return m_IsMonthClosed; }
            set { m_IsMonthClosed = value; }
        }
        #endregion

        #endregion
    }
}

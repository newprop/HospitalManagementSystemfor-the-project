﻿namespace FastReport.Utils
{
    static partial class Config
    {
        #region Public Properties

        /// <summary>
        /// Gets a value indicating that the ASP.NET hosting permission level is set to full trust.
        /// </summary>
        public static bool FullTrust
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value that determines whether to disable some functionality to run in web mode.
        /// </summary>
        /// <remarks>
        /// Use this property if you use FastReport in ASP.Net. Set this property to <b>true</b> <b>before</b>
        /// you access any FastReport.Net objects.
        /// </remarks>
        public static bool WebMode
        {
            get
            {
                return FWebMode;
            }
            set
            {
                FWebMode = value;
            }
        }
        #endregion Public Properties

        #region Private Methods

        /// <summary>
        /// Does nothing
        /// </summary>
        static partial void RestoreUIStyle();

        /// <summary>
        /// Does nothing
        /// </summary>
        static partial void SaveUIStyle();

        static partial void SaveUIOptions();

        static partial void RestoreUIOptions();

        static partial void SaveExportOptions();

        static partial void SaveAuthServiceUser();

        static partial void RestoreAuthServiceUser();

        static partial void RestoreExportOptions();

        #endregion Private Methods

        internal static void DoEvent()
        {
            // do nothing
        }
    }
}
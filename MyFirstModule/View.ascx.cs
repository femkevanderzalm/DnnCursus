using System;
using System.Collections.Generic;
using DNNtc;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.FileSystem;

namespace MyFirstModule
{
    /// <summary>
    ///     The View class displays the content
    /// </summary>
    [ModuleControlProperties("", "Module title", ControlType.View, "", true, false)]
    public partial class View : PortalModuleBase
    {
        private IEnumerable<IFileInfo> GetFiles(string folderName)
        {
            var folder = FolderManager.Instance.GetFolder(PortalId, folderName);
            var files = FolderManager.Instance.GetFiles(folder, true);
            return files;
        }

        private void ShowFiles()
        {
            var folderName = string.Empty; // Root folder
            var files = GetFiles(folderName);


            rptFiles.DataSource = files;
            rptFiles.DataBind();
        }

        #region Event Handlers

        /// <summary>
        ///     Runs when the control is loaded
        /// </summary>
        private void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var user = UserController.Instance.GetCurrentUserInfo();
                var currentUserName = string.Empty;
                var cssClass = "xecMyModuleTitle";

                if (user.UserID == -1)
                {
                    cssClass += " xecNotLoggedIn";
                    currentUserName = LocalizeString("NotLoggedInWarning");

                    lblMyModuleTitle.Visible = false;
                }
                else
                {
                    currentUserName = user.DisplayName;
                }


                lblMyModuleTitle.Text = LocalizeString("MyModuleTitle");

                lblCurrentUser.CssClass = cssClass;
                lblCurrentUser.Text = currentUserName;

                ShowFiles();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion
    }
}
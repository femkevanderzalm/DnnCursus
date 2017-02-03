using System;
using DNNtc;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.FileSystem;

namespace XCESS.Documents
{
    [DNNtc.ModuleControlProperties("Settings", "Configure settings", DNNtc.ControlType.Edit, "", true, true)]
    public partial class Settings : ModuleSettingsBase
    {
        #region Base Method Implementations

        public override void LoadSettings()
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    //Check for existing settings and use those on this page
                    //Settings["SettingName"]
                    if (this.Settings["xec_FolderID"] != null)
                    {
                        var folder= FolderManager.Instance.GetFolder(Convert.ToInt32(this.Settings["xec_FolderID"]));
                        this.DropDownListFolder.SelectedFolder = folder;
                    }
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public override void UpdateSettings()
        {
            try
            {
                var modules = new ModuleController();
                //modules.UpdateTabModuleSetting(this.TabModuleId, "ModuleSetting", (control.value ? "true" : "false"));
                modules.UpdateModuleSetting(this.ModuleId, "xec_FolderID", this.DropDownListFolder.SelectedFolder.FolderID.ToString());

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion
    }
}
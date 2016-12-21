using System;
using DNNtc;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;

namespace MyFirstModule
{
    [ModuleControlProperties("Settings", "Configure settings", ControlType.Edit, "", true, true)]
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
                //modules.UpdateModuleSetting(this.TabModuleId, "LogBreadCrumb", (control.value ? "true" : "false"));
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion
    }
}
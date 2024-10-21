using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxQR
{
    public class MyUserSettings : ApplicationSettingsBase
    {
        [UserScopedSetting()]
        [DefaultSettingValue("")]
        public string txtFileName
        {
            get
            {
                var val = this["txtFileName"] == null ? "" : this["txtFileName"].ToString();

                return val.ToString();
            }
            set
            {
                this["txtFileName"] = value;
            }
        }
        [UserScopedSetting()]
        [DefaultSettingValue("")]
        public string txtDestination
        {
            get
            {
                var val = this["txtDestination"] == null ? "" : this["txtDestination"].ToString();

                return val.ToString();
            }
            set
            {
                this["txtDestination"] = value;
            }
        }
        [UserScopedSetting()]
        [DefaultSettingValue("")]
        public string txtRectangle
        {
            get
            {
                var val = this["txtRectangle"] == null ? "" : this["txtRectangle"].ToString();

                return val.ToString();
            }
            set
            {
                this["txtRectangle"] = value;
            }
        }
    }
}

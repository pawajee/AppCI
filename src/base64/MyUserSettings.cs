using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASE64
{

    public class MyUserSettings : ApplicationSettingsBase
    {
        [UserScopedSetting()]
        [DefaultSettingValue("")]
        public string txtConvert
        {
            get
            {
                var val = this["txtConvert"] == null ? "" : this["txtConvert"].ToString();

                return val.ToString();
            }
            set
            {
                this["txtConvert"] = value;
            }
        }
        [UserScopedSetting()]
        [DefaultSettingValue("")]
        public string txtReverse
        {
            get
            {
                var val = this["txtReverse"] == null ? "" : this["txtReverse"].ToString();

                return val.ToString();
            }
            set
            {
                this["txtReverse"] = value;
            }
        }
    }
}

MyUserSettings.csusing System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRCodeGen
{

    public class MyUserSettings : ApplicationSettingsBase
    {
        [UserScopedSetting()]
        [DefaultSettingValue("0.1")]
        public string txtInterval
        {
            get
            {
                var val = this["txtInterval"] == null ? "" : this["txtInterval"].ToString();

                return val.ToString();
            }
            set
            {
                this["txtInterval"] = value;
            }
        }
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
        [DefaultSettingValue("2800")]
        public string txtChunkSize
        {
            get
            {
                var val = this["txtChunkSize"] == null ? "" : this["txtChunkSize"].ToString();

                return val.ToString();
            }
            set
            {
                this["txtChunkSize"] = value;
            }
        }
    }
}

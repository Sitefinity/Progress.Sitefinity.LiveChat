/* ------------------------------------------------------------------------------
author: Saikrishna Teja Bobba
------------------------------------------------------------------------------ */

using System.Collections.Specialized;
using System.Configuration;
using Telerik.Sitefinity.Configuration;


namespace LiveChat.Config
{
    /// <summary>
    /// Defines module configuration settings.
    /// </summary>
    public class LiveChatConfig : ConfigSection
    {
        [ConfigurationProperty("licenseID", DefaultValue = "0", IsRequired = true)]
        public string LicenseID
        {
            get
            {
                return (string)this["licenseID"];
            }
            set
            {
                this["licenseID"] = value;
            }
        }

        [ConfigurationProperty("email", DefaultValue = "0", IsRequired = true)]
        public string Email
        {
            get
            {
                return (string)this["email"];
            }
            set
            {
                this["email"] = value;
            }
        }

    }
}
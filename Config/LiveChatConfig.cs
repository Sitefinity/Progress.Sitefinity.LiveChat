// <copyright file="LiveChatConfig.cs" company="Progress Software Corporation">
// Copyright (c) Progress Software Corporation. All rights reserved.
// </copyright>

namespace LiveChat.Config
{
    using System.Configuration;
    using Telerik.Sitefinity.Configuration;

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
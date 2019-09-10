// <copyright file="LiveChatController.cs" company="Progress Software Corporation">
// Copyright (c) Progress Software Corporation. All rights reserved.
// </copyright>

namespace LiveChat.Mvc.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using LiveChat.Config;
    using Telerik.Sitefinity.Configuration;
    using Telerik.Sitefinity.Mvc;

    /// <summary>
    /// Controller to handle saving the settings when user logs in and logs off.
    /// </summary>
    [ControllerToolboxItem(Name = "LiveChat", Title = "LiveChat", SectionName = "LiveChat", CssClass = "sfMvcIcn")]
    public class LiveChatController : Controller
    {
        /// <summary>
        /// Set LicenseID if User already signed in when the view is requested.
        /// </summary>
        /// <returns>Action Result Object.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            var config = Telerik.Sitefinity.Configuration.Config.Get<LiveChatConfig>();
            this.ViewBag.License = config.LicenseID;
            this.ViewBag.Email = config.Email;
            return this.View();
        }

        /// <summary>
        /// Set LicenseID if User already signed in and save the configuration.
        /// </summary>
        /// <returns>JsonResult Object.</returns>
        [Route("livechat/setLicense")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SetLicense()
        {
            var result = false;
            string[] licensedetails = this.Request.Form.Get(0).Split(',');
            if (licensedetails.Length > 0)
            {
                var configManager = ConfigManager.GetManager();
                var config = configManager.GetSection<LiveChatConfig>();
                config.LicenseID = licensedetails[0];
                config.Email = licensedetails[1];
                configManager.SaveSection(config);
                result = true;
            }

            return this.Json(new { result = (result == true) ? new HttpStatusCodeResult(HttpStatusCode.OK) : new HttpStatusCodeResult(HttpStatusCode.Forbidden) });
        }

        /// <summary>
        /// Delete LicenseID if User signed out and clear the configuration.
        /// </summary>
        /// <returns>JsonResult Object.</returns>
        [Route("livechat/deleteLicense")]
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteLicense()
        {
            var configManager = ConfigManager.GetManager();
            var config = configManager.GetSection<LiveChatConfig>();
            config.LicenseID = "0";
            config.Email = "0";
            configManager.SaveSection(config);
            return this.Json(new { result = new HttpStatusCodeResult(HttpStatusCode.OK) });
        }
    }
}

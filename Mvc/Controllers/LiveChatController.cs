/* ------------------------------------------------------------------------------
author: Saikrishna Teja Bobba
------------------------------------------------------------------------------ */

using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using System.Threading.Tasks;
//using LiveChat.Database;
using System.Net;
//using LiveChat.Database.Model;
using LiveChat.Config;
using Telerik.Sitefinity.Configuration;

namespace LiveChat.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "LiveChat", Title = "LiveChat", SectionName = "LiveChat", CssClass = "sfMvcIcn")]
    public class LiveChatController: Controller
    {
        
  
        public ActionResult Index()
        {
            var config = Telerik.Sitefinity.Configuration.Config.Get<LiveChatConfig>();
            ViewBag.License = config.LicenseID;
            ViewBag.Email = config.Email;
            return View();
        }

        [Route("livechat/setLicense")]
        [HttpPost]
        public JsonResult SetLicense()
        {
            var result = false;
            string[] licensedetails = Request.Form.Get(0).Split(',');
            if (licensedetails.Length > 0)
            {
                var configManager = ConfigManager.GetManager();
                var config = configManager.GetSection<LiveChatConfig>();
                config.LicenseID = licensedetails[0];
                config.Email = licensedetails[1];
                configManager.SaveSection(config);
                result = true;
            }
            return this.Json(new { result = (result == true) ? new HttpStatusCodeResult(HttpStatusCode.OK) : new HttpStatusCodeResult(HttpStatusCode.Forbidden)});
        }


        [Route("livechat/deleteLicense")]
        [HttpDelete]
        public JsonResult DeleteLicense()
        {
            var configManager = ConfigManager.GetManager();
            var config = configManager.GetSection<LiveChatConfig>();
            config.LicenseID = "0";
            config.Email = "0";
            configManager.SaveSection(config);
            return this.Json(new { result = new HttpStatusCodeResult(HttpStatusCode.OK)});
        }
        
    }
}

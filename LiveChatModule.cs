
using System;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Services;

using LiveChat;
using LiveChat.Mvc.Controllers;
using LiveChat.Config;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Fluent.Modules.Toolboxes;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Web.Events;
using System.Web.UI;

namespace LiveChat
{
   
    public class LiveChatModule : ModuleBase
    {
        #region Constants
        public static readonly Guid ModuleGroupPageId = new Guid("a04424a1-6be7-404b-82bd-c13a8eb705bc");
        public static readonly Guid LiveChatPageId = new Guid("d5c9852f-4c87-467e-8b66-5925c86b2ced");
        public const string LiveChatPageName = "Enable LiveChat";
        public const string moduleName = "LiveChatModule";

        public const string LiveChatVirtualPath = "~/LiveChat/";
        #endregion

        /// <summary>
        /// Provide the GUID of the desired landing page for your module
        /// The LandingPageId specifies which is the default module page
        /// This is the entry point for the module UI.It is usually added under the Administration menu
        /// Some modules may not have landing pages, or have them hiddedn, depending on the use case scenario
        /// </summary>
        public override Guid LandingPageId
        {
            get
            {
                return LiveChatPageId;
            }
        }



        /// <summary>
        /// The install method is called initially, when LiveChat module is added to your Sitefinity website
        /// Here you can install your module configurations, add the module pages and widgets
        /// </summary>
        public override void Install(SiteInitializer initializer)
        {
            initializer.Installer
                .Toolbox(CommonToolbox.PageWidgets)
                       .LoadOrAddSection("Enable LiveChat")
                       
                       .SetTitle("Enable LiveChat")
                       .SetDescription("Chat with your customers, send their details to your CRM, manage their orders and accept their payments – all that without leaving the Sitefinity interface!")
                       .SetTags(ToolboxTags.Backend)
                       .SetOrdinal(-1);

            Guid siblingId = Guid.Empty;
            var dbDiagnosticToolWidget = new MvcControllerProxy();
            dbDiagnosticToolWidget.ControllerName = typeof(LiveChatController).FullName;
            dbDiagnosticToolWidget.Settings = new ControllerSettings(new LiveChatController());

            initializer.Installer
                .CreateModuleGroupPage(ModuleGroupPageId, "LiveChat")
                    .PlaceUnder(SiteInitializer.ToolsNodeId)
                    .SetOrdinal(6f)
               
                    .SetTitle("Enable LiveChat")
                    .SetUrlName("LiveChat")
                    .SetDescription("Chat with your customers, send their details to your CRM, manage their orders and accept their payments – all that without leaving the Sitefinity interface!")
                    .ShowInNavigation()
                    .AddChildPage(LiveChatPageId, "LiveChatPage")
                        .SetOrdinal(1)
                        .SetTitle("Enable LiveChat on Sitefinity")
                        .SetHtmlTitle("Enable LiveChat")
                        .SetUrlName("LiveChatPage")
                        .SetDescription("Chat with your customers, send their details to your CRM, manage their orders and accept their payments – all that without leaving the Sitefinity interface!")
                        .AddControl(dbDiagnosticToolWidget)
                        .HideFromNavigation()
                    .Done();
        }

        /// <summary>
        /// Initializing the LiveChat Module when sitefinity is started or re-started
        /// </summary>
        public override void Initialize(ModuleSettings settings)
        {
            base.Initialize(settings);

            App.WorkWith().Module(settings.Name)
                .Initialize()
                    .Configuration<LiveChatConfig>();

            
        }

        /// <summary>
        /// Subscribe to Page Pre-Render Complete Event.
        /// </summary>
        public override void Load()
        {
            EventHub.Subscribe<IPagePreRenderCompleteEvent>(new SitefinityEventHandler<IPagePreRenderCompleteEvent>(this.OnPagePreRenderCompleteEventHandler));
        }


        /// <summary>
        /// On Page Pre-render complete event, inject LiveChat script to the page. 
        /// Read the License information from Configuration - if user has signed in to their LiveChat account, these values will be set and you should enable LiveChat.
        /// If they haven't signed-in, do not add LiveChat script to the page.
        /// </summary>
        private void OnPagePreRenderCompleteEventHandler(IPagePreRenderCompleteEvent @event)
        {
            
            var config = Telerik.Sitefinity.Configuration.Config.Get<LiveChatConfig>();
            string license = config.LicenseID;
            string email = config.Email;

            if (license != "0"|| email != "0")
            {
                //Apply LicenseID to Script
                string livechatURL = "https://www.livechatinc.com/chat-with/" + license;
                string liveChatScriptTemplate = @"
                                        <script type = 'text/javascript' >
                                           window.__lc = window.__lc || {{}};
                                                    window.__lc.license = {0};
                                                    (function() {{
                                                        var lc = document.createElement('script'); lc.type = 'text/javascript'; lc.async = true;
                                                        lc.src = ('https:' == document.location.protocol ? 'https://' : 'http://') + 'cdn.livechatinc.com/tracking.js';
                                                        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(lc, s);
                                                    }})();
                                        </script>";
                string liveChatScript = String.Format(liveChatScriptTemplate, license);

                @event.Page.Header.Controls.Add(new LiteralControl(liveChatScript));
            }
        }

        /// <summary>
        /// Loading LiveChatConfig to facilitate storing LicenseID and Email from LiveChat.
        /// The actual implementation is set separated in a separate config class
        /// In this case the /Config/LiveChatConfig.cs is used as we need config to store licenseid and email.
        /// </summary>
        protected override ConfigSection GetModuleConfig()
        {
            return Telerik.Sitefinity.Configuration.Config.Get<LiveChatConfig>();
        }

        /// <summary>
        /// Provide a list of managers your module will be working with
        /// It might be your own managers or default Sitefinity ones
        /// Sitefintiy CMS uses this collection to make sure the managers (and their respective providers) are initialized
        /// This way you can safely work with the managers inside your module logic
        /// </summary>
        public override Type[] Managers
        {
            get { return new Type[] { typeof(PageManager) }; }
        }
    }
}
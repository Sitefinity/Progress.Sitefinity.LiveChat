// <copyright file="LiveChatModule.cs" company="Progress Software Corporation">
// Copyright (c) Progress Software Corporation. All rights reserved.
// </copyright>

namespace LiveChat
{
    using System;
    using System.Globalization;
    using System.Web.UI;
    using LiveChat.Config;
    using LiveChat.Mvc.Controllers;
    using Telerik.Sitefinity;
    using Telerik.Sitefinity.Abstractions;
    using Telerik.Sitefinity.Configuration;
    using Telerik.Sitefinity.Fluent.Modules.Toolboxes;
    using Telerik.Sitefinity.Modules.Pages;
    using Telerik.Sitefinity.Modules.Pages.Configuration;
    using Telerik.Sitefinity.Mvc.Proxy;
    using Telerik.Sitefinity.Services;
    using Telerik.Sitefinity.Services.Events;
    using Telerik.Sitefinity.Web.Events;

    /// <summary>
    /// LiveChat Module to enable LiveChat on any Sitefinity Website.
    /// </summary>
    public class LiveChatModule : ModuleBase
    {
        public const string LiveChatPageName = "Enable LiveChat";
        public const string ModuleName = "LiveChatModule";
        public const string LiveChatVirtualPath = "~/LiveChat/";
        public static readonly Guid ModuleGroupPageId = new Guid("a04424a1-6be7-404b-82bd-c13a8eb705bc");
        public static readonly Guid LiveChatPageId = new Guid("d5c9852f-4c87-467e-8b66-5925c86b2ced");

        /// <summary>
        /// Gets provide the GUID of the desired landing page for your module
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
        /// Gets provide a list of managers your module will be working with
        /// It might be your own managers or default Sitefinity ones
        /// Sitefintiy CMS uses this collection to make sure the managers (and their respective providers) are initialized
        /// This way you can safely work with the managers inside your module logic
        /// </summary>
        public override Type[] Managers
        {
            get
            {
                return new Type[] { typeof(PageManager) };
            }
        }

        /// <summary>
        /// The install method is called initially, when LiveChat module is added to your Sitefinity website
        /// Here you can install your module configurations, add the module pages and widgets
        /// </summary>
        /// <param name="initializer">Module initilaizer</param>
        public override void Install(SiteInitializer initializer)
        {
            if (initializer != null)
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
        }

        /// <summary>
        /// Initializing the LiveChat Module when sitefinity is started or re-started
        /// </summary>
        /// <param name="settings">Module settings</param>
        public override void Initialize(ModuleSettings settings)
        {
            if (settings != null)
            {
                base.Initialize(settings);

                App.WorkWith().Module(settings.Name)
                    .Initialize()
                        .Configuration<LiveChatConfig>();
            }
        }

        /// <summary>
        /// Subscribe to Page Pre-Render Complete Event.
        /// </summary>
        public override void Load()
        {
            EventHub.Subscribe<IPagePreRenderCompleteEvent>(new SitefinityEventHandler<IPagePreRenderCompleteEvent>(this.OnPagePreRenderCompleteEventHandler));
        }

        /// <summary>
        /// Loading LiveChatConfig to facilitate storing LicenseID and Email from LiveChat.
        /// The actual implementation is set separated in a separate config class
        /// In this case the /Config/LiveChatConfig.cs is used as we need config to store licenseid and email.
        /// </summary>
        /// <returns>The module configuration class<</returns>
        protected override ConfigSection GetModuleConfig()
        {
            return Telerik.Sitefinity.Configuration.Config.Get<LiveChatConfig>();
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

            if (license != "0" || email != "0")
            {
                // Apply LicenseID to Script
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
                string liveChatScript = string.Format(CultureInfo.InvariantCulture, liveChatScriptTemplate, license);

                @event.Page.Header.Controls.Add(new LiteralControl(liveChatScript));
            }
        }
    }
}
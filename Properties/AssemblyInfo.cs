// <copyright file="AssemblyInfo.cs" company="Progress Software Corporation">
// Copyright (c) Progress Software Corporation. All rights reserved.
// </copyright>

using System.Reflection;
using System.Runtime.InteropServices;
using LiveChat;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Services;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("LiveChat")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Progress")]
[assembly: AssemblyProduct("Progress Sitefinity CMS")]
[assembly: AssemblyCopyright("Copyright © 2005-2022 Progress Software Corporation and/or one of its subsidiaries or affiliates. All rights reserved.")]
[assembly: AssemblyTrademark("Progress")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("497f8cc3-d0e2-42fa-aece-7f26db192bcc")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.8.0")]
[assembly: AssemblyFileVersion("1.0.8.0")]

[assembly: SitefinityModule("LiveChat", typeof(LiveChatModule), "LiveChat for Sitefinity", "Chat with your customers, send their details to your CRM, manage their orders and accept their payments – all that without leaving the Sitefinity interface.", StartupType.OnApplicationStart)]
[assembly: ControllerContainer]
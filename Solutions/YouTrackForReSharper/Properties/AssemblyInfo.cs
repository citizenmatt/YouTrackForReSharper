using System.Reflection;
using System.Runtime.InteropServices;
using JetBrains.ActionManagement;
using JetBrains.Application.Components;
using JetBrains.Application.PluginSupport;
using JetBrains.UI;
using JetBrains.UI.ToolWindowManagement;

using YouTrack.For.ReSharper.SearchAction;

[assembly: AssemblyTitle("YouTrack for ReSharper")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("Visual Studio 2010")]
[assembly: AssemblyCompany("JetBrains s.r.o.")]
[assembly: AssemblyProduct("ReSharper PowerToys")]
[assembly: AssemblyCopyright("Copyright \u00A9 2006-2010 JetBrains s.r.o. All rights reserved.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: ComVisible(false)]
[assembly: Guid("823c1185-dc5d-48c7-b031-e41e9645947b")]
[assembly: PluginTitle("YouTrack for ReSharper")]
[assembly: PluginVendor("JetBrains s.r.o.")]
[assembly: PluginDescription("Search using YouTrack")]
[assembly: ActionsXml("YouTrack.For.ReSharper.Action.xml")]

[assembly: ImagesBase("YouTrack.For.ReSharper.Resources")]
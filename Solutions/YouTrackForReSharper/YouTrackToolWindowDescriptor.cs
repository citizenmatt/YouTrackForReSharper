using JetBrains.Application.Components;
using JetBrains.UI.ToolWindowManagement;
using YouTrack.For.ReSharper.SearchAction;

namespace YouTrack.For.ReSharper
{
    [ToolWindowDescriptor(
        ProgramConfigurations = ProgramConfigurations.VS_ADDIN,
        Id = YouTrackSearchAction.YouTrackBrowserWindowId,
        Text = "Hierarchy",
        Guid = "66DDC50A-E1B3-47DC-83B8-8F8813B81FF4",
        //ToolWindowVisibilityPersistenceScope = ToolWindowVisibilityPersistenceScope.Solution,
        IconResourceID = 305)]
    public class YouTrackToolWindowDescriptor : ToolWindowDescriptor
    {
    }
}
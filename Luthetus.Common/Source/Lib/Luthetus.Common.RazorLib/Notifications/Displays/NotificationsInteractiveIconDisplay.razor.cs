using Fluxor;
using Fluxor.Blazor.Web.Components;
using Luthetus.Common.RazorLib.Dialogs.Models;
using Luthetus.Common.RazorLib.Keys.Models;
using Luthetus.Common.RazorLib.Notifications.States;
using Microsoft.AspNetCore.Components;

namespace Luthetus.Common.RazorLib.Notifications.Displays;

public partial class NotificationsInteractiveIconDisplay : FluxorComponent
{
    [Inject]
    private IState<NotificationState> NotificationStateWrap { get; set; } = null!;
    [Inject]
    private IDialogService DialogService { get; set; } = null!;

    [Parameter]
    public string CssClassString { get; set; } = string.Empty;
    [Parameter]
    public string CssStyleString { get; set; } = string.Empty;

    private readonly DialogRecord NotificationsViewDisplayDialogRecord = new(
        Key<DialogRecord>.NewKey(),
        "Notifications",
        typeof(NotificationsViewDisplay),
        null,
        null)
    {
        IsResizable = true
    };

    private void ShowNotificationsViewDisplayOnClick()
    {
        DialogService.RegisterDialogRecord(NotificationsViewDisplayDialogRecord);
    }
}
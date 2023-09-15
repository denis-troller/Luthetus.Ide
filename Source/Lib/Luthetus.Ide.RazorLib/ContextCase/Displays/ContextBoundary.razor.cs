using Fluxor;
using Luthetus.Ide.RazorLib.ContextCase.Models;
using Luthetus.Ide.RazorLib.ContextCase.States;
using Luthetus.Ide.RazorLib.KeymapCase.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Immutable;

namespace Luthetus.Ide.RazorLib.ContextCase.Displays;

public partial class ContextBoundary : ComponentBase
{
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [CascadingParameter]
    public ContextBoundary? ParentContextBoundary { get; set; }

    [Parameter, EditorRequired]
    public ContextRecord ContextRecord { get; set; } = null!;
    [Parameter, EditorRequired]
    public RenderFragment ChildContent { get; set; } = null!;
    [Parameter]
    public string ClassCssString { get; set; } = null!;
    [Parameter]
    public string StyleCssString { get; set; } = null!;
    [Parameter]
    public int TabIndex { get; set; } = -1;

    public void DispatchSetActiveContextStatesAction(List<ContextRecord> contextRecords)
    {
        contextRecords.Add(ContextRecord);

        if (ParentContextBoundary is not null)
        {
            ParentContextBoundary.DispatchSetActiveContextStatesAction(contextRecords);
        }
        else
        {
            Dispatcher.Dispatch(new ContextRegistry.SetActiveContextRecordsAction(contextRecords.ToImmutableArray()));
        }
    }

    public void HandleOnFocusIn()
    {
        DispatchSetActiveContextStatesAction(new());
    }
    
    public async Task HandleOnKeyDownAsync(KeyboardEventArgs keyboardEventArgs)
    {
        if (keyboardEventArgs.Key == "Shift" ||
            keyboardEventArgs.Key == "Control" ||
            keyboardEventArgs.Key == "Alt" ||
            keyboardEventArgs.Key == "Meta")
        {
            return;
        }

        var keymapArgument = new KeymapArgument(
            keyboardEventArgs.Code,
            null,
            keyboardEventArgs.ShiftKey,
            keyboardEventArgs.ShiftKey,
            keyboardEventArgs.CtrlKey,
            keyboardEventArgs.CtrlKey,
            keyboardEventArgs.AltKey,
            keyboardEventArgs.AltKey);

        await HandleKeymapArgumentAsync(keymapArgument);
    }

    public async Task HandleKeymapArgumentAsync(KeymapArgument keymapArgument)
    {
        var success = ContextRecord.Keymap.Map.TryGetValue(keymapArgument, out var command);

        if (success && command is not null)
        {
            await command.DoAsyncFunc();
        }
        else
        {
            if (ParentContextBoundary is not null)
                await ParentContextBoundary.HandleKeymapArgumentAsync(keymapArgument);
        }
    }

    public ImmutableArray<ContextRecord> GetContextBoundaryHeirarchy(List<ContextRecord> contextRecords)
    {
        contextRecords.Add(ContextRecord);

        if (ParentContextBoundary is not null)
            return ParentContextBoundary.GetContextBoundaryHeirarchy(contextRecords);
        else
            return contextRecords.ToImmutableArray();
    }
}
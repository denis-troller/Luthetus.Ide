﻿using Fluxor;
using Fluxor.Blazor.Web.Components;
using Luthetus.Common.RazorLib.Keys.Models;
using Luthetus.Common.RazorLib.Themes.States;
using Luthetus.Common.RazorLib.Themes.Models;
using Luthetus.TextEditor.RazorLib.Options.States;
using Microsoft.AspNetCore.Components;
using Luthetus.TextEditor.RazorLib.TextEditors.Models;

namespace Luthetus.TextEditor.RazorLib.Options.Displays;

public partial class InputTextEditorTheme : FluxorComponent
{
    [Inject]
    private IState<TextEditorOptionsState> TextEditorOptionsStateWrap { get; set; } = null!;
    [Inject]
    private IState<ThemeState> ThemeStateWrap { get; set; } = null!;
    [Inject]
    private ITextEditorService TextEditorService { get; set; } = null!;

    [Parameter]
    public string TopLevelDivElementCssClassString { get; set; } = string.Empty;
    [Parameter]
    public string InputElementCssClassString { get; set; } = string.Empty;
    [Parameter]
    public string LabelElementCssClassString { get; set; } = string.Empty;

    private void SelectedThemeChanged(ChangeEventArgs changeEventArgs)
    {
        var themeBag = ThemeStateWrap.Value.ThemeBag;

        var chosenThemeKeyGuidString = changeEventArgs.Value?.ToString() ?? string.Empty;

        if (Guid.TryParse(chosenThemeKeyGuidString,
                out var chosenThemeKeyGuid))
        {
            var chosenThemeKey = new Key<ThemeRecord>(chosenThemeKeyGuid);
            var foundTheme = themeBag.FirstOrDefault(x => x.Key == chosenThemeKey);

            if (foundTheme is not null)
                TextEditorService.Options.SetTheme(foundTheme);
        }
        else
        {
            TextEditorService.Options.SetTheme(ThemeFacts.VisualStudioDarkThemeClone);
        }
    }
}
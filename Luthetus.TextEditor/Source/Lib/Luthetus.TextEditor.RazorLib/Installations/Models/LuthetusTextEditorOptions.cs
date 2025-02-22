﻿using Luthetus.Common.RazorLib.Installations.Models;
using Luthetus.Common.RazorLib.Keys.Models;
using Luthetus.Common.RazorLib.Themes.Models;
using Luthetus.TextEditor.RazorLib.Autocompletes.Models;
using Luthetus.TextEditor.RazorLib.Finds.Displays;
using Luthetus.TextEditor.RazorLib.Finds.Models;
using Luthetus.TextEditor.RazorLib.Options.Displays;
using Luthetus.TextEditor.RazorLib.TextEditors.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Immutable;

namespace Luthetus.TextEditor.RazorLib.Installations.Models;

public record LuthetusTextEditorOptions
{
    public Key<ThemeRecord>? InitialThemeKey { get; init; }
    public ImmutableArray<ThemeRecord>? CustomThemeRecordBag { get; init; } = LuthetusTextEditorCustomThemeFacts.AllCustomThemesBag;
    public ThemeRecord InitialTheme { get; init; } = ThemeFacts.VisualStudioDarkThemeClone;
    /// <summary>Default value if left null is: <see cref="WordAutocompleteService"/></summary>
    public Func<IServiceProvider, IAutocompleteService> AutocompleteServiceFactory { get; init; } = serviceProvider => new WordAutocompleteService(serviceProvider.GetRequiredService<IAutocompleteIndexer>());
    /// <summary>Default value if left null is: <see cref="WordAutocompleteIndexer"/></summary>
    public Func<IServiceProvider, IAutocompleteIndexer> AutocompleteIndexerFactory { get; init; } = serviceProvider => new WordAutocompleteIndexer(serviceProvider.GetRequiredService<ITextEditorService>());
    public Type SettingsComponentRendererType { get; init; } = typeof(TextEditorSettings);
    public bool SettingsDialogComponentIsResizable { get; init; } = true;
    public Type FindComponentRendererType { get; init; } = typeof(TextEditorFindDisplay);
    public bool FindDialogComponentIsResizable { get; init; } = true;
    public ImmutableArray<ITextEditorFindProvider> FindProviderBag { get; init; } = FindFacts.DefaultFindProvidersBag;
    /// <summary>Default value is <see cref="true"/>. If one wishes to configure Luthetus.Common themselves, then set this to false, and invoke <see cref="Common.RazorLib.Installations.Models.ServiceCollectionExtensions.AddLuthetusCommonServices(IServiceCollection, Func{LuthetusCommonOptions, LuthetusCommonOptions}?)"/> prior to invoking Luthetus.TextEditor's</summary>
    public bool AddLuthetusCommon { get; init; } = true;
}
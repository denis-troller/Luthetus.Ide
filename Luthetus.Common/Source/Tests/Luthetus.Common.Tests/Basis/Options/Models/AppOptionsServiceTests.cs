﻿using Fluxor;
using Luthetus.Common.RazorLib.BackgroundTasks.Models;
using Luthetus.Common.RazorLib.Options.Models;
using Luthetus.Common.RazorLib.Storages.Models;
using Luthetus.Common.RazorLib.Storages.States;
using Luthetus.Common.RazorLib.Themes.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Luthetus.Common.Tests.Basis.Options.Models;

/// <summary>
/// <see cref="AppOptionsService"/>
/// </summary>
public class AppOptionsServiceTests
{
    /// <summary>
    /// <see cref="AppOptionsService(IState{RazorLib.Options.States.AppOptionsState}, IState{RazorLib.Themes.States.ThemeState}, IDispatcher, IStorageService, StorageSync)"/>
    /// <br/>----<br/>
    /// <see cref="AppOptionsService.AppOptionsStateWrap"/>
    /// <see cref="AppOptionsService.StorageKey"/>
    /// <see cref="AppOptionsService.ThemeCssClassString"/>
    /// <see cref="AppOptionsService.FontSizeCssStyleString"/>
    /// </summary>
    [Fact]
    public void Constructor()
    {
        InitializeAppOptionsServiceTests(out var appOptionsService, out _);

        Assert.NotNull(appOptionsService);
        Assert.NotNull(appOptionsService.AppOptionsStateWrap);
        Assert.NotNull(appOptionsService.StorageKey);
        Assert.NotNull(appOptionsService.ThemeCssClassString);
        Assert.NotNull(appOptionsService.FontSizeCssStyleString);
    }

    /// <summary>
    /// <see cref="AppOptionsService.SetActiveThemeRecordKey(RazorLib.Keys.Models.Key{ThemeRecord}, bool)"/>
    /// </summary>
    [Fact]
    public void SetActiveThemeRecordKey()
    {
        InitializeAppOptionsServiceTests(out var appOptionsService, out _);

        var notCurrentlyChosenTheme = 
            appOptionsService.AppOptionsStateWrap.Value.Options.ThemeKey == ThemeFacts.VisualStudioDarkThemeClone.Key
                ? ThemeFacts.VisualStudioLightThemeClone
                : ThemeFacts.VisualStudioDarkThemeClone;

        Assert.NotEqual(
            notCurrentlyChosenTheme.Key,
            appOptionsService.AppOptionsStateWrap.Value.Options.ThemeKey);
        
        appOptionsService.SetActiveThemeRecordKey(notCurrentlyChosenTheme.Key);
        
        Assert.Equal(
            notCurrentlyChosenTheme.Key,
            appOptionsService.AppOptionsStateWrap.Value.Options.ThemeKey);
    }

    /// <summary>
    /// <see cref="AppOptionsService.SetTheme(ThemeRecord, bool)"/>
    /// </summary>
    [Fact]
    public void SetTheme()
    {
        InitializeAppOptionsServiceTests(out var appOptionsService, out _);

        var notCurrentlyChosenTheme =
            appOptionsService.AppOptionsStateWrap.Value.Options.ThemeKey == ThemeFacts.VisualStudioDarkThemeClone.Key
                ? ThemeFacts.VisualStudioLightThemeClone
                : ThemeFacts.VisualStudioDarkThemeClone;

        Assert.NotEqual(
            notCurrentlyChosenTheme.Key,
            appOptionsService.AppOptionsStateWrap.Value.Options.ThemeKey);

        appOptionsService.SetTheme(notCurrentlyChosenTheme);

        Assert.Equal(
            notCurrentlyChosenTheme.Key,
            appOptionsService.AppOptionsStateWrap.Value.Options.ThemeKey);
    }

    /// <summary>
    /// <see cref="AppOptionsService.SetFontFamily(string?, bool)"/>
    /// </summary>
    [Fact]
    public void SetFontFamily()
    {
        InitializeAppOptionsServiceTests(out var appOptionsService, out _);

        var fontFamily = "monospace";

        Assert.NotEqual(fontFamily, appOptionsService.AppOptionsStateWrap.Value.Options.FontFamily);

        appOptionsService.SetFontFamily(fontFamily);

        Assert.Equal(fontFamily, appOptionsService.AppOptionsStateWrap.Value.Options.FontFamily);
    }

    /// <summary>
    /// <see cref="AppOptionsService.SetFontSize(int, bool)"/>
    /// </summary>
    [Fact]
    public void SetFontSize()
    {
        InitializeAppOptionsServiceTests(out var appOptionsService, out _);

        var fontSize = appOptionsService.AppOptionsStateWrap.Value.Options.FontSizeInPixels + 5;

        appOptionsService.SetFontSize(fontSize);

        Assert.Equal(fontSize, appOptionsService.AppOptionsStateWrap.Value.Options.FontSizeInPixels);
    }

    /// <summary>
    /// <see cref="AppOptionsService.SetIconSize(int, bool)"/>
    /// </summary>
    [Fact]
    public void SetIconSize()
    {
        InitializeAppOptionsServiceTests(out var appOptionsService, out _);

        var iconSize = appOptionsService.AppOptionsStateWrap.Value.Options.IconSizeInPixels + 3;

        appOptionsService.SetIconSize(iconSize);

        Assert.Equal(iconSize, appOptionsService.AppOptionsStateWrap.Value.Options.IconSizeInPixels);
    }

    private void InitializeAppOptionsServiceTests(
        out IAppOptionsService appOptionsService,
        out ServiceProvider serviceProvider)
    {
        var services = new ServiceCollection()
            .AddScoped<IAppOptionsService, AppOptionsService>()
            .AddScoped<IStorageService, DoNothingStorageService>()
            .AddScoped<StorageSync>()
            .AddScoped<IBackgroundTaskService, BackgroundTaskServiceSynchronous>()
            .AddFluxor(options => options.ScanAssemblies(typeof(IAppOptionsService).Assembly));

        serviceProvider = services.BuildServiceProvider();

        var store = serviceProvider.GetRequiredService<IStore>();
        store.InitializeAsync().Wait();

        appOptionsService = serviceProvider.GetRequiredService<IAppOptionsService>();
    }
}
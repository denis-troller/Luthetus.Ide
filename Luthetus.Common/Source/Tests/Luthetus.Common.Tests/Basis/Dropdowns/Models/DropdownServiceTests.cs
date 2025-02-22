﻿using Fluxor;
using Luthetus.Common.RazorLib.Dropdowns.Models;
using Luthetus.Common.RazorLib.Keys.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Luthetus.Common.Tests.Basis.Dropdowns.Models;

/// <summary>
/// <see cref="DropdownService"/>
/// </summary>
public class DropdownServiceTests
{
    /// <summary>
    /// <see cref="DropdownService(IDispatcher, IState{RazorLib.Dropdowns.States.DropdownState})"/>
    /// <br/>----<br/>
    /// <see cref="DropdownService.DropdownStateWrap"/>
    /// </summary>
    [Fact]
    public void Constructor()
    {
        InitializeDropdownServiceTests(out var dropdownService, out _, out _);

        Assert.NotNull(dropdownService);
        Assert.NotNull(dropdownService.DropdownStateWrap);
    }

    /// <summary>
    /// <see cref="DropdownService.AddActiveDropdownKey(Key{DropdownRecord})"/>
    /// </summary>
    [Fact]
    public void AddActiveDropdownKey()
    {
        InitializeDropdownServiceTests(out var dropdownService, out var dropdownKey, out _);

        Assert.Empty(dropdownService.DropdownStateWrap.Value.ActiveKeyBag);

        dropdownService.AddActiveDropdownKey(dropdownKey);
        
        Assert.NotEmpty(dropdownService.DropdownStateWrap.Value.ActiveKeyBag);
        Assert.Single(dropdownService.DropdownStateWrap.Value.ActiveKeyBag);

        Assert.Contains(
            dropdownService.DropdownStateWrap.Value.ActiveKeyBag,
            x => x == dropdownKey);
    }

    /// <summary>
    /// <see cref="DropdownService.RemoveActiveDropdownKey(Key{DropdownRecord})"/>
    /// </summary>
    [Fact]
    public void RemoveActiveDropdownKey()
    {
        InitializeDropdownServiceTests(out var dropdownService, out var dropdownKey, out _);

        Assert.Empty(dropdownService.DropdownStateWrap.Value.ActiveKeyBag);

        dropdownService.AddActiveDropdownKey(dropdownKey);

        Assert.Single(dropdownService.DropdownStateWrap.Value.ActiveKeyBag);

        Assert.Contains(
            dropdownService.DropdownStateWrap.Value.ActiveKeyBag,
            x => x == dropdownKey);

        dropdownService.RemoveActiveDropdownKey(dropdownKey);
        
        Assert.Empty(dropdownService.DropdownStateWrap.Value.ActiveKeyBag);
    }

    /// <summary>
    /// <see cref="DropdownService.ClearActiveDropdownKeysAction()"/>
    /// </summary>
    [Fact]
    public void ClearActiveDropdownKeysAction()
    {
        InitializeDropdownServiceTests(out var dropdownService, out _, out _);

        Assert.Empty(dropdownService.DropdownStateWrap.Value.ActiveKeyBag);

        var keyCount = 3;

        for (int i = 0; i < keyCount; i++)
        {
            dropdownService.AddActiveDropdownKey(Key<DropdownRecord>.NewKey());
        }

        Assert.NotEmpty(dropdownService.DropdownStateWrap.Value.ActiveKeyBag);
        Assert.Equal(3, dropdownService.DropdownStateWrap.Value.ActiveKeyBag.Count);

        dropdownService.ClearActiveDropdownKeysAction();

        Assert.Empty(dropdownService.DropdownStateWrap.Value.ActiveKeyBag);
    }

    private void InitializeDropdownServiceTests(
        out IDropdownService dropdownService,
        out Key<DropdownRecord> sampleDropdownRecordKey,
        out ServiceProvider serviceProvider)
    {
        var services = new ServiceCollection()
            .AddScoped<IDropdownService, DropdownService>()
            .AddFluxor(options => options.ScanAssemblies(typeof(IDropdownService).Assembly));

        serviceProvider = services.BuildServiceProvider();

        var store = serviceProvider.GetRequiredService<IStore>();
        store.InitializeAsync().Wait();

        dropdownService = serviceProvider.GetRequiredService<IDropdownService>();

        sampleDropdownRecordKey = Key<DropdownRecord>.NewKey();
    }
}
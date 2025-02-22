﻿using Fluxor;
using Luthetus.Common.RazorLib.Keys.Models;
using Luthetus.Common.RazorLib.Notifications.Displays;
using Luthetus.Common.RazorLib.Notifications.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Luthetus.Common.Tests.Basis.Notifications.Models;

/// <summary>
/// <see cref="NotificationService"/>
/// </summary>
public class NotificationServiceTests
{
    /// <summary>
    /// <see cref="NotificationService(IDispatcher, IState{RazorLib.Notifications.States.NotificationState})"/>
    /// </summary>
    [Fact]
    public void Constructor()
    {
        var services = new ServiceCollection()
            .AddScoped<INotificationService, NotificationService>()
            .AddFluxor(options => options.ScanAssemblies(typeof(INotificationService).Assembly));

        var serviceProvider = services.BuildServiceProvider();

        var store = serviceProvider.GetRequiredService<IStore>();
        store.InitializeAsync().Wait();

        var notificationService = serviceProvider.GetRequiredService<INotificationService>();

        Assert.NotNull(notificationService);
    }

    /// <summary>
    /// <see cref="NotificationService.NotificationStateWrap"/>
    /// </summary>
    [Fact]
    public void NotificationStateWrap()
    {
        var services = new ServiceCollection()
            .AddScoped<INotificationService, NotificationService>()
            .AddFluxor(options => options.ScanAssemblies(typeof(INotificationService).Assembly));

        var serviceProvider = services.BuildServiceProvider();
        
        var store = serviceProvider.GetRequiredService<IStore>();
        store.InitializeAsync().Wait();

        var notificationService = serviceProvider.GetRequiredService<INotificationService>();

        Assert.NotNull(notificationService.NotificationStateWrap);
    }

    /// <summary>
    /// <see cref="NotificationService.RegisterNotificationRecord(NotificationRecord)"/>
    /// </summary>
    [Fact]
    public void RegisterNotificationRecord()
    {
        var services = new ServiceCollection()
            .AddScoped<INotificationService, NotificationService>()
            .AddFluxor(options => options.ScanAssemblies(typeof(INotificationService).Assembly));

        var serviceProvider = services.BuildServiceProvider();

        var store = serviceProvider.GetRequiredService<IStore>();
        store.InitializeAsync().Wait();

        var notificationService = serviceProvider.GetRequiredService<INotificationService>();

        Assert.Empty(notificationService.NotificationStateWrap.Value.DefaultBag);

        var notificationRecord = new NotificationRecord(
            Key<NotificationRecord>.NewKey(),
            "Test",
            typeof(CommonInformativeNotificationDisplay),
            new Dictionary<string, object?>
            {
                {
                    nameof(CommonInformativeNotificationDisplay.Message),
                    "Message testing"
                }
            },
            null,
            true,
            null);

        notificationService.RegisterNotificationRecord(notificationRecord);

        Assert.NotEmpty(notificationService.NotificationStateWrap.Value.DefaultBag);

        Assert.Contains(notificationService.NotificationStateWrap.Value.DefaultBag,
            x => x == notificationRecord);
    }

    /// <summary>
    /// <see cref="NotificationService.DisposeNotificationRecord(Key{NotificationRecord})"/>
    /// </summary>
    [Fact]
    public void DisposeNotificationRecord()
    {
        var services = new ServiceCollection()
            .AddScoped<INotificationService, NotificationService>()
            .AddFluxor(options => options.ScanAssemblies(typeof(INotificationService).Assembly));

        var serviceProvider = services.BuildServiceProvider();

        var store = serviceProvider.GetRequiredService<IStore>();
        store.InitializeAsync().Wait();

        var notificationService = serviceProvider.GetRequiredService<INotificationService>();

        Assert.Empty(notificationService.NotificationStateWrap.Value.DefaultBag);

        var notificationRecord = new NotificationRecord(
            Key<NotificationRecord>.NewKey(),
            "Test",
            typeof(CommonInformativeNotificationDisplay),
            new Dictionary<string, object?>
            {
                {
                    nameof(CommonInformativeNotificationDisplay.Message),
                    "Message testing"
                }
            },
            null,
            true,
            null);

        notificationService.RegisterNotificationRecord(notificationRecord);

        Assert.NotEmpty(notificationService.NotificationStateWrap.Value.DefaultBag);

        Assert.Contains(notificationService.NotificationStateWrap.Value.DefaultBag,
            x => x == notificationRecord);

        notificationService.DisposeNotificationRecord(notificationRecord.Key);

        Assert.Empty(notificationService.NotificationStateWrap.Value.DefaultBag);
    }
}
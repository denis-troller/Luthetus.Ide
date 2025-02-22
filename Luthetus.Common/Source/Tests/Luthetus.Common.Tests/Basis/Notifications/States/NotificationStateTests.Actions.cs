﻿using Luthetus.Common.RazorLib.Keys.Models;
using Luthetus.Common.RazorLib.Notifications.Displays;
using Luthetus.Common.RazorLib.Notifications.Models;
using Luthetus.Common.RazorLib.Notifications.States;

namespace Luthetus.Common.Tests.Basis.Notifications.States;

/// <summary>
/// <see cref="NotificationState"/>
/// </summary>
public class NotificationStateActionsTests
{
    /// <summary>
    /// <see cref="NotificationState.RegisterAction"/>
    /// </summary>
    [Fact]
    public void RegisterAction()
    {
        InitializeNotificationStateActionsTests(out var notificationRecord);

        var registerAction = new NotificationState.RegisterAction(notificationRecord);
        Assert.Equal(notificationRecord, registerAction.Notification);
    }

    /// <summary>
    /// <see cref="NotificationState.DisposeAction"/>
    /// </summary>
    [Fact]
    public void DisposeAction()
    {
        InitializeNotificationStateActionsTests(out var notificationRecord);

        var disposeAction = new NotificationState.DisposeAction(notificationRecord.Key);
        Assert.Equal(notificationRecord.Key, disposeAction.Key);
    }

    /// <summary>
    /// <see cref="NotificationState.MakeReadAction"/>
    /// </summary>
    [Fact]
    public void MakeReadAction()
    {
        InitializeNotificationStateActionsTests(out var notificationRecord);

        var makeReadAction = new NotificationState.MakeReadAction(notificationRecord.Key);
        Assert.Equal(notificationRecord.Key, makeReadAction.Key);
    }

    /// <summary>
    /// <see cref="NotificationState.UndoMakeReadAction"/>
    /// </summary>
    [Fact]
    public void UndoMakeReadAction()
    {
        InitializeNotificationStateActionsTests(out var notificationRecord);

        var undoMakeReadAction = new NotificationState.UndoMakeReadAction(notificationRecord.Key);
        Assert.Equal(notificationRecord.Key, undoMakeReadAction.Key);
    }

    /// <summary>
    /// <see cref="NotificationState.MakeDeletedAction"/>
    /// </summary>
    [Fact]
    public void MakeDeletedAction()
    {
        InitializeNotificationStateActionsTests(out var notificationRecord);

        var makeDeletedAction = new NotificationState.MakeDeletedAction(notificationRecord.Key);
        Assert.Equal(notificationRecord.Key, makeDeletedAction.Key);
    }

    /// <summary>
    /// <see cref="NotificationState.UndoMakeDeletedAction"/>
    /// </summary>
    [Fact]
    public void UndoMakeDeletedAction()
    {
        InitializeNotificationStateActionsTests(out var notificationRecord);

        var undoMakeDeletedAction = new NotificationState.UndoMakeDeletedAction(notificationRecord.Key);
        Assert.Equal(notificationRecord.Key, undoMakeDeletedAction.Key);
    }

    /// <summary>
    /// <see cref="NotificationState.MakeArchivedAction"/>
    /// </summary>
    [Fact]
    public void MakeArchivedAction()
    {
        InitializeNotificationStateActionsTests(out var notificationRecord);

        var makeArchivedAction = new NotificationState.MakeArchivedAction(notificationRecord.Key);
        Assert.Equal(notificationRecord.Key, makeArchivedAction.Key);
    }

    /// <summary>
    /// <see cref="NotificationState.UndoMakeArchivedAction"/>
    /// </summary>
    [Fact]
    public void UndoMakeArchivedAction()
    {
        InitializeNotificationStateActionsTests(out var notificationRecord);

        var undoMakeArchivedAction = new NotificationState.UndoMakeArchivedAction(notificationRecord.Key);
        Assert.Equal(notificationRecord.Key, undoMakeArchivedAction.Key);
    }

    /// <summary>
    /// <see cref="NotificationState.ClearDefaultAction"/>
    /// </summary>
    [Fact]
    public void ClearDefaultAction()
    {
        InitializeNotificationStateActionsTests(out var notificationRecord);

        var clearDefaultAction = new NotificationState.ClearDefaultAction();
        Assert.NotNull(clearDefaultAction);
    }

    /// <summary>
    /// <see cref="NotificationState.ClearReadAction"/>
    /// </summary>
    [Fact]
    public void ClearReadAction()
    {
        InitializeNotificationStateActionsTests(out var notificationRecord);

        var clearReadAction = new NotificationState.ClearReadAction();
        Assert.NotNull(clearReadAction);
    }

    /// <summary>
    /// <see cref="NotificationState.ClearDeletedAction"/>
    /// </summary>
    [Fact]
    public void ClearDeletedAction()
    {
        InitializeNotificationStateActionsTests(out var notificationRecord);

        var clearDeletedAction = new NotificationState.ClearDeletedAction();
        Assert.NotNull(clearDeletedAction);
    }

    /// <summary>
    /// <see cref="NotificationState.ClearArchivedAction"/>
    /// </summary>
    [Fact]
    public void ClearArchivedAction()
    {
        InitializeNotificationStateActionsTests(out var notificationRecord);

        var clearArchivedAction = new NotificationState.ClearArchivedAction();
        Assert.NotNull(clearArchivedAction);
    }

    private void InitializeNotificationStateActionsTests(
        out NotificationRecord sampleNotificationRecord)
    {
        sampleNotificationRecord = new NotificationRecord(
            Key<NotificationRecord>.NewKey(),
            "Test title",
            typeof(CommonInformativeNotificationDisplay),
            new Dictionary<string, object?>
            {
                {
                    nameof(CommonInformativeNotificationDisplay.Message),
                    "Test message"
                }
            },
            null,
            true,
            null);
    }
}
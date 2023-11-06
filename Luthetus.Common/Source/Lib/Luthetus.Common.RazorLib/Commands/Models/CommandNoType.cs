﻿namespace Luthetus.Common.RazorLib.Commands.Models;

public abstract class CommandNoType
{
    public CommandNoType(
        Func<ICommandArgs, Task> doAsyncFunc,
        string displayName,
        string internalIdentifier,
        bool shouldBubble)
    {
        DoAsyncFunc = doAsyncFunc;
        DisplayName = displayName;
        InternalIdentifier = internalIdentifier;
        ShouldBubble = shouldBubble;
    }

    public Func<ICommandArgs, Task> DoAsyncFunc { get; }
    public string DisplayName { get; }
    public string InternalIdentifier { get; }
    public bool ShouldBubble { get; }
}

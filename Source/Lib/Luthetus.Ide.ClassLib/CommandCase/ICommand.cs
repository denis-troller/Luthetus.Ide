﻿namespace Luthetus.Ide.ClassLib.CommandCase;

public interface ICommand
{
    public Func<Task> DoAsyncFunc { get; }
    public string DisplayName { get; }
    public string InternalIdentifier { get; }
    /// <summary><see cref="ShouldBubble"/> is false by default</summary>
    public bool ShouldBubble { get; }
}
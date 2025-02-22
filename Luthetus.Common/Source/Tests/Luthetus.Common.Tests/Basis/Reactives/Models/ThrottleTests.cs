﻿using Luthetus.Common.RazorLib.Reactives.Models;

namespace Luthetus.Common.Tests.Basis.Reactives.Models;

/// <summary>
/// <see cref="Throttle"/>
/// </summary>
public class ThrottleTests
{
    /// <summary>
    /// <see cref="Throttle(TimeSpan)"/>
    /// <br/>----<br/>
    /// <see cref="Throttle.ThrottleTimeSpan"/>
    /// </summary>
    [Fact]
    public void Constructor()
    {
        var timeSpan = TimeSpan.FromMilliseconds(500);

        var throttle = new Throttle(timeSpan);

        Assert.Equal(timeSpan, throttle.ThrottleTimeSpan);
    }

    /// <summary>
    /// <see cref="Throttle.FireAsync(Func{Task})"/>
    /// </summary>
    [Fact]
    public void FireAsync()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// <see cref="Throttle.Dispose()"/>
    /// </summary>
    [Fact]
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
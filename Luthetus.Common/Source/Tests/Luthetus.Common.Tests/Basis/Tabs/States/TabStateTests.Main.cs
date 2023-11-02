﻿using Fluxor;
using Luthetus.Common.RazorLib.Tabs.Models;
using System.Collections.Immutable;

namespace Luthetus.Common.RazorLib.Tabs.States;

[FeatureState]
public partial record TabStateTests(ImmutableList<TabGroup> TabGroupBag)
{
    private TabState() : this(ImmutableList<TabGroup>.Empty)
    {
    }
}

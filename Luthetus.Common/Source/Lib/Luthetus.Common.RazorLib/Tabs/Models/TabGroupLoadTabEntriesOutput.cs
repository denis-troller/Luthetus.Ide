﻿using System.Collections.Immutable;

namespace Luthetus.Common.RazorLib.Tabs.Models;

public record TabGroupLoadTabEntriesOutput(ImmutableList<TabEntryNoType> OutTabEntries);

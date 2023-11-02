﻿using Luthetus.Common.RazorLib.Keys.Models;
using Luthetus.Common.RazorLib.Themes.Models;

namespace Luthetus.Common.RazorLib.Options.Models;

public record CommonOptionsTests(
    int FontSizeInPixels,
    int IconSizeInPixels,
    Key<ThemeRecord> ThemeKey,
    string? FontFamily);

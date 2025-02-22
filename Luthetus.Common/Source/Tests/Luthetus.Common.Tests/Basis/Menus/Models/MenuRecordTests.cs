using Luthetus.Common.RazorLib.Menus.Models;

namespace Luthetus.Common.Tests.Basis.Menus.Models;

/// <summary>
/// <see cref="MenuRecord"/>
/// </summary>
public class MenuRecordTests
{
    /// <summary>
    /// <see cref="MenuRecord.Empty"/>
    /// </summary>
    [Fact]
    public void Empty()
    {
        Assert.Single(MenuRecord.Empty.MenuOptionBag);

        var menuOption = MenuRecord.Empty.MenuOptionBag.Single();

        Assert.Equal("No menu options exist for this item.", menuOption.DisplayName);
        Assert.Equal(MenuOptionKind.Other, menuOption.MenuOptionKind);
        Assert.Null(menuOption.OnClick);
        Assert.Null(menuOption.SubMenu);
        Assert.Null(menuOption.WidgetRendererType);
        Assert.Null(menuOption.WidgetParameterMap);
    }
}
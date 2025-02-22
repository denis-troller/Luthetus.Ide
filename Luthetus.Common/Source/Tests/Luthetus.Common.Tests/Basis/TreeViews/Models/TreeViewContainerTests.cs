﻿using Luthetus.Common.RazorLib.ComponentRenderers.Models;
using Luthetus.Common.RazorLib.Keys.Models;
using Luthetus.Common.RazorLib.TreeViews.Models;
using Luthetus.Common.RazorLib.WatchWindows.Models;
using Luthetus.Common.RazorLib.Notifications.Displays;
using Luthetus.Common.RazorLib.WatchWindows.Displays;
using System.Collections.Immutable;

namespace Luthetus.Common.Tests.Basis.TreeViews.Models;

/// <summary>
/// <see cref="TreeViewContainer"/>
/// </summary>
public record TreeViewContainerTests
{
    /// <summary>
    /// <see cref="TreeViewContainer(Key{TreeViewContainer}, TreeViewNoType?, ImmutableList{TreeViewNoType})"/>
    /// <br/>----<br/>
    /// <see cref="TreeViewContainer.Key"/>
    /// <see cref="TreeViewContainer.RootNode"/>
    /// <see cref="TreeViewContainer.ActiveNode"/>
    /// <see cref="TreeViewContainer.SelectedNodeBag"/>
    /// <see cref="TreeViewContainer.StateId"/>
    /// </summary>
    [Fact]
    public void Constructor()
    {
        InitializeTreeViewContainerTests(out var commonTreeViews, out var commonComponentRenderers);

        // rootNode=Null, selectedNodeBag=Empty
        {
            var key = Key<TreeViewContainer>.NewKey();
            var selectedNodeBag = ImmutableList<TreeViewNoType>.Empty;
            var stateId = Guid.NewGuid();

            var treeViewContainer = new TreeViewContainer(
                key,
                null,
                selectedNodeBag)
            {
                StateId = stateId
            };

            Assert.Equal(key, treeViewContainer.Key);
            Assert.IsType<TreeViewAdhoc>(treeViewContainer.RootNode);
            Assert.Equal(selectedNodeBag, treeViewContainer.SelectedNodeBag);
            Assert.Null(treeViewContainer.ActiveNode);
            Assert.Equal(stateId, treeViewContainer.StateId);
        }

        // rootNode=treeViewText, selectedNodeBag=Empty
        {
            var key = Key<TreeViewContainer>.NewKey();
            var rootNode = new TreeViewText("Hello World!", true, false, commonComponentRenderers);
            var selectedNodeBag = ImmutableList<TreeViewNoType>.Empty;
            var stateId = Guid.NewGuid();
            
            var treeViewContainer = new TreeViewContainer(
                key,
                rootNode,
                selectedNodeBag)
            {
                StateId = stateId
            };

            Assert.Equal(key, treeViewContainer.Key);
            Assert.Equal(rootNode, treeViewContainer.RootNode);
            Assert.Equal(selectedNodeBag, treeViewContainer.SelectedNodeBag);
            Assert.Null(treeViewContainer.ActiveNode);
            Assert.Equal(stateId, treeViewContainer.StateId);
        }

        // rootNode=treeViewText, selectedNodeBag={ treeViewText }
        {
            var key = Key<TreeViewContainer>.NewKey();
            var rootNode = new TreeViewText("Hello World!", true, false, commonComponentRenderers);
            var selectedNodeBag = new TreeViewNoType[] { rootNode }.ToImmutableList();
            var stateId = Guid.NewGuid();
            
            var treeViewContainer = new TreeViewContainer(
                key,
                rootNode,
                selectedNodeBag)
            {
                StateId = stateId
            };

            Assert.Equal(key, treeViewContainer.Key);
            Assert.Equal(rootNode, treeViewContainer.RootNode);
            Assert.Equal(selectedNodeBag, treeViewContainer.SelectedNodeBag);
            Assert.Equal(rootNode, treeViewContainer.ActiveNode);
            Assert.Equal(stateId, treeViewContainer.StateId);
        }
    }

    private void InitializeTreeViewContainerTests(
        out LuthetusCommonTreeViews commonTreeViews,
        out LuthetusCommonComponentRenderers commonComponentRenderers)
    {
        commonTreeViews = new LuthetusCommonTreeViews(
            typeof(TreeViewExceptionDisplay),
            typeof(TreeViewMissingRendererFallbackDisplay),
            typeof(TreeViewTextDisplay),
            typeof(TreeViewReflectionDisplay),
            typeof(TreeViewPropertiesDisplay),
            typeof(TreeViewInterfaceImplementationDisplay),
            typeof(TreeViewFieldsDisplay),
            typeof(TreeViewExceptionDisplay),
            typeof(TreeViewEnumerableDisplay));

        commonComponentRenderers = new LuthetusCommonComponentRenderers(
            typeof(CommonErrorNotificationDisplay),
            typeof(CommonInformativeNotificationDisplay),
            commonTreeViews);
    }
}
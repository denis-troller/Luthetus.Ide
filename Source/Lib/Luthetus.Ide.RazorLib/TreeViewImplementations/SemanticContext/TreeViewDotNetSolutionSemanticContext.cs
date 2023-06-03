﻿using Luthetus.Common.RazorLib.TreeView.TreeViewClasses;
using Luthetus.Common.RazorLib.WatchWindow.TreeViewClasses;
using Luthetus.Ide.ClassLib.ComponentRenderers;
using Luthetus.Ide.ClassLib.FileSystem.Interfaces;
using Luthetus.Ide.ClassLib.CompilerServices.Languages.CSharp.SemanticContextCase.Implementations;
using Luthetus.Ide.RazorLib.TreeViewImplementations.SemanticContext;
using Luthetus.Ide.ClassLib.Store.SemanticContextCase;
using System.Linq;

namespace Luthetus.Ide.ClassLib.TreeViewImplementations.SemanticContext;

public class TreeViewDotNetSolutionSemanticContext : TreeViewWithType<(SemanticContextState semanticContextState, DotNetSolutionSemanticContext dotNetSolutionSemanticContext)>
{
    public TreeViewDotNetSolutionSemanticContext(
        (SemanticContextState semanticContextState, DotNetSolutionSemanticContext dotNetSolutionSemanticContext) semanticContextTuple,
        ILuthetusIdeComponentRenderers luthetusIdeComponentRenderers,
        IFileSystemProvider fileSystemProvider,
        IEnvironmentProvider environmentProvider,
        bool isExpandable,
        bool isExpanded)
            : base(
                semanticContextTuple,
                isExpandable,
                isExpanded)
    {
        LuthetusIdeComponentRenderers = luthetusIdeComponentRenderers;
        FileSystemProvider = fileSystemProvider;
        EnvironmentProvider = environmentProvider;
    }

    public ILuthetusIdeComponentRenderers LuthetusIdeComponentRenderers { get; }
    public IFileSystemProvider FileSystemProvider { get; }
    public IEnvironmentProvider EnvironmentProvider { get; }

    public override bool Equals(object? obj)
    {
        if (obj is null ||
            obj is not TreeViewDotNetSolutionSemanticContext treeViewDotNetSolutionSemanticContext ||
            treeViewDotNetSolutionSemanticContext.Item.dotNetSolutionSemanticContext is null ||
            Item.dotNetSolutionSemanticContext is null)
        {
            return false;
        }

        return treeViewDotNetSolutionSemanticContext.Item.dotNetSolutionSemanticContext.DotNetSolution.NamespacePath.AbsoluteFilePath.GetAbsoluteFilePathString() ==
               Item.dotNetSolutionSemanticContext.DotNetSolution.NamespacePath.AbsoluteFilePath.GetAbsoluteFilePathString();
    }

    public override int GetHashCode()
    {
        return Item.dotNetSolutionSemanticContext.DotNetSolution.NamespacePath.AbsoluteFilePath
            .GetAbsoluteFilePathString()
            .GetHashCode();
    }

    public override TreeViewRenderer GetTreeViewRenderer()
    {
        return new TreeViewRenderer(
            typeof(TreeViewDotNetSolutionSemanticContextDisplay),
            new Dictionary<string, object?>
            {
                {
                    nameof(TreeViewDotNetSolutionSemanticContextDisplay.DotNetSolutionSemanticContext),
                    Item.dotNetSolutionSemanticContext
                },
            });
    }

    public override async Task LoadChildrenAsync()
    {
        if (Item.dotNetSolutionSemanticContext is null)
            return;

        try
        {
            var newChildren = Item.dotNetSolutionSemanticContext.DotNetProjectContextMap.Values
                .Select(dnp => (TreeViewNoType)new TreeViewDotNetProjectSemanticContext(
                    (Item.semanticContextState, dnp),
                    LuthetusIdeComponentRenderers,
                    FileSystemProvider,
                    EnvironmentProvider,
                    true,
                    false))
                .ToList();

            var oldChildrenMap = Children
                .ToDictionary(child => child);

            foreach (var newChild in newChildren)
            {
                if (oldChildrenMap.TryGetValue(newChild, out var oldChild))
                {
                    newChild.IsExpanded = oldChild.IsExpanded;
                    newChild.IsExpandable = oldChild.IsExpandable;
                    newChild.IsHidden = oldChild.IsHidden;
                    newChild.TreeViewNodeKey = oldChild.TreeViewNodeKey;
                    newChild.Children = oldChild.Children;
                }
            }

            for (int i = 0; i < newChildren.Count; i++)
            {
                var newChild = newChildren[i];

                newChild.IndexAmongSiblings = i;
                newChild.Parent = this;
                newChild.TreeViewChangedKey = TreeViewChangedKey.NewTreeViewChangedKey();
            }

            Children = newChildren;
        }
        catch (Exception exception)
        {
            Children = new List<TreeViewNoType>
            {
                new TreeViewException(
                    exception,
                    false,
                    false,
                    LuthetusIdeComponentRenderers.LuthetusCommonComponentRenderers.WatchWindowTreeViewRenderers)
                {
                    Parent = this,
                    IndexAmongSiblings = 0,
                }
            };
        }

        TreeViewChangedKey = TreeViewChangedKey.NewTreeViewChangedKey();
    }

    public override void RemoveRelatedFilesFromParent(List<TreeViewNoType> siblingsAndSelfTreeViews)
    {
        return;
    }
}
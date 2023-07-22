﻿using Luthetus.Common.RazorLib.FileSystem.Interfaces;
using Luthetus.Common.RazorLib.TreeView.TreeViewClasses;
using Luthetus.CompilerServices.Lang.DotNet.CSharp;
using Luthetus.Ide.ClassLib.ComponentRenderers;
using Luthetus.Ide.ClassLib.ComponentRenderers.Types;

namespace Luthetus.Ide.ClassLib.TreeViewImplementations;

public class TreeViewCSharpProjectToProjectReference : TreeViewWithType<CSharpProjectToProjectReference>
{
    public TreeViewCSharpProjectToProjectReference(
        CSharpProjectToProjectReference cSharpProjectToProjectReference,
        ILuthetusIdeComponentRenderers luthetusIdeComponentRenderers,
        IFileSystemProvider fileSystemProvider,
        IEnvironmentProvider environmentProvider,
        bool isExpandable,
        bool isExpanded)
            : base(
                cSharpProjectToProjectReference,
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
            obj is not TreeViewCSharpProjectToProjectReference)
        {
            return false;
        }

        return true;
    }

    public override int GetHashCode()
    {
        return Item.GetHashCode();
    }

    public override TreeViewRenderer GetTreeViewRenderer()
    {
        return new TreeViewRenderer(
            LuthetusIdeComponentRenderers.TreeViewCSharpProjectToProjectReferenceRendererType!,
            new Dictionary<string, object?>
            {
            {
                nameof(ITreeViewCSharpProjectToProjectReferenceRendererType.CSharpProjectToProjectReference),
                Item
            },
            });
    }

    public override Task LoadChildrenAsync()
    {
        TreeViewChangedKey = TreeViewChangedKey.NewTreeViewChangedKey();
        return Task.CompletedTask;
    }

    public override void RemoveRelatedFilesFromParent(List<TreeViewNoType> siblingsAndSelfTreeViews)
    {
        return;
    }
}
﻿using Fluxor;
using Luthetus.Common.RazorLib.BackgroundTasks.Models;
using Luthetus.Common.RazorLib.Keys.Models;
using Luthetus.Ide.RazorLib.TreeViewImplementations.Models;

namespace Luthetus.Ide.RazorLib.InputFiles.States;

public partial record InputFileState
{
    private static class Reducer
    {
        [ReducerMethod]
        public static InputFileState ReduceStartInputFileStateFormAction(
            InputFileState inState,
            StartInputFileStateFormAction startInputFileStateFormAction)
        {
            return inState with
            {
                SelectionIsValidFunc = startInputFileStateFormAction.SelectionIsValidFunc,
                OnAfterSubmitFunc = startInputFileStateFormAction.OnAfterSubmitFunc,
                InputFilePatternsBag = startInputFileStateFormAction.InputFilePatterns,
                SelectedInputFilePattern = startInputFileStateFormAction.InputFilePatterns.First(),
                Message = startInputFileStateFormAction.Message
            };
        }

        [ReducerMethod]
        public static InputFileState ReduceSetSelectedTreeViewModelAction(
            InputFileState inState,
            SetSelectedTreeViewModelAction setSelectedTreeViewModelAction)
        {
            return inState with
            {
                SelectedTreeViewModel = setSelectedTreeViewModelAction.SelectedTreeViewModel
            };
        }

        [ReducerMethod]
        public static InputFileState ReduceSetOpenedTreeViewModelAction(
            InputFileState inState,
            SetOpenedTreeViewModelAction setOpenedTreeViewModelAction)
        {
            if (setOpenedTreeViewModelAction.TreeViewModel.Item.IsDirectory)
            {
                return NewOpenedTreeViewModelHistory(
                    inState,
                    setOpenedTreeViewModelAction.TreeViewModel,
                    setOpenedTreeViewModelAction.IdeComponentRenderers,
                    setOpenedTreeViewModelAction.CommonComponentRenderers,
                    setOpenedTreeViewModelAction.FileSystemProvider,
                    setOpenedTreeViewModelAction.EnvironmentProvider);
            }

            return inState;
        }

        [ReducerMethod]
        public static InputFileState ReduceSetSelectedInputFilePatternAction(
            InputFileState inState,
            SetSelectedInputFilePatternAction setSelectedInputFilePatternAction)
        {
            return inState with
            {
                SelectedInputFilePattern = setSelectedInputFilePatternAction.InputFilePattern
            };
        }

        [ReducerMethod(typeof(MoveBackwardsInHistoryAction))]
        public static InputFileState ReduceMoveBackwardsInHistoryAction(InputFileState inState)
        {
            if (inState.CanMoveBackwardsInHistory)
                return inState with { IndexInHistory = inState.IndexInHistory - 1 };

            return inState;
        }

        [ReducerMethod(typeof(MoveForwardsInHistoryAction))]
        public static InputFileState ReduceMoveForwardsInHistoryAction(InputFileState inState)
        {
            if (inState.CanMoveForwardsInHistory)
                return inState with { IndexInHistory = inState.IndexInHistory + 1 };

            return inState;
        }

        [ReducerMethod]
        public static InputFileState ReduceOpenParentDirectoryAction(
            InputFileState inState,
            OpenParentDirectoryAction openParentDirectoryAction)
        {
            var currentSelection = inState.OpenedTreeViewModelHistoryBag[inState.IndexInHistory];

            TreeViewAbsolutePath? parentDirectoryTreeViewModel = null;

            // If has a ParentDirectory select it
            if (currentSelection.Item.AncestorDirectoryBag.Any())
            {
                var parentDirectoryAbsolutePath = currentSelection.Item.AncestorDirectoryBag.Last();

                parentDirectoryTreeViewModel = new TreeViewAbsolutePath(
                    parentDirectoryAbsolutePath,
                    openParentDirectoryAction.IdeComponentRenderers,
                    openParentDirectoryAction.CommonComponentRenderers,
                    openParentDirectoryAction.FileSystemProvider,
                    openParentDirectoryAction.EnvironmentProvider,
                    false,
                    true);

                openParentDirectoryAction.BackgroundTaskService.Enqueue(Key<BackgroundTask>.NewKey(), ContinuousBackgroundTaskWorker.Queue.Key,
                    "Open Parent Directory",
                    async () =>
                    {
                        await parentDirectoryTreeViewModel.LoadChildBagAsync();
                    });
            }

            if (parentDirectoryTreeViewModel is not null)
            {
                return NewOpenedTreeViewModelHistory(
                    inState,
                    parentDirectoryTreeViewModel,
                    openParentDirectoryAction.IdeComponentRenderers,
                    openParentDirectoryAction.CommonComponentRenderers,
                    openParentDirectoryAction.FileSystemProvider,
                    openParentDirectoryAction.EnvironmentProvider);
            }

            return inState;
        }

        [ReducerMethod]
        public static InputFileState ReduceRefreshCurrentSelectionAction(
            InputFileState inState,
            RefreshCurrentSelectionAction refreshCurrentSelectionAction)
        {
            var currentSelection = inState.OpenedTreeViewModelHistoryBag[inState.IndexInHistory];
            currentSelection.ChildBag.Clear();

            refreshCurrentSelectionAction.BackgroundTaskService.Enqueue(Key<BackgroundTask>.NewKey(), ContinuousBackgroundTaskWorker.Queue.Key,
                "Refresh Current Selection",
                async () =>
                {
                    await currentSelection.LoadChildBagAsync();
                });

            return inState;
        }

        [ReducerMethod]
        public static InputFileState ReduceSetSearchQueryAction(
            InputFileState inState,
            SetSearchQueryAction setSearchQueryAction)
        {
            var openedTreeViewModel = inState.OpenedTreeViewModelHistoryBag[inState.IndexInHistory];

            foreach (var treeViewModel in openedTreeViewModel.ChildBag)
            {
                var treeViewAbsolutePath = (TreeViewAbsolutePath)treeViewModel;

                treeViewModel.IsHidden = !treeViewAbsolutePath.Item.NameWithExtension.Contains(
                    setSearchQueryAction.SearchQuery,
                    StringComparison.InvariantCultureIgnoreCase);
            }

            return inState with { SearchQuery = setSearchQueryAction.SearchQuery };
        }
    }
}
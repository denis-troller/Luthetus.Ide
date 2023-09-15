using Fluxor;
using Luthetus.Common.RazorLib.BackgroundTaskCase.BaseTypes;

namespace Luthetus.Ide.RazorLib.FolderExplorerCase.States;

public partial record FolderExplorerState
{
    private class Reducer
    {
        [ReducerMethod]
        public static FolderExplorerState ReduceSetFolderExplorerStateAction(
            FolderExplorerState inState,
            SetFolderExplorerAction inTask)
        {
            inTask.Sync.BackgroundTaskService.Enqueue(BackgroundTaskKey.NewKey(), ContinuousBackgroundTaskWorker.Queue.Key,
                "SetDotNetSolutionAsync",
                async () => await inTask.Sync.SetFolderExplorer(inTask));

            return inState;
        }
        
        [ReducerMethod]
        public static FolderExplorerState ReduceSetFolderExplorerTreeViewAction(
            FolderExplorerState inState,
            SetFolderExplorerTreeViewAction inTask)
        {
            inTask.Sync.BackgroundTaskService.Enqueue(BackgroundTaskKey.NewKey(), ContinuousBackgroundTaskWorker.Queue.Key,
                "SetDotNetSolutionAsync",
                async () => await inTask.Sync.SetFolderExplorerTreeView(inTask));

            return inState;
        }
        
        [ReducerMethod]
        public static FolderExplorerState ReduceWithAction(
            FolderExplorerState inState,
            WithAction withAction)
        {
            return withAction.WithFunc.Invoke(inState);
        }
    }
}
﻿using Fluxor;
using System.Collections.Immutable;
using Luthetus.TextEditor.RazorLib.Groups.States;
using Luthetus.TextEditor.RazorLib.Groups.Models;
using Luthetus.Common.RazorLib.Keys.Models;

namespace Luthetus.TextEditor.RazorLib.TextEditors.Models;

public partial interface ITextEditorService
{
    public interface IGroupApi
    {
        public void AddViewModel(Key<TextEditorGroup> textEditorGroupKey, Key<TextEditorViewModel> textEditorViewModelKey);
        public TextEditorGroup? FindOrDefault(Key<TextEditorGroup> textEditorGroupKey);
        public void Register(Key<TextEditorGroup> textEditorGroupKey);
        public void Dispose(Key<TextEditorGroup> textEditorGroupKey);
        public void RemoveViewModel(Key<TextEditorGroup> textEditorGroupKey, Key<TextEditorViewModel> textEditorViewModelKey);
        public void SetActiveViewModel(Key<TextEditorGroup> textEditorGroupKey, Key<TextEditorViewModel> textEditorViewModelKey);
    }

    public class GroupApi : IGroupApi
    {
        private readonly IDispatcher _dispatcher;
        private readonly ITextEditorService _textEditorService;

        public GroupApi(ITextEditorService textEditorService, IDispatcher dispatcher)
        {
            _textEditorService = textEditorService;
            _dispatcher = dispatcher;
        }

        public void SetActiveViewModel(Key<TextEditorGroup> textEditorGroupKey, Key<TextEditorViewModel> textEditorViewModelKey)
        {
            _dispatcher.Dispatch(new TextEditorGroupState.SetActiveViewModelOfGroupAction(
                textEditorGroupKey,
                textEditorViewModelKey));
        }

        public void RemoveViewModel(Key<TextEditorGroup> textEditorGroupKey, Key<TextEditorViewModel> textEditorViewModelKey)
        {
            _dispatcher.Dispatch(new TextEditorGroupState.RemoveViewModelFromGroupAction(
                textEditorGroupKey,
                textEditorViewModelKey));
        }

        public void Register(Key<TextEditorGroup> textEditorGroupKey)
        {
            var textEditorGroup = new TextEditorGroup(
                textEditorGroupKey,
                Key<TextEditorViewModel>.Empty,
                ImmutableList<Key<TextEditorViewModel>>.Empty);

            _dispatcher.Dispatch(new TextEditorGroupState.RegisterAction(textEditorGroup));
        }

        public void Dispose(Key<TextEditorGroup> textEditorGroupKey)
        {
            _dispatcher.Dispatch(new TextEditorGroupState.DisposeAction(textEditorGroupKey));
        }

        public TextEditorGroup? FindOrDefault(Key<TextEditorGroup> textEditorGroupKey)
        {
            return _textEditorService.GroupStateWrap.Value.GroupBag.FirstOrDefault(
                x => x.GroupKey == textEditorGroupKey);
        }

        public void AddViewModel(Key<TextEditorGroup> textEditorGroupKey, Key<TextEditorViewModel> textEditorViewModelKey)
        {
            _dispatcher.Dispatch(new TextEditorGroupState.AddViewModelToGroupAction(
                textEditorGroupKey,
                textEditorViewModelKey));
        }
    }
}
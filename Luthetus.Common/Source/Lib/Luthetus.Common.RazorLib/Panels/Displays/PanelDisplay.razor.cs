﻿using Fluxor;
using Fluxor.Blazor.Web.Components;
using Luthetus.Common.RazorLib.Dimensions.Models;
using Luthetus.Common.RazorLib.Drags.Displays;
using Luthetus.Common.RazorLib.Keys.Models;
using Luthetus.Common.RazorLib.Panels.States;
using Luthetus.Common.RazorLib.Panels.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Luthetus.Common.RazorLib.Panels.Displays;

public partial class PanelDisplay : FluxorComponent
{
    [Inject]
    private IState<PanelsState> PanelsStateWrap { get; set; } = null!;
    [Inject]
    private IDispatcher Dispatcher { get; set; } = null!;

    [Parameter, EditorRequired]
    public Key<PanelGroup> PanelGroupKey { get; set; } = Key<PanelGroup>.Empty;
    [Parameter, EditorRequired]
    public ElementDimensions AdjacentElementDimensions { get; set; } = null!;
    [Parameter, EditorRequired]
    public DimensionAttributeKind DimensionAttributeKind { get; set; }
    [Parameter, EditorRequired]
    public Func<Task> ReRenderSelfAndAdjacentElementDimensionsFunc { get; set; } = null!;

    [Parameter]
    public string CssClassString { get; set; } = null!;
    [Parameter]
    public RenderFragment? JustifyEndRenderFragment { get; set; } = null;

    public string DimensionAttributeModificationPurpose => $"take_size_of_adjacent_hidden_panel_{PanelGroupKey}";

    private string PanelPositionCssClass => GetPanelPositionCssClass();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
            await PassAlongSizeIfHiddenAsync();

        await base.OnAfterRenderAsync(firstRender);
    }

    private string GetPanelPositionCssClass()
    {
        var position = string.Empty;

        if (PanelFacts.LeftPanelRecordKey == PanelGroupKey)
            position = "left";
        else if (PanelFacts.RightPanelRecordKey == PanelGroupKey)
            position = "right";
        else if (PanelFacts.BottomPanelRecordKey == PanelGroupKey)
            position = "bottom";

        return $"luth_ide_panel_{position}";
    }

    private async Task PassAlongSizeIfHiddenAsync()
    {
        var panelsState = PanelsStateWrap.Value;

        var panelRecord = panelsState.PanelGroupBag.FirstOrDefault(x => x.Key == PanelGroupKey);

        if (panelRecord is not null)
        {
            var activePanelTab = panelRecord.TabBag.FirstOrDefault(
                x => x.Key == panelRecord.ActiveTabKey);

            var adjacentElementSizeDimensionAttribute = AdjacentElementDimensions.DimensionAttributeBag.First(
                x => x.DimensionAttributeKind == DimensionAttributeKind);

            var indexOfPreviousPassAlong = adjacentElementSizeDimensionAttribute.DimensionUnitBag.FindIndex(
                x => x.Purpose == DimensionAttributeModificationPurpose);

            if (activePanelTab is null && indexOfPreviousPassAlong == -1)
            {
                var panelRecordSizeDimensionsAttribute = panelRecord.ElementDimensions.DimensionAttributeBag.First(
                    x => x.DimensionAttributeKind == DimensionAttributeKind);

                var panelRecordPercentageSize = panelRecordSizeDimensionsAttribute.DimensionUnitBag.First(
                    x => x.DimensionUnitKind == DimensionUnitKind.Percentage);

                adjacentElementSizeDimensionAttribute.DimensionUnitBag.Add(new DimensionUnit
                {
                    Value = panelRecordPercentageSize.Value,
                    DimensionUnitKind = panelRecordPercentageSize.DimensionUnitKind,
                    DimensionOperatorKind = DimensionOperatorKind.Add,
                    Purpose = DimensionAttributeModificationPurpose
                });

                await ReRenderSelfAndAdjacentElementDimensionsFunc.Invoke();
            }
            else if (activePanelTab is not null && indexOfPreviousPassAlong != -1)
            {
                adjacentElementSizeDimensionAttribute.DimensionUnitBag.RemoveAt(indexOfPreviousPassAlong);

                await ReRenderSelfAndAdjacentElementDimensionsFunc.Invoke();
            }
        }
    }

    private string GetElementDimensionsStyleString(PanelGroup? panelRecord, PanelTab? activePanelTab)
    {
        if (activePanelTab is null)
        {
            return "calc(" +
                   "var(--luth_ide_panel-tabs-font-size)" +
                   " + var(--luth_ide_panel-tabs-margin)" +
                   " + var(--luth_ide_panel-tabs-bug-are-not-aligning-need-to-fix-todo))";
        }

        return panelRecord?.ElementDimensions.StyleString ?? string.Empty;
    }

    private Task TopDropzoneOnMouseUp(MouseEventArgs mouseEventArgs)
    {
        var panelsState = PanelsStateWrap.Value;

        var panelRecord = panelsState.PanelGroupBag.FirstOrDefault(x => x.Key == PanelGroupKey);

        if (panelRecord is null)
            return Task.CompletedTask;

        var panelDragEventArgs = panelsState.DragEventArgs;

        if (panelDragEventArgs is not null)
        {
            Dispatcher.Dispatch(new PanelsState.DisposePanelTabAction(
                panelDragEventArgs.Value.PanelGroup.Key,
                panelDragEventArgs.Value.PanelTab.Key));

            Dispatcher.Dispatch(new PanelsState.RegisterPanelTabAction(
                panelRecord.Key,
                panelDragEventArgs.Value.PanelTab,
                true));

            Dispatcher.Dispatch(new PanelsState.SetDragEventArgsAction(null));

            Dispatcher.Dispatch(new DragState.WithAction(inState => inState with
            {
                ShouldDisplay = false,
                MouseEventArgs = null
            }));
        }

        return Task.CompletedTask;
    }

    private Task BottomDropzoneOnMouseUp(MouseEventArgs mouseEventArgs)
    {
        var panelsState = PanelsStateWrap.Value;

        var panelRecord = panelsState.PanelGroupBag.FirstOrDefault(x => x.Key == PanelGroupKey);

        if (panelRecord is null)
            return Task.CompletedTask;

        var panelDragEventArgs = panelsState.DragEventArgs;

        if (panelDragEventArgs is not null)
        {
            Dispatcher.Dispatch(new PanelsState.DisposePanelTabAction(
                panelDragEventArgs.Value.PanelGroup.Key,
                panelDragEventArgs.Value.PanelTab.Key));

            Dispatcher.Dispatch(new PanelsState.RegisterPanelTabAction(
                panelRecord.Key,
                panelDragEventArgs.Value.PanelTab,
                false));

            Dispatcher.Dispatch(new PanelsState.SetDragEventArgsAction(null));

            Dispatcher.Dispatch(new DragState.WithAction(inState => inState with
            {
                ShouldDisplay = false,
                MouseEventArgs = null,
            }));
        }

        return Task.CompletedTask;
    }
}
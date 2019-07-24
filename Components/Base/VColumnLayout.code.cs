using System.Threading.Tasks;
using Frontend.Tazor.Codes;
using Frontend.Tazor.Enums;
using Microsoft.AspNetCore.Components;

namespace Frontend.Tazor.Components.Base {
    public class VColumnLayoutCode: VItem {
        #region Fields
    [Parameter] protected RenderFragment ChildContent { get; set; }
    [Parameter] protected string Spacing { get; set; }
    #endregion

    #region VerticalAlignItems
    private string _justifyContent; //verticalmente
    [Parameter]
    VerticalAlignItems VerticalAlignItems {
        get {
            switch (_justifyContent) {
                case "flex-start":
                    return VerticalAlignItems.Top;
                case "flex-end":
                    return VerticalAlignItems.Bottom;
                case "center":
                    return VerticalAlignItems.Center;
            }
            return VerticalAlignItems.Top;
        }
        set {
            switch (value) {
                case VerticalAlignItems.Top:
                    _justifyContent = "flex-start";
                    break;
                case VerticalAlignItems.Center:
                    _justifyContent = "center";
                    break;
                case VerticalAlignItems.Bottom:
                    _justifyContent = "flex-end";
                    break;
                case VerticalAlignItems.SpaceAround:
                    _justifyContent = "space-around";
                    break;
                case VerticalAlignItems.SpaceBetween:
                    _justifyContent = "space-between";
                    break;
            }
            UpdateStyle(CssHelper.JustifyContent, _justifyContent);
        }
    }
    #endregion

    #region HorizontalAlignItems
    private string _alignItems; //horizontalmente
    [Parameter]
    HorizontalAlignItems HorizontalAlignItems {
        get {
            switch (_alignItems) {
                case "flex-start":
                    return HorizontalAlignItems.Left;
                case "flex-end":
                    return HorizontalAlignItems.Right;
                case "center":
                    return HorizontalAlignItems.Center;
            }
            return HorizontalAlignItems.Left;
        }
        set {
            switch (value) {
                case HorizontalAlignItems.Left:
                    _alignItems = "flex-start";
                    break;
                case HorizontalAlignItems.Center:
                    _alignItems = "center";
                    break;
                case HorizontalAlignItems.Right:
                    _alignItems = "flex-end";
                    break;
                case HorizontalAlignItems.SpaceAround:
                    _alignItems = "space-around";
                    break;
                case HorizontalAlignItems.SpaceBetween:
                    _alignItems = "space-between";
                    break;
            }
            UpdateStyle(CssHelper.AlignItems, _alignItems);
        }
    }
    #endregion

    //=================================FUNCTIONS BELOW=================================

    #region OnParametersSet
    protected override async Task OnParametersSetAsync() {
        await base.OnParametersSetAsync();
        StyleDict[CssHelper.Display]= "flex";
        StyleDict[CssHelper.FlexDirection]= "column";
        Style = Helper.ConvertDictToCssStyle(StyleDict);
        //base.AddValueToStyleDictIfNotEmpty(CssHelper.JustifyContent, _justifyContent);
        //base.AddValueToStyleDictIfNotEmpty(CssHelper.AlignItems, _alignItems);
        //base.AddValueToStyleDictIfNotEmpty(CssHelper.BackgroundColor, BackgroundColor);
        //Console.WriteLine("height" + Height);
        //if (!string.IsNullOrEmpty(Height) && ))StyleDict.Add(CssHelper.Height, Height);
    }
    #endregion
    }
}
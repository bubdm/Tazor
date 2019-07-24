using System.Threading.Tasks;
using Frontend.Tazor.Codes;
using Frontend.Tazor.Enums;
using Microsoft.AspNetCore.Components;

namespace Frontend.Tazor.Components.Base {
    public class VRowLayoutCode : VItem {
        #region Fields
        [Parameter] protected RenderFragment ChildContent { get; set; }
        #endregion

        #region HorizontalAlignItems
        private string _justifyContent; //horizontalmente

        [Parameter]
        protected HorizontalAlignItems HorizontalAlignItems {
            get {
                switch (_justifyContent) {
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
                        _justifyContent = "flex-start";
                        break;
                    case HorizontalAlignItems.Center:
                        _justifyContent = "center";
                        break;
                    case HorizontalAlignItems.Right:
                        _justifyContent = "flex-end";
                        break;
                    case HorizontalAlignItems.SpaceAround:
                        _justifyContent = "space-around";
                        break;
                    case HorizontalAlignItems.SpaceBetween:
                        _justifyContent = "space-between";
                        break;
                }
                UpdateStyle(CssHelper.JustifyContent, _justifyContent);
            }
        }
        #endregion

        #region VerticalAlignItems
        private string _alignItems; //verticalmente

        [Parameter]
        protected VerticalAlignItems VerticalAlignItems {
            get {
                switch (_alignItems) {
                    case "flex-start":
                        return VerticalAlignItems.Top;
                    case "flex-end":
                        return VerticalAlignItems.Bottom;
                    case "center":
                        return VerticalAlignItems.Center;
                    case "stretch":
                        return VerticalAlignItems.Stretch;
                }
                return VerticalAlignItems.Top;
            }
            set {
                switch (value) {
                    case VerticalAlignItems.Top:
                        _alignItems = "flex-start";
                        break;
                    case VerticalAlignItems.Center:
                        _alignItems = "center";
                        break;
                    case VerticalAlignItems.Bottom:
                        _alignItems = "flex-end";
                        break;
                    case VerticalAlignItems.SpaceAround:
                        _alignItems = "space-around";
                        break;
                    case VerticalAlignItems.SpaceBetween:
                        _alignItems = "space-between";
                        break;
                    case VerticalAlignItems.Stretch:
                        _alignItems = "stretch";
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
            StyleDict[CssHelper.Display] = "flex";
            StyleDict[CssHelper.FlexDirection] = "row";
            Style = Helper.ConvertDictToCssStyle(StyleDict);
        }
        #endregion
    }
}
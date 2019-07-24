using System;
using System.Threading.Tasks;
using Frontend.Tazor.Codes;
using Frontend.Tazor.Enums;
using Microsoft.AspNetCore.Components;

namespace Frontend.Tazor.Components.Base {
    public class VRectangleCode : VItem {
        #region BorderColor
        private string _borderColor;

        [Parameter]
        protected string BorderColor {
            get => _borderColor;
            set {
                if (_borderColor == value) return;
                _borderColor = value;
                UpdateStyle(CssHelper.BorderColor, _borderColor);
            }
        }
        #endregion

        #region BorderWidth
        private string _borderWidth;

        [Parameter]
        protected string BorderWidth {
            get => _borderWidth;
            set {
                if (_borderWidth == value) return;
                _borderWidth = value;
                UpdateStyle(CssHelper.BorderWidth, _borderWidth);
            }
        }
        #endregion

        #region BorderRadius
        private string _borderRadius;

        [Parameter]
        protected string BorderRadius {
            get => _borderRadius;
            set {
                if (_borderRadius == value) return;
                _borderRadius = value;
                UpdateStyle(CssHelper.BorderRadius, _borderRadius);
            }
        }
        #endregion

        #region Color
        [Parameter]
        protected string Color {
            get => base.BackgroundColor;
            set => base.BackgroundColor = value;
        }
        #endregion

        #region BorderStyle
        private BorderStyle _borderStyle = BorderStyle.None;

        [Parameter]
        protected BorderStyle BorderStyle {
            get => _borderStyle;
            set {
                if (_borderStyle == value) return;
                _borderStyle = value;
                UpdateStyle(CssHelper.BorderStyle, _borderStyle.ToString());
            }
        }
        #endregion

        #region Fields
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [Parameter] protected Action<VRectangleCode> MouseClicked { get; set; }
        #endregion

        //=================================FUNCTIONS BELOW=================================

        #region OnInit
        protected override void OnInit() {
            base.OnInit();
            Console.WriteLine($"carregou rect: {Height}");
            //Console.WriteLine($"(VRectangle)OnInit. Altura:{Height}. Largura{Width}");
        }
        #endregion

        #region OnParametersSet
        protected override async Task OnParametersSetAsync() {
            await base.OnParametersSetAsync();
        }
        #endregion

        #region MouseClicked
        protected void _MouseClicked() {
            MouseClicked?.Invoke(this);
        }
        #endregion
    }
}
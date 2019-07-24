#region Imports
using System.Threading.Tasks;
using Frontend.Tazor.Components.Base;
using Frontend.Tazor.Enums;
using Microsoft.AspNetCore.Components;
#endregion

namespace Frontend.Tazor.Components.Material {
    public class VMatButtonCode: VButton {

        #region Fields
        [Parameter] MaterialButtonType MaterialButtonType { get; set; } = MaterialButtonType.RaisedButton;
        [Parameter] bool RippleEnabled { get; set; } = true;

        private string _colorType = "primary";
        [Parameter]
        ColorType ColorType {
            get {
                switch (_colorType) {
                    case "primary":
                        return ColorType.Primary;
                    case "secondary":
                        return ColorType.Secondary;
                    case "danger":
                        return ColorType.Danger;
                    case "info":
                        return ColorType.Info;
                    case "success":
                        return ColorType.Success;
                    case "warning":
                        return ColorType.Warning;
                    case "dark":
                        return ColorType.Dark;
                    case "light":
                        return ColorType.Light;
                    case "link":
                        return ColorType.Link;
                }
                return ColorType.Primary;
            }
            set => _colorType = value.ToString().ToLower();
        }
        #endregion

        #region OnInit
        protected override void OnInit() {
            base.OnInit();
            cssClass = "btn btn-primary";
            //StyleDict[CssHelper.BackgroundColor] = "#d4edda";
        }
        #endregion


        #region OnParametersSet
        protected override async Task OnParametersSetAsync() {
            await base.OnParametersSetAsync();
            string completeCssClass = "btn btn-"+_colorType;
            //if (RippleEnabled) completeCssClass += " mdl-js-ripple-effect";
            switch (MaterialButtonType) {
                case MaterialButtonType.ColoredFab:
                    //completeCssClass += " mdl-button--fab mdl-button--colored";
                    IconCssClass = "material-icons";
                    break;
                case MaterialButtonType.ColoredRaisedButton:
                    //completeCssClass += " mdl-button--raised mdl-button--colored";
                    break;
            }
            base.cssClass = completeCssClass;
        }
        #endregion
    }
}
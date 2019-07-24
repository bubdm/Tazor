using Frontend.Tazor.Enums;
using Microsoft.AspNetCore.Components;

namespace Frontend.Tazor.Components.Material {
    public class VMatAlertCode :ComponentBase{
        [Parameter]protected string Text { get; set; }
        [Parameter] protected bool Visible { get; set; } = false;
        protected string alertClass = "alert alert-danger alert-dismissible fade show";

        protected ColorType _colorType = ColorType.Danger;
        [Parameter]
        protected ColorType ColorType {
            get => _colorType;
            set {
                _colorType = value;
                alertClass = "alert alert-" + _colorType.ToString().ToLower() + " alert-dismissible fade show";
                StateHasChanged();
            }
        }

        public void DisplayError(string msg) {
            Text = msg;
            Visible = true;
            StateHasChanged();
        }
        

        public void Hide() {
            Visible = false;
            StateHasChanged();
        }
    }
}
#region Imports
using System;
using System.Globalization;
using System.Threading.Tasks;
using Frontend.Codes;
using Frontend.Tazor.Codes;
using Frontend.Tazor.Components.Base;
using Frontend.Tazor.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
#endregion

namespace Frontend.Tazor.Components.Material {
    public class VMatDatePickerCode : VItem {
        [Parameter] protected string FloatingLabelText { get; set; }
        [Parameter] protected string PlaceHolderText { get; set; }
        [Inject] private IJSRuntime JSRuntime { get; set; }
        [Parameter] protected Action<DateTime?> DateChanged { get; set; }
        [Parameter] protected DateTime? Date { get; set; }
        protected string Text;
        protected DateTime? dt;
        protected string dateFormat = "dd/MM/yyyy";
        
        
        #region OnParametersSet
        protected override async Task OnParametersSetAsync() {
            await base.OnParametersSetAsync();
            if (string.IsNullOrEmpty(Width)) UpdateStyle(CssHelper.Width,"100%");//textfield tem 100% width por default
        }
        #endregion
        
        #region OnAfterRenderAsync
        protected override async Task OnAfterRenderAsync() {
            await base.OnAfterRenderAsync();
            await new Javascript(JSRuntime).SetupFloatingLabels();
        }
        #endregion

        protected async void OnInput() {
            Console.WriteLine("esta no input");
            await new Javascript(JSRuntime).DisplayDatePicker(this,ID);
        }

        [JSInvokable]
        public void SetValueFromJavascript(string value) {
            bool ok = System.DateTime.TryParseExact(value, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt);
            Console.WriteLine("vai setar valor:" + value+". "+ok.ToString());
            Date = dt;
            if(ok)DateChanged?.Invoke(dt);
            else DateChanged?.Invoke(null);
        }
    }
}
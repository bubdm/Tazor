using System;
using System.Threading.Tasks;
using Frontend.Tazor.Codes;
using Frontend.Tazor.Components.Base;
using Microsoft.AspNetCore.Components;

namespace Frontend.Tazor.Components.Material {
    public class VMatTextAreaCode : VItem {
        [Parameter] protected string PlaceHolderText { get; set; }
        [Parameter] protected string FloatingLabelText { get; set; }
        [Parameter] protected Action<string> TextChanged { get; set; }
        [Parameter] protected string Text { get; set; }
        
        
        #region OnParametersSet
        protected override async Task OnParametersSetAsync() {
            await base.OnParametersSetAsync();
            if (string.IsNullOrEmpty(Width)) {//textarea tem 100% width por default
                UpdateStyle(CssHelper.Width,"100%");
            }
        }
        #endregion
        
        #region HandleTextChange
        protected void HangleTextChange(UIChangeEventArgs evt) {
            Text = (string) evt.Value;
            Console.WriteLine($"{ID} alterou texto:" + Text);
            TextChanged?.Invoke(Text);
        }
        #endregion
    }
}
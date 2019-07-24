using System;
using System.Threading.Tasks;
using DotnetBase.Codes;
using Frontend.Codes;
using Frontend.Tazor.Codes;
using Frontend.Tazor.Components.Base;
using Frontend.Tazor.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Frontend.Tazor.Components.Material {
    public class VMatNumberFieldCode<ValueType> : VItem {
        [Parameter] protected string PlaceHolderText { get; set; }
        [Parameter] protected string FloatingLabelText { get; set; }
        [Inject] private IJSRuntime JSRuntime { get; set; }
        [Parameter] protected string UnderText { get; set; }
        [Parameter] protected Action<ValueType> ValueChanged { get; set; }
        protected string text { get; set; }


        [Parameter]
        protected ValueType Value {
            get {
                return ConverterHelper.TryChangeType<ValueType>( text, out var value ) ? value : default;
            }
            set { text = value.ToString(); }
        }

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

        #region HandleTextChange
        protected void HangleTextChange(UIChangeEventArgs evt) {
            text = (string) evt.Value;
            Console.WriteLine($"{ID} alterou texto:" + text);
            Value = ConverterHelper.TryChangeType<ValueType>( text, out var value ) ? value : default;
            Console.WriteLine($"setando numero {Value}");
            ValueChanged?.Invoke(Value);
        }
        #endregion
    }
}
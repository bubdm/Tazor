#region Imports
using System;
using System.Globalization;
using System.Threading.Tasks;
using DotnetBase.Codes;
using Frontend.Codes;
using Frontend.Tazor.Codes;
using Frontend.Tazor.Components.Base;
using Frontend.Tazor.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
#endregion

namespace Frontend.Tazor.Components.Material {
    public class VMatTextFieldCode: VItem {
        #region Fields
        [Parameter] protected string Text { get; set; }
        [Inject] private IJSRuntime JSRuntime { get; set; }
        [Parameter] protected Action<string> TextChanged { get; set; }
        [Parameter] protected Action<DateTime?> DateTimeChanged { get; set; }
        [Parameter] protected Action<TimeSpan?> TimeChanged { get; set; }
        [Parameter] protected Action<Decimal?> DecimalValueChanged { get; set; }
        [Parameter] protected string PlaceHolderText { get; set; }
        [Parameter] protected string FloatingLabelText { get; set; }
        [Parameter] protected string UnderText { get; set; }
        [Parameter] protected string InputCssClass { get; set; }
        [Parameter] protected string LabelCssClass { get; set; }
        [Parameter] protected string Type { get; set; } = "text";
        [Parameter] protected bool ReadOnly { get; set; } = false;
        #endregion

        #region DateTime
        [Parameter]
        protected DateTime? DateTime {
            get {
                if (string.IsNullOrEmpty(Text)) return null;
                if (this.InputType == InputType.Date) {
                    bool ok = System.DateTime.TryParseExact(Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt);
                    if (ok) return dt;
                }
                return null;
            }
            set {
                if (this.InputType == InputType.Date) {
                    if (value == null) Text = "";
                    else Text = value.Value.ToString("dd/MM/yyyy");
                    Console.WriteLine("setando data:" + Text);
                }
            }
        }
        #endregion

        #region TimeSpan
        [Parameter]
        protected TimeSpan? Time {
            get {
                if (string.IsNullOrEmpty(Text)) return null;
                if (this.InputType == InputType.Time) {
                    bool ok = System.TimeSpan.TryParseExact(Text, "HH:mm", CultureInfo.InvariantCulture, out TimeSpan dt);
                    if (ok) return dt;
                }
                return null;
            }
            set {
                if (this.InputType == InputType.Time) {
                    if (!value.HasValue) Text = "";
                     else {
                        Console.WriteLine("setando hora:" + value.ToString());
                        Text = value.Value.ToString();
                    }
                }
            }
        }
        #endregion
        
        #region DecimalValue
        [Parameter]
        protected Decimal? DecimalValue {
            get {
                if (string.IsNullOrEmpty(Text)) return null;
                if (decimal.TryParse(Text, out decimal dc)) return dc;
                else return null;
            }
            set {
                if (this.InputType != InputType.Number) return;
                Text = value?.ToString();
            }
        }
        #endregion

        #region InputType
        [Parameter]
        InputType InputType {
            get {
                switch (Type) {
                    case "text":
                        return InputType.Text;
                    case "time":
                        return InputType.Time;
                    case "date":
                        return InputType.Date;
                    case "number":
                        return InputType.Number;
                }
                return InputType.Text;
            }
            set {
                switch (value) {
                    case InputType.Text:
                        Type = "text";
                        break;
                    case InputType.Time:
                        Type = "time";
                        break;
                    case InputType.Date:
                        Type = "date";
                        break;
                    case InputType.Number:
                        Type = "number";
                        break;
                }
            }
        }
        #endregion

        #region HandleTextChange
        protected void HangleTextChange(UIChangeEventArgs evt) {
            Text = (string) evt.Value;
            Console.WriteLine($"{ID} alterou texto:" + Text);
            TextChanged?.Invoke(Text);
            switch (InputType) {
                case InputType.Date: {
                    bool ok = System.DateTime.TryParseExact(Text, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt);
                    DateTime = dt;
                    if (ok) {
                        DateTimeChanged?.Invoke(dt);
                    }
                    break;
                }
                case InputType.Time: {
                    bool ok = System.TimeSpan.TryParse(Text, out TimeSpan tm);
                    Time = tm;
                    if (ok) TimeChanged?.Invoke(tm);
                    else TimeChanged?.Invoke(null);
                    break;
                }
                case InputType.Number: {
                    bool ok = decimal.TryParse(Text, out decimal dc);
                    DecimalValue = dc;
                    Console.WriteLine($"alterou numero {dc.ToString()}");
                    if (ok) DecimalValueChanged?.Invoke(dc);
                    else DecimalValueChanged?.Invoke(null);
                    break;
                }    
            }
        }
        #endregion

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
    }
}
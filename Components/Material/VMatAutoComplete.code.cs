using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Frontend.Codes;
using Frontend.Tazor.Codes;
using Frontend.Tazor.Components.Base;
using Frontend.Tazor.Entities;
using Frontend.Tazor.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Frontend.Tazor.Components.Material {
    public class VMatAutoCompleteCode : VItem {
        
        [Parameter] protected string Text { get; set; }
        [Parameter] protected string PlaceHolderText { get; set; }
        [Parameter] protected string FloatingLabelText { get; set; }
        [Parameter] protected string UnderText { get; set; }
        [Inject] private IJSRuntime JSRuntime { get; set; }
        protected  List<EComboBoxItem> ComboItems { get; set; }=new List<EComboBoxItem>();
        [Parameter] protected Func<UIChangeEventArgs, Task> OnTextChanged { get; set; }


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
            Text = (string) evt.Value;
            OnTextChanged.Invoke(evt);
        }
        #endregion

        public void SetComboItems(List<EComboBoxItem> items) {
            if (!items.Any()) return;
            //Console.WriteLine("setou combo itens:"+items.Count);
            ComboItems = items;
            StateHasChanged();
        }

        public string GetSelectedValue() {
            //Console.WriteLine("procurando item.ComboItemsCount:" +ComboItems.Count);
            foreach (var item in ComboItems) {
                if (item.Text == Text) {
                    return item.Value;
                }
            }
            return null;
        }

        public void SelectValue(List<EComboBoxItem> items, EComboBoxItem selectedItem) {
            ComboItems = items;
            Text = selectedItem.Text;
            //Console.WriteLine("selecinounando" +Text);
            StateHasChanged();
        }
    }
}
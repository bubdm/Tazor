#region Imports
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotnetBase.Codes;
using Frontend.Tazor.Codes;
using Frontend.Tazor.Components.Base;
using Frontend.Tazor.Entities;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
#endregion

namespace Frontend.Tazor.Components.Material {
    public class VMatComboBoxCode<ItemType, ValueType>: VItem {

        #region Fields
        //usados exclusivamente com tipo do usuário abaixo
        [Parameter] protected IList<ItemType> Items { get; set; }
        [Parameter] protected string ValueField { get; set; }
        [Parameter] protected string TextField { get; set; }
        [Parameter] protected bool IsFirstItemNull { get; set; } = true;        
        //usados exclusivamente com EComboBoxItem abaixo
        [Parameter] protected List<EComboBoxItem> ComboItems { get; set; }
        //usados por ambos abaixo
        [Parameter] protected string FloatingLabelText { get; set; }
        [Parameter] protected Action<object> OnItemChanged { get; set; }
        [Parameter] protected ValueType SelectedValue { get; set; }
        [Parameter] protected Action<ValueType> SelectedValueChanged { get; set; }
        
        #endregion

        #region OnParametersSet
        protected override async Task OnParametersSetAsync() {
            await base.OnParametersSetAsync();
            if (string.IsNullOrEmpty(Width)) {//combo tem 100% width por default
                UpdateStyle(CssHelper.Width,"100%");
            }
        }
        #endregion

        #region OnSelectChanged
        protected void OnSelectChanged(UIChangeEventArgs e) {
            //nao sei pq, mas nao funciona se eu deixo o value=@eItem
            Console.WriteLine("alterou combo item");
            SelectedValue = ConverterHelper.TryChangeType<ValueType>( e.Value, out var value ) ? value : default;
            SelectedValueChanged?.Invoke(SelectedValue);
            if (IsFirstItemNull && e.Value == null) {
                OnItemChanged?.Invoke(null);
                return;
            }
            if (Items != null) {
                foreach (object obj in Items) {
                    var valor = ReflectionHelper.GetPropertyValue(obj, ValueField);
                    if (!valor.Equals(e.Value)) continue;
                    OnItemChanged?.Invoke(obj);
                    break;
                }
            } else if (ComboItems != null) {
                foreach (EComboBoxItem item in ComboItems) {
                    if (item.Value != e.Value as string) continue;
                    OnItemChanged?.Invoke(item);
                    break;
                }
            }
        }
        #endregion
    }
}
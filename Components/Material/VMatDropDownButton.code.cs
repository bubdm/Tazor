using System.Collections.Generic;
using Frontend.Tazor.Components.Base;
using Frontend.Tazor.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Tazor.Components.Material {
    public class VMatDropDownButtonCode : VItem {
        [Parameter] protected List<EComboBoxItem> Items { get; set; }
        [Parameter] protected string Text { get; set; }
    }
}
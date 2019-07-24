using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Frontend.Tazor.Components.Base;
using Frontend.Tazor.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Tazor.Components.Material {
    public class VMatToolBarCode :VItem{
        [Parameter] protected string Title { get; set; }
        [Parameter] protected List<EToolbarItem> Items { get; set; } = new List<EToolbarItem>();
        [Parameter] protected Func<EToolbarItem, Task> OnItemClicked { get; set; }
        [Parameter] protected EventCallback<EToolbarItem> OnItemClick { get; set; }//usar esse


        protected async Task OnToolButtonClicked(EToolbarItem eToolbarItem) {
            if (OnItemClicked == null) return;
            await OnItemClicked?.Invoke(eToolbarItem);
        }
    }
}
#region Imports
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frontend.Tazor.Components.Base;
using Frontend.Tazor.Entities;
using Microsoft.AspNetCore.Components;
#endregion

namespace Frontend.Tazor.Components.Material {
    public class VMatTabViewCode :VItem{
        [Parameter] protected List<ETabTitle> TabBarTitles { get; set; } = new List<ETabTitle>();
        [Parameter] protected RenderFragment ChildContent { get; set; }
        protected string ActiveTabPanelID { get; set; }
        

        #region OnParametersSet
        protected override async Task OnParametersSetAsync() {
            await base.OnParametersSetAsync();
            if (TabBarTitles.Any()) {
                foreach (ETabTitle eTabTitle in TabBarTitles) {
                    if (eTabTitle.IsActive) ActiveTabPanelID = eTabTitle.TabPanelID;
                }
            }
        }
        #endregion
        
        protected  void OnTabClicked(ETabTitle eTabTitle) {
           Console.WriteLine("clicou no tab "+eTabTitle.Text);
           ActiveTabPanelID = eTabTitle.TabPanelID;
        }
    }
}
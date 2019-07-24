#region Imports
using System.Threading.Tasks;
using Frontend.Tazor.Codes;
using Frontend.Tazor.Components.Base;
using Microsoft.AspNetCore.Components;
#endregion

namespace Frontend.Tazor.Components.Material {
    public class VMatCardCode :VItem{

        #region Fields
        [Parameter] protected string Title { get; set; }
        [Parameter] protected string SubTitle { get; set; }   
        [Parameter] protected RenderFragment ChildContent { get; set; }
        #endregion
        
        
        #region OnParametersSet
        protected override async Task OnParametersSetAsync() {
            await base.OnParametersSetAsync();
            if (string.IsNullOrEmpty(Width)) UpdateStyle(CssHelper.Width,"100%");//matcard tem 100% width por default
        }
        #endregion
    }
}
using Frontend.Tazor.Components.Base;
using Microsoft.AspNetCore.Components;

namespace Frontend.Tazor.Components.Material {
    public class VMatButtonGroupCode : VItem {
        [Parameter] protected RenderFragment ChildContent { get; set; }        
    }
}
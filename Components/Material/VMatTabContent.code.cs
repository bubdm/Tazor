using Frontend.Tazor.Components.Base;
using Frontend.Tazor.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Tazor.Components.Material {
    public class VMatTabContentCode:ComponentBase {
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [Parameter] protected  string ID { get; set; }
        [CascadingParameter] protected  string ActiveTabPanelID { get; set; }
    }
}
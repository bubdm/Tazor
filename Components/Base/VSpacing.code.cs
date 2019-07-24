using Frontend.Tazor.Codes;
using Microsoft.AspNetCore.Components;

namespace Frontend.Tazor.Components.Base {
    public class VSpacingCode:VItem {

        //=================================FUNCTIONS BELOW=================================

        #region OnParametersSet
        protected override void OnParametersSet() {
            base.OnParametersSet();
            Style = Helper.ConvertDictToCssStyle(StyleDict);
        }
        #endregion
    }
}
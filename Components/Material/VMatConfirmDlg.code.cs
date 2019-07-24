using System;
using Frontend.Tazor.Components.Base;
using Microsoft.AspNetCore.Components;

namespace Frontend.Tazor.Components.Material {
    public class VMatConfirmDlgCode : VItem {
        #region Fields
        [Parameter] protected  string Title { get; set; }
        [Parameter] protected string CancelText { get; set; } = "Cancel";
        [Parameter] protected string ConfirmText { get; set; } = "OK";
        [Parameter] protected Action OnConfirmClicked { get; set; }
        [Parameter] protected bool Visible { get; set; }
        #endregion


        protected void OnBtnConfirmClicked() {
            OnConfirmClicked?.Invoke();
        }
    }
}
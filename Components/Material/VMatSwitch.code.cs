using System;
using Frontend.Tazor.Components.Base;
using Microsoft.AspNetCore.Components;

namespace Frontend.Tazor.Components.Material {
    public class VMatSwitchCode : VItem {
        #region Fields
        [Parameter] protected string Text { get; set; }
        [Parameter] protected bool Checked { get; set; }
        [Parameter] protected Action<bool> CheckedChanged { get; set; }
        #endregion

        protected void CheckedChangedHandled(UIChangeEventArgs e) {
            Console.WriteLine("alterou o status do check"+ e.Value?.ToString().ToLowerInvariant());
            Checked = e.Value?.ToString().ToLowerInvariant() == ("true");
            CheckedChanged?.Invoke(Checked);
        }
    }
}
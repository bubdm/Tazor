#region Imports
using System.Threading.Tasks;
using Frontend.Tazor.Codes;
using Microsoft.AspNetCore.Components;
#endregion

namespace Frontend.Tazor.Components.Base {
    public class VGridLayoutCode :VItem {

        #region Fields
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [Parameter] protected string Columns { get; set; }
        [Parameter] protected string Rows { get; set; }
        [Parameter] protected string GridAutoFlow { get; set; } = "row";//significa que vai colocar os itens em cada coluna. Qdo acabar as colunas, coloca na linha de baixo.
        [Parameter] protected string ColumnGap { get; set; }//espaçamento entre colunas. 
        [Parameter] protected string RowGap { get; set; }//espaçamento entre linhas
        #endregion
        
        #region OnParametersSet
        protected override async Task OnParametersSetAsync() {
            await base.OnParametersSetAsync();
            StyleDict[CssHelper.Display] = "grid";
            StyleDict[CssHelper.GridAutoFlow] = GridAutoFlow;
            if (!string.IsNullOrEmpty(ColumnGap)) StyleDict[CssHelper.GridColumnGap] = ColumnGap;
            if (!string.IsNullOrEmpty(RowGap)) StyleDict[CssHelper.GridRowGap] = RowGap;
            if (!string.IsNullOrEmpty(Width)) StyleDict[CssHelper.Width] = "100%";
            if (!string.IsNullOrEmpty(Columns)) StyleDict[CssHelper.GridTemplateColumns] = Columns;
            if (!string.IsNullOrEmpty(Rows)) StyleDict[CssHelper.GridTemplateRows] = Rows;
            Style = Helper.ConvertDictToCssStyle(StyleDict);
        }
        #endregion
    }
}
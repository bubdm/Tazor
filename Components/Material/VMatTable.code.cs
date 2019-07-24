#region Imports
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DotnetBase.Codes;
using Frontend.Codes;
using Frontend.Tazor.Codes;
using Frontend.Tazor.Components.Base;
using Frontend.Tazor.Entities;
using Microsoft.AspNetCore.Components;
#endregion

namespace Frontend.Tazor.Components.Material {
    public class VMatTableCode:VItem {
        #region Fields
        [Parameter] protected List<ETableColumn> Columns { get; set; } = new List<ETableColumn>();
        //[Parameter] protected List<ETableRow> Rows { get; set; } = new List<ETableRow>();
        [Parameter] protected List<object> Rows { get; set; } = new List<object>();
        [Parameter] protected Int64 Count { get; set; }
        [Parameter] protected int RowsPerPage { get; set; } = 50;
        [Parameter] protected Action<int> RowsPerPageChanged { get; set; }
        [Parameter] protected Action<int> OnPageChanged { get; set; }
        [Parameter] protected bool Bordered { get; set; }
        [Parameter] protected bool DisplayInCard { get; set; } = true;
        protected int currentPage = 0;
        protected string previousButtonCssClass = "btn btn-outline disabled";
        protected string nextButtonCssClass="btn btn-outline";
        [Parameter] protected bool PagerVisible { get; set; } = true;
        protected string parentDivClass = "";
        protected string tableClass = "table table-hover table-sm mb-0";
        #endregion

        protected override void OnInit() {  
            base.OnInit();
        }

        #region OnParametersSet
        protected override async Task OnParametersSetAsync() {
            await base.OnParametersSetAsync();
            if(((currentPage*RowsPerPage)+Rows.Count)>=Count)nextButtonCssClass="btn btn-outline disabled";
            else nextButtonCssClass="btn btn-outline";
            if (string.IsNullOrEmpty(Width)) UpdateStyle(CssHelper.Width,"100%");//table tem 100% width por default
            if (DisplayInCard) parentDivClass = "card";
            if (Bordered) {
                if (!tableClass.Contains("table-bordered")) tableClass += " table-bordered";
            } else tableClass=StringHelper.RemoveString(tableClass,"table-bordered");
        }
        #endregion

        protected void SetRowsPerPage(int value) {
            RowsPerPage = value;
            RowsPerPageChanged?.Invoke(value);
            currentPage = 0;
            OnPageChanged?.Invoke(currentPage);
        }

        protected void OnPreviousPageClicked() {
            if (currentPage <= 0) {
                previousButtonCssClass = "btn btn-outline disabled";
                return;
            }
            currentPage--;
            OnPageChanged?.Invoke(currentPage);
        }

        protected void OnNextPageClicked() {
            currentPage++;
            previousButtonCssClass = "btn btn-outline";
            OnPageChanged?.Invoke(currentPage);
            var bla = (currentPage * RowsPerPage) + Rows.Count;
            if(((currentPage*RowsPerPage)+Rows.Count)>=Count)nextButtonCssClass="btn btn-outline disabled";
            else nextButtonCssClass="btn btn-outline";
        }

        protected string GetCellValue(object myObject, int column) {
            if (myObject == null) return "";
            ETableColumn eTableColumn = Columns.ElementAt(column);
            string propertyName = eTableColumn.BindTo;
            var result=myObject.GetType().GetProperty(propertyName).GetValue(myObject, null);
            if (result == null) return "";
            if (string.IsNullOrEmpty(eTableColumn.DateFormat)) return result.ToString();
            else {
                if(result!=null&&DateTime.TryParse(result.ToString(), out DateTime dt))return dt.ToString(eTableColumn.DateFormat);
            }
            //IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            return "";
        }

        protected void OnMaterialIconClicked(int column, object obj) {
            Columns.ElementAt(column).OnClicked?.Invoke(obj);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetBase.Codes;
using Frontend.Tazor.Codes;
using Frontend.Tazor.Components.Base;
using Frontend.Tazor.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Tazor.Components.Material {
    public class VMatNestedTableCode : VItem {
        #region Fields
        [Parameter] protected List<ETableColumn> Columns { get; set; } = new List<ETableColumn>();
        [Parameter] protected List<ETableColumn> ChildColumns { get; set; } = new List<ETableColumn>();
        [Parameter] protected string Key { get; set; }
        [Parameter] protected List<object> Rows { get; set; } = new List<object>();
        protected Dictionary<string,List<object>> ChildRows { get; set; }=new Dictionary<string, List<object>>();        
        [Parameter] protected Int64 Count { get; set; }
        [Parameter] protected int RowsPerPage { get; set; } = 50;
        [Parameter] protected Action<int> RowsPerPageChanged { get; set; }
        [Parameter] protected Action<int> OnPageChanged { get; set; }
        [Parameter] protected Func<object, Task> OnExpandClicked { get; set; }
        protected int currentPage = 0;
        protected Dictionary<string, bool> rowExpanded=new Dictionary<string, bool>();
        protected string previousButtonCssClass = "btn btn-outline disabled";
        protected string nextButtonCssClass = "btn btn-outline";
        #endregion

        protected override void OnInit() {
            base.OnInit();
        }

        #region OnParametersSet
        protected override async Task OnParametersSetAsync() {
            await base.OnParametersSetAsync();
            if (((currentPage * RowsPerPage) + Rows.Count) >= Count) nextButtonCssClass = "btn btn-outline disabled";
            else nextButtonCssClass = "btn btn-outline";
            if (string.IsNullOrEmpty(Width)) UpdateStyle(CssHelper.Width, "100%"); //table tem 100% width por default
        }
        #endregion

        public void SetChildTable(string parentKey, List<object> childItems) {
            Console.WriteLine("SetChildTable:"+parentKey+"========================");
            ChildRows[parentKey] = childItems;
            StateHasChanged();
        }

        protected async Task BtnExpandChildTableClicked(object obj) {
            var keyValue = ObjectHelper.GetPropertyValue<string>(obj, Key);
            if (!rowExpanded.ContainsKey(keyValue)) {
                rowExpanded[keyValue] = true;
                await OnExpandClicked.Invoke(obj);
            }
            else {
                if (rowExpanded[keyValue] == false) {
                    await OnExpandClicked.Invoke(obj);
                    rowExpanded[keyValue] = true;
                } else rowExpanded[keyValue] = false;
            }
            StateHasChanged();
            Console.WriteLine("clicou em expandir child");
        }

        protected string GetArrowIcon(string keyValue) {
            if (rowExpanded.ContainsKey(keyValue) && rowExpanded[keyValue] == true) return "arrow_drop_down";
            else return "arrow_right";
        }

        protected string GetTrStyle(string keyValue) {
            if (rowExpanded.ContainsKey(keyValue) && rowExpanded[keyValue] == true) return "";
            else return "display: none";
        }

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
            if (((currentPage * RowsPerPage) + Rows.Count) >= Count) nextButtonCssClass = "btn btn-outline disabled";
            else nextButtonCssClass = "btn btn-outline";
        }

        protected string GetCellValue(object myObject, int column) {
            if (myObject == null) return "";
            ETableColumn eTableColumn = Columns.ElementAt(column);
            string propertyName = eTableColumn.BindTo;
            var result = myObject.GetType().GetProperty(propertyName).GetValue(myObject, null);
            if (result == null) return "";
            if (string.IsNullOrEmpty(eTableColumn.DateFormat)) return result.ToString();
            else {
                if (result != null && DateTime.TryParse(result.ToString(), out DateTime dt)) return dt.ToString(eTableColumn.DateFormat);
            }
            //IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            return "";
        }

        protected void OnMaterialIconClicked(int column, object obj) {
            Columns.ElementAt(column).OnClicked?.Invoke(obj);
        }
    }
}
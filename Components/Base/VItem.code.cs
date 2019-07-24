#region Imports
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Frontend.Codes;
using Frontend.Tazor.Codes;
using Frontend.Tazor.Components.Base;
using Frontend.Tazor.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
#endregion

namespace Frontend.Tazor.Components.Base {
    public class VItemCode : ComponentBase {
        #region Fields
        [Inject] private IJSRuntime JSRuntime { get; set; }
        protected Dictionary<string, string> StyleDict = new Dictionary<string, string>();
        protected string Style { get; set; }
        //[Parameter] List<EStyle> OuterStyle { get; set; } = new List<EStyle>();
        [Parameter] protected Dictionary<string, string> OuterStyleDict { get; set; } = new Dictionary<string, string>();

        //private string itemCss;
        public bool updatingHGeometry = false;
        public bool updatingVGeometry = false;
        public bool isUsingImplicitWidth = true;
        public bool isUsingImplicitHeight = true;
        public VItem parentItem;
        protected List<VItemCode> contentChildrenList = new List<VItemCode>();
        public Action<VItemCode> dimensionsChanged { get; set; }
        public string cssClass;
        public string CssClass2;
        public string CssClass3;

        [Parameter]
        protected string CssClass {
            get => cssClass;
            set {
                if (cssClass == value) return;
                cssClass = value;
            }
        }

        [Parameter] protected string HoverColor { get; set; } // = "transparent";
        #endregion

        #region Width
        private string _width;

        [Parameter] protected Action<string> WidthChanged { get; set; }

        [Parameter]
        protected string Width {
            get => _width;
            set {
                if (_width == value) return;
                _width = value;
                UpdateStyle(CssHelper.Width, _width);
                WidthChanged?.Invoke(_width);
            }
        }
        #endregion

        #region Height
        private string _height;
        [Parameter] protected Action<string> HeightChanged { get; set; }

        [Parameter]
        protected string Height {
            get => _height;
            set {
                if (_height == value) return;
                _height = value;
                UpdateStyle(CssHelper.Height, _height);
                HeightChanged?.Invoke(_height);
            }
        }
        #endregion

        #region Background
        private string _backgroundColor;

        [Parameter]
        protected string BackgroundColor {
            get => _backgroundColor;
            set {
                if (_backgroundColor == value) return;
                _backgroundColor = value;
                UpdateStyle(CssHelper.BackgroundColor, _backgroundColor);
            }
        }
        #endregion

        #region CenterHorizontally
        private bool _centerHorizontally;

        [Parameter]
        protected bool CenterHorizontally {
            get => _centerHorizontally;
            set {
                if (_centerHorizontally == value) return;
                _centerHorizontally = value;
                if (_centerHorizontally) UpdateStyle(CssHelper.Margin, "auto");
            }
        }
        #endregion

        #region PaddingLeft
        private string _paddingLeft;

        [Parameter]
        protected string PaddingLeft {
            get => _paddingLeft;
            set {
                if (_paddingLeft == value) return;
                _paddingLeft = value;
                UpdateStyle(CssHelper.PaddingLeft, _paddingLeft);
            }
        }
        #endregion

        #region PaddingRight
        private string _paddingRight;

        [Parameter]
        protected string PaddingRight {
            get => _paddingRight;
            set {
                if (_paddingRight == value) return;
                _paddingRight = value;
                UpdateStyle(CssHelper.PaddingRight, _paddingRight);
            }
        }
        #endregion

        #region PaddingTop
        private string _paddingTop;

        [Parameter]
        protected string PaddingTop {
            get => _paddingTop;
            set {
                if (_paddingTop == value) return;
                _paddingTop = value;
                UpdateStyle(CssHelper.PaddingTop, _paddingTop);
            }
        }
        #endregion

        #region PaddingBottom
        private string _paddingBottom;

        [Parameter]
        protected string PaddingBottom {
            get => _paddingBottom;
            set {
                if (_paddingBottom == value) return;
                _paddingBottom = value;
                UpdateStyle(CssHelper.PaddingBottom, _paddingBottom);
            }
        }
        #endregion

        #region Padding
        private string _padding;

        [Parameter]
        protected string Padding {
            get => _padding;
            set {
                if (_padding == value) return;
                _padding = value;
                UpdateStyle(CssHelper.Padding, _padding);
            }
        }
        #endregion

        #region VRowLayout_FillWidth
        private int _growRate { get; set; } = 0;

        [Parameter]
        protected bool VRowLayout_FillWidth {
            get => _growRate == 0 ? false : true;
            set {
                _growRate = value == true ? 2 : 0;
                UpdateStyle(CssHelper.FlexGrow, _growRate.ToString());
            }
        }
        #endregion

        #region VColumnLayout_FillHeight
        [Parameter]
        protected bool VColumnLayout_FillHeight {
            get => _growRate == 0 ? false : true;
            set { _growRate = value == true ? 2 : 0; }
        }
        #endregion

        #region VGridLayout_ColumnStart //comeca com coluna 1
        private string _gridColumnStart { get; set; }

        [Parameter]
        protected string VGridLayout_ColumnStart {
            get => _gridColumnStart;
            set {
                if (_gridColumnStart == value) return;
                _gridColumnStart = value;
                UpdateStyle(CssHelper.GridColumnStart, _gridColumnStart);
            }
        }
        #endregion

        #region VGridLayout_ColumnEnd
        private string _gridColumnEnd { get; set; } 
        [Parameter] protected string VGridLayout_ColumnEnd {
            get => _gridColumnEnd;
            set {
                if (_gridColumnEnd == value) return;
                _gridColumnEnd = value;
                UpdateStyle(CssHelper.GridColumnEnd, _gridColumnEnd);
            }
        }
        #endregion
        
        #region VGridLayout_Column
        private string _gridColumn { get; set; } 
        [Parameter] protected string VGridLayout_Column {
            get => _gridColumn;
            set {
                if (_gridColumn == value) return;
                _gridColumn = value;
                UpdateStyle(CssHelper.GridColumn, _gridColumn);
            }
        }
        #endregion

        #region VGridLayout_RowStart //comeca com linha 1
        private string _gridRowStart { get; set; }

        [Parameter]
        protected string VGridLayout_RowStart {
            get => _gridRowStart;
            set {
                if (_gridRowStart == value) return;
                _gridRowStart = value;
                UpdateStyle(CssHelper.GridRowStart, _gridRowStart);
            }
        }
        #endregion

        #region VGridLayout_RowEnd
        private string _gridRowEnd { get; set; }

        [Parameter]
        protected string VGridLayout_RowEnd {
            get => _gridRowEnd;
            set {
                if (_gridRowEnd == value) return;
                _gridRowEnd = value;
                UpdateStyle(CssHelper.GridRowEnd, _gridRowEnd);
            }
        }
        #endregion

        #region VGridLayout_VerticalAlign
        private string _gridItemAlignSelf { get; set; }

        [Parameter]
        protected GridLayoutVerticalAlign VGridLayout_VerticalAlign {
            get {
                switch (_gridItemAlignSelf) {
                    case "start":
                        return GridLayoutVerticalAlign.Top;
                    case "end":
                        return GridLayoutVerticalAlign.Bottom;
                    case "center":
                        return GridLayoutVerticalAlign.Center;
                    case "stretch":
                        return GridLayoutVerticalAlign.Stretch;
                }
                return GridLayoutVerticalAlign.Top;
            }
            set {
                switch (value) {
                    case GridLayoutVerticalAlign.Top:
                        _gridItemAlignSelf = "start";
                        break;
                    case GridLayoutVerticalAlign.Bottom:
                        _gridItemAlignSelf = "end";
                        break;
                    case GridLayoutVerticalAlign.Center:
                        _gridItemAlignSelf = "center";
                        break;
                    case GridLayoutVerticalAlign.Stretch:
                        _gridItemAlignSelf = "stretch";
                        break;
                }
                UpdateStyle(CssHelper.AlignSelf, _gridItemAlignSelf);
            }
        }
        #endregion

        #region GrowRate
        //    [Parameter]
        //    protected GrowRate GrowRate {
        //        get {
        //            switch (_growRate) {
        //                case 1:
        //                    return GrowRate.DontGrow;
        //                case 2:
        //                    return GrowRate.Twice;
        //                case 3:
        //                    return GrowRate.ThreeTimes;
        //                case 4:
        //                    return GrowRate.FourTimes;
        //
        //            }
        //            return GrowRate.DontGrow;
        //        }
        //        set {
        //            switch (value) {
        //                case GrowRate.DontGrow:
        //                    _growRate = 1;//default
        //                    break;
        //                case GrowRate.Twice:
        //                    _growRate = 2;
        //                    break;
        //                case GrowRate.ThreeTimes:
        //                    _growRate = 3;
        //                    break;
        //                case GrowRate.FourTimes:
        //                    _growRate = 4;
        //                    break;
        //            }
        //        }
        //    }
        #endregion

        #region Parent
        IComponent _parent;

        [Parameter]
        protected IComponent Parent {
            get => _parent;
            set {
                //Console.WriteLine($"{ID} recebeu parent {value}");
                _parent = value;
            }
        }
        #endregion

        #region ID
        public string _id;

        [Parameter]
        protected string ID {
            get => _id;
            set {
                if (_id == value) return;
                _IDChanged(value);
            }
        }

        [Parameter] protected Action<string> IDChanged { get; set; }

        private void _IDChanged(string value) {
            _id = value;
            IDChanged?.Invoke(value);
            //dimensionService.SetupComponent(this);
        }

        public string GetID() => ID;
        #endregion

        #region Z
        protected int _z = -100;

        [Parameter]
        protected int z {
            get => _z;
            set {
                if (_z == value) return;
                _z = value;
                ZChanged(value);
            }
        }

        [Parameter] protected Action<int> zChanged { get; set; }

        private void ZChanged(int value) {
            zChanged?.Invoke(value);
            dimensionsChanged?.Invoke(this);
        }
        #endregion

        #region IsContentChild
        public bool IsContentChild(VItem item) {
            foreach (var contentChild in contentChildrenList) {
                if (contentChild == item) return true;
            }
            return false;
        }
        #endregion

        //=================================FUNCTIONS BELOW=================================

        #region OnInit
        protected override void OnInit() {
            //Console.WriteLine("(VItem)OnInit");
            //todo arrumar
            //        id = elementRef.nativeElement.id;
            //        if(parent)parent.contentChildrenList.push(this);
            //        parentItem = parent;
            //todo estava assim
            //        horizontalCenter = new EAnchorLine(Defines.horizontalCenter, null, this);
            //        verticalCenter = new EAnchorLine(Defines.verticalCenter, null, this);
            //        left = new EAnchorLine(Defines.left, 0, this);
            //        right = new EAnchorLine(Defines.right, null, this);
            //        _top = new EAnchorLine(Defines.top, 0, this);
            //        bottom = new EAnchorLine(Defines.bottom, null, this);
            //dimensionService.AddComponent(this);
        }
        #endregion

        #region OnAfterRenderAsync
        protected override async Task OnAfterRenderAsync() {
            await base.OnAfterRenderAsync();
        }
        #endregion

        #region OnParameterSet
        protected override void OnParametersSet() {
            //o child item deve chamar base.OnParameterSet() logo no inicio
            //if (string.IsNullOrEmpty(_id)) Console.WriteLine($"ID should not be null: {this.GetType().Name}");
            foreach (var pair in OuterStyleDict) {
                StyleDict[pair.Key] = pair.Value;
            }
            Style = Helper.ConvertDictToCssStyle(StyleDict);
        }
        #endregion

        #region UpdateStyle
        public void UpdateStyle(string key, string value) {
            if (string.IsNullOrEmpty(value)) return;
            StyleDict[key] = value;
            Style = Helper.ConvertDictToCssStyle(StyleDict);
        }
        #endregion

        #region UpdateStyle
        protected void UpdateStyle() {
            foreach (var pair in OuterStyleDict) {
                StyleDict[pair.Key] = pair.Value;
            }
            Style = Helper.ConvertDictToCssStyle(StyleDict);
        }
        #endregion
        
        
    }
}
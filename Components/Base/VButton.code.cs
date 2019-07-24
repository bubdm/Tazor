using System;
using System.Threading.Tasks;
using Frontend.Tazor.Codes;
using Frontend.Tazor.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Frontend.Tazor.Components.Base {
    public class VButtonCode : VItem {
        #region TextColor
        private string _textColor;

        [Parameter]
        protected string TextColor {
            get => _textColor;
            set {
                if (_textColor == value) return;
                _textColor = value;
                UpdateStyle(CssHelper.Color, _textColor);
            }
        }
        #endregion

        #region TooltipPlacement
        protected string _tooltipPlacement = "bottom";

        [Parameter]
        protected TooltipPlacement TooltipPlacement {
            get {
                switch (_tooltipPlacement) {
                    case "left":
                        return TooltipPlacement.Left;
                    case "right":
                        return TooltipPlacement.Right;
                    case "top":
                        return TooltipPlacement.Top;
                    case "bottom":
                        return TooltipPlacement.Bottom;
                }
                return TooltipPlacement.Bottom;
            }
            set => _tooltipPlacement = value.ToString();
        }
        #endregion

        #region Fields
        [Parameter] protected string Text { get; set; } = "";
        //[Parameter] protected Action<UIMouseEventArgs> OnClicked { get; set; }
        [Parameter] protected Func<UIMouseEventArgs, Task> OnClicked { get; set; }
        protected string IconCssClass { get; set; } = "";
        [Parameter] protected string IconName { get; set; } = "";
        [Parameter] protected string Tooltip { get; set; }
        #endregion

        //=================================FUNCTIONS BELOW=================================


        #region BuildRenderTree - Hack enquanto o blazor nao suporta conditional attributes: https://github.com/aspnet/Blazor/issues/373
        protected override void BuildRenderTree(RenderTreeBuilder builder) {
            base.BuildRenderTree(builder);
            //Console.WriteLine("executando render tree");
            var seq = 0;
            builder.OpenElement(seq, "button");
            if(!string.IsNullOrEmpty(ID)) builder.AddAttribute(++seq,"id",ID);
            if(!string.IsNullOrEmpty(Style)) builder.AddAttribute(++seq,"style",Style);
            if(!string.IsNullOrEmpty(cssClass)) builder.AddAttribute(++seq,"class",CssClass);
            builder.AddAttribute(++seq,"onclick",OnClicked);
            if (!string.IsNullOrEmpty(Tooltip)) {
                builder.AddAttribute(++seq,"data-toggle","tooltip");
                builder.AddAttribute(++seq,"data-placement",_tooltipPlacement);
                builder.AddAttribute(++seq,"title",Tooltip);
            }
            if (!string.IsNullOrEmpty(IconCssClass)){
                builder.OpenElement(++seq, "i");
                builder.AddAttribute(++seq, "class",IconCssClass);
                builder.AddContent(++seq,IconName);
                builder.CloseElement();
            }
            builder.AddContent(++seq,Text);
            builder.CloseElement();
        }
        #endregion

        #region OnParametersSet
        protected override async Task OnParametersSetAsync() {
            await base.OnParametersSetAsync();
        }
        #endregion

        #region BtnClicked
        protected void _BtnClicked(UIMouseEventArgs e) {
            OnClicked?.Invoke(e);
        }
        #endregion
    }
}
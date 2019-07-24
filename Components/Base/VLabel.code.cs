using System.Threading.Tasks;
using Frontend.Tazor.Codes;
using Frontend.Tazor.Enums;
using Microsoft.AspNetCore.Components;

namespace Frontend.Tazor.Components.Base {
    public class VLabelCode : VItem {
        #region FontStyle
        private FontStyle _fontStyle = FontStyle.Normal;

        [Parameter]
        FontStyle FontStyle {
            get => _fontStyle;
            set {
                if (_fontStyle == value) return;
                _fontStyle = value;
                UpdateStyle(CssHelper.FontStyle, _fontStyle.ToString());
            }
        }
        #endregion

        #region FontFamily
        private string _fontFamily;

        [Parameter]
        string FontFamily {
            get => _fontFamily;
            set {
                if (_fontFamily == value) return;
                _fontFamily = value;
                UpdateStyle(CssHelper.FontFamily, _fontFamily);
            }
        }
        #endregion

        #region Color
        private string _color;

        [Parameter]
        string Color {
            get => _color;
            set {
                if (_color == value) return;
                _color = value;
                UpdateStyle(CssHelper.Color, _color);
            }
        }
        #endregion

        #region Bold
        FontWeight _fontWeight = FontWeight.Normal;

        [Parameter]
        bool Bold {
            get => _fontWeight == FontWeight.Bold;
            set {
                _fontWeight = value == true ? FontWeight.Bold : FontWeight.Normal;
                UpdateStyle(CssHelper.FontWeight, _fontWeight.ToString());
            }
        }
        #endregion

        #region FontItalic
        [Parameter]
        bool FontItalic {
            get => _fontStyle == FontStyle.Italic;
            set {
                _fontStyle = value == true ? FontStyle.Italic : FontStyle.Normal;
                UpdateStyle(CssHelper.FontStyle, _fontStyle.ToString());
            }
        }
        #endregion

        #region TextOverflow
        TextOverflow _textOverflow = TextOverflow.Clip;

        [Parameter]
        protected TextOverflow TextOverflow {
            get => _textOverflow;
            set {
                _textOverflow = value;
                if (_textOverflow != TextOverflow.Clip) {
                    //doc em https://www.w3schools.com/cssref/css3_pr_text-overflow.asp
                    _whiteSpace = WhiteSpace.NoWrap;
                    _overflow = Overflow.Hidden;
                } else {
                    _whiteSpace = WhiteSpace.Normal;
                    _overflow = Overflow.Visible;
                }
                UpdateStyle(CssHelper.TextOverflow, _textOverflow.ToString());
            }
        }
        #endregion

        #region FontSize
        private string _fontSize;

        [Parameter]
        string FontSize {
            get => _fontSize;
            set {
                if (_fontSize == value) return;
                _fontSize = value;
                UpdateStyle(CssHelper.FontSize, _fontSize);
            }
        }
        #endregion

        #region TextAlign
        private TextAlign _textAlign = TextAlign.Left;

        [Parameter]
        protected TextAlign TextAlign {
            get => _textAlign;
            set {
                _textAlign = value;
                UpdateStyle(CssHelper.TextAlign, _textAlign.ToString());
            }
        }
        #endregion

        #region Fields
        WhiteSpace _whiteSpace = WhiteSpace.Normal;
        Overflow _overflow = Overflow.Visible;
        [Parameter] protected string Text { get; set; }
        #endregion

        //=================================FUNCTIONS BELOW=================================

        #region OnParametersSet
        protected override async Task OnParametersSetAsync() {
            await base.OnParametersSetAsync();
            StyleDict[CssHelper.Whitespace] = _whiteSpace.ToString();
            StyleDict[CssHelper.Overflow] = _overflow.ToString();
            Style = Helper.ConvertDictToCssStyle(StyleDict);
        }
        #endregion
    }
}
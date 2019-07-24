using Frontend.Tazor.Enums;

namespace Frontend.Tazor.Entities {
    public class EBorder {
        public string radius { get; set; }
        public BorderStyle style { get; set; } = BorderStyle.Solid;
        public string color { get; set; } = "black";
    }
}
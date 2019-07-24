using System;
using Frontend.Tazor.Enums;

namespace Frontend.Tazor.Entities {
    public class ETableColumn {
        public TableColumnType Type { get; set; } = TableColumnType.Text;
        public string Name { get; set; }
        public string BindTo { get; set; }
        public string DateFormat { get; set; }
        public Action<object> OnClicked { get; set; }
    }
}
using Frontend.Tazor.Components.Base;

namespace Frontend.Tazor.Entities {
    public class EAnchors {
        public EAnchorLine left { get; set; }
        public EAnchorLine right { get; set; }
        public EAnchorLine top { get; set; }
        public EAnchorLine bottom { get; set; }
        public EAnchorLine horizontalCenter { get; set; }
        public EAnchorLine verticalCenter { get; set; }
        public VItem fill { get; set; }
        public VItem centerIn { get; set; }
        public int margins { get; set; } = 0;
        public int leftMargin { get; set; }= 0;
        public int rightMargin { get; set; }= 0;
        public int topMargin { get; set; }= 0;
        public int bottomMargin { get; set; }= 0;
        
        
        public object GetPropValue(string propName){
            return this.GetType().GetProperty(propName).GetValue(this, null);
        }
    }
}
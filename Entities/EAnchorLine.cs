using System;
using Frontend.Tazor.Components.Base;

namespace Frontend.Tazor.Entities {
    public class EAnchorLine {
        #region Fields
        public VItem obj { get; set; }
        public string name { get; set; }
        public bool relativeToParent { get; set; } = false;
        #endregion

        #region value
        public int _value; //chamar _value qdo nao precisa atualizar outras props
        public int value {
            get => _value;
            set {
                if (obj == null) {
                    Console.WriteLine("setando valor em objeto nulo");
                    return;
                }
                Console.WriteLine($"(EAnchorLine). {obj.GetID()}, {name}, {value}");
                //obj.dimensionService.ExplicitUpdate(obj, name, value);
            }
        }
        #endregion        
        
        #region Constructor
        public EAnchorLine(string name, int value, VItem obj) {
            this.name = name;
            this._value = value;
            this.obj = obj;
        }
        #endregion

        #region MapToLocalCoordinates - left, right, top... tem coordenadas com relação ao parent.
        public double MapToLocalCoordinates() {
            /*switch (this.name) {
                case "left":
                    return 0;
                case "right":
                    return this.obj.GetWidth();
                case "top":
                    return 0;
                case "bottom":
                    return this.obj.GetHeight();
                case "horizontalCenter":
                    return this.obj.GetWidth() / 2;
                case "verticalCenter":
                    return this.obj.GetWidth() / 2;
            }
            Console.WriteLine($"(EAnchorLine.MapToLocalCoordinates)invalid name:{name}");*/
            return -1;
        }
        #endregion
    }
}



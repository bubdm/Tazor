using Frontend.Tazor.Components.Base;
using Frontend.Tazor.Entities;

namespace Frontend.Tazor.Interfaces {
    public interface IDimensionService {
        void AddComponent(VItem item);
        void SetupComponent(VItem item);
        void ExplicitUpdate(VItem item, string propertyName, dynamic propertyValue);
        void AddDependant(VItem item, string propertyName, EAnchorLine eAnchorLine);
        void PrintDimensions(string itemID);
    }
}
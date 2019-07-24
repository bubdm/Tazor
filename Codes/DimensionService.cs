#region Imports
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DotnetBase.Codes;
using Frontend.Tazor.Components;
using Frontend.Tazor.Entities;
using Frontend.Tazor.Interfaces;
#endregion

namespace Frontend.Tazor.Codes {
    public class DimensionService/*: IDimensionService*/ {
        /*#region Fields
        protected bool debugOn = true;
        protected string[] dbgObjIDs = new[] {"bozo", "rectPurple"};
        protected string objName = "dimensionService";
        protected Dictionary<string, VItem> components = new Dictionary<string, VItem>();
        protected Dictionary<string, List<VItem>> leftChangeDependants = new Dictionary<string, List<VItem>>();
        protected Dictionary<string, List<VItem>> rightChangeDependants = new Dictionary<string, List<VItem>>();
        protected Dictionary<string, List<VItem>> topChangeDependants = new Dictionary<string, List<VItem>>();
        protected Dictionary<string, List<VItem>> bottomChangeDependants = new Dictionary<string, List<VItem>>();
        protected Dictionary<string, List<VItem>> horizontalCenterChangeDependants = new Dictionary<string, List<VItem>>();
        protected Dictionary<string, List<VItem>> verticalCenterChangeDependants = new Dictionary<string, List<VItem>>();
        #endregion

        #region SetupComponent
        public void SetupComponent(VItem item) {
            Console.WriteLine($"(DimensionService)SetupComponent.{item._id}");
            //hack pq nao exite o evento onbeforeInit ou um construtor no componente para setar isso
            if (!components.ContainsKey(item._id)) components[item._id] = item;            
            if (item.Left == null) {                
                item.Left=new EAnchorLine(Defines.left, 0, item);
                item.right=new EAnchorLine(Defines.right, item.GetWidth(), item);
                item._top=new EAnchorLine(Defines.top, 0, item);
                item.bottom=new EAnchorLine(Defines.bottom, item._height, item);
                item.horizontalCenter=new EAnchorLine(Defines.horizontalCenter, item.GetWidth()>0?item.GetWidth()/2:0, item);
                item.verticalCenter=new EAnchorLine(Defines.verticalCenter, item._height>0?item._height/2:0, item);
            }
        }
        #endregion

        #region AddComponent
        public void AddComponent(VItem item) {
            Console.WriteLine("adicionando componente na lista:"+item._id);
            components[item._id] = item;
        }
        #endregion

        #region ExplicitUpdate - called whenever the property explicitly receives a new value by the user
        public void ExplicitUpdate(VItem item, string propertyName, dynamic propertyValue) {
            //if (debugOn && StringHelper.ContainsStringInArray(item._id, dbgObjIDs)) Console.WriteLine($"{item._id} [{propertyName}]= {propertyValue}");            
            SetInternalValue(item, propertyName, propertyValue);
            switch (propertyName) {
                case Defines.top:
                    Console.WriteLine($"Setando o top{item._id}");
                    UpdateVGeometry(item);
                    break;
                case Defines.bottom:
                    UpdateVGeometry(item);
                    break;
                case Defines.left:
                    UpdateHGeometry(item);
                    break;
                case Defines.right:
                    UpdateHGeometry(item);
                    break;
                case Defines.width:
                    UpdateHGeometry(item);
                    break;
                case Defines.height:
                    UpdateVGeometry(item);
                    break;
                case Defines.anchorsLeft: {
                    //[anchors.left]=
                    EAnchorLine eAnchorLine = (EAnchorLine) propertyValue;
                    AddDependant(item, eAnchorLine.name, eAnchorLine);
                    UpdateHGeometry(item);
                    break;
                }
                case Defines.anchorsRight: {
                    //[anchors.right]=
                    EAnchorLine eAnchorLine = (EAnchorLine) propertyValue;
                    AddDependant(item, eAnchorLine.name, eAnchorLine);
                    UpdateHGeometry(item);
                    break;
                }
                case Defines.anchorsTop: {
                    //[anchors.top]=
                    EAnchorLine eAnchorLine = (EAnchorLine) propertyValue;
                    AddDependant(item, eAnchorLine.name, eAnchorLine);
                    UpdateVGeometry(item);
                    break;
                }
                case Defines.anchorsBottom: {
                    //[anchors.bottom]=
                    EAnchorLine eAnchorLine = (EAnchorLine) propertyValue;
                    AddDependant(item, eAnchorLine.name, eAnchorLine);
                    UpdateVGeometry(item);
                    break;
                }
                case Defines.anchorsHorizontalCenter: {
                    //[anchors.horizontalCenter]=...
                    EAnchorLine eAnchorLine = (EAnchorLine) propertyValue;
                    AddDependant(item, eAnchorLine.name, eAnchorLine);
                    UpdateHGeometry(item);
                    break;
                }
                case Defines.anchorsVerticalCenter: {
                    //[anchors.verticalCenter]=...
                    EAnchorLine eAnchorLine = (EAnchorLine) propertyValue;
                    AddDependant(item, eAnchorLine.name, eAnchorLine);
                    UpdateVGeometry(item);
                    break;
                }
                default:
                    Console.WriteLine($"(ExplicitUpdate)unknown property:{propertyName}");
                    return;
            }
        }
        #endregion

        #region UpdateHGeometry
        //todo criar o updateVGeometry. essa parte do codigo veio do qmlinangular
        //todo usar mais essa funcao ao chamar explicitamente o width, right, left, horiziontalcenter. Ver no QmlInAngular
        private void UpdateHGeometry(VItem item) {
            if (debugOn && StringHelper.ContainsStringInArray(item._id, dbgObjIDs)) Console.WriteLine(item._id + " updating horizontal geometry...");
            if (item.updatingHGeometry) return;
            item.updatingHGeometry = true;
            //const left = item.parentItem ? item.parentItem.left : 0;
            int left = item.Left._value;
            //horizontal Anchors
            //if(item.anchors.fill!=undefined)setFillHorizontallyAnchor();
            //if(item.anchors.centerIn!=undefined)updateGeometryUsingAnchorsHorizontalCenter(item);
            if (item.anchors.left != null) UpdateGeometryUsingAnchorsLeft(item);
            else if (item.anchors.right != null) UpdateGeometryUsingAnchorsRight(item);
            else if (item.anchors.horizontalCenter != null) UpdateGeometryUsingAnchorsHorizontalCenter(item);
            else {
                //left = x + left;
                SetInternalValue(item, Defines.right, item.Left._value + item.GetWidth());
                SetInternalValue(item, Defines.horizontalCenter, item.Left._value + item.GetWidth() / 2);
            }
            ImplicitUpdateDependants(item, Defines.left);
            ImplicitUpdateDependants(item, Defines.right);
            ImplicitUpdateDependants(item, Defines.horizontalCenter);
            item.updatingHGeometry = false;
        }
        #endregion

        #region UpdateVGeometry
        private void UpdateVGeometry(VItem item) {
            if (debugOn && StringHelper.ContainsStringInArray(item._id, dbgObjIDs)) Console.WriteLine($"{item._id} updating vertical geometry...");
            if (item.updatingVGeometry) return;
            item.updatingVGeometry = true;
            const int top = 0;
            //if(item.anchors.fill!=undefined)item.setFillVerticallyInAnchor();
            //else if(item.anchors.centerIn!=undefined)item.setCenterInVerticallyInAnchor();
            if (item.anchors.top != null) UpdateGeometryUsingAnchorsTop(item);
            else if (item.anchors.bottom != null) UpdateGeometryUsingAnchorsBottom(item);
            else if (item.anchors.verticalCenter != null) UpdateGeometryUsingAnchorsVerticalCenter(item);
            else {
                // if (parentID) {
                //   const topProp = parentID.top;
                //   //topProp.changed.connect(this, $updateVGeometry, flags);
                // }
                //top = _y + top;
                SetInternalValue(item, Defines.bottom, item._top._value + item.GetHeight());
                SetInternalValue(item, Defines.verticalCenter, item._top._value + (item.GetHeight() / 2));
            }
            ImplicitUpdateDependants(item, Defines.top);
            ImplicitUpdateDependants(item, Defines.bottom);
            ImplicitUpdateDependants(item, Defines.verticalCenter);
            item.updatingVGeometry = false;
        }
        #endregion

        #region AddDependant
        public void AddDependant(VItem item, string propertyName, EAnchorLine eAnchorLine) {
            if (debugOn && StringHelper.ContainsStringInArray(item._id, dbgObjIDs)) Console.WriteLine($"{item._id} dimensions will change whenever {eAnchorLine.obj._id}.{eAnchorLine.name} changes");
            List<VItem> dependants = new List<VItem>();
            Dictionary<string, List<VItem>> map; //=new Dictionary<string, List<VItem>>();
            switch (propertyName) {
                case Defines.left:
                    map = leftChangeDependants;
                    break;
                case Defines.right:
                    map = rightChangeDependants;
                    break;
                case Defines.top:
                    map = topChangeDependants;
                    break;
                case Defines.bottom:
                    map = bottomChangeDependants;
                    break;
                case Defines.horizontalCenter:
                    map = horizontalCenterChangeDependants;
                    break;
                case Defines.verticalCenter:
                    map = verticalCenterChangeDependants;
                    break;
                default:
                    Console.WriteLine($"(AddDependant)unknown property:{propertyName}");
                    return;
            }
            if (map != null) {
                if (!map.ContainsKey(eAnchorLine.obj._id)) map[eAnchorLine.obj._id] = new List<VItem>() {item};
                else map[eAnchorLine.obj._id].Add(item);
                dependants = map[eAnchorLine.obj._id];
            }
            if (debugOn && StringHelper.ContainsStringInArray(eAnchorLine.obj._id, dbgObjIDs)) Console.WriteLine(eAnchorLine.obj._id + $"{propertyName}Change dependants============" + dependants.Count);
        }
        #endregion

        #region ImplicitUpdateDependants
        private void ImplicitUpdateDependants(VItem item, string type) {
            //Console.WriteLine(item._id + "(ImplicitUpdateDependants)" + type);
            Dictionary<string, List<VItem>> map = null;
            switch (type) {
                case Defines.left:
                    map = leftChangeDependants;
                    break;
                case Defines.right:
                    map = rightChangeDependants;
                    break;
                case Defines.top:
                    map = topChangeDependants;
                    break;
                case Defines.bottom:
                    map = bottomChangeDependants;
                    break;
                case Defines.horizontalCenter:
                    map = horizontalCenterChangeDependants;
                    break;
                case Defines.verticalCenter:
                    map = verticalCenterChangeDependants;
                    break;
            }
            if (!map.ContainsKey(item._id)) return;
            if (map == null) return;
            if (debugOn && StringHelper.ContainsStringInArray(item._id, dbgObjIDs)) Console.WriteLine($"changing dimensions of all that depend on ${item._id}.${type}");
            List<VItem> dependantList = map[item._id];
            foreach (VItem dependantItem in dependantList) {
                if (debugOn && StringHelper.ContainsStringInArray(dependantItem._id, dbgObjIDs)) Console.WriteLine($"dependant:{dependantItem._id}");
                EAnchorLine eAnchorLine = (EAnchorLine) dependantItem.anchors.GetPropValue(type);
                if (type == Defines.top || type == Defines.bottom || type == Defines.verticalCenter) {
                    //Ex: só pode depender do item.bottom de outro componente:
                    // dependantItem.anchors.top, dependantItem.anchors.bottom, dependantItem.anchors.verticalCenter
                    if (dependantItem.anchors.top != null && dependantItem.anchors.top.obj._id == item._id) {
                        //console.log(`${dependantitem._id}.anchors[top] depende do ${item._id}.${type}`);
                        //console.log(dependantItem.anchors["top"].name);
                        UpdateGeometryUsingAnchorsTop(dependantItem);
                    }
                    if (dependantItem.anchors.verticalCenter != null && dependantItem.anchors.verticalCenter.obj._id == item._id) {
                        //console.log(`${dependantitem._id}.anchors[top] depende do ${item._id}.${type}`);
                        UpdateGeometryUsingAnchorsVerticalCenter(dependantItem);
                    }
                    if (dependantItem.anchors.bottom != null && dependantItem.anchors.bottom.obj._id == item._id) {
                        //console.log(`${dependantitem._id}.anchors[bottom] depende do ${item._id}.${type}`);
                        UpdateGeometryUsingAnchorsBottom(dependantItem);
                    }
                } else if (type == Defines.left || type == Defines.right || type == Defines.horizontalCenter) {
                    //Ex: só pode depender do item.right de outro componente:
                    // dependantItem.anchors.left, dependantItem.anchors.right, dependantItem.anchors.horizontalCenter
                    if (dependantItem.anchors.left != null && dependantItem.anchors.left.obj._id == item._id) {
                        //console.log(`${dependantitem._id}.anchors[left] depende do ${item._id}.${type}`);
                        //console.log(dependantItem.anchors["top"].name);
                        UpdateGeometryUsingAnchorsLeft(dependantItem);
                    }
                    if (dependantItem.anchors.right != null && dependantItem.anchors.right.obj._id == item._id) {
                        //console.log(`${dependantitem._id}.anchors[right] depende do ${item._id}.${type}`);
                        //console.log(dependantItem.anchors["top"].name);
                        UpdateGeometryUsingAnchorsRight(dependantItem);
                    }
                    if (dependantItem.anchors.horizontalCenter != null && dependantItem.anchors.horizontalCenter.obj._id == item._id) {
                        //console.log(`${dependantitem._id}.anchors[horizontalCenter] depende do ${item._id}.${type}`);
                        //console.log(dependantItem.anchors["top"].name);
                        UpdateGeometryUsingAnchorsHorizontalCenter(dependantItem);
                    }
                }
            }
        }
        #endregion

        #region [anchors.left]=...
        private void UpdateGeometryUsingAnchorsLeft(VItem item) {
            if (item.anchors.left == null || (item.anchors.right == null && item.GetWidth() == 0)) return;
            if (debugOn && StringHelper.ContainsStringInArray(item._id, dbgObjIDs)) Console.WriteLine(item._id + " considering anchors.left...");
            bool withAnchorsRight = false;
            int leftMargin = item.anchors.leftMargin > 0 ? item.anchors.leftMargin : item.anchors.margins;
            int rightMargin = item.anchors.rightMargin > 0 ? item.anchors.rightMargin : item.anchors.margins;
            bool inParent = item.anchors.left.obj == item.parentItem;
            //const leftOffset = inParent ? parentItem.left.value : 0;
            if (inParent) {
                item.anchors.left.relativeToParent = true;
                SetInternalValue(item, Defines.left, item.anchors.left.MapToLocalCoordinates() + leftMargin);
            } else {
                item.anchors.left.relativeToParent = false;
                SetInternalValue(item, Defines.left, item.anchors.left._value + leftMargin);
            }
            if (item.anchors.right != null) {
                withAnchorsRight = true;
                //_x = left.value;
                if (inParent) {
                    SetInternalValue(item, Defines.right, item.anchors.right.MapToLocalCoordinates() - rightMargin);
                } else {
                    if (item.parentItem != null && (!item.parentItem.IsContentChild(item.anchors.right.obj))) {
                        Console.WriteLine("error in " + item._id + ". [anchors.right]=" + item.anchors.right.obj._id + "." + item.anchors.right.name + " is invalid. " + item.anchors.right.obj._id + " is not parent or sibling of " + item._id);
                        return;
                    }
                    SetInternalValue(item, Defines.right, item.anchors.right._value - rightMargin);
                }
                item.isUsingImplicitWidth = false;
                SetInternalValue(item, Defines.width, item.right._value - item.Left._value);
                SetInternalValue(item, Defines.horizontalCenter, (item.right._value + item.Left._value) / 2);
            } else {
                //without anchors.right
                //item._x = item.left._value;
                SetInternalValue(item, Defines.right, item.Left._value + item.GetWidth());
                SetInternalValue(item, Defines.horizontalCenter, item.Left._value + item.GetWidth() / 2);
            }
        }
        #endregion        

        #region [anchors.right]=...
        private void UpdateGeometryUsingAnchorsRight(VItem item) {
            if (item.anchors.right == null || (item.GetWidth() == 0)) return;
            if (debugOn && StringHelper.ContainsStringInArray(item._id, dbgObjIDs)) Console.WriteLine(item._id + " considering anchors.right...");
            int rightMargin = item.anchors.rightMargin > 0 ? item.anchors.rightMargin : item.anchors.margins;
            bool inParent = item.anchors.right.obj == item.parentItem;
            if (inParent) {
                item.anchors.right.relativeToParent = true;
                SetInternalValue(item, Defines.right, item.anchors.right.MapToLocalCoordinates() - rightMargin);
            } else {
                item.anchors.right.relativeToParent = false;
                if (item.parentItem != null && (!item.parentItem.IsContentChild(item.anchors.right.obj))) {
                    Console.WriteLine("error in " + item._id + ". [anchors.right]=" + item.anchors.right.obj._id + "." + item.anchors.right.name + " is invalid. " + item.anchors.right.obj._id + " is not parent or sibling of " + item._id);
                    return;
                }
                SetInternalValue(item, Defines.right, item.anchors.right._value - rightMargin);
            }
            SetInternalValue(item, Defines.left, item.right._value - item.GetWidth());
            SetInternalValue(item, Defines.horizontalCenter, item.right._value - item.GetWidth() / 2);
        }
        #endregion
        
        #region [anchors.horizontalCenter]=....
        private void UpdateGeometryUsingAnchorsHorizontalCenter(VItem item) {
            if (item.anchors.horizontalCenter == null) return;
            if (debugOn && StringHelper.ContainsStringInArray(item._id, dbgObjIDs)) Console.WriteLine(item._id + " considering anchors.horizontalCenter...");
            bool inParent = item.anchors.horizontalCenter.obj == item.parentItem;
            if (inParent) {
                item.anchors.horizontalCenter.relativeToParent = true;
                SetInternalValue(item, Defines.horizontalCenter, item.anchors.horizontalCenter.MapToLocalCoordinates());
            } else {
                item.anchors.horizontalCenter.relativeToParent = false;
                if (item.parentItem != null && (!item.parentItem.IsContentChild(item.anchors.horizontalCenter.obj))) {
                    Console.WriteLine("error in " + item._id + ". [anchors.horizontalCenter]=" + item.anchors.horizontalCenter.obj._id + "." + item.anchors.horizontalCenter.name + " is invalid. " + item.anchors.horizontalCenter.obj._id + " is not parent or sibling of " + item._id);
                    return;
                }
                SetInternalValue(item, Defines.horizontalCenter, item.anchors.horizontalCenter._value);
            }
            SetInternalValue(item, Defines.left, item.horizontalCenter._value - item.GetWidth() / 2);
            SetInternalValue(item, Defines.right, item.horizontalCenter._value + item.GetWidth() / 2);
        }
        #endregion

        #region [anchors.verticalCenter]=...
        private void UpdateGeometryUsingAnchorsVerticalCenter(VItem item) {
            if (item.anchors.verticalCenter == null || (item._height == 0)) return;
            if (debugOn && StringHelper.ContainsStringInArray(item._id, dbgObjIDs)) Console.WriteLine(item._id, "considering anchors.verticalCenter...");
            //const topOffset = inParent ? parentItem.top.value : 0;
            bool inParent = item.anchors.verticalCenter.obj == item.parentItem;
            if (inParent) {
                item.anchors.verticalCenter.relativeToParent = true;
                SetInternalValue(item, Defines.verticalCenter, item.anchors.verticalCenter.MapToLocalCoordinates());
            } else {
                item.anchors.verticalCenter.relativeToParent = false;
                if (item.parentItem != null && (!item.parentItem.IsContentChild(item.anchors.verticalCenter.obj))) {
                    Console.WriteLine("error in " + item._id + ". [anchors.verticalCenter]=" + item.anchors.verticalCenter.obj._id + "." + item.anchors.verticalCenter.name + " is invalid. " + item.anchors.verticalCenter.obj._id + " is not parent or sibling of " + item._id);
                    return;
                }
                SetInternalValue(item, Defines.verticalCenter, item.anchors.verticalCenter._value);
            }
            //item._y = item.top._value=;
            SetInternalValue(item, Defines.top, item.verticalCenter._value - item._height / 2);
            SetInternalValue(item, Defines.bottom, item.verticalCenter._value + item._height / 2);
            // if(item._id=="rectBlackInRed"){
            //   Console.WriteLine("rectBlackInred=======================================================================================");
            //   Console.WriteLine("anchors.verticalCenter:",item.anchors.verticalCenter._value);
            //   Console.WriteLine("anchors.verticalCenter in parent",item.anchors.verticalCenter.relativeToParent);
            // }
        }
        #endregion
        
        #region [anchors.top]=...
        private void UpdateGeometryUsingAnchorsTop(VItem item) {
            Console.WriteLine($"{item.GetID()} setando top:{item.anchors.top?.value} ");
            if (item.anchors.top == null || (item.anchors.bottom == null && item._height == 0)) return;
            if (debugOn && StringHelper.ContainsStringInArray(item._id, dbgObjIDs)) Console.WriteLine(item._id, "considering anchors.top...");
            bool withAnchorsBottom = false;
            int topMargin = item.anchors.topMargin != 0 ? item.anchors.topMargin : item.anchors.margins;
            int bottomMargin = item.anchors.bottomMargin != 0 ? item.anchors.bottomMargin : item.anchors.margins;
            //const topOffset = inParent ? parentItem.top.value : 0;
            bool inParent = item.anchors.top.obj == item.parentItem;
            if (inParent) {
                item.anchors.top.relativeToParent = true;
                SetInternalValue(item, Defines.top, item.anchors.top.MapToLocalCoordinates() + topMargin);
            } else {
                item.anchors.top.relativeToParent = false;
                if (item.parentItem != null && (!item.parentItem.IsContentChild(item.anchors.top.obj))) {
                    Console.WriteLine("error in " + item._id + ". [anchors.top]=" + item.anchors.top.obj._id + "." + item.anchors.top.name + " is invalid. " + item.anchors.top.obj._id + " is not parent or sibling of " + item._id);
                    return;
                }
                SetInternalValue(item, Defines.top, item.anchors.top._value + topMargin);
            }
            if (item.anchors.bottom != null) {
                withAnchorsBottom = true;
                //_y = item.top._value;
                if (inParent) {
                    SetInternalValue(item, Defines.bottom, item.anchors.bottom.MapToLocalCoordinates() - bottomMargin);
                } else {
                    if (item.parentItem != null && (!item.parentItem.IsContentChild(item.anchors.bottom.obj))) {
                        Console.WriteLine("error in " + item._id + ". [anchors.bottom]=" + item.anchors.bottom.obj._id + "." + item.anchors.bottom.name + " is invalid. " + item.anchors.bottom.obj._id + " is not parent or sibling of " + item._id);
                        return;
                    }
                    SetInternalValue(item, Defines.bottom, item.anchors.bottom._value - bottomMargin);
                }
                item.isUsingImplicitHeight = false;
                SetInternalValue(item, Defines.height, item.bottom._value - item._top._value);
                SetInternalValue(item, Defines.verticalCenter, (item.bottom._value + item._top._value) / 2);
            } else {
                //without anchors.bottom
                //item._y = item.top._value;
                SetInternalValue(item, Defines.bottom, item._top._value + item._height);
                SetInternalValue(item, Defines.verticalCenter, item._top._value + item._height / 2);
            }
        }
        #endregion
        
        #region [anchors.bottom]=...
        private void UpdateGeometryUsingAnchorsBottom(VItem item) {
            if (item.anchors.bottom == null || (item._height == 0)) return;
            if (debugOn && StringHelper.ContainsStringInArray(item._id, dbgObjIDs)) Console.WriteLine(item._id, "considering anchors.bottom...");
            int bottomMargin = item.anchors.bottomMargin != 0 ? item.anchors.bottomMargin : item.anchors.margins;
            bool inParent = item.anchors.bottom.obj == item.parentItem;
            if (inParent) {
                item.anchors.bottom.relativeToParent = true;
                SetInternalValue(item, Defines.bottom, item.anchors.bottom.MapToLocalCoordinates() - bottomMargin);
            } else {
                item.anchors.bottom.relativeToParent = false;
                if (item.parentItem != null && (!item.parentItem.IsContentChild(item.anchors.bottom.obj))) {
                    Console.WriteLine("%cerror in " + item._id + ". [anchors.bottom]=" + item.anchors.bottom.obj._id + "." + item.anchors.bottom.name + " is invalid. " + item.anchors.bottom.obj._id + " is not parent or sibling of " + item._id, "color: red");
                    return;
                }
                SetInternalValue(item, Defines.bottom, item.anchors.bottom._value - bottomMargin);
            }
            //_y = bottom.value - height ;
            SetInternalValue(item, Defines.top, item.bottom._value - item._height);
            SetInternalValue(item, Defines.verticalCenter, item.bottom._value - item._height / 2);
        }
        #endregion
        
        #region SetInternalValue
        private void SetInternalValue(VItem item, string propertyName, dynamic propertyValue) {
            //Console.WriteLine("(DimensionService)SetInternalValue:");
            string propertyType = propertyValue.GetType().ToString();
            EAnchorLine eAnchorLine = null;
            if (propertyName.IndexOf("anchors") >= 0) {
                eAnchorLine = (EAnchorLine) propertyValue;
            } else if (propertyType != "System.Int32") {
                Console.WriteLine("(DimensionService)SetInternalValue. PropertyType should be Int32");
                return;
            }
            switch (propertyName) {
                case Defines.height:
                    item._height = propertyValue;
                    break;
                case Defines.width:
                    item.SetInternalWidth(propertyValue);
                    break;
                case Defines.left:
                    item.Left._value = propertyValue;
                    break;
                case Defines.right:
                    item.right._value = propertyValue;
                    break;
                case Defines.top:
                    item._top._value = propertyValue;
                    break;
                case Defines.bottom:
                    Console.WriteLine($"Setando o bottom para {item._id}:"+propertyValue);
                    item.bottom._value = propertyValue;
                    break;
                case Defines.horizontalCenter:
                    item.horizontalCenter._value = propertyValue;
                    break;
                case Defines.verticalCenter:
                    item.verticalCenter._value = propertyValue;
                    break;
                case Defines.anchorsLeft:
                    item.anchors.left = eAnchorLine;
                    break;
                case Defines.anchorsRight:
                    item.anchors.right = eAnchorLine;
                    break;
                case Defines.anchorsTop:
                    item.anchors.top = eAnchorLine;
                    break;
                case Defines.anchorsBottom:
                    item.anchors.bottom = eAnchorLine;
                    break;
                case Defines.anchorsVerticalCenter:
                    item.anchors.verticalCenter = eAnchorLine;
                    break;
                case Defines.anchorsHorizontalCenter:
                    item.anchors.horizontalCenter = eAnchorLine;
                    break;
                default:
                    Console.WriteLine($"(SetInternalValue)unknown property:{propertyName}");
                    return;
            }            
            if (debugOn && StringHelper.ContainsStringInArray(item._id, dbgObjIDs)) Console.WriteLine(item._id+ $" [{propertyName}]={propertyValue}");
        }
        #endregion

        #region PrintDimensions
        public void PrintDimensions(string itemID) {
            if (!components.ContainsKey(itemID)) return;
            Console.WriteLine("${itemID} dimensions==================");
            VItem item = components[itemID];
            Console.WriteLine("width ${item.width}");
            Console.WriteLine("height ${item.height}");
            Console.WriteLine("left ${item.left.value}");
            Console.WriteLine("right ${item.right.value}");
            Console.WriteLine("top ${item._top.value}");
            Console.WriteLine("bottom ${item.bottom.value}");
            if(item.anchors.left!=null) Console.WriteLine($"[anchors.left]={item.anchors.left.obj._id}.{item.anchors.left.name}({item.anchors.left._value}). RelativeToParent: {item.anchors.left.relativeToParent}");
            if(item.anchors.right!=null) Console.WriteLine($"[anchors.right]={item.anchors.right.obj._id}.{item.anchors.right.name}({item.anchors.right._value}). RelativeToParent: {item.anchors.right.relativeToParent}");
            if(item.anchors.top!=null) Console.WriteLine($"[anchors.top]={item.anchors.top.obj._id}.{item.anchors.top.name}({item.anchors.top._value}). RelativeToParent: {item.anchors.top.relativeToParent}");
            if(item.anchors.bottom!=null) Console.WriteLine($"[anchors.bottom]={item.anchors.bottom.obj._id}.{item.anchors.bottom.name}({item.anchors.bottom._value}). RelativeToParent: ${item.anchors.bottom.relativeToParent}");
            if(item.anchors.horizontalCenter!=null) Console.WriteLine($"[anchors.horizontalCenter]={item.anchors.horizontalCenter.obj._id}.{item.anchors.horizontalCenter.name}({item.anchors.horizontalCenter._value}). RelativeToParent: {item.anchors.horizontalCenter.relativeToParent}");
            if(item.anchors.verticalCenter!=null) Console.WriteLine($"[anchors.verticalCenter]={item.anchors.verticalCenter.obj._id}.{item.anchors.verticalCenter.name}({item.anchors.verticalCenter._value}). RelativeToParent: {item.anchors.verticalCenter.relativeToParent}");
            Console.WriteLine("${itemID} end dimensions==================");
        }
        #endregion
      */  
    }
}
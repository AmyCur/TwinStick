using System;

namespace Inventory{
    [Serializable]
    public class InventoryClickedData{
        public int index;
        public Type type;

        public InventoryClickedData(int index, Type type){
            this.index=index;
            this.type=type;
        }
    } 
}
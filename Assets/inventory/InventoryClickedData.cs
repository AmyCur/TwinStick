using System;

namespace Inventory{
    [Serializable]
    public class InventoryClickedData{
        public int index;
        public Type type;
        public Item item;

        public InventoryClickedData(int index, Type type){
            this.index=index;
            this.type=type;
        }
    } 
}
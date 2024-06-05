using System.Collections.Generic;
using System.ComponentModel;
using ScriptableObjects;


namespace NativeClasses
{
    public class InventoryService
    {
        #region SingletonPattern
            private static readonly InventoryService Instance = new InventoryService();

            public static InventoryService GetInstance()
            {
                return Instance;
            }
        #endregion


        #region Private Assets references
            private readonly InventoryScriptableObject _inventoryScriptableObject;
        #endregion

        private InventoryService()
        {
            _inventoryScriptableObject =
                ResourceLoader.Load<InventoryScriptableObject>("Scriptable Objects/Game Systems/Inventory/PlayerInventory");
        }

        public bool HasSpace()
        {
            return _inventoryScriptableObject.HasSpace();
        }

        public Dictionary<InventoryItem, int> GetInventory()
        {
            return _inventoryScriptableObject.GetFullDictionary();
        }
        
        public void AddItem(InventoryItem item, int amount)
        {
            switch (_inventoryScriptableObject.AddItem(item, amount))
            {
                case 0:
                case -1:
                case -2:
                    break;
                default:
                    return;
            }
        }

        public void ConsumeItem(InventoryItem item, int amount)
        {
            switch (_inventoryScriptableObject.ConsumeItem(item, amount))
            {
                case 0:
                case -1:
                case -2:
                        break;
                default:
                    return;
            }
        }

        public bool ValidateConsumable(InventoryItem item)
        {
            return (_inventoryScriptableObject.ItemAmount(item) > 0);
        }
    }
}

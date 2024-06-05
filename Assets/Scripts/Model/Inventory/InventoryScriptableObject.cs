using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Scriptables/InventoryScriptableObject")]
    public class InventoryScriptableObject : ScriptableObject
    {
        private readonly Dictionary<InventoryItem, int> _inventoryDictionary = new Dictionary<InventoryItem, int>();
        [Header("Inventory proprieties")]
        [SerializeField] private int maxInventorySize = 32;
        [SerializeField] private int maxStackSize = 64;
        
        
            
        public int AddItem(InventoryItem item,int amount = 1)
        {
            if (_inventoryDictionary.TryGetValue(item, out var value))
            {
                if (value.Equals(maxStackSize))
                {
                    Debug.Log("Cannot hold more than " + maxStackSize + "  Duplicated items");
                    return -2;
                }

                if (_inventoryDictionary[item] + amount > maxStackSize)
                {
                    _inventoryDictionary[item] = maxStackSize;
                    return amount - maxStackSize;
                };
                _inventoryDictionary[item] += amount;
                return amount;
            }
            
            if (!HasSpace())
            {
                Debug.Log("Inventory full");
                return -1;
            }
            _inventoryDictionary.Add(item,amount);
            return amount;
        }

        public int ConsumeItem(InventoryItem item, int amount = 1)
        {
            if (_inventoryDictionary.TryGetValue(item, out var value))
            {
                if (value < amount)
                {
                    Debug.Log("Cannot consume " + amount + " Itens, inventory only contains " + value + " Items");
                    return -2;
                }

                if (value - amount == 0)
                {
                    Debug.Log("All copies of the item were used!");
                    _inventoryDictionary.Remove(item);
                    return amount;
                }
                _inventoryDictionary[item] -= value;
            }
            Debug.Log("Item not found in inventory");
            return -1;
        }
        
        public int ItemAmount(InventoryItem item)
        {
            return _inventoryDictionary.GetValueOrDefault(item, 0);
        }

        
        public bool HasSpace()
        {
            return (_inventoryDictionary.Keys.Count < maxInventorySize && _inventoryDictionary.Keys.Count >= 0);
        }

        public Dictionary<InventoryItem, int> GetFullDictionary()
        {
            return _inventoryDictionary;
        }
    }
}
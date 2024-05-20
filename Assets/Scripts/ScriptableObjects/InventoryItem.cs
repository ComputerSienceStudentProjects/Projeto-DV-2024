using System;
using UnityEngine;

namespace ScriptableObjects
{
    public enum ItemType
    {
        NotDefined,
        Consumable,
        Armor,
        Weapon
    }
    
    [CreateAssetMenu(menuName = "Scriptables/InventoryItem")]
    public class InventoryItem : ScriptableObject
    {
        [Header("Inventory Proprieties")]
        [SerializeField] private GameObject itemObjectReference = default;
        [SerializeField] private Texture2D itemIcon;
        [SerializeField] private ItemType itemType = ItemType.NotDefined;
        [SerializeField] private ItemConsumableEffect consumableEffect;

        private void OnEnable()
        {
            itemIcon = Texture2D.redTexture;
        }

        public Texture2D GetIcon()
        {
            return itemIcon;
        }

        public ItemType GetItemType()
        {
            return itemType;
        }

        public GameObject GetItemObject()
        {
            return itemObjectReference;
        }

        public ItemConsumableEffect GetEffects()
        {
            return consumableEffect;
        }
    }
}
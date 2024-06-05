using UnityEngine;

namespace ScriptableObjects
{
    public class ItemConsumableEffect : ScriptableObject
    {
        public int hpToAdd = 1;
        public int maxHpToAdd = 1;
        public int defToAdd = 1;
        public int attackToAdd = 1;
        public bool resurectCharacter = false;
    }
}
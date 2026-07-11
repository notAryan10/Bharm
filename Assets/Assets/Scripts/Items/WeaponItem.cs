using UnityEngine;

namespace SG {
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject weaponPrefab;
        public bool isUnarmed;
        
    }
}
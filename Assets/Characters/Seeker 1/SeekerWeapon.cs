using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerWeapon : MonoBehaviour
{
    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponSheath;

    private GameObject _currentWeaponInHand;
    private GameObject _currentWeaponInSheath;

    void Start()
    {
        _currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
    }

    public void DrawWeapon()
    {
        _currentWeaponInHand = Instantiate(weapon, weaponHolder.transform);
        Destroy(_currentWeaponInSheath);
    }

    public void StoreWeapon()
    {
        _currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
        Destroy(_currentWeaponInHand);
    }


}

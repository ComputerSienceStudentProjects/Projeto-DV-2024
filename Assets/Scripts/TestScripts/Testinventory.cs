using System;
using System.Collections;
using System.Collections.Generic;
using NativeClasses;
using ScriptableObjects;
using UnityEngine;

public class Testinventory : MonoBehaviour
{
    public InventoryItem itemToAdd;
    private InventoryService  _inventoryService;
    private string ammount = "1";

    private void OnGUI()
    {
        ammount = GUI.TextField(new Rect(10, 130, 200, 50), ammount,25);
        if (GUI.Button(new Rect(10, 10, 200, 50), "List Inventory"))
        {
            foreach (var item in _inventoryService.GetInventory())
            {
                Debug.Log("Item -> " + item.Key + ", value -> " + item.Value);
            }
        }

        if (GUI.Button(new Rect(10, 75, 200, 50), "Add Item to inv"))
        {
            _inventoryService.AddItem(itemToAdd,int.Parse(ammount));
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        _inventoryService = InventoryService.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

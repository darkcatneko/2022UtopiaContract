using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBackPack : MonoBehaviour
{
    public InventoryObject inventory;
    public ItemObject[] starterPack;
    private void Awake()
    {        
    }
    private void Start()
    {
        inventory.Load();
        inventory.EmptyFarmLoad();
    }
    public void AddItemInBackPack(ItemObject item, int _amount)
    {        
        inventory.AddItem(new TrueItem(item), _amount);
        //ItemBarDisplay.instance.OnLoad();
    }
    private void OnApplicationQuit()
    { 
        inventory.Clear();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {            
            AddStarterItem();          
        }
    }
    public void AddStarterItem()
    {
        for (int i = 0; i < starterPack.Length; i++)
        {
            AddItemInBackPack(starterPack[i], 1);
        }
    }
    public void SaveButton()
    {
        InGameTime.instance.TimeSave(inventory.TimeData);//¦s®É¶¡
        inventory.Save();
    }
}

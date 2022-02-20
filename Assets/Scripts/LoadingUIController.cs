using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LoadingUIController : MonoBehaviour
{
    [SerializeField] GameObject Havefile;[SerializeField] GameObject DontHaveFile;
    [SerializeField]
    public InventoryObject inventory;
    public ItemObject[] starterPack;
    [SerializeField] string savePath;
    private void Start()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            Havefile.SetActive(true);
            DontHaveFile.SetActive(false);
        }
        else
        {
            Havefile.SetActive(false);
            DontHaveFile.SetActive(true);
        }
    }
    public void AddItemInBackPack(ItemObject item, int _amount)
    {
        inventory.AddItem(new TrueItem(item), _amount);
    }
    private void OnApplicationQuit()
    {
        inventory.Clear();
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            InGameTime.instance.TimeSave(inventory.TimeData);//¦s®É¶¡
            inventory.Save();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            inventory.Load();
            inventory.EmptyFarmLoad();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            inventory.Clear();
            AddStarterItem();
            inventory.SaveEmpty();
        }
    }
    public void AddStarterItem()
    {
        for (int i = 0; i < starterPack.Length; i++)
        {
            AddItemInBackPack(starterPack[i], 1);
        }
    }
    public void NewSaveButton()
    {
        inventory.Clear();
        AddStarterItem();
        inventory.SaveEmpty();
        SceneManager.LoadScene(1);
    }
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
}

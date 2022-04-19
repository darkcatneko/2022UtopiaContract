using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using UnityEngine.SceneManagement;
[CreateAssetMenu(fileName = "New Inventory",menuName ="Inventory System/Inventory")]

public class InventoryObject : ScriptableObject,ISerializationCallbackReceiver
{
    // 之後更新 要記的clear



    public string savePath;
    private ItemDatabaseObject data;
    public GameTimeData TimeData;
    public Player_BattleStatus BattleStatus;
    public List<PlantIdentity> emptyfarmData = new List<PlantIdentity>();
    public InventorySlot[] Container = new InventorySlot[20];

    private void OnEnable()
    {
#if UNITY_EDITOR
        data = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabaseObject));
#else
        data = Resources.Load<ItemDatabaseObject>("Database");
#endif
    }
    public ItemObject GetItem(int i)
    {
       return data.GetItem[i];
    }
    public void EmptyFarmLoad()
    {
        GameObject[] EmptyFarms;
        EmptyFarms = GameObject.FindGameObjectsWithTag("Emptyfarm");
        for (int i = 0; i < EmptyFarms.Length; i++)
        {
            for (int j = 0; j < EmptyFarms.Length; j++)
            {
                if (EmptyFarms[j].GetComponent<EmptyFarmSpace>().PlantSaveFile.plantspaceID == i)
                {
                    EmptyFarms[j].GetComponent<EmptyFarmSpace>().PlantIdentityUpdate(emptyfarmData[i]);
                    EmptyFarms[j].GetComponent<EmptyFarmSpace>().FarmReload();                
                }
            }
        }
    }
    public void EmptyFarmListSave()
    {
        emptyfarmData = new List<PlantIdentity>();
        GameObject[] EmptyFarms;
        EmptyFarms = GameObject.FindGameObjectsWithTag("Emptyfarm");
        for (int i = 0; i < EmptyFarms.Length; i++)
        {
            for (int j = 0; j < EmptyFarms.Length; j++)
            {
                if (EmptyFarms[j].GetComponent<EmptyFarmSpace>().PlantSaveFile.plantspaceID == i)
                {
                emptyfarmData.Add(EmptyFarms[j].GetComponent<EmptyFarmSpace>().PlantSaveFile);
                }
            }
        }
    }



    public void AddItem(TrueItem _item,int _amount)
    {
        if (_item.type == ItemType.Potion)
        {
            SetEmptySlot(_item, _amount);
            return;
        }     
        if (_item.buffs.Length > 0)
        {
            //Container.Add(new InventorySlot(_item.Id, _item, _amount));
            SetEmptySlot(_item, _amount);
            return;
        }
        for (int i = 0; i < Container.Length; i++)
        {
            if (Container[i].ID == _item.Id)
            {
                Container[i].AddAmount(_amount);
                return;
            }
        }
        //Container.Add(new InventorySlot(_item.Id, _item, _amount));
        SetEmptySlot(_item, _amount);
    }
    public void AddNamedPotion(TrueItem _item, int _amount,string name)
    {
        if (_item.type == ItemType.Potion)
        {
            SetEmptySlot_NamedOBJ(_item, _amount,name);
            return;
        }
        SetEmptySlot(_item, _amount);
    }
    public InventorySlot SetEmptySlot(TrueItem _item,int _amount)
    {
        for (int i = 0; i < Container.Length; i++)
        {
            if (Container[i].ID<=-1)
            {
               Container[i].UpdateSlot(_item.Id,_item,_amount);
               return Container[i];
            }
        }
        //set up if inventory is full
        return null;
    }
    public InventorySlot SetEmptySlot_NamedOBJ(TrueItem _item, int _amount,string named)
    {
        for (int i = 0; i < Container.Length; i++)
        {
            if (Container[i].ID <= -1)
            {
                Container[i].UpdateSlot(_item.Id, _item, _amount);
                Container[i].Player_named = named;
                return Container[i];
            }
        }
        //set up if inventory is full
        return null;
    }
    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.ID, item2.item, item2.amount);
        item2.UpdateSlot(item1.ID, item1.item, item1.amount);
        item2.Player_named = item1.Player_named;
        item1.UpdateSlot(temp.ID, temp.item, temp.amount);
        item1.Player_named = temp.Player_named;
    }
    public void MinusAmount(TrueItem _item,int Amount)
    {
        for (int i = 0; i < Container.Length; i++)
        {
            if (Container[i].item == _item)
            {
                if (Container[i].amount-Amount<=0)
                {
                    RemoveItem(_item);
                }
                else
                {
                    Container[i].UpdateSlot(_item.Id, _item, Container[i].amount - Amount);
                }                
            }
        }
    }
    public void RemoveItem(TrueItem _item)
    {
        for (int i = 0; i < Container.Length; i++)
        {
            if (Container[i].item == _item)
            {
                Container[i].UpdateSlot(-1, null, 0);
            }
        }
    }
    [ContextMenu("Save")]
    public void Save()
    {        
        if (SceneManager.GetActiveScene().buildIndex==1)
        {
            EmptyFarmListSave();
        }
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        Debug.Log(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }
    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(),this);
            ItemBarDisplay.instance.OnLoad();            
            file.Close();
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        Debug.Log(124);
        for (int i = 0; i < Container.Length; i++)
        {
            Container[i].Reset();
        }
        emptyfarmData = new List<PlantIdentity>();
        for (int i = 0; i < 6; i++)
        {
            emptyfarmData.Add(new PlantIdentity(PlantState.seed, WhichPlant.EmptySpace, i, false));
        }
        TimeData.GAMEDAY = 1;
        TimeData.ENERGYWASTE = 0;
        TimeData.PASSSEC = 420 * 60;
        BattleStatus = new Player_BattleStatus(1, 20, 20);
    }
    [ContextMenu("SaveEmpty")]
    public void SaveEmpty()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Container.Length; i++)
        {
            if (Container[i].ID>0)
            {
                Container[i].item =new TrueItem(data.GetItem[Container[i].ID]);
            }           
        }
    }

    public void OnBeforeSerialize()
    {
        
    }
}
[System.Serializable]
public class InventorySlot
{
    public ItemType[] AllowedType = new ItemType[0];
    public UserInterface parent;
    public int ID;
    public TrueItem item;
    public string Player_named;
    public int amount;
    public InventorySlot(int _id, TrueItem _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public InventorySlot()
    {
        ID = -1;
        item = null;
        amount = 0;
    }
    public void Reset()
    {
        ID = -1;
        item = null;
        amount = 0;
    }
    public void UpdateSlot(int _id, TrueItem _item, int _amount)
    {       
            ID = _id;
            item = _item;
            amount = _amount;
       
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
    public bool CanPlaceInSlot(ItemObject _item)
    {
        if (AllowedType.Length<=0)
        {
            return true;
        }
        for (int i = 0; i < AllowedType.Length; i++)
        {
            if (_item.type == AllowedType[i])
            {
                return true;
            }
        }
        return false;
    }
}
[System.Serializable]
public class Player_BattleStatus
{
    public int Player_Level = 1;
    public int Player_Health = 20;
    public int Player_MaxHealth = 20;
    public void Level_change(int lv)
    {
        Player_Level = lv;
    }
    public void Max_health_Change(int i)
    {
        Player_MaxHealth = i;
    }
    public void health_change(int change)
    {
        Player_Health = change;
    }
    public Player_BattleStatus(int _lv, int hp, int maxhp)
    {
        Player_Level = _lv;
        Player_Health = hp;
        Player_MaxHealth = maxhp;
    }
}
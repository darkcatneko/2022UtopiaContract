using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerAttributes
{
    HP,
    VIT,
    INT,
    DEF,
    MND,
    AGI,
    LUK,
}
public enum ItemType
{
    Potion,
    Seed,
    Default
}
public abstract class ItemObject : ScriptableObject
{
    public Sprite sprite;
    public ItemType type;
    public int Item_ID;
    [TextArea(15,20)]
    public string description;
    public ItemBuff[] buffs;

    public TrueItem CreateItem()
    {
        TrueItem newItem = new TrueItem(this);
        return newItem;
    }
}
[System.Serializable]
public class TrueItem
{
    public string Name;
    public int Id;
    public ItemType type;
    public ItemBuff[] buffs;
    public TrueItem(ItemObject _item)
    {
        Name = _item.name;
        type = _item.type;
        Id = _item.Item_ID;
        buffs = new ItemBuff[_item.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(_item.buffs[i].min, _item.buffs[i].max);
            buffs[i].attribute = _item.buffs[i].attribute;
        }
}
}
[System.Serializable]
public class ItemBuff
{
    public PlayerAttributes attribute;
    public int value;
    public int min;
    public int max;
    public ItemBuff(int _min,int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }
    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }
}

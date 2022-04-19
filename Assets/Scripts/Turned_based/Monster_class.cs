using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Monster_class
{
    public int Monster_ID;//之後要用scriptable object改
    public int Monster_MaxHP;
    public string Monster_name;
    public int Monster_Lv;
    public int Monster_Health;

    public Monster_class(string _name,int _Lv, int _hp,int _id,int Maxhp)
    {
        Monster_MaxHP = Maxhp;
        Monster_ID = _id;
        Monster_name = _name;
        Monster_Lv = _Lv;
        Monster_Health = _hp;
    }
    public bool change_health(int result)
    {
        Monster_Health = result;
        if (Monster_Health <= 0)
        {
            return true;
        }
        return false;
    }
}

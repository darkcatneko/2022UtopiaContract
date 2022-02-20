using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string Name;
}

public class PlayerSaveData
{
    public Vector3 PlayerPosition;
    public PlantState[] PlantStates;


    public PlayerSaveData(Vector3 playerpos, PlantState[] plantStates)
    {
        PlayerPosition = playerpos;
        PlantStates = plantStates;
    }
}

public class backpacktest : MonoBehaviour
{
    public static backpacktest instance;
    private void Awake()
    {
        instance = this;
    }

    

    public ItemData[] itemData;




    [SerializeField] PlantPerform[] PlantPerforms;

    void Start()
    {
        Vector3 Playerpos = transform.position;

        //PlantState[] SavePlantState = GetPlantState();

        //PlayerSaveData playerSaveData = new PlayerSaveData(Playerpos, SavePlantState);

        //§âplayerSaveData ¦s¶ijson
    }

    //PlantState[] GetPlantState()
    //{
    //    //PlantState[] plantStates = new PlantState[PlantPerforms.Length];

    //    //for(int i = 0; i < plantStates.Length; i++)
    //    //{
    //    //    plantStates[i] = PlantPerforms[i].plantState;
    //    //}

    //    //return plantStates;
    //}
}

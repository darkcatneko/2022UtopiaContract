using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum PlantState
{
    seed,
    young,
    mature,
    grown,
}

public enum WhichPlant
{
    EmptySpace,
    cabbage,
    tomato,
    corn,
}
[System.Serializable]
public class PlantIdentity
{
   public int plantspaceID;
   public bool Is_fertilize;
   public PlantState plantState;
   public WhichPlant which;
   public PlantIdentity(PlantState plant_state, WhichPlant _which,int id,bool is_fertilize)
    {
        Is_fertilize = is_fertilize;
        plantState = plant_state;
        which = _which;
        plantspaceID = id;
    }
}

public class PlantPerform : MonoBehaviour
{
    public Material SeedMat;
    public Material YoungMat;
    public Material MatureMat;
    public Material grownMat;

    public PlantIdentity This_Plant;
    void Update()
    {        
        
    }

    public void TimePass()
    {
        if (This_Plant.Is_fertilize == true)
        {
            This_Plant.plantState++;
            PlantUpdate();
            Mathf.Clamp((int)This_Plant.plantState, 0, 3);
            This_Plant.Is_fertilize = false;
            this.GetComponent<CabbageInteract>().GetSameEmptyFarm().GetComponent<EmptyFarmSpace>().PlantIdentityUpdate(This_Plant);
        }        
    }

    public int GetPlantState()
    {
        int state;
        state = (int)This_Plant.plantState;
        return state;
    }

    public void PlantUpdate()
    {
        switch ((int)This_Plant.plantState)
        {
            case 0:
                this.gameObject.GetComponent<MeshRenderer>().material = SeedMat;
                break;
            case 1:
                this.gameObject.GetComponent<MeshRenderer>().material = YoungMat;
                break;
            case 2:
                this.gameObject.GetComponent<MeshRenderer>().material = MatureMat;
                break;
            case 3:
                this.gameObject.GetComponent<MeshRenderer>().material = grownMat;
                break;

        }
    }
    public void SetPlantIdentity(PlantState plant_state, WhichPlant _which,int id,bool is_fertilize)
    {
        This_Plant = new PlantIdentity(plant_state, _which,id, is_fertilize);
    }
}

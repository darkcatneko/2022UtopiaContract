using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster_Area : MonoBehaviour
{
    Monster_class encounter_monster; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {            
            encounter_monster = new Monster_class("IceMaker", 1, 20,0,20);
            GameObject.Find("Monster_encounter_data").GetComponent<On_encounter>().Monster = encounter_monster;
            SceneManager.LoadScene(3);
        }
    }    
}

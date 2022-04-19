using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class On_encounter : MonoBehaviour
{
   public Monster_class Monster;
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}

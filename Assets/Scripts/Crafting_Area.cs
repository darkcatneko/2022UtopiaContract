using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting_Area : MonoBehaviour
{
    bool Incollider;
    Collider player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Incollider = true;
            player = other;
        }
    }
    private void OnTriggerExit(Collider others)
    {
        if (others.gameObject.CompareTag("Player"))
        {
            Incollider = false;
            player = null;           
        }
    }
    private void Update()
    {
        if (Incollider == true && Input.GetKeyDown(KeyCode.F))
        {
            UI_Controller.instance.CraftingOpen();
        }
    }
}

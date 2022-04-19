using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Base_TO_RPG : MonoBehaviour
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
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Incollider == true && Input.GetKeyDown(KeyCode.F) && UI_Controller.instance.player.playerState == PlayerState.FreeMove)
        {
            player.GetComponentInParent<PlayerBackPack>().SaveButton();
            SceneManager.LoadScene(2);
        }
    }
}

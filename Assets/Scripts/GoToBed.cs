using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToBed : MonoBehaviour
{
    public AudioClip MorningSF;
    public AudioClip NightSF;
    AudioSource audioSource;
    public GameObject Sign;
    public GameObject CanvasLayer1;
    bool Incollider;
    Collider player;    
    public InventoryObject playerInventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Incollider = true;
            player = other;
            playerInventory = player.GetComponentInParent<PlayerBackPack>().inventory;
            Sign.SetActive(true);
        }
    }
    private void Update()
    {
        if (Incollider == true)
        {
            if (Input.GetKeyDown(KeyCode.F) && player.GetComponentInParent<PlayerMovement>().playerState == PlayerState.FreeMove)
            {
                InGameTime.instance.TimeForBed();//stop the clock 
                player.GetComponentInParent<PlayerMovement>().IM_Sleeping();
                audioSource = GameObject.FindGameObjectWithTag("system").GetComponent<AudioSource>();
                audioSource.PlayOneShot(NightSF, 1f * GameObject.FindGameObjectWithTag("system").GetComponent<BGM_Center>().volume);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Incollider = false;
            player = null;
            Sign.GetComponentInChildren<Animator>().SetBool("Out",true);
            StartCoroutine(Delay.DelayToInvokeDo(() => { Sign.GetComponentInChildren<Animator>().SetBool("Out", false); Sign.SetActive(false); }, 0.5f));
        }
    }

    public void AllPlantGrow()
    {
        Debug.Log("growing time");
        GameObject[] Plants;
        Plants = GameObject.FindGameObjectsWithTag("Plant");
        foreach (var item in Plants)
        {
            item.GetComponent<PlantPerform>().TimePass();
            item.GetComponent<CabbageInteract>().Defertilize();
        }
    }
    void wakePart2()
    {
        CanvasLayer1.GetComponent<Animator>().SetBool("DayTime", true);

    }
    public void WakeUpButtonVer2()
    {
        Debug.Log("bruh");
        audioSource = GameObject.FindGameObjectWithTag("system").GetComponent<AudioSource>();
        audioSource.PlayOneShot(MorningSF, 0.1f * GameObject.FindGameObjectWithTag("system").GetComponent<BGM_Center>().volume);
        InGameTime.instance.WakeUpButton();
        wakePart2();
        InGameTime.instance.TimeSave(playerInventory.TimeData);//¦s®É¶¡
        playerInventory.Save();
        StartCoroutine(Delay.DelayToInvokeDo(() => 
        {

            GameObject.FindGameObjectsWithTag("Player")[0].GetComponentInParent<PlayerMovement>().playerState = PlayerState.FreeMove;
            CanvasLayer1.SetActive(false);
        }
        , 2.5f));
    }
}

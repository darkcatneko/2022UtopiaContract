using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{
    public static UI_Controller instance;

    public Text Daytext;
    public Text Timetext;

    public GameObject DayCycleUI;
    public GameObject PlayerStatusUI;

    [SerializeField] GameObject MenuBar;

    public GameObject BackPack;
    public PlayerMovement player;
    public Sprite[] DayCircle;
    public Sprite[] Player_status;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        Daytext.text = "4 / " + InGameTime.instance.GameDay.ToString();
        PlayerStatusUpdate();
        DaycycleUIUpdate();
        if (Input.GetKeyDown(KeyCode.B) )
        {
            if ( BackPack.activeSelf == false && player.playerState == PlayerState.FreeMove)
            {
                BackPack.SetActive(true);
                player.movement.x = 0;
                player.movement.z = 0;
                player.setAnimate();
                player.playerState = PlayerState.BackpackChoosing;
            }
            else if (BackPack.activeSelf == true)
            {
                BackPack.SetActive(false);
                player.playerState = PlayerState.FreeMove;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if ( BackPack.activeSelf == true)
            {
                BackPack.SetActive(false);
                player.playerState = PlayerState.FreeMove;
            }                
        }
    }
    public void StopPressed()
    {
        MenuBar.SetActive(true);
        player.playerState = PlayerState.MenuOpening;
        Time.timeScale = 0f;        
    }
    public void Resume()
    {
        MenuBar.SetActive(false);
        Time.timeScale = 1f;
        if (BackPack.activeSelf)
        {
            player.playerState = PlayerState.BackpackChoosing;
        }
        else
        { player.playerState = PlayerState.FreeMove; }

    }
    public void Quit()
    {
        Application.Quit();
    }
    public void BackOpen()
    {
        if (BackPack.activeSelf == true)
        {
            BackPack.SetActive(false);
            player.playerState = PlayerState.FreeMove;
        }
        else if(BackPack.activeSelf == false && player.playerState == PlayerState.FreeMove)
        {
            BackPack.SetActive(true);
            player.movement.x = 0;
            player.movement.z = 0;
            player.setAnimate();
            player.playerState = PlayerState.BackpackChoosing;
        }
    }
    public void PlayerStatusUpdate()
    {
        switch (InGameTime.instance.EnergyWaste)
        {
            case 0:
                PlayerStatusUI.GetComponent<Image>().sprite = Player_status[0];
                return;
            case 1:
                PlayerStatusUI.GetComponent<Image>().sprite = Player_status[1];
                return;
            case 2:
                PlayerStatusUI.GetComponent<Image>().sprite = Player_status[2];
                return;
        }
    }
    public void DaycycleUIUpdate()
    {
        if (InGameTime.instance.PassMin < 900)
        {
            DayCycleUI.GetComponent<Image>().sprite = DayCircle[0];
        }
        else if (InGameTime.instance.PassMin == 1020)
        {
            this.GetComponentInChildren<Animator>().enabled = true;
            StartCoroutine(Delay.DelayToInvokeDo(() => { DayCycleUI.GetComponent<Image>().sprite = DayCircle[1]; }, 0.5f));
            StartCoroutine(Delay.DelayToInvokeDo(() => { this.GetComponentInChildren<Animator>().enabled = false; }, 1f));

        }
        else if(InGameTime.instance.PassMin > 1020 && InGameTime.instance.PassMin < 1140)
        {
            DayCycleUI.GetComponent<Image>().sprite = DayCircle[1];

        }
        else if (InGameTime.instance.PassMin == 1140)
        {
            this.GetComponentInChildren<Animator>().enabled = true;
            StartCoroutine(Delay.DelayToInvokeDo(() => { DayCycleUI.GetComponent<Image>().sprite = DayCircle[2]; }, 0.5f));
            StartCoroutine(Delay.DelayToInvokeDo(() => { this.GetComponentInChildren<Animator>().enabled = false; }, 1f));
        }
        else if (InGameTime.instance.PassMin > 1140)
        {
            DayCycleUI.GetComponent<Image>().sprite = DayCircle[2];
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameTimeData
{
   public int GAMEDAY;
   public int ENERGYWASTE;
    public int PASSSEC;
    public  GameTimeData(int _day,int energy,int sec)
    {
        PASSSEC = sec;
        GAMEDAY = _day;
        ENERGYWASTE = energy;
    }
}
public class InGameTime : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip NightSF;
    public InventoryObject data;
    [SerializeField] GameObject JR;
    [SerializeField] GameObject JRBack;
    [SerializeField] GameObject Light;
    [SerializeField]
    private bool TimeToWake = false;
    [SerializeField]
    private GoToBed gotobed;

    public GameObject CanvasLayer1;
    public GameObject gameText;

    public Text text;
    public int PassSec = 420 * 60;
    public int PassMin;
    public int Hour;
    public int Min;
    public int Sec;

    public int RealTimePass = 30;

    public int TimeToGoToBed = 1500 ;

    public int GameDay;

    public int EnergyWaste;    

    public static InGameTime instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        data.Load();
        TimeLoad(data.TimeData);
        Start_A_New_Day(GameDay,EnergyWaste,0);
        TimeLoad(data.TimeData);
        TimeTranslate();
        DisPlayTime();
        Light.GetComponent<EnviormentalLight>().UpdateLight();
        Light.GetComponent<EnviormentalLight>().UpdateLightStrong();
        JR.GetComponent<PlayerSprtieColorChange>().PlayerColorChange();
        JRBack.GetComponent<PlayerSprtieColorChange>().PlayerColorChange();
    }
    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {
            PassSec = 54000;
            RealTimePass = 240;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            PassSec = 61200;
            RealTimePass = 240;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            PassSec = 22*3600;
            RealTimePass = 240;
        }
    }

    void PlusGameTime()
    {
        PassSec += RealTimePass;
        PassSec = Mathf.Clamp(PassSec, 420 * 60, 1500 * 60);
        PassMin = PassSec / 60;
        PassMin = Mathf.Clamp(PassMin, 420, 1500);
        Light.GetComponent<EnviormentalLight>().UpdateLight();
        Light.GetComponent<EnviormentalLight>().UpdateLightStrong();
        JR.GetComponent<PlayerSprtieColorChange>().PlayerColorChange();
        JRBack.GetComponent<PlayerSprtieColorChange>().PlayerColorChange();
        TimeTranslate();
        DisPlayTime();
    }
    void DisPlayTime()
    {
        if (Min < 10 && Hour != 25)
        {
            text.text = (Hour + ":0" + Min);
        }
        else if (Hour == 25)
        {
            text.text = ("1" + ":0" + Min);
        }
        else
        {
            text.text = (Hour +":"+ Min);
        }        
    }
    public void Start_A_New_Day(int gameday,int EnergyWaste,int dayPassed)
    {
        GameDay = gameday+dayPassed;
        PassSec = 420 * 60 + EnergyWaste * 7200;
        PassMin = PassSec / 60;
        PassMin = Mathf.Clamp(PassMin, 420, 1500);
        TimeTranslate();
        DisPlayTime();
        InvokeRepeating("PlusGameTime", 1f, 1f);
    }
    public void TimeForBed()//for Bed code
    {
        CancelInvoke();
        Debug.Log("TimeForBed");
        TimeToWake = true;
        CanvasLayer1.SetActive(true);
        gameText.GetComponent<Text>().text = InGameTime.instance.GameDay.ToString() + " >> " + (InGameTime.instance.GameDay + 1).ToString();
    }
    void ToTired()
    {
        EnergyWaste++;
        if (EnergyWaste == 3)
        {
            TimeForBed();
            JR.GetComponentInParent<PlayerMovement>().IM_Sleeping();
            audioSource = GameObject.FindGameObjectWithTag("system").GetComponent<AudioSource>();
            audioSource.PlayOneShot(NightSF, 1f * GameObject.FindGameObjectWithTag("system").GetComponent<BGM_Center>().volume);
        }
    }
    void TimeTranslate()
    {
        if (PassMin <= 1440)
        {
            Hour = PassMin / 60;
            Min = PassMin % 60;
            Sec = PassSec % 60;
            if (PassMin == 1440)
            {
                ToTired();                
            }
        }
        else if (PassMin < 1500)
        {
            Hour = 24;
            Min = PassMin - 1440;
            Sec = PassSec % 60;
        }
        else if (PassMin == 1500)
        {
            Hour = 25;
            Min = 0;
            Sec =0;
            DisPlayTime();
            ToTired();
            TimeForBed();
            JR.GetComponentInParent<PlayerMovement>().IM_Sleeping();
            audioSource = GameObject.FindGameObjectWithTag("system").GetComponent<AudioSource>();
            audioSource.PlayOneShot(NightSF, 1f * GameObject.FindGameObjectWithTag("system").GetComponent<BGM_Center>().volume);
        }
    }
    public void WakeUpButton()
    {
        if (TimeToWake)
        {
            if (Hour<23)
            {
                EnergyWaste = 0;
                gotobed.AllPlantGrow();
                Start_A_New_Day(GameDay,0,1);
                TimeToWake = false;
                Light.GetComponent<EnviormentalLight>().ResetLight();
                JR.GetComponent<PlayerSprtieColorChange>().PlayerColorReset();
                JRBack.GetComponent<PlayerSprtieColorChange>().PlayerColorReset();
            }
            else
            {
                gotobed.AllPlantGrow();
                Start_A_New_Day(GameDay,EnergyWaste,1);
                if (EnergyWaste == 3)
                {
                    EnergyWaste = 0;                }
                TimeToWake = false;
                Light.GetComponent<EnviormentalLight>().ResetLight();
                JR.GetComponent<PlayerSprtieColorChange>().PlayerColorReset();
                JRBack.GetComponent<PlayerSprtieColorChange>().PlayerColorReset();
            }
        }
    }
    public void TimeSave(GameTimeData _data)
    {
        _data.GAMEDAY = GameDay;
        _data.ENERGYWASTE = EnergyWaste;
        _data.PASSSEC = PassSec;
    }
    public void TimeLoad(GameTimeData _data)
    {
        PassSec = _data.PASSSEC;
        GameDay = _data.GAMEDAY;
        EnergyWaste = _data.ENERGYWASTE;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviormentalLight : MonoBehaviour
{
    [SerializeField] GameObject System;

    void Start()
    {
        
    }

    
    void Update()
    {

    }
    public void UpdateLight()
    {
        if (System.GetComponent<InGameTime>().PassMin >=900 && System.GetComponent<InGameTime>().PassMin <=1020)
        {
            this.GetComponent<Light>().color = new Color(1f, (1 - ((1 - 0.73f) / 120) * (System.GetComponent<InGameTime>().PassMin - 900)), (1 - ((1 - 0.25f) / 120) * (System.GetComponent<InGameTime>().PassMin - 900)));
        }
        else if (System.GetComponent<InGameTime>().PassMin > 1020)
        {            
            this.GetComponent<Light>().color = new Color(1f, 0.73f, 0.25f, 1f);
        }
        else 
        {
            this.GetComponent<Light>().color = new Color(1f, 1f, 1f, 1f);
        }
    }
    public void UpdateLightStrong()
    {
        if ((System.GetComponent<InGameTime>().PassMin >= 1020 && System.GetComponent<InGameTime>().PassMin < 1140))
        {
            this.GetComponent<Light>().intensity = (6f - (6f / 120f * (System.GetComponent<InGameTime>().PassMin - 1020f)));
        }
        else if (System.GetComponent<InGameTime>().PassMin >= 1140)
        {
            this.GetComponent<Light>().intensity = 0;
        }
        else
        {
            this.GetComponent<Light>().intensity = 6;
        }
    }
    public void ResetLight()
    {
        this.GetComponent<Light>().intensity = 6;
        this.GetComponent<Light>().color = new Color(1, 1, 1, 1);
    }
}

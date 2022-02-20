using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprtieColorChange : MonoBehaviour
{
    [SerializeField] GameObject System;
    public SpriteRenderer[] sprites;
    private void Start()
    {
        sprites  = this.GetComponentsInChildren<SpriteRenderer>();
        if (this.gameObject.name == "JR back")
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayerColorChange()
    {
        if ((System.GetComponent<InGameTime>().PassMin>=1020 && System.GetComponent<InGameTime>().PassMin < 1140))
        {
            foreach (var item in sprites)
            {
                item.color = new Color(1f - (1f - 0.5f) / 120 * (System.GetComponent<InGameTime>().PassMin - 1020), 1f - (1f - 0.5f) / 120f * (System.GetComponent<InGameTime>().PassMin - 1020f), 1f - (1f - 0.5f) / 120f * (System.GetComponent<InGameTime>().PassMin - 1020f));
            }
        }
        else if (System.GetComponent<InGameTime>().PassMin >= 1140)
        {
            foreach (var item in sprites)
            {
                item.color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
        }
        else
        {
            foreach (var item in sprites)
            {
                item.color = new Color(1f,1f,1f,1);
            }
        }
    }
    public void PlayerColorReset()
    {
        foreach (var item in sprites)
        {
            item.color = new Color(1, 1, 1);
        }
    }
}

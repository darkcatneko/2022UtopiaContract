using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class OnMouseEnter : MonoBehaviour 
{
    public Animator scroller;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void OnbuttonClick()
    {
        scroller.SetBool("clicked", true);
        if (scroller.GetBool("clicked") == false)
        {
            scroller.SetBool("clicked", true);
        }
        else
        {
            scroller.SetBool("Back", true);
            StartCoroutine(Delay.DelayToInvokeDo(() => {
                scroller.SetBool("clicked", false);
                scroller.SetBool("Back", false);
            }, 1f));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGM_Center : MonoBehaviour
{
    public static BGM_Center instance;
    public Slider slide;
    public float volume = 0.5f;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        this.GetComponent<AudioSource>().volume = volume;
    }
    public void SliderChenge()
    {
        volume = slide.value;
    }
}

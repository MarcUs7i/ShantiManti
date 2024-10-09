using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicValue : MonoBehaviour
{
    private Slider mainSlider;

    void Awake()
    {
        mainSlider = GetComponent<Slider>();
    }

    void Update()
    {
        mainSlider.value = SoundBar.SoundVolume;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicValue : MonoBehaviour
{
    private Slider mainSlider;

    // Start is called before the first frame update
    void Awake()
    {
        mainSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        mainSlider.value = SoundBar.SoundVolume;
    }
}

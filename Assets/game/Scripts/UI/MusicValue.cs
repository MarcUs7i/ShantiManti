using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicValue : MonoBehaviour
{
    private Slider _mainSlider;

    private void Awake()
    {
        _mainSlider = GetComponent<Slider>();
    }

    private void Update()
    {
        _mainSlider.value = SoundBar.SoundVolume;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

//Important to remember: Slider value is linear, but the fader value (-80db to 0 db) is a logrithmic scale.
//For example, half volume is actually -10db, but if you connect it to a linear scale, like the slider, then half 
//volume will end up being -40db which sounds silent at that point. 
//Therefore, instead of setting slider's min/max values to -80 and 0 db, set the min to 0.001 and max to 1.
//Then concert the linear value from the slider to an attenuation level
//It is important to set the min value to 0.001, otherwise dropping the slider all the way to zero breaks the calculation and puts the volume up again.
public class VolumeControl : MonoBehaviour
{
    [SerializeField] private string _volumeParameter = "MasterVolume";
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _multiplier = 20f;
    [SerializeField] private Toggle _toggle;
    private bool _disableToggleEvent;

    private void OnEnable()
    {
        _slider.minValue = 0.001f; //Set to 0.001 so it is not 0
        _slider.onValueChanged.AddListener(HandleSliderValueChanged);
        _toggle.onValueChanged.AddListener(HandleToggleValueChanged);
    }


    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_volumeParameter, _slider.value);
    }

    void Start()
    {
        _slider.value = PlayerPrefs.GetFloat(_volumeParameter, _slider.value);
    }

    private void HandleSliderValueChanged(float value)
    {
        _mixer.SetFloat(_volumeParameter, Mathf.Log10(value) * _multiplier);

        //Using flag to disable toggle callback when slider value is changed 
        _disableToggleEvent = true; 
        _toggle.isOn = _slider.value > _slider.minValue;
        _disableToggleEvent = false;
    }

    private void HandleToggleValueChanged(bool enableSound)
    {
        if (_disableToggleEvent)
            return;

        if (enableSound)
            _slider.value = _slider.maxValue;
        else
            _slider.value = _slider.minValue; //do not use _slider.minValue (0) b/c;
    }
}

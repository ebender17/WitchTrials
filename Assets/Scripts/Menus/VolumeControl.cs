using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private string _volumeParameter = "MasterVolume";
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _multiplier = 30f;
    [SerializeField] private Toggle _toggle;
    private bool _disableToggleEvent;

    private void OnEnable()
    {
        Debug.Log("OnAwake VolumeControls");
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
        Debug.Log("Handle Slide Value Changed");
        _mixer.SetFloat(_volumeParameter, Mathf.Log10(value) * _multiplier);

        //Using flag to disable toggle callback when slider value is changed 
        _disableToggleEvent = true; 
        _toggle.isOn = _slider.value > _slider.minValue;
        _disableToggleEvent = false;
    }

    private void HandleToggleValueChanged(bool enableSound)
    {
        Debug.Log("Handle toggle value changed.");
        if (_disableToggleEvent)
            return;

        if (enableSound)
            _slider.value = _slider.maxValue;
        else
            _slider.value = _slider.minValue;
    }
}

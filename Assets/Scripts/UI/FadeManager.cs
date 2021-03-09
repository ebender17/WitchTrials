using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class FadeManager : MonoBehaviour
{
    [SerializeField] private FadeChannelSO _fadeChannelSO; 
    [SerializeField] private Image _imageComponent;

    private bool _isCurrentlyFading = false;

    private void OnEnable()
    {
        // Subscribing to Fade Channel Broadcast 
        _fadeChannelSO.OnEventRaised += InitiateFade; 
    }

    private void OnDisable()
    {
        _fadeChannelSO.OnEventRaised -= InitiateFade; 
    }

    /// <summary> 
    /// Enumerator that fades in the canva's imageComponent to turn the screen to a flat color over time. 
    /// FadeIns called simultaneously will only fade in the earliest call and discard any others. 
    /// </summary>
    private IEnumerator FadeCoroutine(bool fadeIn, float duration, Color endColor = default)
    {
        Color startColor = _imageComponent.color;
        if (fadeIn)
            endColor = Color.clear;

        float totalTime = 0f; 

        while(totalTime <= duration)
        {
            totalTime += Time.deltaTime;
            _imageComponent.color = Color.Lerp(startColor, endColor, totalTime / duration);

            // pause coroutine until next frame
            yield return null; 
        }

        _imageComponent.color = endColor; //Force to end result
        _isCurrentlyFading = false; 
    }


    /// <summary> 
    /// Controls the fade-in, fade-out. 
    /// </summary>
    /// <param name="fadeIn"> If true, rectangle fades out and gameplay is visible. If false, the screen becomes black. </param>
    /// <param name="duration"> How long it takes the iumage to fade in or out. </param>
    /// <param name="color"> Target color for the image to reach. Disregarded when fading out. </param>
    private void InitiateFade(bool fadeIn, float duration, Color desiredColor)
    {
        // Ensures mult fade-ins or fade-outs do not happen at same time. This means fade-outs called at the same time will be discarded.
        if(!_isCurrentlyFading) //
        {
            _isCurrentlyFading = true;
            StartCoroutine(FadeCoroutine(fadeIn, duration, desiredColor));
        }
    }

}

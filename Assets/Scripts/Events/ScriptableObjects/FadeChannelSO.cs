using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

[CreateAssetMenu(menuName = "Events/UI/Fade Channel")]
public class FadeChannelSO : ScriptableObject
{
    public UnityAction<bool, float, Color> OnEventRaised;

    /// <summary> 
    /// Generic fade function
    /// </summary>
    /// <param name="fadeIn"> If true, the rectangle fades in. If false, the rectangle fades out. </param>
    /// <param name="duration"> How long it takes the image to fade in or out.</param>
    /// <param name="color">Target color for image to reach. Diregarded when fading out. </param>
    public void Fade(bool fadeIn, float duration, Color color = default)
    {
        if(color == default && fadeIn) //If no fadeIn color is assigned, black is given as default. If we are supposed to fadeout the rectangle, default is simply passed through. 
            color = Color.black;
        if (OnEventRaised != null)
            OnEventRaised.Invoke(fadeIn, duration, color);
    }

    /// <summary> 
    /// Fade helper function to simplify usage. Fades in the rectangle. 
    /// </summary>
    /// <param name="duration"> How long it takes the image to fade in. </param>
    /// <param name="color"> Target color for the image to reach. </param>
    public void FadeIn(float duration, Color color = default)
    {
        if (color == default)
            color = Color.black;
        Fade(true, duration, color);
    }

    /// <summary> 
    /// Fade helper function to simplify usage. Fades out the rectangle. 
    /// </summary>
    /// <param name="duration"> How long it takes the image to fade in. </param>
    /// <param name="color"> Target color for the image to reach. </param>
    public void FadeOut(float duration, Color color = default)
    {
        if (color == default)
            color = Color.black;
        Fade(false, duration, color);
    }



}

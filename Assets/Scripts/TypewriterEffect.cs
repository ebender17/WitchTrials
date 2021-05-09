using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float _writingSpeed = 50f;
    public void Run(string textToType, TMP_Text textLabel)
    {
        StartCoroutine(TypeText(textToType, textLabel));

    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        textLabel.text = string.Empty;

        float t = 0; //elapsed time since begun writing
        int charIndex = 0; //floored value of t that is used to calc how how many chars we wish to type on screen at a given frame

        while(charIndex < textToType.Length)
        {
            t += Time.deltaTime * _writingSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            textLabel.text = textToType.Substring(0, charIndex);

            yield return null; //wait one frame
        }

        textLabel.text = textToType;
    }
}

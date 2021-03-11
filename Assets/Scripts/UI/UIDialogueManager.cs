using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Attach to Dialogue Panel containing dialogue UI
public class UIDialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lineText = default;
    [SerializeField] private TextMeshProUGUI _actorNameText = default; 

    public void SetDialogue(string dialogueLine, ActorSO actor)
    {
        _lineText.SetText(dialogueLine);
        _actorNameText.SetText($"{actor.ActorName}:");
    }
}

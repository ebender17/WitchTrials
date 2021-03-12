using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Listens on channels to open and close dialgoue.
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("Listening on channels")]
    [Header("Dialogue Events")]
    [SerializeField] private DialogueLineChannelSO _openUIDialogueEvent = default;
    [SerializeField] private VoidEventChannelSO _closeUIDialogueEvent = default;


    [SerializeField] private UIDialogueManager _dialogueController = default;
    private void OnEnable()
    {
        //Check if event exits to avoid null refs 
        if (_openUIDialogueEvent != null)
            _openUIDialogueEvent.OnEventRaised += OpenUIDialogue;
        if (_closeUIDialogueEvent != null)
            _closeUIDialogueEvent.OnEventRaised += CloseUIDialogue;
    }

    private void OnDisable()
    {
        if (_openUIDialogueEvent != null)
            _openUIDialogueEvent.OnEventRaised -= OpenUIDialogue;
        if (_closeUIDialogueEvent != null)
            _closeUIDialogueEvent.OnEventRaised -= CloseUIDialogue;
    }

    public void OpenUIDialogue(string dialogueLine, ActorSO actor)
    {
        _dialogueController.SetDialogue(dialogueLine, actor);
        _dialogueController.gameObject.SetActive(true);
    }

    public void CloseUIDialogue()
    {
        _dialogueController.gameObject.SetActive(false);
    }
}

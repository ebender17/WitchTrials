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

    [Header("Interaction Events")]
    [SerializeField] private VoidEventChannelSO _onInteractionEndedEvent = default; 
    [SerializeField] private InteractionUIEventChannelSO _setInteractionEvent = default; 

    [Header("UI Panels")]
    [SerializeField] private UIDialogueManager _dialoguePanel = default;
    [SerializeField] private UIInteractionManager _interactionPanel = default; 
    private void OnEnable()
    {
        //Check if event exits to avoid null refs 
        if (_openUIDialogueEvent != null)
            _openUIDialogueEvent.OnEventRaised += OpenUIDialogue;
        if (_closeUIDialogueEvent != null)
            _closeUIDialogueEvent.OnEventRaised += CloseUIDialogue;

        if (_setInteractionEvent != null)
            _setInteractionEvent.OnEventRaised += SetInteractionPanel;
    }

    private void OnDisable()
    {
        if (_openUIDialogueEvent != null)
            _openUIDialogueEvent.OnEventRaised -= OpenUIDialogue;
        if (_closeUIDialogueEvent != null)
            _closeUIDialogueEvent.OnEventRaised -= CloseUIDialogue;

        if (_setInteractionEvent != null)
            _setInteractionEvent.OnEventRaised -= SetInteractionPanel;
    }

    public void OpenUIDialogue(string dialogueLine, ActorSO actor)
    {
        _dialoguePanel.SetDialogue(dialogueLine, actor);
        _dialoguePanel.gameObject.SetActive(true);
    }

    public void CloseUIDialogue()
    {
        _dialoguePanel.gameObject.SetActive(false);
    }

    public void SetInteractionPanel(bool isOpen, InteractionType interactionType)
    {
        if (isOpen)
            _interactionPanel.FillInteractionPanel(interactionType);
        
        _interactionPanel.gameObject.SetActive(isOpen);
    }
}

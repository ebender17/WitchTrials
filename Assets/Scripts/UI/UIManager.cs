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
    //[SerializeField] private VoidEventChannelSO _onInteractionEndedEvent = default;
    [SerializeField] private InteractionUIEventChannelSO _setInteractionEvent = default;

    [Header("Game Result Events")]
    [SerializeField] private GameResultChannelSO _openGameResultUIEvent = default;

    [Header("HUD Events")]
    [SerializeField] private FloatEventChannelSO _updateHealthUIEvent = default;
    [SerializeField] private IntEventChannelSO _updateScoreUIEvent = default;

    [Header("UI Panels")]
    [SerializeField] private UIDialogueManager _dialoguePanel = default;
    [SerializeField] private UIInteractionManager _interactionPanel = default;
    [SerializeField] private UIGameManager _gamePanel = default;
    [SerializeField] private UIHUDManager _HUDPanel = default;

  
    private void OnEnable()
    {
        //Check if event exists to avoid null refs 
        if (_openUIDialogueEvent != null)
            _openUIDialogueEvent.OnEventRaised += OpenUIDialogue;
        if (_closeUIDialogueEvent != null)
            _closeUIDialogueEvent.OnEventRaised += CloseUIDialogue;
        if (_openGameResultUIEvent != null)
            _openGameResultUIEvent.OnEventRaised += OpenUIGameResult;

        if (_setInteractionEvent != null)
            _setInteractionEvent.OnEventRaised += SetInteractionPanel;

        if (_updateHealthUIEvent != null)
            _updateHealthUIEvent.OnEventRaised += UpdateHealthPanel;
        if (_updateScoreUIEvent != null)
            _updateScoreUIEvent.OnEventRaised += UpdateScorePanel;
    }

    private void OnDisable()
    {
        if (_openUIDialogueEvent != null)
            _openUIDialogueEvent.OnEventRaised -= OpenUIDialogue;
        if (_closeUIDialogueEvent != null)
            _closeUIDialogueEvent.OnEventRaised -= CloseUIDialogue;
        if (_openGameResultUIEvent != null)
            _openGameResultUIEvent.OnEventRaised -= OpenUIGameResult;

        if (_setInteractionEvent != null)
            _setInteractionEvent.OnEventRaised -= SetInteractionPanel;

        if (_updateHealthUIEvent != null)
            _updateHealthUIEvent.OnEventRaised -= UpdateHealthPanel;
        if (_updateScoreUIEvent != null)
            _updateScoreUIEvent.OnEventRaised -= UpdateScorePanel;
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

    public void OpenUIGameResult(bool isWon, string playerScore)
    {
        string result;

        if (isWon)
            result = "Won";
        else
            result = "Lost";

        _gamePanel.SetGameResults(result, playerScore);
        _HUDPanel.gameObject.SetActive(false);
        _gamePanel.gameObject.SetActive(true); 

    }

    private void UpdateHealthPanel(float value)
    {
        _HUDPanel.SetValue(value);
    }

    private void UpdateScorePanel(int value)
    {
        _HUDPanel.SetScore(value);
    }
}

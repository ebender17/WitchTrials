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

    [Header("Menu Events")]
    [SerializeField] private BoolEventChannelSO _setMenuUIEvent = default;

    [Header("UI Panels")]
    [SerializeField] private UIDialogueManager _dialoguePanel = default;
    [SerializeField] private UIInteractionManager _interactionPanel = default;
    [SerializeField] private UIGameManager _gamePanel = default;
    [SerializeField] private UIHUDManager _HUDPanel = default;
    [SerializeField] private GameObject _pauseMenuPanel = default;

    private bool wasInteractionActive = false; 
  
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

        if (_setMenuUIEvent != null)
            _setMenuUIEvent.OnEventRaised += SetMenuPanel;
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

        if (_setMenuUIEvent != null)
            _setMenuUIEvent.OnEventRaised -= SetMenuPanel;
    }

    public void OpenUIDialogue(string dialogueLine, ActorSO actor)
    {
        _dialoguePanel.SetDialogue(dialogueLine, actor);
        _dialoguePanel.transform.GetChild(0).gameObject.SetActive(true);

        _HUDPanel.gameObject.SetActive(false);

        if(_interactionPanel.gameObject.activeSelf)
        {
            wasInteractionActive = true;
            _interactionPanel.gameObject.SetActive(false);
        }
        else
        {
            wasInteractionActive = false;
        }
    }

    public void CloseUIDialogue()
    {
        _dialoguePanel.transform.GetChild(0).gameObject.SetActive(false);

        _HUDPanel.gameObject.SetActive(true);

        if(wasInteractionActive)
            _interactionPanel.gameObject.SetActive(true);
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

    private void SetMenuPanel(bool isOpen)
    {
        _pauseMenuPanel.SetActive(isOpen);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Takes care of all things dialogue, whether coming from within a Timeline or from interaction with a character.
/// Gives back control to gameplay action map when appropriate.
/// </summary>
public class DialogueManager : MonoBehaviour
{
    [SerializeField] InputReader _inputReader = default;
    private int _counter; 
    private bool _reachedEndOfDialogue { get => _counter >= _currentDialogue.DialogueLines.Count;  }

    [Header("Listening on channels")]
    [SerializeField] private DialogueDataChannelSO _startDialogue = default;

    [Header("Broadcasting on channels")]
    [SerializeField] private DialogueLineChannelSO _openUIDialogueEvent = default;
    [SerializeField] private DialogueDataChannelSO _endDialogue = default;
    [SerializeField] private VoidEventChannelSO _closeDialogueUIEvent = default;

    private DialogueDataSO _currentDialogue = default;

    // Start is called before the first frame update
    void Start()
    {
        if (_startDialogue != null)
            _startDialogue.OnEventRaised += DisplayDialogueData;
        
    }

    /// <summary>
    /// Displays DialogueData in the UI, one by one.
    /// </summary>
    /// <param name="dialgoueDataSO"></param>
    public void DisplayDialogueData(DialogueDataSO dialgoueDataSO)
    {
        BeginDialogueData(dialgoueDataSO);
        DisplayDialogueLine(_currentDialogue.DialogueLines[_counter], dialgoueDataSO.Actor);

    }

    /// <summary>
    /// Prepare DialogueManager when displaying Dilogue Data for the first time.
    /// </summary>
    /// <param name="dialogueDataSO"></param>
    private void BeginDialogueData(DialogueDataSO dialogueDataSO)
    {
        _counter = 0;
        _inputReader.EnableDialogueInput();
        _inputReader.advanceDialogueEvent += OnAdvance;
        _currentDialogue = dialogueDataSO;

    }

    /// <summary>
    /// Displays a line of dialogue in the UI, by request from <see cref="DialogueManager"/>
    /// This function is also called by <see cref="DialogueBehavior"/> from clips on Timeline during cutscenes.
    /// </summary>
    /// <param name="dialogueLine"></param>
    /// <param name="actor"></param>
    public void DisplayDialogueLine(string dialogueLine, ActorSO actor)
    {
        if(_openUIDialogueEvent != null)
        {
            _openUIDialogueEvent.RaiseEvent(dialogueLine, actor);
        }
    }

    private void OnAdvance()
    {
        _counter++;

        if(!_reachedEndOfDialogue)
        {
            DisplayDialogueLine(_currentDialogue.DialogueLines[_counter], _currentDialogue.Actor);
        }
        else
        {
            DialogueEndedAndCloseDialogueUI();
        }
    }

    public void DialogueEndedAndCloseDialogueUI()
    {
        if (_endDialogue != null)
            _endDialogue.RaiseEvent(_currentDialogue);
        if (_closeDialogueUIEvent != null)
            _closeDialogueUIEvent.RaiseEvent();

        _inputReader.advanceDialogueEvent -= OnAdvance;
        _inputReader.EnableGameplayInput();

    }

}

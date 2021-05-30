using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogueType
{
    //Options for other types if extend in future
    startDialogue,
    winDialogue, 
    loseDialogue,
    defaultDialogue
}

public enum ChoiceActionType
{
    doNothing, 
    continueWithStep
}

/// <summary>
/// Dialogue is a list of consecutive DialogueLines. They play in sequence using the input of the player to skip forward.
/// </summary>
[CreateAssetMenu(fileName = "newDialogue", menuName = "Dialogues/Dialogue Data")]
public class DialogueDataSO : ScriptableObject
{
    [SerializeField] private ActorSO _actor = default;
    [SerializeField] [TextArea] private List<string> _dialogueLines = default;
    [SerializeField] private DialogueType _dialogueType = default;

    public ActorSO Actor => _actor;
    public List<string> DialogueLines => _dialogueLines;
    public DialogueType DialogueType => _dialogueType;
}

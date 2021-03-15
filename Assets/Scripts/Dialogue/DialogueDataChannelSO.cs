using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Used for talk interaction events. 
/// </summary>
[CreateAssetMenu(fileName = "newDialogueDataChannel", menuName = "Events/Dialogue Data Channel")]
public class DialogueDataChannelSO : ScriptableObject
{
    public UnityAction<DialogueDataSO> OnEventRaised;

    public void RaiseEvent(DialogueDataSO dialogue)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(dialogue);
    }
}

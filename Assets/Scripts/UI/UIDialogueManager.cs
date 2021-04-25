using UnityEngine;
using TMPro;

/// <summary>
/// Used by <see cref="UIManager"/> to set dialogue for UI.
/// Attach script to Dialogue Panel containing dialogue textbox.
/// </summary>
public class UIDialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lineText = default;
    [SerializeField] private TextMeshProUGUI _actorNameText = default; 

    public void SetDialogue(string dialogueLine, ActorSO actor)
    {
        _lineText.SetText(dialogueLine);
        _actorNameText.SetText($"{actor.ActorName}");
    }
}

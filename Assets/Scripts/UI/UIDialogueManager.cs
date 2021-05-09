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

    private TypewriterEffect _typewriterEffect = null;

    private void Start()
    {
        _typewriterEffect = GetComponent<TypewriterEffect>();
    }

    public void SetDialogue(string dialogueLine, ActorSO actor)
    {
        if (_typewriterEffect)
        {
            _typewriterEffect.Run(dialogueLine, _lineText);
        }
        else
        {
            _lineText.SetText(dialogueLine);
            Debug.Log("Attached Typewriter component for typewriter effect.");
        }
        
        _actorNameText.SetText($"{actor.ActorName}");
    }
}

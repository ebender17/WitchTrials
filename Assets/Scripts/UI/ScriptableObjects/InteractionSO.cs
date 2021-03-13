using UnityEngine;

[CreateAssetMenu(fileName = "newInteraction", menuName = "UI/Interaction")]
public class InteractionSO : ScriptableObject
{
    [Tooltip("The Interaction Name")]
    [SerializeField] private string _interactionName = default;

    [Tooltip("The Interaction Type")]
    [SerializeField] private InteractionType _interactionType = default;

    public string InteractionName => _interactionName;
    public InteractionType InteractionType => _interactionType;
}

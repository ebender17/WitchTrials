using UnityEngine;

/// <summary>
/// Name to display to dialogue.
/// </summary>
[CreateAssetMenu(fileName = "newActor", menuName = "Dialogues/Actor")]
public class ActorSO : ScriptableObject
{
    public string ActorName { get => _actorName; }

    [SerializeField] private string _actorName = default;
}

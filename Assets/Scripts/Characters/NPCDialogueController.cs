using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place on NPC Actors for Dialogue. 
/// </summary>
public class NPCDialogueController : MonoBehaviour
{
    [Header("Data")]
    //[SerializeField] private ActorSO _actor = default;
    [SerializeField] private DialogueDataSO _dialogue = default;

    [Header("Listening to channels")]
    //[SerializeField] DialogueActorChannelSO _interactionEvent = default;

    [Header("Broadcasting on channels")]
    [SerializeField] private DialogueDataChannelSO _startDialogueEvent = default;

    [SerializeField] private Transform player;
    private int facingDirection;

    private void Start()
    {
        facingDirection = 1;
    }
    private void Update()
    {
        if (player.position.x < transform.position.x)
            facingDirection = -1;
        else
            facingDirection = 1;
    }
    public void InteractWithCharacter()
    {
        if(_dialogue != null)
        {
            StartDialogue();
            
        }
    }

    private void StartDialogue()
    {
        if (_startDialogueEvent != null)
            _startDialogueEvent.RaiseEvent(_dialogue);
    }

    private void Flip()
    {
        transform.Rotate(0, 180f, 0);
    }
}

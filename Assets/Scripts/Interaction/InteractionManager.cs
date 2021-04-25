using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages world interactions between player and other non-combative objects.
/// </summary>
public enum InteractionType {  None = 0, PickUp, Talk, Read }

public class Interaction
{
    public InteractionType type;
    public GameObject interactableObject;

    public Interaction(InteractionType iType, GameObject obj)
    {
        type = iType;
        interactableObject = obj;
    }
}


public class InteractionManager : MonoBehaviour
{
    [HideInInspector] public InteractionType currentInteractionType;
    [SerializeField] private InputReader _inputReader = default;

    private LinkedList<Interaction> _potentialInteractions = new LinkedList<Interaction>();

    //Events for the different interaction types 
    [Header("Broadcasting on channels")]
    [SerializeField] private DialogueActorChannelSO _startTalking = default;
    //UI event 
    [SerializeField] private InteractionUIEventChannelSO _toggleInteractionUI = default;

    [Header("Listening to channels")]
    [SerializeField] private VoidEventChannelSO _onInteractionEnded = default;

    private void OnEnable()
    {
        _inputReader.interactEvent += OnInteractionButtonPress;
        _onInteractionEnded.OnEventRaised += OnInteractionEnd;
    }

    private void OnDisable()
    {
        _inputReader.interactEvent -= OnInteractionButtonPress;
        _onInteractionEnded.OnEventRaised -= OnInteractionEnd;
    }

    private void OnInteractionButtonPress()
    {
        if (_potentialInteractions.Count == 0)
            return;

        currentInteractionType = _potentialInteractions.First.Value.type;

        if(currentInteractionType == InteractionType.Talk || currentInteractionType == InteractionType.Read)
        {
            if(_startTalking != null)
            {
                _potentialInteractions.First.Value.interactableObject.GetComponent<DialogueController>().InteractWithCharacter();
                _inputReader.EnableDialogueInput();
            }
        }
    }

  
    /// <summary>
    /// Called by the Event on the trigger collider on child GameObject named "InteractionDetector"
    /// Example Event <see cref="ZoneTriggerController"/>
    /// </summary>
    /// <param name="isWithin"></param>
    /// <param name="obj"></param>
    public void OnTriggerChangeDetected(bool isWithin, GameObject obj)
    {
        //Add this check so interaction UI does not get triggered during a cutscene with dialogue
        if(_inputReader.CurrentSchema == GameSchemas.Gameplay)
        {
            if (isWithin)
                AddPotentialInteraction(obj);
            else
                RemovePotentialInteraction(obj);
        }     
    }

    private void AddPotentialInteraction(GameObject obj)
    {
        Interaction newPotentialInteraction = new Interaction(InteractionType.None, obj);

        if(obj.CompareTag("NPC"))
        {
            newPotentialInteraction.type = InteractionType.Talk;
        }
        else if(obj.CompareTag("Sign"))
        {
            newPotentialInteraction.type = InteractionType.Read;
        }

        if(newPotentialInteraction.type != InteractionType.None)
        {
            _potentialInteractions.AddFirst(newPotentialInteraction);
            RequestUIUpdate(true);
        }
    }

    private void RemovePotentialInteraction(GameObject obj)
    {
        LinkedListNode<Interaction> currentNode = _potentialInteractions.First;

        //Loop through LinkedList until object is found and removed
        while(currentNode != null)
        {
            if(currentNode.Value.interactableObject == obj)
            {
                _potentialInteractions.Remove(currentNode);
                break;
            }
            currentNode = currentNode.Next;
        }

        //Toggle UI depending on if there are more interactions or not 
        RequestUIUpdate(_potentialInteractions.Count > 0);
    }
    private void RequestUIUpdate(bool isVisible)
    {
        if (isVisible)
            _toggleInteractionUI.RaiseEvent(true, _potentialInteractions.First.Value.type);
        else
            _toggleInteractionUI.RaiseEvent(false, InteractionType.None);
    }
    private void OnInteractionEnd()
    {
        if (currentInteractionType == InteractionType.Talk)
            //Show UI in case player wants to interact again 
            RequestUIUpdate(true);

        _inputReader.EnableGameplayInput();

    }

}

using UnityEngine;

/// <summary>
/// Using when we communicate directly between events and delegates, we reference the class that owns the 
/// Data Structure and subsribe to start listening for messages. 
/// This approach creates in scene dependencies with the objects involved since the event subscribers need 
/// to reference the senders to work. 
/// Solve this issue using Scriptable Objects as an intermediate point of communication. These SO are called channels.
/// They represent channels to broadcast events on and listen to them creating a communication between two or 
/// more parts. 
/// 
/// Action/Trigger (MonoBehaviour) ---Raise---> Event Invoke() (SO)(Channel) <---Listening to--- Event Listener (MonoBehaviour)
/// </summary>
public class EventChannelBaseSO : ScriptableObject
{
    [TextArea] public string description;
}

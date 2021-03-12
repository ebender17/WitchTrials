using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class BoolEvent : UnityEvent<bool, GameObject> 
{ 

}

/// <summary>
/// Generic class for a "zone" that is a trigger collider that can detect if an object of a certain type (layer) entered or exited it. 
/// Needs to be on the same object that holds the Collider b/c it implements OnTriggerEnter and OnTriggerExit 
/// </summary>
public class ZoneTriggerController : MonoBehaviour
{
    [SerializeField] private BoolEvent _enterZone = default;
    [SerializeField] private LayerMask _layers = default;

    private void OnTriggerEnter(Collider other)
    {
        //Shift 1 to corresponding bit in layer mask 
        //Compared shifted bit with layer mask 
        //If object is on layer included in layer mask, we will not get 0 
        if((1 << other.gameObject.layer & _layers) != 0)
        {
            _enterZone.Invoke(true, other.gameObject);
        }
                     
    }

    private void OnTriggerExit(Collider other)
    {
        if((1 << other.gameObject.layer & _layers) != 0)
        {
            _enterZone.Invoke(false, other.gameObject); 
        }
    }
}

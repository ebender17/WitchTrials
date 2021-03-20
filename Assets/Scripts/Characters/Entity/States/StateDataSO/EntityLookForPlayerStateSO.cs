using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newLookForPlayerStateData", menuName = "Entity Data/State/LookForPlayer State Data")]
public class EntityLookForPlayerStateSO : ScriptableObject
{
    public int amountOfTurns = 2;
    public float timeBetweenTurns = 0.75f; 
    
}

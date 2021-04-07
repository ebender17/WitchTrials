using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDeadStateData", menuName = "Entity Data/State/Dead State Data")]
public class EntityDeadStateSO : ScriptableObject
{
    public GameObject deathPartParticle;
    public GameObject deathBloodParticle;
    
}

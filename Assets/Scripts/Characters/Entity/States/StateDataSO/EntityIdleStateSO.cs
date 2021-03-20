using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Entity Data/State/Idle State Data")]
public class EntityIdleStateSO : ScriptableObject
{
    public float minIdleTime = 1f;
    public float maxIdleTime = 2f;
}

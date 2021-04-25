using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDeadStateData", menuName = "Entity Data/State/Dead State Data")]
public class EntityDeadStateSO : ScriptableObject
{
    public GameObject deathParticleOne;
    public GameObject deathParticleTwo;
    public GameObject deathParticleThree;
    public Vector3 deathParticleTwoOffset = new Vector3(-2, 2, 0);
    public Vector3 deathParticleThreeOffset = new Vector3(3, 1, 0);

}

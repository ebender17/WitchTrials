using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Object pooling for particles
public class ParticleController : MonoBehaviour
{
    private void FinishAnimation()
    {
        Destroy(gameObject);
    }
}

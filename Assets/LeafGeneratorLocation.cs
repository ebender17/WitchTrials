using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafGeneratorLocation : MonoBehaviour
{
    [SerializeField] private Transform location = null;

    void Start()
    {
        transform.position = location.position;
    }

    void Update()
    {
        transform.position = location.position;
    }
}

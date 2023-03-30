using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBurst : MonoBehaviour
{
    private void Start()
    {
        float destroyTime =  GetComponent<ParticleSystem>().main.duration;
        Destroy(gameObject, destroyTime);
    }
}

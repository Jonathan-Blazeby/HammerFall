using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointScript : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<SpawnManager>().RegisterSpawnPoints(transform);
    }
}

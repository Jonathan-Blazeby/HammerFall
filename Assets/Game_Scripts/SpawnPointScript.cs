using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointScript : MonoBehaviour
{
    #region MonoBehaviour Callbacks
    private void Awake()
    {
        FindObjectOfType<SpawnManager>().RegisterSpawnPoints(transform);
    }
    #endregion

}

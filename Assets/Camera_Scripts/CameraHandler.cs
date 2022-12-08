using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private float yOffset;
    [SerializeField] private float zOffset;

    private void Start()
    {
        Initialise();
    }

    private void Initialise()
    {
        Cursor.lockState = CursorLockMode.Locked;
        yOffset = transform.localPosition.y;
        zOffset = transform.localPosition.z;
    }

}

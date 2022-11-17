using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private float yOffset = 4;
    [SerializeField] private float zOffset = -3;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.localPosition += new Vector3(0, yOffset, zOffset);
    }

}

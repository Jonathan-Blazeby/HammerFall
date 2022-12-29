using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    #region Private Fields
    [SerializeField] private float yOffset;
    [SerializeField] private float zOffset;
    #endregion

    #region MonoBehaviour Callbacks
    private void Start()
    {
        Initialise();
    }
    #endregion

    #region Private Methods
    private void Initialise()
    {
        Cursor.lockState = CursorLockMode.Locked;
        yOffset = transform.localPosition.y;
        zOffset = transform.localPosition.z;
    }
    #endregion
}

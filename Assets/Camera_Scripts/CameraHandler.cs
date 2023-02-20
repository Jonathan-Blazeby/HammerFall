using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    #region Private Fields
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform camTransform;
    [SerializeField] private Transform camPivotTransform;

    [SerializeField] private float followSpeed = 1.0f;
    [SerializeField] private float cameraSphereRadius = 0.2f;
    [SerializeField] private float cameraCollisionOffset = 0.2f;
    [SerializeField] private float minimumCollisionOffset = 0.2f;

    private Transform myTransform;
    private Vector3 camTransformPos;
    private LayerMask ignoreLayers;
    private Vector3 cameraFollowVel = Vector3.zero;

    private float targetPos;
    private float defaultPos;
    #endregion

    #region MonoBehaviour Callbacks
    private void Awake()
    {
        Initialise();
    }

    private void Update()
    {
        FollowTarget(Time.deltaTime);
    }
    #endregion

    #region Private Methods
    private void Initialise()
    {
        myTransform = transform;
        defaultPos = camTransform.localPosition.z;
        ignoreLayers = ~(1 << 5);

        Cursor.lockState = CursorLockMode.Locked;
        Application.targetFrameRate = 60;
    }

    private void FollowTarget(float delta)
    {
        Vector3 targetPos = Vector3.SmoothDamp(myTransform.position, targetTransform.position, ref cameraFollowVel, delta / followSpeed);
        myTransform.position = targetPos;

        HandleCameraCollisions(delta);
    }

    private void HandleCameraCollisions(float delta)
    {
        targetPos = defaultPos;
        RaycastHit hit;
        Vector3 direction = camTransform.position - camPivotTransform.position;
        direction.Normalize();

        if (Physics.SphereCast(camPivotTransform.position, cameraSphereRadius, direction, out hit, Mathf.Abs(targetPos), ignoreLayers))
        {
            float dis = Vector3.Distance(camPivotTransform.position, hit.point);
            targetPos = -(dis - cameraCollisionOffset);
        }

        if (Mathf.Abs(targetPos) < minimumCollisionOffset)
        {
            targetPos = -minimumCollisionOffset;
        }

        camTransformPos.z = Mathf.Lerp(camTransform.localPosition.z, targetPos, delta / 0.2f);
        camTransform.localPosition = camTransformPos;
    }
    #endregion
}

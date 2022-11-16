using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    [SerializeField] float gravityMultiplier = 1.5f;
    private List<IMovement> allMovingList = new List<IMovement>();

    public void AddNewMoving(GameObject newObject)
    {
        MonoBehaviour[] allScripts = newObject.GetComponents<MonoBehaviour>();
        for (int i = 0; i < allScripts.Length; i++)
        {
            if (allScripts[i] is IMovement)
                allMovingList.Add(allScripts[i] as IMovement);
        }
    }

    private void Start()
    {
        Physics.gravity *= gravityMultiplier;

        MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();
        for (int i = 0; i < allScripts.Length; i++)
        {
            if (allScripts[i] is IMovement)
                allMovingList.Add(allScripts[i] as IMovement);
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < allMovingList.Count; i++)
        {
            float verticalVelocity = allMovingList[i].GetVerticalVelocity();
            float newVerticalVelocity = 0;
            bool jumpState = allMovingList[i].GetJumpState();
            bool isGrounded = allMovingList[i].GetIsGrounded();

            if (verticalVelocity < 0 && isGrounded)
            {
                newVerticalVelocity = Physics.gravity.y * Time.fixedDeltaTime;
            }
            else
            {
                newVerticalVelocity += Physics.gravity.y * Time.fixedDeltaTime;
            }

            if (jumpState && isGrounded)
            {
                allMovingList[i].SetIsGrounded(false);
                newVerticalVelocity += Mathf.Sqrt(allMovingList[i].GetJumpAmount() * -Physics.gravity.y);
            }

            if (verticalVelocity < 0 && jumpState)
            {
                jumpState = false;
            }

            allMovingList[i].SetVerticalVelocity(newVerticalVelocity);
        }
    }
}

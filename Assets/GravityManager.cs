using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    [SerializeField] float gravityMultiplier = 2f;
    private float gravity;
    private List<IMovement> allMovingList = new List<IMovement>();

    //If new moveable characters created, this should be called to add them to the list
    public void AddNewMoving(GameObject newObject)
    {
        MonoBehaviour[] allScripts = newObject.GetComponents<MonoBehaviour>();
        for (int i = 0; i < allScripts.Length; i++)
        {
            if (allScripts[i] is IMovement)
                allMovingList.Add(allScripts[i] as IMovement);
        }
    }

    //Apply gravity multiplier, find and add all IMoveable components to list
    private void Start()
    {
        gravity = Physics.gravity.y;
        gravity *= gravityMultiplier;

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

            if (verticalVelocity < 0 && isGrounded) //Continues applying gravity in case isGrounded check sets off too high
            {
                newVerticalVelocity = gravity * Time.fixedDeltaTime;
            }
            else
            {
                newVerticalVelocity += gravity * Time.fixedDeltaTime;
            }

            if (jumpState && isGrounded) //IsGrounded set to false to prevent more than one application of the jump velocity being added
            {
                allMovingList[i].SetIsGrounded(false);
                newVerticalVelocity += Mathf.Sqrt(allMovingList[i].GetJumpAmount() * -gravity);
            }

            if (verticalVelocity < 0 && jumpState) //Jumpstate swiched to false when falling
            {
                allMovingList[i].SetJumpState(false);
            }

            allMovingList[i].SetVerticalVelocity(newVerticalVelocity);
        }
    }
}

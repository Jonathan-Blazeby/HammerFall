using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    #region Private Fields
    [SerializeField] private float gravityMultiplier = 2f;
    private float gravity;
    private List<IMovement> allMovingList = new List<IMovement>();
    #endregion

    #region Public Fields
    public static GravityManager Instance;
    #endregion

    #region MonoBehaviour Callbacks
    private void Awake()
    {
        Instance = this;
    }

    //Apply gravity multiplier, find and add all IMoveable components to list
    private void Start()
    {
        Initialise();
    }

    private void FixedUpdate()
    {
        //GravityUpdate();
    }
    #endregion

    #region Private Methods
    private void Initialise()
    {
        gravity = Physics.gravity.y;
        gravity *= gravityMultiplier;
    }

    private void GravityUpdate()
    {
        for (int i = 0; i < allMovingList.Count; i++)
        {
            float verticalVelocity = allMovingList[i].GetVerticalVelocity();
            float newVerticalVelocity = 0;
            //bool jumpState = allMovingList[i].GetJumpState();
            bool isGrounded = allMovingList[i].GetIsGrounded();

            if (verticalVelocity < 0 && isGrounded) //Continues applying gravity in case isGrounded check sets off too high
            {
                newVerticalVelocity = gravity * Time.fixedDeltaTime;
            }
            else
            {
                newVerticalVelocity += gravity * Time.fixedDeltaTime;
            }

            allMovingList[i].SetVerticalVelocity(newVerticalVelocity);
        }
    }
    #endregion

    #region Public Methods
    //If new moveable characters created, this should be called to add them to the list
    public void AddNewMoving(IMovement moveComp)
    {
        allMovingList.Add(moveComp);
    }
    #endregion
        
}

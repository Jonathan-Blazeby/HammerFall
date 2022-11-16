using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterAnimator : MonoBehaviour
{
    private IMovement movement;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator animator;
    private int horizontal;
    private int vertical;

    private void Start()
    {
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
        movement = playerController.GetMovement();
    }

    public void UpdateAnimatorValues()
    {
        float horizontalMove = movement.GetDirection().x;
        float verticalMove = movement.GetDirection().z;

        #region Horizontal Clamping
        float h = 0;

        if (horizontalMove > 0 && horizontalMove < 0.55f)
        {
            h = 0.5f;
        }
        else if (horizontalMove > 0.55f)
        {
            h = 1;
        }
        else if (horizontalMove < 0 && horizontalMove > -0.55f)
        {
            h = 0.5f;
        }
        else if (horizontalMove < -0.55f)
        {
            h = 1;
        }
        else
        {
            h = 0;
        }
        #endregion

        #region Vertical Clamping
        float v = 0;

        if (verticalMove > 0 && verticalMove < 0.55f)
        {
            v = 0.5f;
        }
        else if (verticalMove > 0.55f)
        {
            v = 1;
        }
        else if (verticalMove < 0 && verticalMove > -0.55f)
        {
            v = 0.5f;
        }
        else if (verticalMove < -0.55f)
        {
            v = 1;
        }
        else
        {
            v = 0;
        }
        #endregion
                
        animator.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, v, 0.1f, Time.deltaTime);
    }
}

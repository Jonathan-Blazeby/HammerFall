using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterAnimator : MonoBehaviour
{
    private IMovement movement;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator moveAnimator;
    [SerializeField] private Animator hammerAnimator;
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
                
        moveAnimator.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        moveAnimator.SetFloat(vertical, v, 0.1f, Time.deltaTime);
    }

    public void Attack(int attackInt)
    {
        if(attackInt == 1)
        {
            hammerAnimator.Play("LeftSwing");
        }
        else if(attackInt == 2)
        {
            hammerAnimator.Play("RightSwing");
        }
    }

    public bool HammerSwingFinishedCheck()
    {
        if(HammerAnimatorIsFinishedPlaying("LeftSwing") || HammerAnimatorIsFinishedPlaying("RightSwing"))
        {
            return true;
        }
        else { return false; }
    }

    private bool HammerAnimatorIsFinishedPlaying()
    {
        return hammerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }

    private bool HammerAnimatorIsFinishedPlaying(string stateName)
    {
        return HammerAnimatorIsFinishedPlaying() && hammerAnimator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
}

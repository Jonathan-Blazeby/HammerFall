using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterAnimator : MonoBehaviour, IAnimation
{
    #region Private Fields
    private IMovement movement;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator moveAnimator;
    [SerializeField] private Animator hammerAnimator;
    private int horizontal;
    private int vertical;
    #endregion

    #region Public Methods
    public void Initialise()
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
        float h = Mathf.Abs(horizontalMove);

        if (h > 0 && h < 0.55f)
        {
            h = 0.5f;
        }
        else if (h > 0.55f)
        {
            h = 1;
        }
        else
        {
            h = 0;
        }
        #endregion

        #region Vertical Clamping
        float v = Mathf.Abs(verticalMove);

        if (v > 0 && v < 0.55f)
        {
            v = 0.5f;
        }
        else if (v > 0.55f)
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

    public bool IsWeaponActive()
    {
        return hammerAnimator.GetBool("WeaponActive");
    }

    public void Attack(int attackInt)
    {
        if (attackInt == 1)
        {
            hammerAnimator.Play("LeftSwing");
        }
        else if (attackInt == 2)
        {
            hammerAnimator.Play("RightSwing");
        }
    }

    public void Die()
    {
        moveAnimator.Play("Death");
        hammerAnimator.Play("HammerDisappear");
    }
    #endregion

}

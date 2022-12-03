using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacterAnimator : MonoBehaviour, IAnimation
{
    private IMovement movement;
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private Animator moveAnimator;
    [SerializeField] private Animator punchAnimator;
    private int horizontal;
    private int vertical;

    private void Start()
    {
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
        movement = enemyController.GetMovement();
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

    public bool IsWeaponActive()
    {
        return punchAnimator.GetBool("WeaponActive");
    }

    public void Attack(int attackInt)
    {
        punchAnimator.Play("Punch");
    }

    public void Stop()
    {
        moveAnimator.Play("Idle");
        punchAnimator.Play("Standby");
    }
}

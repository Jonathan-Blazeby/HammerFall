using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyAnimations
{
    Punch, IdleEnemy, Standby, Death
}

public class EnemyCharacterAnimator : MonoBehaviour, IAnimation
{
    #region Private Fields
    private IMovement movement;
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private Animator moveAnimator;
    [SerializeField] private Animator punchAnimator;
    [SerializeField] private GameObject smokeBurstObject;
    #endregion

    #region IAnimation Implementation
    public bool IsWeaponActive()
    {
        return punchAnimator.GetBool("WeaponActive");
    }
    #endregion

    #region Public Methods
    public void Initialise(MovementManager moveManager)
    {
        movement = moveManager;
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

        moveAnimator.SetFloat(AnimationProperties.Horizontal.ToString(), h, 0.1f, Time.deltaTime);
        moveAnimator.SetFloat(AnimationProperties.Vertical.ToString(), v, 0.1f, Time.deltaTime);
    }

    public void Attack()
    {
        punchAnimator.Play(EnemyAnimations.Punch.ToString());
    }

    public void Stop()
    {
        moveAnimator.Play(EnemyAnimations.IdleEnemy.ToString());
        punchAnimator.Play(EnemyAnimations.Standby.ToString());
    }

    public void Die()
    {
        moveAnimator.Play(EnemyAnimations.Death.ToString());
    }

    public void SmokeBurst()
    {
        Instantiate(smokeBurstObject, moveAnimator.rootPosition, Quaternion.identity);
    }

    #endregion

}

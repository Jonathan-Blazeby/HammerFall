using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacterAnimator : MonoBehaviour, IAnimation
{
    #region Private Fields
    private IMovement movement;
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private Animator moveAnimator;
    [SerializeField] private Animator punchAnimator;
    private int horizontal;
    private int vertical;
    #endregion

    #region MonoBehaviour Callbacks
    private void Start()
    {
        Initialise();
    }
    #endregion

    #region IAnimation Implementation
    public bool IsWeaponActive()
    {
        return punchAnimator.GetBool("WeaponActive");
    }
    #endregion

    #region Private Methods
    private void Initialise()
    {
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
        movement = enemyController.GetMovement();
    }
    #endregion

    #region Public Methods
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

    public void Attack()
    {
        punchAnimator.Play("Punch");
    }

    public void Stop()
    {
        moveAnimator.Play("Idle");
        punchAnimator.Play("Standby");
    }
    #endregion

}

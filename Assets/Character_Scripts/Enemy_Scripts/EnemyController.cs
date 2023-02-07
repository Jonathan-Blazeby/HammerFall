using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, ICharacterController
{
    #region Private Fields
    [SerializeField] private MovementManager moveManager;
    [SerializeField] private AttackManager attackManager;
    [SerializeField] private EnemyAIBasic aiInput;
    [SerializeField] private EnemyCharacterAnimator animator;
    private IDamageDealer attackApplicationComponent;
    [SerializeField] private float corpseRemainTime = 4.0f;
    //[SerializeField] private float jumpForce = 5;
    [SerializeField] private float attackForce = 1;
    [SerializeField] private float attackDelay = 3f;
    [SerializeField] private int attackDamage = 5;

    private bool isGrounded;
    private bool canAttack;
    #endregion

    #region MonoBehaviour Callbacks
    private void Start()
    {
        Initialise();
    }

    private void Update()
    {
        ControllerUpdate();
    }

    private void LateUpdate()
    {
        attackManager.SetWeaponActive(animator.IsWeaponActive());
    }
    #endregion

    #region ICharacterController Implementation
    public void Daze()
    {
        aiInput.SetDazed();
    }
    #endregion

    #region Private Methods
    private void Initialise()
    {

        //moveManager.SetJumpAmount(jumpForce);
        moveManager.SetUseCalculatedRotation(false);

        attackApplicationComponent = GetComponentInChildren<IDamageDealer>();

        attackManager.SetAttackApplicationComponent(attackApplicationComponent);
        attackApplicationComponent.SetWeaponDamage(attackDamage);
        attackApplicationComponent.SetWeaponForce(attackForce);

        canAttack = true;
    }

    private void ControllerUpdate()
    {
        aiInput.SetIsGrounded(isGrounded);

        if (aiInput.GetState() != AIStates.Dead) { MoveFunction(); }

        if (aiInput.GetState() == AIStates.Attacking) { AttackFunction(); }

        if (aiInput.GetState() == AIStates.Dazed) { animator.Stop(); }
        else if (aiInput.GetState() == AIStates.Dead) { animator.Die(); }
        else { animator.UpdateAnimatorValues(); }
    }

    //Looks for horizontal & vertical input to be applied to x z axis, and looks for jump
    private void MoveFunction()
    {
        Vector2 movementDirection;
        //bool willJump = false;

        movementDirection = aiInput.GetDirection();

        moveManager.Move(movementDirection);
        moveManager.Rotate(0);
        isGrounded = moveManager.GetIsGrounded();
    }

    private IEnumerator CorpseDisappearTimer()
    {
        yield return new WaitForSeconds(corpseRemainTime);
        gameObject.SetActive(false);
    }

    private IEnumerator AttackTimer()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }

    private void AttackFunction()
    {
        if (canAttack)
        {
            StartCoroutine(AttackTimer());
            animator.Attack();
        }
    }
    #endregion

    #region Public Methods
    public MovementManager GetMovement() { return moveManager; }

    public void Death()
    {
        aiInput.SetDead();
        StartCoroutine(CorpseDisappearTimer());
    }
    #endregion

}

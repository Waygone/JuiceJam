using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private bool canAttack = true;
    [SerializeField]
    private float
        inputTimer,
        attackRadius,
        slashRadius,
        attackDamage,
        slashDamage;
    [SerializeField] private float stunDamageAmount = 1f;
    [SerializeField] private Transform attackHitBoxPos;
    [SerializeField] private Transform slashHitBoxPos;
    [SerializeField] private LayerMask damageableLayer;

    private bool 
        gotInput, 
        isAttacking, 
        isFirstAttack;


    
    private float lastInputTime = Mathf.NegativeInfinity;
    private AttackDetails attackDetails;

    private Animator animator;
    private Player player;
    private PlayerStats PS;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("canAttack", canAttack);
        player = GetComponent<Player>();
        PS = GetComponent<PlayerStats>();
    }
    private void Update()
    {
        if (Time.timeScale != 0 && !player.Stats.isDead)
        {
            CheckCombatInput();
            CheckAttacks();

        }
    }

    #region Combat
    private void CheckCombatInput()
    {
        if (player.InputHandler.attacking)
        {
            if(canAttack)
            {
                //Combat
                player.InputHandler.attacking = false;
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }

    private void CheckAttacks()
    {
        if(gotInput)
        {
            if(!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                animator.SetBool("attack1", true);
                animator.SetBool("firstAttack", isFirstAttack);
                animator.SetBool("isAttacking", isAttacking);
                player.smallWeapon.PlayOneShot(player.smallWeaponClip);
            }
        }
        if(Time.time >= lastInputTime + inputTimer)
        {
            gotInput = false;
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackHitBoxPos.position, attackRadius, damageableLayer);

        attackDetails.damageAmount = (int)Random.Range(attackDamage, attackDamage + attackDamage / 1.5f); 
        attackDetails.position = transform.position;
        attackDetails.stunAmount = stunDamageAmount;

        foreach(Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attackDetails);
        }
    }

    private void CheckSlashHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(slashHitBoxPos.position, slashRadius, damageableLayer);

        attackDetails.damageAmount = slashDamage;
        attackDetails.position = transform.position;
        attackDetails.stunAmount = stunDamageAmount;

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attackDetails);
        }
    }

    private void FinishAttacking()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("attack1", false);
    }
    private void FinishSlashing()
    {
        animator.SetBool("dash", false);
    }
    #endregion

    #region Take Damage
    private void Damage(AttackDetails attackDetails)
    {
        if (!player.GetDashStatus())
        {
            int direction;

            PS.TakeDamage(attackDetails.damageAmount);

            if (attackDetails.position.x < transform.position.x)
            {
                direction = 1;
            }
            else { direction = -1; }

            player.Knockback(direction);
        }
    }
    #endregion

    #region DrawGizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackHitBoxPos.position, attackRadius);
        Gizmos.DrawWireSphere(slashHitBoxPos.position, slashRadius);
    }
    #endregion
}

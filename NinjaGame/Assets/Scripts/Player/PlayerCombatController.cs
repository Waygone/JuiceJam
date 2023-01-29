using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private bool canAttack = true;
    [SerializeField] 
    private float 
        inputTimer,
        attack1Radius,
        attack1Damage;
    [SerializeField] private Transform attackHitBoxPos;
    [SerializeField] private LayerMask damageableLayer;

    private bool 
        gotInput, 
        isAttacking, 
        isFirstAttack;

    
    private float lastInputTime = Mathf.NegativeInfinity;


    private Animator animator;

    private void Start()
    {
        animator= GetComponent<Animator>();
        animator.SetBool("canAttack", canAttack);
    }
    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }
    private void CheckCombatInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(canAttack)
            {
                //Combat
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
            }
        }
        if(Time.time >= lastInputTime + inputTimer)
        {
            gotInput = false;
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackHitBoxPos.position, attack1Radius, damageableLayer);

        foreach(Collider2D collider in detectedObjects)
        {
            float[] attackDetails = new float[] { attack1Damage, 0 };
            collider.transform.parent.SendMessage("Damage", attackDetails);
            print("Damge something");
            //Attack feedback
        }
    }

    private void FinishAttack1()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("attack1", false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackHitBoxPos.position, attack1Radius);
    }
}

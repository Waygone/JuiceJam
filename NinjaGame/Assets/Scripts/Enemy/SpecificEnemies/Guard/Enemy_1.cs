using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_1 : Entity
{
    public Enemy1_IdleState idleState { get; private set; }
    public Enemy1_MoveState moveState { get; private set; }
    public Enemy1_PlayerDetectedState playerDetectedState { get; private set; }

    public Enemy1_ChargeState chargeState { get; private set; }

    public Enemy1_LookForPlayerState lookForPlayerState { get; private set; }

    public Enemy1_MeleeAttackState meleeAttackState { get; private set; }

    public Enemy1_StunState stunState { get; private set; }

    public Enemy1_DeadState deadState { get; private set; }

    [SerializeField] private Transform meleeAttackPosition;

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_ChargeState chargeStateData;
    [SerializeField] private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField] private D_MeleeAttackState meleeAttackStateData;
    [SerializeField] private D_StunState stunStateData;
    [SerializeField] private D_DeadState deadStateData;

    [SerializeField] private Score score;
    [SerializeField] private float scorePoints = 5f;
    public GameObject FloatingTextPrefab;
    [SerializeField] public AudioSource damage;
    [SerializeField] public AudioClip damageClip;

    public override void Start()
    {
        base.Start();

        moveState = new Enemy1_MoveState(stateMachine,  this, "walk", moveStateData, this);
        idleState = new Enemy1_IdleState(stateMachine, this, "idle", idleStateData, this);
        playerDetectedState = new Enemy1_PlayerDetectedState(stateMachine, this, "playerDetected", playerDetectedStateData, this);
        chargeState = new Enemy1_ChargeState(stateMachine, this, "charge", chargeStateData, this);
        lookForPlayerState = new Enemy1_LookForPlayerState(stateMachine, this, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new Enemy1_MeleeAttackState(stateMachine, this, "meleeAttack",meleeAttackPosition ,meleeAttackStateData, this);
        stunState = new Enemy1_StunState(stateMachine, this, "stun", stunStateData, this);
        deadState = new Enemy1_DeadState(stateMachine, this, "dead", deadStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        ShowFloatingText(attackDetails.damageAmount.ToString(), Random.Range(30, 70), GetRandomColor(), TextAnchor.UpperLeft);
        ShowFloatingText(currentHp.ToString(), Random.Range(30, 70), Color.red, TextAnchor.LowerRight);
        CameraShake.Instance.ShakeCamera(5f, 0.1f);
        damage.PlayOneShot(damageClip);

        if (isDead)
        {
            if (score != null)
            {
                score.UpdateScore(scorePoints);
            }
            stateMachine.ChangeState(deadState);

            CameraShake.Instance.ShakeCamera(10f * (1 + (score.comboCount / 10)), 0.2f);
            ShowFloatingText("ComboX" + score.comboCount, Random.Range(60, 80), Color.white, TextAnchor.MiddleCenter);
        }
        else if (isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }
    }

    private void ShowFloatingText(string message, int fontSize, Color color, TextAnchor anchor)
    {
        if (FloatingTextPrefab)
        {
            GameObject prefab = Instantiate(FloatingTextPrefab, textPosition.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = message;
            prefab.GetComponentInChildren<TextMesh>().fontSize = fontSize;
            prefab.GetComponentInChildren<TextMesh>().color = color;
            prefab.GetComponentInChildren<TextMesh>().anchor = anchor;
            prefab.GetComponentInChildren<MeshRenderer>().sortingLayerName = "ForeGround";
        }

    }

    private Color GetRandomColor()
    {
        return new Color(
      Random.Range(0f, 1f),
      Random.Range(0f, 1f),
      Random.Range(0f, 1f));
    }
}

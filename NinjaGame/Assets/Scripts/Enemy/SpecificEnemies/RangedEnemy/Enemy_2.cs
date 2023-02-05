using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Entity
{

    #region States
    public E2_LookForPlayerState lookForPlayerState { get; private set; }
    public E2_PlayerDetectedState playerDetectedState { get; private set; }
    public E2_ShootState shootState { get; private set; }

    public E2_DeadState deadState { get; private set; }

    #endregion

    [SerializeField]  public D_LookForPlayerState lookForPlayerStateData;
    [SerializeField] public D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] public D_ShootState shootStateData;
    [SerializeField] public D_DeadState deadStateData;

    [SerializeField] private Transform attackPosition;

    [SerializeField] private Score score;
    [SerializeField] private float scorePoints = 5f;
    public GameObject FloatingTextPrefab;
    public GameObject arrowPrefab;
    [SerializeField] public AudioSource damage;
    [SerializeField] public AudioClip damageClip;

    [SerializeField] public float fireRate = 2f;

    public override void Start()
    {
        base.Start();

        lookForPlayerState = new E2_LookForPlayerState(stateMachine, this, "idle", lookForPlayerStateData, this);
        playerDetectedState = new E2_PlayerDetectedState(stateMachine, this, "playerDetected", playerDetectedStateData,this);
        shootState = new E2_ShootState(stateMachine, this, "shoot", attackPosition, shootStateData,this);
        deadState = new E2_DeadState(stateMachine, this, "dead", deadStateData, this);

        stateMachine.Initialize(lookForPlayerState);
    }

    public void ShootArrow()
    {
        Instantiate(arrowPrefab, attackPosition.position, Quaternion.identity);
    }

    #region Damage and Death
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
    #endregion
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private GameObject target;
    private Rigidbody2D rb;

    public float arrowForce = 5.0f;
    public float attackDamage = 10f;

    public LayerMask whatIsPlayer;

    protected AttackDetails attackDetails;

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");

        attackDetails.damageAmount = attackDamage;
        attackDetails.position = transform.position;

        Vector3 direction = target.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * arrowForce;

        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SendMessage("Damage", attackDetails);
            Destroy(gameObject);
        }
    }
}

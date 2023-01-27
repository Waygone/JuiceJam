using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    GameObject enemy;
    [SerializeField] Vector3 targetPos;
    [SerializeField] float speed;
    [SerializeField] float time = 3;
    [SerializeField] float randDiff;

    private void Start()
    {
        enemy = this.gameObject;
        targetPos = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
    }

    private void Update()
    {
        
        time += Time.deltaTime;

        if (time > 3)
        {
            time = 0;
            randDiff = Random.Range(-3, 3);
        }

        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPos + new Vector3(randDiff, 0, 0), Time.deltaTime * speed);
    }

}

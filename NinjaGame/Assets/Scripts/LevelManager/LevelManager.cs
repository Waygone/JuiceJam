using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //OBJECTS
    public Player player;
    public Transform enemyFolder;
    public Transform enemyPrefab;

    //LEVEL DATA
    public Vector2 playerPos;
    public Vector2[] enemyPos;

    private void Start()
    {
        setupLevel();
    }

    void setupLevel()
    {
        clearAll();
        player.transform.position = new Vector2(playerPos.x, playerPos.y);
        spawnEnemies();
    }

    void spawnEnemies()
    {
        for (int i = 0; i < enemyPos.Length; i++)
        {
            Transform newEnemy = Instantiate(enemyPrefab) as Transform;
            newEnemy.position = new Vector2(enemyPos[i].x, enemyPos[i].y);
            newEnemy.parent = enemyFolder;
        }
    }

    void clearAll()
    {
        foreach(Transform child in enemyFolder)
        {
            Destroy(child.gameObject);
        }
    }
}

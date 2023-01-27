using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //OBJECTS
    public Player player;
    public Transform enemyFolder;
    public Transform enemyPrefab;
    public Transform platformFolder;
    public Transform platformPrefab;

    //LEVEL DATA
    public Vector2 playerPos;
    public Vector2[] enemyPos;
    public Vector2[] platformPos;

    private void Start()
    {
        setupLevel();
    }

    void setupLevel()
    {
        clearAll();
        player.transform.position = new Vector2(playerPos.x, playerPos.y);
        spawnEnemies();
        spawnPlatforms();
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

    void spawnPlatforms()
    {
        for (int i = 0; i < platformPos.Length; i++)
        {
            Transform newPlatform = Instantiate(platformPrefab) as Transform;
            newPlatform.position = new Vector2(platformPos[i].x, platformPos[i].y);
            newPlatform.parent = platformFolder;
        }
    }

    void clearAll()
    {
        foreach(Transform child in enemyFolder)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in platformFolder)
        {
            Destroy(child.gameObject);
        }
    }
}

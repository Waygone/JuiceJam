using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private string nextScene;

    public void NextLevel()
    {
        SceneManager.LoadScene(nextScene);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            NextLevel();
        }
    }
}

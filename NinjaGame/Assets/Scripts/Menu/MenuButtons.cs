using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    private const string LEVEL_1 = "Level1";
    public void Play()
    {
        SceneManager.LoadScene(LEVEL_1);
    }
    public void RepeatLevel()
    {
        SceneManager.LoadScene(LEVEL_1);
    }
}

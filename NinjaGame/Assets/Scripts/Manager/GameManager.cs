using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPoint;
    //[SerializeField]
    private GameObject player;
    [SerializeField]
    private float respawnTime;

    private float respawnTimeStart;
    private bool respawn;
    public bool hasAlreadyRespawned;

    private CinemachineVirtualCamera CVC;

    private void Start()
    {
        player = GameObject.Find("Player");
        CVC = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
        Screen.SetResolution(1280, 720, FullScreenMode.FullScreenWindow, 60);
        hasAlreadyRespawned = false;
    }
    private void Update()
    {
        CheckResapwn();
    }
    public void Respawn()
    {
        hasAlreadyRespawned = false;
        respawnTimeStart = Time.time;
        Time.timeScale = 1f;
        respawn = true;
    }

    private void CheckResapwn()
    {
        if(Time.time >= respawnTimeStart + respawnTime && respawn)
        {
            print("Respawned");
            //var playerTemp = Instantiate(player, respawnPoint.position, Quaternion.Euler(0f,0f,0f), transform);
            player.transform.position = respawnPoint.position;
            hasAlreadyRespawned = true;
            //CVC.m_Follow = playerTemp.transform;
            respawn = false;
            //ReloadScene();
        }
    }


    public void SetCheckpoint(Transform newPos)
    {
        respawnPoint = newPos;
    }
    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}

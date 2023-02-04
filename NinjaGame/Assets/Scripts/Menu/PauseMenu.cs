using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private PlayerInputController playerInput;
    private InputAction menu;

    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject pauseOptionsUI;
    [SerializeField] private bool isPaused;

    private void Awake()
    {
        playerInput = new PlayerInputController();
    }

    private void Update()
    {
        
    }
    private void OnEnable()
    {
        menu = playerInput.Menus.Pause;
        menu.Enable();

        menu.performed += SetPause;
    }
    private void OnDisable()
    {
        menu.Disable();
    }

    void SetPause(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }
    public void Pause()
    {
        Time.timeScale = 0f; 
        AudioListener.pause = true;
        pauseUI.SetActive(true);
        isPaused = true;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        pauseUI.SetActive(false);
        pauseOptionsUI.SetActive(false);
        isPaused = false;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

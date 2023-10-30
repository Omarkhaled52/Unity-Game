using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] public GameObject PauseMenuPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                resumeGame();
                isPaused = false;
            }
            else
            {
                pauseGame();
                isPaused = true;
            }

        }
    }

    public void resumeGame()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void pauseGame()
    {
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void restartGame()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;


    }
    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

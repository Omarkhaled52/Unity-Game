using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField]   TextMeshProUGUI mute;
    [SerializeField] TextMeshProUGUI score;
    public static Boolean isMuted = false;

    private void Start()
    { 
    }   

    private void Update()
    {
        
        setScore();
        setMute();

        
    }

    public  void setMute()
    {
        if (isMuted)
        {
            mute.text = "Unmute";
        }
        else
        {
            mute.text = "Mute";
        }
    }

    public void playGame()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void restartGame()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }

    public void optionsMenu()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void muteSounds()
    {
        AudioListener.pause = !AudioListener.pause;
        isMuted = !isMuted;
    }

    public void howToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void credits()
    {
        SceneManager.LoadScene("Credits");
    }

   
    public void setScore()
    {
        int fScore = PlayerScript.counter;
        score.text = "Final Score: " + fScore;
    }

}

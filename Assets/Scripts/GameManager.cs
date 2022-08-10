using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public class GameSettings
    {
        public static float generalVolume = 1;
        public static bool gameIsPaused;
        public static void ChangeVolume(float value)
        {
            foreach (AudioSource audioSource in GameObject.FindObjectsOfType<AudioSource>())
            {
                audioSource.volume = value;
            }
        }
        public static void ChangeResolution(int resolutionIndex)
        {
            Resolution[] resolutions = Screen.resolutions;
            Resolution newResolution = resolutions[resolutionIndex];
            Screen.SetResolution(newResolution.width, newResolution.height, true);
        }
        public static void ChangeScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        public static void CloseGame()
        {
            Application.Quit();
        }
        public static void PauseGame()
        {
            if (gameIsPaused)
            {
                Time.timeScale = 0;
                AudioListener.pause = true;
            }
            else
            {
                Time.timeScale = 1;
                AudioListener.pause = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Variables

    [SerializeField] private string m_GameSceneName;

    #endregion

    #region Exit

    public void Exit()
    {
        Application.Quit();
    }

    #endregion

    #region Play

    public void Play()
    {
        SceneManager.LoadScene(m_GameSceneName);
    }

    #endregion

    #region ToggleFullscreen

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    #endregion
}

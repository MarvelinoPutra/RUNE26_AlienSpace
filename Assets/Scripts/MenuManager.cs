using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static ObstacleManager Instance { get; private set; }
    //fungsi ngeload ke scene gamenya
    public GameObject Menu, Setting, Credits_UI, Logo;
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void ExitGame()
    {
        //fix some issue ig
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
        Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
#if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE) 
    Application.Quit();
#elif (UNITY_WEBGL)
    Application.OpenURL("about:blank");
#endif
    }

    public void Settings()
    {
        Menu.SetActive(false);
        Setting.SetActive(true);
        Logo.SetActive(false);
        AudioManager.Instance.PlaySFX("Click");
    }

    public void BackToMenu()
    {
        Menu.SetActive(true);
        Setting.SetActive(false);
        Credits_UI.SetActive(false);
        Logo.SetActive(true);
        AudioManager.Instance.PlaySFX("Click");
    }

    public void Credits()
    {
        Credits_UI.SetActive(true);
        Menu.SetActive(false);
        Logo.SetActive(false);
        AudioManager.Instance.PlaySFX("Click");
    }
}

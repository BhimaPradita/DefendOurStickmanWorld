using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuCanvas : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject Panel_SelectLevel;
    [SerializeField] private GameObject Panel_Settings;
    [SerializeField] private GameObject Panel_Credit;

    public void Start()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SelectLevel()
    {
        Panel_SelectLevel.SetActive(true);
    }

    public void Close_SelectLevel()
    {
        Panel_SelectLevel.SetActive(false);
    }

    public void Settings()
    {
        Panel_Settings.SetActive(true);
    }

    public void Close_Settings()
    {
        Panel_Settings.SetActive(false);
    }

    public void Credit()
    {
        Panel_Credit.SetActive(true);
    }

    public void Close_Credit()
    {
        Panel_Credit.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

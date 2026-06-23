using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image fadePanel;

    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private Color fadeColor = Color.black;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private float elapsedTime = 0f;

    [Header("Scene Settings")]
    [SerializeField] private string firstLevel;

    private bool isTransitioning = false;

    // private void Start()
    // {
    //     fadePanel.gameObject.SetActive(true);

    //     Color color = fadePanel.color;
    //     color.a = 0f;
    //     fadePanel.color = color;
    // }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        if (isTransitioning) yield break;

        isTransitioning = true;

        fadePanel.gameObject.SetActive(true);

        Color color = fadePanel.color;
        color.a = 0f;
        fadePanel.color = color;

        float elapsedTime = 0f;
        float startVolume = musicSource.volume;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float progress = elapsedTime / fadeDuration;

            // Fade layar menjadi hitam
            color.a = Mathf.Lerp(0f, 1f, progress);
            fadePanel.color = color;

            // Fade out musik
            musicSource.volume = Mathf.Lerp(startVolume, 0f, progress);

            yield return null;
        }

        color.a = 1f;
        fadePanel.color = color;
        musicSource.volume = 0f;

        yield return new WaitForSeconds(0.2f);

        SceneManager.LoadScene(sceneName);
    }

    public void NewGame()
    {
        StartCoroutine(FadeOutAndLoadScene(firstLevel));
    }
    
    public void Exit()
    {
        StartCoroutine(FadeOutAndQuit());
    }

    private IEnumerator FadeOutAndQuit()
    {
        if (isTransitioning) yield break;
        
        isTransitioning = true;
        
        fadePanel.gameObject.SetActive(true);
        
        float elapsedTime = 0f;
        Color color = fadePanel.color;
        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            color.a = alpha;
            fadePanel.color = color;
            yield return null;
        }
        
        color.a = 1f;
        fadePanel.color = color;
        
        yield return new WaitForSeconds(0.2f);
        
        Application.Quit();
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        
        isTransitioning = false;
    }
}

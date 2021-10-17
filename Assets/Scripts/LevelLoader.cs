 using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject GameScreen;
    public GameObject FadeScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;
    public Animator animator;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void FadeOutScene()
    {
        animator.SetTrigger("FadeOut");
    }

    public void TutorialsLoad()
    {
        SceneManager.LoadScene("Tutorials");
    }

    public void TutorialsBack()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        GameScreen.SetActive(false);
        FadeScreen.SetActive(false);
        loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }
    }
}

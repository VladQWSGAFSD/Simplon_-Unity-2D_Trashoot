using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 2.0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        fadeImage.gameObject.SetActive(false);
    }

    public void FadeToBlack(Action onFadeComplete)
    {
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(FadeToBlackCoroutine(onFadeComplete));
    }

    private IEnumerator FadeToBlackCoroutine(Action onFadeComplete)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float fillAmount = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.fillAmount = fillAmount;
            yield return null;
        }

        onFadeComplete?.Invoke();
    }

    public void FadeFromBlack(Action onFadeComplete = null)
    {
        StartCoroutine(FadeFromBlackCoroutine(onFadeComplete));
    }

    private IEnumerator FadeFromBlackCoroutine(Action onFadeComplete)
    {
        float elapsedTime = fadeDuration;
        while (elapsedTime > 0)
        {
            elapsedTime -= Time.unscaledDeltaTime;
            float fillAmount = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.fillAmount = fillAmount;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
        onFadeComplete?.Invoke();
    }
}

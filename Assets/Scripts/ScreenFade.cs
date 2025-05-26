using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    [SerializeField] SpriteRenderer fadeScreen;


    void Awake()
    {
        fadeScreen = GameObject.Find("ScreenFade").GetComponent<SpriteRenderer>();
    }

    public IEnumerator FadeInCoroutine(float duration)
    {
        Color startColor = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, 1);
        Color targetColor = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, 0);

        yield return FadeCoroutine(startColor, targetColor, duration);

        gameObject.SetActive(false);
    }

    public IEnumerator FadeOutCoroutine(float duration)
    {
        Color startColor = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, 0);
        Color targetColor = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, 1);

        yield return FadeCoroutine(startColor, targetColor, duration);

        gameObject.SetActive(false);
    }

    private IEnumerator FadeCoroutine(Color startColor, Color targetColor, float duration)
    {
        float elapsedTime = 0;
        float elapsedPercentage = 0;

        while (elapsedPercentage < 1)
        {
            elapsedPercentage = elapsedTime / duration;
            fadeScreen.color = Color.Lerp(startColor, targetColor, elapsedPercentage);

            yield return null;
            elapsedTime += Time.deltaTime;
        }
    }
}
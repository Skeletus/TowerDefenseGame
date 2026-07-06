using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Animator : MonoBehaviour
{
    [Header("UI-Feedback - Shake Effects")]
    [SerializeField] private float shakeMagnitude;
    [SerializeField] private float shakeDuration;
    [Space]
    [SerializeField] private float defaultUIScale = 1.5f;
    [SerializeField] private bool scaleChangeAvailable;

    public void Shake(Transform transformToShake)
    {
        RectTransform rectTransform = transformToShake.GetComponent<RectTransform>();
        StartCoroutine(ShakeCoroutine(rectTransform));
    }

    private IEnumerator ShakeCoroutine(RectTransform rectTransform)
    {
        float time = 0;
        Vector3 originalPosition = rectTransform.anchoredPosition;
        float currentScale = rectTransform.localScale.x;

        if (scaleChangeAvailable )
        {
            StartCoroutine(ChangeScaleCo(rectTransform, currentScale * 1.1f, shakeDuration/ 2));
        }

        while(time < shakeDuration)
        {
            float xOffset = Random.Range(-shakeMagnitude, shakeMagnitude);
            float yOffset = Random.Range(-shakeMagnitude, shakeMagnitude);

            rectTransform.anchoredPosition = originalPosition + new Vector3 (xOffset, yOffset);

            time += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = originalPosition;

        if (scaleChangeAvailable)
        {
            StartCoroutine(ChangeScaleCo(rectTransform, defaultUIScale, shakeDuration / 2));
        }
    }

    public void ChangePosition(Transform transform, Vector3 offset, float duration = .1f)
    {
        RectTransform rectTransform = transform.GetComponent<RectTransform>();
        StartCoroutine(ChangePositionCo(rectTransform, offset, duration));
    }

    private IEnumerator ChangePositionCo(RectTransform rectTransform, Vector3 offset, float duration)
    {
        float time = 0;

        Vector3 initialPosition = rectTransform.anchoredPosition;
        Vector3 targetPosition = initialPosition + offset;

        while (time < duration)
        {
            rectTransform.anchoredPosition =
                Vector3.Lerp(initialPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = targetPosition;
    }

    public void ChangeScale(Transform transform, float targetScale, float duration = .25f)
    {
        RectTransform rectTransform = transform.GetComponent<RectTransform>();
        StartCoroutine(ChangeScaleCo(rectTransform, targetScale, duration));
    }

    public IEnumerator ChangeScaleCo(RectTransform rectTransform, float newScale, float duration = .25f)
    {
        float time = 0f;
        Vector3 initialScale = rectTransform.localScale;
        Vector3 targetScale = new Vector3(newScale, newScale, newScale);

        while (time < duration)
        {
            rectTransform.localScale = Vector3.Lerp(initialScale, targetScale, time / duration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        rectTransform.localScale = targetScale;
    }

    public void ChangeColor(Image image, float targetAlpha, float duration)
    {
        StartCoroutine(ChangeColorCo(image, targetAlpha, duration));
    }

    private IEnumerator ChangeColorCo(Image image, float targetAlpha, float duration)
    {
        float time = 0;
        Color currentColor = image.color;
        float startAplha = currentColor.a;

        while(time < duration)
        {
            float alpha = Mathf.Lerp(startAplha, targetAlpha, time / duration);
            image.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);

            time += Time.deltaTime;

            yield return null;
        }

        image.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
    }

}

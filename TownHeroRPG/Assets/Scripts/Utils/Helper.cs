using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static IEnumerator IEFade(CanvasGroup canvasGroup, float desiredValue, float fadeTime)
    {
        float timer = 0;
        float initialValue = canvasGroup.alpha;
        while(timer < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(initialValue, desiredValue, timer / fadeTime);
            timer += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = desiredValue;
    }
}
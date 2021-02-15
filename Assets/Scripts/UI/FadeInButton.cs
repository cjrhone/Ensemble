using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInButton : MonoBehaviour
{

    public CanvasGroup CanvasGroup;
    public float FadeInSeconds;

    void Awake() {
        CanvasGroup.interactable = false;
        CanvasGroup.alpha = 0.0f;
    }
    
    public void StartFadingIn(){
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn(){
        var startTime = Time.time;
        var endTime = startTime + FadeInSeconds;

        while(Time.time < endTime){
            var elapsedTime = Time.time - startTime;
            CanvasGroup.alpha = Mathf.Lerp(0.0f, 1.0f, elapsedTime/FadeInSeconds);
            yield return new WaitForFixedUpdate();
        }

        CanvasGroup.interactable = true;

    }
}

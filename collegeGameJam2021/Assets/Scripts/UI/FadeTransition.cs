using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTransition : MonoBehaviour
{
    static FadeState state = FadeState.Unset;

    [Header("Canvas Variables")]
    [SerializeField] Image fadeImage;


    float fadeSpeed = 4f;

    private void Awake()
    {
        StaticDelegates.Fade = this.FadeOut;
    }

    private void OnDestroy()
    {
        StaticDelegates.Fade = null;
    }


    // Start is called before the first frame update
    void Start()
    {
        switch(state)
        {
            case FadeState.Unset:
                Color c = fadeImage.color;
                c.a = 0f;
                fadeImage.color = c;
                state = FadeState.FadeOut;
                fadeImage.gameObject.SetActive(false);
                break;
            case FadeState.FadeIn:
                StaticDelegates.UpdateAllMovement(false);
                StartCoroutine(FadeFromBlack());
                break;
      
        }
    }

    void FadeOut()
    {
        StartCoroutine(FadeToBlack());
    }

    IEnumerator FadeToBlack()
    {
        fadeImage.gameObject.SetActive(true);
        Color c = fadeImage.color;
        c.a = 0f;

        while(c.a < 1f)
        {
            c.a += fadeSpeed * Time.deltaTime;
            fadeImage.color = c;
            yield return null;
        }

        c.a = 1f;
        fadeImage.color = c;
        state = FadeState.FadeIn;

        StaticDelegates.TransitionToNextLevel();
    }

    IEnumerator FadeFromBlack()
    {
        Color c = fadeImage.color;
        c.a = 1f;
        fadeImage.color = c;

        while (c.a > 0f)
        {
            c.a -= fadeSpeed * Time.deltaTime;
            fadeImage.color = c;
            yield return null;
        }

        c.a = 0f;
        fadeImage.color = c;
        state = FadeState.FadeOut;
        fadeImage.gameObject.SetActive(false);
        StaticDelegates.PlaySequence(true);
    }

}

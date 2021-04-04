using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeTransition : MonoBehaviour
{
    static FadeState state = FadeState.Unset;

    [Header("Canvas Variables")]
    [SerializeField] Image fadeImage;
    
    [Header("Other")]
    [SerializeField] float DelayBeforeBlack = 1f;


    float fadeSpeed = 4f;

    private void Awake()
    {
        StaticDelegates.Fade = this.FadeOut;

        if (SceneManager.GetActiveScene().buildIndex == 0)
            state = FadeState.Unset;
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
                state = FadeState.FadeIn;
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
        yield return new WaitForSeconds(DelayBeforeBlack);

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
        Debug.Log("Fading");
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

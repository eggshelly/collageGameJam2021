using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationSequence : MonoBehaviour
{
    [Header("Canvas Variables")]
    [SerializeField] GameObject pregamePanel;
    [SerializeField] GameObject postgamePanel;

    [Header("Images")]
    [SerializeField] List<Sprite> pregame;
    [SerializeField] List<Sprite> postgame;

    [Header("Animation Variables")]
    [SerializeField] float timeBetweenImageSwap;

    WaitForSeconds swapDelay;

    private void Awake()
    {
        for(int i = 0; i < pregame.Count; ++i)
        {
            if(i < pregamePanel.transform.childCount)
            {
                pregamePanel.transform.GetChild(i).GetComponent<Image>().sprite = pregame[i];
            }
            else
            {
                GameObject newobj = Instantiate(pregamePanel.transform.GetChild(0).gameObject, pregamePanel.transform);
                newobj.GetComponent<Image>().sprite = pregame[i];
            }
        }

        for (int i = 0; i < postgame.Count; ++i)
        {
            if (i < postgamePanel.transform.childCount)
            {
                postgamePanel.transform.GetChild(i).GetComponent<Image>().sprite = postgame[i];
            }
            else
            {
                GameObject newobj = Instantiate(postgamePanel.transform.GetChild(0).gameObject, postgamePanel.transform);
                newobj.GetComponent<Image>().sprite = postgame[i];
            }
        }

        if (pregame.Count == 0)
            pregamePanel.SetActive(false);

        postgamePanel.SetActive(false);


        swapDelay = new WaitForSeconds(timeBetweenImageSwap);

        StaticDelegates.Sequence += this.PlaySequence;
    }

    private void OnDestroy()
    {
        StaticDelegates.Sequence -= this.PlaySequence;
    }

    void PlaySequence(bool pre)
    {
        if(pre)
        {
            StartCoroutine(PregameSequence());
        }
        else
        {
            StartCoroutine(PostgameSequence());
        }
    }

    IEnumerator PregameSequence()
    {
        int i = 0; 
        if(pregame.Count > 0)
        {
            while (i < pregamePanel.transform.childCount)
            {
                pregamePanel.transform.GetChild(i).SetAsLastSibling();
                i += 1;
                yield return swapDelay;
            }
        }

        StaticDelegates.UpdateGameState(true);
    }

    IEnumerator PostgameSequence()
    {
        if (postgame.Count > 0)
        {
            postgamePanel.SetActive(true);
            int i = 0;
            while (i < postgamePanel.transform.childCount)
            {
                postgamePanel.transform.GetChild(i).SetAsLastSibling();
                i += 1;
                yield return swapDelay;
            }

        }
        StaticDelegates.FadeOut();

    }
}

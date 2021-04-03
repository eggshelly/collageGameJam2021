using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameSprite : MonoBehaviour
{
    [SerializeField] List<SpriteToResult> sprites;

    SpriteRenderer rend;

    private void Awake()
    {
        StaticDelegates.GameState += this.GameEnd;

        rend = this.GetComponent<SpriteRenderer>();
    }

    private void OnDestroy()
    {
        StaticDelegates.GameState -= this.GameEnd;
    }

    void GameEnd(bool start)
    {
        if(!start)
        {
            if(sprites.Count > 0)
            {
                foreach(SpriteToResult r in sprites)
                {
                    if(r.GetResult() == GameManager.GetFinalResult())
                    {
                        rend.sprite = r.GetSprite();
                        break;
                    }
                }
            }
        }
    }
}

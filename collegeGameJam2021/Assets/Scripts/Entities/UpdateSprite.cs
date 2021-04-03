using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSprite : MonoBehaviour
{
    [SerializeField] UpdateSpriteType UpdateType;
    [SerializeField] List<Sprite> spritesToUse;

    [SerializeField] bool ShouldChangeResult;
    [SerializeField] ResultType ChangedResult;

    //Cycling through sprites
    [SerializeField] Actions ActionToCycle;


    //Updating sprite on certain keys
    [SerializeField] List<Actions> actionsToMap;

    //Toggle
    [SerializeField] Actions ActionToToggle;

    int currentSpriteIndex = 0;
    List<Actions> actionsStarted = new List<Actions>();


    bool canMove = true;

    SpriteRenderer rend;

    private void Awake()
    {
        StaticDelegates.UpdateMovement += ToggleMovement;

        rend = this.GetComponent<SpriteRenderer>();
    }

    private void OnDestroy()
    {
        StaticDelegates.UpdateMovement -= ToggleMovement;
    }

    void ToggleMovement(bool canMove)
    {
        this.canMove = canMove;
    }

    public void UpdateCurrentSprite(Actions action, bool released)
    {
        if (spritesToUse.Count == 0 || !canMove)
            return;
        switch(UpdateType)
        {
            case UpdateSpriteType.Cycle:
                if (!released && action == ActionToCycle)
                    CycleSprite();
                break;
            case UpdateSpriteType.UpdateOnCertainKeys:
                if (!released)
                    UpdateSpriteOnKey(action);
                break;
            case UpdateSpriteType.UpdateOnCertainKeysWithIdle:
                UpdateSpriteOnKeyWithIdle(action, released);
                break;
            case UpdateSpriteType.Toggle:
                if(action == ActionToToggle)
                    ToggleSprite(released);
                break;

        }
    }

    void CycleSprite()
    {
        currentSpriteIndex = (currentSpriteIndex + 1) % spritesToUse.Count;
        rend.sprite = spritesToUse[currentSpriteIndex];
    }

    void UpdateSpriteOnKey(Actions action)
    {
        for(int i = 0; i < actionsToMap.Count; ++i)
        {
            if (actionsToMap[i] == action)
                rend.sprite = spritesToUse[i];
        }
    }

    void UpdateSpriteOnKeyWithIdle(Actions action, bool released)
    {
        if (released)
        {
            if (actionsStarted.Contains(action))
                actionsStarted.Remove(action);

            if (actionsStarted.Count == 0)
                rend.sprite = spritesToUse[spritesToUse.Count - 1];
            else
                rend.sprite = spritesToUse[actionsToMap.IndexOf(actionsStarted[actionsStarted.Count - 1])];
        }
        else
        {
            actionsStarted.Add(action);
            rend.sprite = spritesToUse[actionsToMap.IndexOf(action)];
        }
        
    }

    void ToggleSprite(bool released)
    {
        Debug.Log(released);
        if (spritesToUse.Count < 2)
            return;

        if(released)
        {
            rend.sprite = spritesToUse[0];
        }
        else
        {
            rend.sprite = spritesToUse[1];
        }
    }

    public ResultType CheckIfResultChanged()
    {
        if (ShouldChangeResult && currentSpriteIndex != 0)
            return ChangedResult;
        return ResultType.None;
    }


}

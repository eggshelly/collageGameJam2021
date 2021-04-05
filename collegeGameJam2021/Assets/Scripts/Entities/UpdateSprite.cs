using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSprite : MonoBehaviour
{
    [SerializeField] UpdateSpriteType UpdateType;
    [SerializeField] List<SpriteToCollider> spritesToUse;

    [SerializeField] bool ShouldChangeResult;
    [SerializeField] ResultType ChangedResult = ResultType.None;

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
        StaticDelegates.ChangeSprite += UpdateCurrentSprite;

        rend = this.GetComponent<SpriteRenderer>();
        ToggleListeners(true);
    }

    private void OnDestroy()
    {
        StaticDelegates.UpdateMovement -= ToggleMovement;
        StaticDelegates.ChangeSprite -= UpdateCurrentSprite;

        ToggleListeners(false);
    }

    void ToggleMovement(bool canMove)
    {
        this.canMove = canMove;
    }

    void ToggleListeners(bool awake)
    {
        switch(UpdateType)
        {
            case UpdateSpriteType.OnEvent:
                if (awake)
                    StaticDelegates.TriggerEvent += this.OnEvent;
                else
                    StaticDelegates.TriggerEvent -= this.OnEvent;
                break;
        }
    }

    void UpdateCurrentSprite(Actions action, bool released, System.Action<Collider2D, ResultType> callback)
    {
        if (spritesToUse.Count == 0 || !canMove)
            return;
        switch(UpdateType)
        {
            case UpdateSpriteType.Cycle:
                if (!released && action == ActionToCycle)
                    CycleSprite(callback);
                break;
            case UpdateSpriteType.UpdateOnCertainKeys:
                if (!released)
                    UpdateSpriteOnKey(action, callback);
                break;
            case UpdateSpriteType.UpdateOnCertainKeysWithIdle:
                UpdateSpriteOnKeyWithIdle(action, released, callback);
                break;
            case UpdateSpriteType.Toggle:
                if(action == ActionToToggle)
                    ToggleSprite(released, callback);
                break;

        }
    }

    void CycleSprite(System.Action<Collider2D, ResultType> callback)
    {

        currentSpriteIndex = (currentSpriteIndex + 1) % spritesToUse.Count;
        rend.sprite = spritesToUse[currentSpriteIndex].GetSprite();
        SetCollider(callback, currentSpriteIndex);

        StaticDelegates.PlayAudio(false);
    }

    void UpdateSpriteOnKey(Actions action, System.Action<Collider2D, ResultType> callback)
    {
        for(int i = 0; i < actionsToMap.Count; ++i)
        {
            if (actionsToMap[i] == action)
            {
                rend.sprite = spritesToUse[i].GetSprite();
                SetCollider(callback, i);
                StaticDelegates.PlayAudio(false);
            }
        }
    }

    void UpdateSpriteOnKeyWithIdle(Actions action, bool released, System.Action<Collider2D, ResultType> callback)
    {
        if (released)
        {
            if (actionsStarted.Contains(action))
                actionsStarted.Remove(action);

            if (actionsStarted.Count == 0)
            {
                rend.sprite = spritesToUse[spritesToUse.Count - 1].GetSprite();
                SetCollider(callback, spritesToUse.Count - 1);
            }
            else
            {
                rend.sprite = spritesToUse[actionsToMap.IndexOf(actionsStarted[actionsStarted.Count - 1])].GetSprite();
                SetCollider(callback, actionsToMap.IndexOf(actionsStarted[actionsStarted.Count - 1]));
            }
            StaticDelegates.PlayAudio(false);
        }
        else
        {
            if(actionsToMap.Contains(action))
            {
                actionsStarted.Add(action);
                rend.sprite = spritesToUse[actionsToMap.IndexOf(action)].GetSprite();
                SetCollider(callback, actionsToMap.IndexOf(action));
                StaticDelegates.PlayAudio(false);
            }
  
        }
        
    }

    void SetCollider(System.Action<Collider2D, ResultType> callback, int index)
    {

        if (spritesToUse[index].GetCollider() != null)
        {
            Debug.Log(ShouldChangeResult + " " + ChangedResult);
            callback(spritesToUse[index].GetCollider(), ShouldChangeResult ? (index == 0 ? ResultType.Win : ChangedResult) : ResultType.None);
        }
    }

    void ToggleSprite(bool released, System.Action<Collider2D, ResultType> callback)
    {
        if (spritesToUse.Count < 2)
            return;

        if(released)
        {
            rend.sprite = spritesToUse[0].GetSprite();
            SetCollider(callback, 0);
        }
        else
        {
            rend.sprite = spritesToUse[1].GetSprite();
            SetCollider(callback, 1);
        }
        StaticDelegates.PlayAudio(false);
    }

    void OnEvent()
    {
        if(currentSpriteIndex < spritesToUse.Count)
        {
            rend.sprite = spritesToUse[currentSpriteIndex].GetSprite();
            currentSpriteIndex += 1;
        }

        StaticDelegates.PlayAudio(false);
    }

    public ResultType CheckIfResultChanged()
    {
        if (ShouldChangeResult && currentSpriteIndex != 0)
            return ChangedResult;
        return ResultType.None;
    }

    public void ChangeSpriteAndRemove()
    {
        if (spritesToUse.Count == 0)
            return;

        rend.sprite = spritesToUse[0].GetSprite();
        this.gameObject.layer = LayerMask.NameToLayer("Default");
    }


}

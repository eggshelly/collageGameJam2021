using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] List<Actions> directionsToMove;


    [Header("Completion")]
    [SerializeField] CompletionType CompleteType;

    //For Collection and CollectThenMoveToLocation
    [SerializeField] int NumItemsToCollect;

    //For MoveTargetToLocation
    [SerializeField] GameObject PlayerMinion;

    //For Hold For Time and HoldToggle
    [SerializeField] float TimeToHold;
    [SerializeField] bool ContinuousHold = false;

    //For MoveToLocationAndHoldKey
    [SerializeField] bool ShouldTriggerEvent;
    [SerializeField] List<ThresholdToResult> eventSlots;

    //PressKeyMultipleTimes
    [SerializeField] int TimesToPress;

    //Match Output
    [SerializeField] TMP_InputField field;
    [SerializeField] string output;

    bool SelectKeyPressed = false;

    System.Func<bool> completionFunction;

    ResultType result = ResultType.Lose;
    float timer = 0f;
    float eventTimer = 0f;
    int counter = 0;

    GeneralMovement movement;
    Movement controls;
    Collider2D coll;

    private void Awake()
    {
        movement = this.GetComponent<GeneralMovement>();
        this.gameObject.layer = LayerMask.NameToLayer("Player");


        SetupControls();
        AssignCompletionFunction();
    }



    private void Start()
    {
        movement.UpdateSpeed(speed);
    }

    private void Update()
    {
        if(movement.CanCurrentlyMove())
        {
            if (completionFunction())
            {
                if (result == ResultType.Lose)
                    result = ResultType.Win;
                GameManager.GameFinished(result);
            }
        }
    }


    private void OnEnable()
    {
        controls.Move.Enable();
    }

    private void OnDisable()
    {
        controls.Move.Disable();
    }

    void SetupControls()
    {
        controls = new Movement();
        
        foreach(Actions d in directionsToMove)
        {
            switch(d)
            {
                case Actions.Up:
                    controls.Move.Up.started += up => movement.UpdateInput(d, false);
                    controls.Move.Up.canceled += up => movement.UpdateInput(d, true);
                    controls.Move.Up.started += up => StaticDelegates.InvokeChangeSprite(d, false, SetCollider);
                    controls.Move.Up.canceled += up => StaticDelegates.InvokeChangeSprite(d, true, SetCollider);
                    break;
                case Actions.Down:
                    controls.Move.Down.started += up => movement.UpdateInput(d, false);
                    controls.Move.Down.canceled += up => movement.UpdateInput(d, true);                  
                    controls.Move.Down.started += up => StaticDelegates.InvokeChangeSprite(d, false, SetCollider);
                    controls.Move.Down.canceled += up => StaticDelegates.InvokeChangeSprite(d, true, SetCollider);
                    break;
                case Actions.Left:
                    controls.Move.Left.started += up => movement.UpdateInput(d, false);
                    controls.Move.Left.canceled += up => movement.UpdateInput(d, true);
                    controls.Move.Left.started += up => StaticDelegates.InvokeChangeSprite(d, false, SetCollider);
                    controls.Move.Left.canceled += up => StaticDelegates.InvokeChangeSprite(d, true, SetCollider);
                    break;
                case Actions.Right:
                    controls.Move.Right.started += up => movement.UpdateInput(d, false);
                    controls.Move.Right.canceled += up => movement.UpdateInput(d, true);
                    controls.Move.Right.started += up => StaticDelegates.InvokeChangeSprite(d, false, SetCollider);
                    controls.Move.Right.canceled += up => StaticDelegates.InvokeChangeSprite(d, true, SetCollider);
                    break;
                case Actions.Select:
                    controls.Move.Select.started += up => movement.UpdateInput(d, false);
                    controls.Move.Select.canceled += up => movement.UpdateInput(d, true);
                    controls.Move.Select.started += up => StaticDelegates.InvokeChangeSprite(d, false, SetCollider);
                    controls.Move.Select.canceled += up => StaticDelegates.InvokeChangeSprite(d, true, SetCollider);
                    break;
            }    
        }

        switch(CompleteType)
        {
            case CompletionType.MoveToLocationsAndSelect:
            case CompletionType.MoveToLocationAndSelect:
                controls.Move.Select.started += up => UpdateSelectKeyState(false);
                controls.Move.Select.canceled += up => UpdateSelectKeyState(true);
                break;
            default:
                if(ShouldTriggerEvent)
                {
                    goto case CompletionType.MoveToLocationAndSelect;
                }
                break;

        }
    }

    void UpdateSelectKeyState(bool released)
    {
        SelectKeyPressed = !released;
    }


    void SetCollider(Collider2D coll, ResultType newResult)
    {
        this.coll.enabled = false;
        this.coll = coll;
        this.coll.enabled = true;

        result = newResult == ResultType.None ? result : newResult;
    }

    void AssignCompletionFunction()
    {
        switch(CompleteType)
        {
            case CompletionType.MoveToLocation:
                coll = this.GetComponent<Collider2D>();
                completionFunction = this.MoveToLocation;
                break;
            case CompletionType.MoveToCorrectLocation:
                coll = this.GetComponent<Collider2D>();
                completionFunction = this.MoveToCorrectLocation;
                break;
            case CompletionType.Collection:
                coll = this.GetComponent<Collider2D>();
                completionFunction = this.Collection;
                break;
            case CompletionType.CollectThenMoveToLocation:
                coll = this.GetComponent<Collider2D>();
                completionFunction = this.CollectThenMoveToLocation;
                break;
            case CompletionType.MoveTargetToLocation:
                completionFunction = this.MoveTargetToLocation;
                break;
            case CompletionType.HoldForTime:
            case CompletionType.HoldToggle:
                coll = this.GetComponent<Collider2D>();
                completionFunction = this.HoldForTime;
                break;
            case CompletionType.MoveToLocationAndSelect:
                coll = this.GetComponent<Collider2D>();
                completionFunction = this.MoveToLocationAndSelect;
                break;
            case CompletionType.MoveToLocationAndHoldKey:
                coll = this.GetComponent<Collider2D>();
                completionFunction = this.MoveToLocationAndHold;
                break;
            case CompletionType.PressKeyMultipleTimes:
                completionFunction = this.PressKeyMultipleTimes;
                break;
            case CompletionType.CollectToLocationAndHold:
                coll = this.GetComponent<Collider2D>();
                completionFunction = this.CollectToLocationAndHold;
                break;
            case CompletionType.MoveToLocationsAndSelect:
                coll = this.GetComponent<Collider2D>();
                completionFunction = MoveToLocationsAndSelect;
                break;
            case CompletionType.MatchOutput:
                completionFunction = this.MatchOutput;
                break;

        }
    }

    #region Completion Functions
    bool MoveToLocation()
    {
        if (coll.enabled == false)
            return false;

        Collider2D loc = Physics2D.OverlapBox(coll.bounds.center, coll.bounds.extents * 2f, this.transform.eulerAngles.z, (1 << LayerMask.NameToLayer("EndLocation")));
        if (loc != null)
        {
            result = loc.GetComponent<EndLocation>().GetResult();
            return true;
        }
        return false;
    }

    bool MoveToCorrectLocation()
    {
        if (coll.enabled == false)
            return false;


        Collider2D[] colls = Physics2D.OverlapBoxAll(coll.bounds.center, coll.bounds.extents * 2f, this.transform.eulerAngles.z, (1 << LayerMask.NameToLayer("EndLocation")));
        if (colls.Length > 0)
        {
            Collider2D closest = colls[0];
            foreach(Collider2D coll in colls)
            {
                if (Vector3.Distance(coll.gameObject.transform.position, this.transform.position) < Vector3.Distance(closest.transform.position, this.transform.position))
                {
                    closest = coll;
                }
            }

            result = closest.GetComponent<EndLocation>().GetResult();
            return true;
        }
        return false;
    }

    bool Collection()
    {
        if (coll.enabled == false)
            return false;


        Collider2D c = Physics2D.OverlapBox(coll.bounds.center, coll.bounds.extents * 2f, this.transform.eulerAngles.z, (1 << LayerMask.NameToLayer("Item")));
        if (c != null)
        {
            c.gameObject.transform.parent = this.gameObject.transform;
            c.gameObject.layer = LayerMask.NameToLayer("Player");
            this.coll = c;
        }

        if (this.transform.childCount == NumItemsToCollect)
        {
            return true;
        }
        return false;
    }

    bool CollectThenMoveToLocation()
    {
        return Collection() && MoveToLocation();
    }

    bool MoveTargetToLocation()
    {
        this.completionFunction = PlayerMinion.GetComponent<Minion>().GetCompletionFunction();
        return false;
    }

    bool HoldForTime()
    {
        if (MoveToLocation())
            timer += Time.deltaTime;
        else
        {
            if (ContinuousHold)
                timer = 0f;
        }

        if (timer >= TimeToHold)
            return true;
        return false;
    }

    bool MoveToLocationAndSelect()
    {
        return MoveToLocation() && SelectKeyPressed;
    }

    bool MoveToLocationAndHold()
    {
        if(MoveToLocation())
        {
            if(SelectKeyPressed)
            {
                timer += Time.deltaTime;
                eventTimer += Time.deltaTime;
                
                if(ShouldTriggerEvent && eventSlots.Count > 0 && Mathf.FloorToInt(eventTimer) >= eventSlots[0].GetThreshold())
                {
                    StaticDelegates.InvokeTriggerEvent();

                    switch(eventSlots[0].GetResult())
                    {
                        case ResultType.Neutral:
                            GameManager.UpdateFinalResult(ResultType.Neutral);
                            break;
                    }

                    eventSlots.RemoveAt(0);
                }
            }
        }
        if (timer >= TimeToHold)
            return true;
        return false;
    }

    bool PressKeyMultipleTimes()
    {
        if(SelectKeyPressed)
        {
            counter += 1;
            SelectKeyPressed = false;
            if(ShouldTriggerEvent && eventSlots.Count > 0 && counter >= eventSlots[0].GetThreshold())
            {
                StaticDelegates.InvokeTriggerEvent();

                switch (eventSlots[0].GetResult())
                {
                    case ResultType.Neutral:
                        GameManager.UpdateFinalResult(ResultType.Neutral);
                        break;
                }
            }
        }
        if (counter >= TimesToPress)
            return true;
        return false;
    }

    bool CollectToLocationAndHold()
    {
        return Collection() && MoveToLocationAndHold();
    }


    bool MoveToLocationsAndSelect()
    {
        if (coll.enabled == false)
            return false;

        Collider2D loc = Physics2D.OverlapBox(coll.bounds.center, coll.bounds.extents * 2f, this.transform.eulerAngles.z, (1 << LayerMask.NameToLayer("EndLocation")));
        if (loc != null)
        {
            if(loc.GetComponent<UpdateSprite>() != null && SelectKeyPressed)
            {
                loc.GetComponent<UpdateSprite>().ChangeSpriteAndRemove();
                SelectKeyPressed = false;
                counter += 1;
                GameManager.UpdateFinalResult(ResultType.Neutral);
            }
        }

        if (counter >= TimesToPress)
            return true;
        return false;
    }

    bool MatchOutput()
    {
        if(!field.isFocused)
        {
            field.Select();
            field.ActivateInputField();
        }

        if (field.text == output)
        {
            field.interactable = false;
            return true;
        }
        return false;
    }

    #endregion
}

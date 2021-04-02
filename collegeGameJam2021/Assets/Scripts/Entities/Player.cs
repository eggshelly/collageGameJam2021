﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] List<Actions> directionsToMove;

    [Header("Completion")]
    [SerializeField] CompletionType CompleteType;

    //For Collection and CollectThenMoveToLocation
    [HideInInspector] public int NumItemsToCollect;

    //For MoveTargetToLocation
    [HideInInspector] public GameObject PlayerMinion;

    //For Hold For Time and HoldToggle
    [HideInInspector] public float TimeToHold;

    System.Func<bool> completionFunction;

    ResultType result = ResultType.Lose;
    bool completed;
    float timer = 0f;

    GeneralMovement movement;
    Movement controls;

    [SerializeField] Collider2D coll;

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

                PlayerData.UpdateData(result);
                completed = true;
                StaticDelegates.UpdateGameState(false);
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
                    break;
                case Actions.Down:
                    controls.Move.Down.started += up => movement.UpdateInput(d, false);
                    controls.Move.Down.canceled += up => movement.UpdateInput(d, true);
                    break;
                case Actions.Left:
                    controls.Move.Left.started += up => movement.UpdateInput(d, false);
                    controls.Move.Left.canceled += up => movement.UpdateInput(d, true);
                    break;
                case Actions.Right:
                    controls.Move.Right.started += up => movement.UpdateInput(d, false);
                    controls.Move.Right.canceled += up => movement.UpdateInput(d, true);
                    break;
            }    
        }
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

        }
    }

    #region Completion Functions
    bool MoveToLocation()
    {
        if(Physics2D.OverlapBox(coll.gameObject.transform.position, coll.bounds.extents * 2f, this.transform.eulerAngles.z, (1 << LayerMask.NameToLayer("CorrectLocation"))) != null)
        {
            return true;
        }
        return false;
    }

    bool MoveToCorrectLocation()
    {
        Collider2D[] colls = Physics2D.OverlapBoxAll(coll.bounds.center, coll.bounds.extents * 2f, this.transform.eulerAngles.z, (1 << LayerMask.NameToLayer("CorrectLocation")) | (1 << LayerMask.NameToLayer("WrongLocation")));
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

            if (closest.gameObject.layer == LayerMask.NameToLayer("WrongLocation"))
                result = ResultType.Neutral;
            return true;
        }
        return false;
    }

    bool Collection()
    {
        Collider2D c = Physics2D.OverlapBox(coll.gameObject.transform.position, coll.bounds.extents * 2f, this.transform.eulerAngles.z, (1 << LayerMask.NameToLayer("Item")));
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

        if (timer >= TimeToHold)
            return true;
        return false;
    }



    #endregion
}
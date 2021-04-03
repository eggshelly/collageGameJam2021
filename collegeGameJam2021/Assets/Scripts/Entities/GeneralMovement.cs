using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GeneralMovement : MonoBehaviour
{
    [SerializeField] MovementType TypeOfMovement;

    #region Rotation
    [SerializeField] bool Flipped;
    [SerializeField] GameObject Pivot;
    [SerializeField] float EndAngle;
    float originalRotation;
    #endregion



    #region LookDirectional
    [SerializeField] Actions StartingAction = Actions.None;
    [SerializeField] float DistanceToMoveCollider;
    #endregion

    #region Directional

    float leftBoundary;
    float rightBoundary;
    float upperBoundary;
    float lowerBoundary;

    #endregion


    float speed;
    bool hitBoundary = false;

    BoxCollider2D boundary;
    Collider2D collider;

    bool canMove = true;
    bool holdingRight;
    bool holdingLeft;
    bool holdingUp;
    bool holdingDown;
    bool holdingSpace;
    Actions MostRecentAction = Actions.None;

    private void Awake()
    {
        StaticDelegates.UpdateMovement += ToggleMovement;


        boundary = GameObject.FindGameObjectWithTag("Boundary").GetComponent<BoxCollider2D>();
        collider = this.GetComponent<Collider2D>();


        CheckTypeOfMovement();
    }

    void CheckTypeOfMovement()
    {
        switch(TypeOfMovement)
        {
            case MovementType.RotateAroundPivot:
                originalRotation = this.transform.eulerAngles.z;
                if (Flipped)
                    this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, 180f, this.transform.eulerAngles.z);
                break;
            case MovementType.MoveDirectional:
                SetBoundary();
                break;
            case MovementType.LookDirectional:
                if (StartingAction == Actions.None)
                    collider.enabled = false;
                else
                    MostRecentAction = StartingAction;
                LookDirectionally();
                break;
            case MovementType.ToggleHitbox:
                collider.enabled = false;
                break;

        }
    }

    private void OnDestroy()
    {
        StaticDelegates.UpdateMovement -= ToggleMovement;
    }


    private void Update()
    {
        if (!canMove)
            return;

        switch (TypeOfMovement)
        {
            case MovementType.RotateAroundPivot:
                MoveRotational();
                break;
            case MovementType.MoveDirectional:
                MoveDirectional();
                break;
            case MovementType.LookDirectional:
                LookDirectionally();
                break;
            case MovementType.ToggleHitbox:
                ToggleCollider();
                break;

        }
    }

    public void UpdateInput(Actions action, bool released)
    {
        switch (action)
        {
            case Actions.Up:
                holdingUp = !released;
                break;
            case Actions.Down:
                holdingDown = !released;
                break;
            case Actions.Left:
                holdingLeft = !released;
                break;
            case Actions.Right:
                holdingRight = !released;
                break;
            case Actions.Select:
                holdingSpace = !released;
                break;

        }

        if (!released)
            MostRecentAction = action;
    }


    void ToggleMovement(bool canMove)
    {
        this.canMove = canMove;
    }

    #region 4 Directional Movement

    void SetBoundary()
    {
        leftBoundary = boundary.bounds.center.x - boundary.bounds.extents.x;
        rightBoundary = boundary.bounds.center.x + boundary.bounds.extents.x;
        lowerBoundary = boundary.bounds.center.y - boundary.bounds.extents.y;
        upperBoundary = boundary.bounds.center.y + boundary.bounds.extents.y;

        Debug.Log(lowerBoundary + " " + upperBoundary);
    }


    void MoveDirectional()
    {
        if (holdingLeft)
        {
            MoveLeft();
        }

        if (holdingRight)
        {
            MoveRight();
        }

        if (holdingDown)
        {
            MoveDown();
        }

        if (holdingUp)
        {
            MoveUp();
        }
    }

    public void MoveLeft()
    {
        float step = speed * Time.deltaTime;

        if(collider.bounds.center.x - collider.bounds.extents.x - step > leftBoundary)
        {
            this.transform.position += Vector3.left * step;
            hitBoundary = false;
        }
        else
        {
            this.transform.position += Vector3.right * ((leftBoundary + collider.bounds.extents.x) - this.transform.position.x);
            hitBoundary = true;
        }
    }

    public void MoveRight()
    {

        float step = speed * Time.deltaTime;

        if (collider.bounds.center.x + collider.bounds.extents.x + step < rightBoundary)
        {
            this.transform.position += Vector3.right * step;
            hitBoundary = false;
        }
        else
        {
            this.transform.position += Vector3.right * ((rightBoundary - collider.bounds.extents.x) - this.transform.position.x);
            hitBoundary = true;
        }
    }

    public void MoveUp()
    {

        float step = speed * Time.deltaTime;
        if (collider.bounds.center.y + collider.bounds.extents.y + step < upperBoundary)
        {
            this.transform.position += Vector3.up * step;
            hitBoundary = false;
        }
        else
        {
            Debug.Log(this.transform.position.ToString("F4"));
            this.transform.position += Vector3.up * ((upperBoundary - collider.bounds.extents.y) - this.transform.position.y);
            hitBoundary = true;
        }
    }

    public void MoveDown()
    {
        float step = speed * Time.deltaTime;
        if (collider.bounds.center.y - collider.bounds.extents.y - step > lowerBoundary)
        {
            this.transform.position += Vector3.down * step;
            hitBoundary = false;
        }
        else
        {
            this.transform.position += Vector3.up * ((lowerBoundary + collider.bounds.extents.y) - this.transform.position.y);
            hitBoundary = true;
        }

    }

    #endregion

    #region Rotational Movement

    void MoveRotational()
    {


        if (holdingDown)
        {
                RotateClockwise();
        }

        if (holdingUp)
        {
                RotateCounterClockwise();
        }
    }

    void RotateClockwise()
    {
        float angle = -speed * Time.deltaTime;
        if (this.transform.eulerAngles.z + angle > EndAngle)
            this.transform.RotateAround(Pivot.transform.position, Vector3.forward, (Flipped ? -1 : 1) * angle);
        else if (this.transform.eulerAngles.z > EndAngle)
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, EndAngle);
    }

    void RotateCounterClockwise()
    {

        float angle = speed * Time.deltaTime;
        if (this.transform.eulerAngles.z + angle < originalRotation)
            this.transform.RotateAround(Pivot.transform.position, Vector3.forward, (Flipped ? -1 : 1) * angle);
        else if (this.transform.eulerAngles.z < originalRotation)
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, originalRotation);
    }

    #endregion

    #region Toggle

    void ToggleCollider()
    {
        bool isToggling = holdingUp || holdingDown || holdingRight || holdingLeft || holdingSpace;

        if(collider.enabled != isToggling)
            collider.enabled = isToggling;

    }

    #endregion

    #region Look Direction

    void LookDirectionally()
    {
        float height, width;
        if (MostRecentAction != Actions.None)
            collider.enabled = true;

        switch(MostRecentAction)
        {
            case Actions.Up:
                height = Mathf.Max(collider.bounds.extents.x, collider.bounds.extents.y);
                width = Mathf.Min(collider.bounds.extents.x, collider.bounds.extents.y);

                if((collider as BoxCollider2D) != null)
                {
                    (collider as BoxCollider2D).size = new Vector2(width, height);
                }
                collider.offset = new Vector2(0, DistanceToMoveCollider);

                break;
            case Actions.Down:
                height = Mathf.Max(collider.bounds.extents.x, collider.bounds.extents.y);
                width = Mathf.Min(collider.bounds.extents.x, collider.bounds.extents.y);

                if ((collider as BoxCollider2D) != null)
                {
                    (collider as BoxCollider2D).size = new Vector2(width, height);
                }
                collider.offset = new Vector2(0, -DistanceToMoveCollider);

                break;
            case Actions.Left:
                height = Mathf.Max(collider.bounds.extents.x, collider.bounds.extents.y);
                width = Mathf.Min(collider.bounds.extents.x, collider.bounds.extents.y);

                if ((collider as BoxCollider2D) != null)
                {
                    (collider as BoxCollider2D).size = new Vector2(height, width);
                }
                collider.offset = new Vector2(-DistanceToMoveCollider, 0);

                break;
            case Actions.Right:
                height = Mathf.Max(collider.bounds.extents.x, collider.bounds.extents.y);
                width = Mathf.Min(collider.bounds.extents.x, collider.bounds.extents.y);

                if ((collider as BoxCollider2D) != null)
                {
                    (collider as BoxCollider2D).size = new Vector2(height, width);
                }
                collider.offset = new Vector2(DistanceToMoveCollider, 0);

                break;
        }
    }

    #endregion


    public void UpdateSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public bool IsHittingBoundary()
    {
        return hitBoundary;
    }

    public bool CanCurrentlyMove()
    {
        return canMove;
    }
}



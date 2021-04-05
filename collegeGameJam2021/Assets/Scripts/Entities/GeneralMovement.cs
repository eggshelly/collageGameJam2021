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
    Rigidbody2D rb;

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

        GameObject b = GameObject.FindGameObjectWithTag("Boundary");
        if(b != null)
        {
            boundary = GameObject.FindGameObjectWithTag("Boundary").GetComponent<BoxCollider2D>();
            boundary.gameObject.SetActive(false);
        }
        collider = this.GetComponent<Collider2D>();


        CheckTypeOfMovement();
    }

    void CheckTypeOfMovement()
    {
        switch(TypeOfMovement)
        {
            case MovementType.MoveDirectional:
                rb = this.GetComponent<Rigidbody2D>();
                if (rb == null)
                {
                    rb = this.gameObject.AddComponent<Rigidbody2D>();
                }
                rb.gravityScale = 0f;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                break;
        }

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

        if (!canMove && rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }

    #region 4 Directional Movement

    void SetBoundary()
    {
        leftBoundary = boundary.bounds.center.x - boundary.bounds.extents.x;
        rightBoundary = boundary.bounds.center.x + boundary.bounds.extents.x;
        lowerBoundary = boundary.bounds.center.y - boundary.bounds.extents.y;
        upperBoundary = boundary.bounds.center.y + boundary.bounds.extents.y;

    }


    void MoveDirectional()
    {
        if(holdingLeft && !holdingRight)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else if(holdingRight && !holdingLeft)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (holdingUp && !holdingDown)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
        else if (holdingDown && !holdingUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        /*
        if (holdingLeft)
        {
            //MoveLeft();
        }
        else
        {
            if(!holdingRight)
                
        }

        if (holdingRight)
        {
            //MoveRight();
        }

        if (holdingDown)
        {
            //MoveDown();
        }

        if (holdingUp)
        {
            //MoveUp();
        }*/
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
            this.transform.position += Vector3.right * ((leftBoundary + collider.bounds.extents.x) - collider.bounds.center.x);
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
            this.transform.position += Vector3.right * ((rightBoundary - collider.bounds.extents.x) - collider.bounds.center.x);
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
            this.transform.position += Vector3.up * ((upperBoundary - collider.bounds.extents.y) - collider.bounds.center.y);
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
            this.transform.position += Vector3.up * ((lowerBoundary + collider.bounds.extents.y) - collider.bounds.center.y);
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



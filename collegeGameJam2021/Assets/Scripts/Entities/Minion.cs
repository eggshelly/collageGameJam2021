using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    [SerializeField] float StartDelay;
    [SerializeField] float speed;

    bool canMove = false;


    Rigidbody2D rb;
    Collider2D coll;

    private void Awake()
    {
        StaticDelegates.UpdateMovement += ToggleMovement;

        coll = this.GetComponent<Collider2D>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void OnDestroy()
    {
        StaticDelegates.UpdateMovement -= ToggleMovement;
    }

    void ToggleMovement(bool canMove)
    {
        this.canMove = canMove;
    }

    private void Start()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(StartDelay);
        rb.velocity = Vector2.right * speed;

        while (canMove)
        {
            yield return null;
        }

        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    bool ReachTargetLocation()
    {
        if (Physics2D.OverlapBox(this.transform.position, coll.bounds.extents * 2f, this.transform.eulerAngles.z, (1 << LayerMask.NameToLayer("EndLocation"))) != null)
        {
            return true;
        }
        return false;
    }

    public System.Func<bool> GetCompletionFunction()
    {
        return ReachTargetLocation;
    }
}

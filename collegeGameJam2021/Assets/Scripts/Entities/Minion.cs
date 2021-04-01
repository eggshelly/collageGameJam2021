using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{

    Collider2D coll;

    private void Awake()
    {
        coll = this.GetComponent<Collider2D>();
    }

    bool ReachTargetLocation()
    {
        if (Physics2D.OverlapBox(this.transform.position, coll.bounds.extents * 2f, this.transform.eulerAngles.z, (1 << LayerMask.NameToLayer("CorrectLocation"))) != null)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DelayAndDuration
{
    [SerializeField] float delay;
    [SerializeField] float duration;
    [SerializeField] Collider2D coll;

    public float GetDelay()
    {
        return delay;
    }

    public float GetDuration()
    {
        return duration;
    }

    public Collider2D GetCollider()
    {
        return coll;
    }
}

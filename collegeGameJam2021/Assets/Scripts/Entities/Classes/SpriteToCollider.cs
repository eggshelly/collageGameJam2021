using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteToCollider: SpriteTo
{
    [SerializeField] Collider2D collider;
    public Collider2D GetCollider()
    {
        return collider;
    }
}

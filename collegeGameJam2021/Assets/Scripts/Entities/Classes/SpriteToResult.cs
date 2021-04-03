using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class SpriteToResult: SpriteTo
{
    [SerializeField] ResultType result;

    public ResultType GetResult()
    {
        return result;
    }

}

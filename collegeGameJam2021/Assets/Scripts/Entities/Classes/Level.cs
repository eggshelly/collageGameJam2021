using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class Level
{
    [SerializeField] LevelOutcome possibleOutcome;
    [SerializeField] LevelType type;
    [SerializeField] Sprite thumbnail;

    public LevelOutcome GetOutcome()
    {
        return possibleOutcome;
    }

    public LevelType GetLevelType()
    {
        return type;
    }

    public Sprite GetThumbnail()
    {
        return thumbnail;
    }
}

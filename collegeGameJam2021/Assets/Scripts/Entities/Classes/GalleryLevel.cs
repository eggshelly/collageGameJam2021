using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryLevel
{
    public string name;
    public int levelNumber;
    public Sprite thumbnail;
    public LevelOutcome outcome;
    public LevelType levelType;
    public List<int> resultsAchieved;

    public void Initialize(Level level, string name, int index)
    {
        this.outcome = level.GetOutcome();
        this.levelType = level.GetLevelType();

        this.thumbnail = level.GetThumbnail();

        this.name = name;
        this.levelNumber = index;

        resultsAchieved = new List<int>();
        for(int i = 0; i < 3; ++i)
        {
            resultsAchieved.Add(0);
        }

    }
}

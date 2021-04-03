using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData
{
    static Dictionary<int, GalleryLevel> LevelsCompleted = new Dictionary<int, GalleryLevel>();

    static int intrapersonal = 0;
    static int interpersonal = 0;

    static int neutral = 0;

    public static void UpdateData(ResultType type, Level level)
    {

        CompleteLevel(type, level);

        if(level.GetLevelType() == LevelType.interpersonal)
        {
            switch(type)
            {
                case ResultType.Win:
                    interpersonal += 1;
                    break;
                case ResultType.Lose:
                    interpersonal -= 1;
                    break;
                case ResultType.Neutral:
                    neutral += 1;
                    break;
            }
        }
        else
        {
            switch (type)
            {
                case ResultType.Win:
                    intrapersonal += 1;
                    break;
                case ResultType.Lose:
                    intrapersonal -= 1;
                    break;
                case ResultType.Neutral:
                    neutral += 1;
                    break;
            }
        }
    }

    static void CompleteLevel(ResultType result, Level level)
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        if (!LevelsCompleted.ContainsKey(scene))
        {
            GalleryLevel gl = new GalleryLevel();
            gl.Initialize(level, SceneManager.GetSceneByBuildIndex(scene).name, scene);

            LevelsCompleted.Add(scene, gl);
        }
        LevelsCompleted[scene].resultsAchieved[(int)result] = 1;
    }
    
    public static GalleryLevel GetLevelByIndex(int index)
    {
        if (LevelsCompleted.ContainsKey(index))
            return LevelsCompleted[index];
        return null;
    }

    public static int GetIntrapersonal()
    {
        return intrapersonal;
    }

    public static int GetInterpersonal()
    {
        return interpersonal;
    }
    
    public static int GetNeutralAmount()
    {
        return neutral;
    }
}

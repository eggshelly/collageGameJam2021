using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerData
{
    public static Dictionary<int, GalleryLevel> LevelsCompletedOnRun = new Dictionary<int, GalleryLevel>();
    public static Dictionary<int, GalleryLevel> LevelsCompleted = new Dictionary<int, GalleryLevel>();
    public static Dictionary<ResultType, int> OverallResults = new Dictionary<ResultType, int>();
    public static Dictionary<LevelType, List<int>> ResultsByLevelType = new Dictionary<LevelType, List<int>>();

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


        if (!GameManager.IsFromGallery() && !LevelsCompletedOnRun.ContainsKey(scene))
        {
            GalleryLevel gl = new GalleryLevel();
            gl.Initialize(level, SceneManager.GetSceneByBuildIndex(scene).name, scene);

            LevelsCompletedOnRun.Add(scene, gl);
            LevelsCompletedOnRun[scene].resultsAchieved[(int)result] = 1;
        }

        if (!OverallResults.ContainsKey(result))
        {
            OverallResults.Add(result, 0);
        }

        OverallResults[result] += 1;

        if(!ResultsByLevelType.ContainsKey(level.GetLevelType()))
        {
            List<int> data = new List<int>();
            data.Add(0);
            data.Add(0);
            data.Add(0);
            ResultsByLevelType.Add(level.GetLevelType(), data);
        }
        ResultsByLevelType[level.GetLevelType()][(int)result] += 1;

    }
    
    public static GalleryLevel GetLevelByIndex(int index)
    {
        if (LevelsCompleted.ContainsKey(index))
            return LevelsCompleted[index];
        return null;
    }

    public static void NewRun()
    {
        LevelsCompletedOnRun.Clear();
        OverallResults.Clear();
        ResultsByLevelType.Clear();
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

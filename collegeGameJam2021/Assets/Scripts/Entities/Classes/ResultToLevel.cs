using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResultToLevel
{
    [SerializeField] ResultType result;
    [SerializeField] int levelToLoad;

    public ResultType GetResult()
    {
        return result;
    }

    public int GetLevel()
    {
        return levelToLoad;
    }
}

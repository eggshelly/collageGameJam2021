using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ThresholdToResult
{
    [SerializeField] int threshold;
    [SerializeField] ResultType result;

    public int GetThreshold()
    {
        return threshold;
    }

    public ResultType GetResult()
    {
        return result;
    }
}

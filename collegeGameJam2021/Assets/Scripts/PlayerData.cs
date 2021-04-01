using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    static List<ResultType> results = new List<ResultType>();

    static int intrapersonal = 0;
    static int extrapersonal = 0;

    static int neutral;

    public static void Reset()
    {
        intrapersonal = 0;
        extrapersonal = 0;
        neutral = 0;
    }

    public static void UpdateData(ResultType type)
    {
        Debug.Log("Updating: " + type.ToString());
        results.Add( type);
        if(GameManager.GetTypeOfGame() == GameType.extrapersonal)
        {
            switch(type)
            {
                case ResultType.Win:
                    extrapersonal += 1;
                    break;
                case ResultType.Lose:
                    extrapersonal -= 1;
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

    public static int GetIntrapersonal()
    {
        return intrapersonal;
    }

    public static int GetExtrapersonal()
    {
        return extrapersonal;
    }
    
    public static int GetNeutralAmount()
    {
        return neutral;
    }
}

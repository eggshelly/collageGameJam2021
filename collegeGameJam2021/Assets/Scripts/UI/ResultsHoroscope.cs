using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ResultsHoroscope : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelType;
    [SerializeField] TextMeshProUGUI positive;
    [SerializeField] TextMeshProUGUI negative;


    private void Awake()
    {
        int inter = 0;
        int intra = 0;


        if(PlayerData.ResultsByLevelType.ContainsKey(LevelType.interpersonal))
        {
            inter = PlayerData.ResultsByLevelType[LevelType.interpersonal].Sum();
        }


        if (PlayerData.ResultsByLevelType.ContainsKey(LevelType.intrapersonal))
        {
            intra = PlayerData.ResultsByLevelType[LevelType.intrapersonal].Sum();
        }


        int score = 0;
        if(inter > intra)
        {
            score = PlayerData.ResultsByLevelType[LevelType.interpersonal][0] - PlayerData.ResultsByLevelType[LevelType.interpersonal][2];
            levelType.text = "**** Interpersonal ****";

            positive.text = "You are highly connected with others";

        }
        else
        {
            score = PlayerData.ResultsByLevelType[LevelType.intrapersonal][0] - PlayerData.ResultsByLevelType[LevelType.intrapersonal][2];

            levelType.text = "Intrapersonal!!!!!!!!!!!";

            positive.text = "You are highly connected with yourself";

        }
        negative.text = score < 0 ? "...though maybe not always for the best" : "...and you make those connections count!";
    }
}

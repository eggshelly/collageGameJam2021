using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ResultsGraph : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] TextMeshProUGUI numConnected;
    [SerializeField] TextMeshProUGUI numNeutral;
    [SerializeField] TextMeshProUGUI numUnconnected;

    [Header("Graphs")]
    [SerializeField] RectTransform interConnected;
    [SerializeField] RectTransform interNeutral;
    [SerializeField] RectTransform interUnconnected;
    [SerializeField] RectTransform intraConnected;
    [SerializeField] RectTransform intraNeutral;
    [SerializeField] RectTransform intraUnconnected;

    [Header("Labels")]
    [SerializeField] TextMeshProUGUI interConnectedLabel;
    [SerializeField] TextMeshProUGUI interNeutralLabel;
    [SerializeField] TextMeshProUGUI interUnconnectedLabel;
    [SerializeField] TextMeshProUGUI intraConnectedLabel;
    [SerializeField] TextMeshProUGUI intraNeutralLabel;
    [SerializeField] TextMeshProUGUI intraUnconnectedLabel;

    private void Awake()
    {
        SetupStats();
        SetupGraphs();
        SetupLabels();

    }

    void SetupStats()
    {
        numConnected.text = (PlayerData.OverallResults.ContainsKey(ResultType.Win) ? PlayerData.OverallResults[ResultType.Win].ToString() : "0");
        numNeutral.text = (PlayerData.OverallResults.ContainsKey(ResultType.Neutral) ? PlayerData.OverallResults[ResultType.Neutral].ToString() : "0");
        numUnconnected.text = (PlayerData.OverallResults.ContainsKey(ResultType.Lose) ? PlayerData.OverallResults[ResultType.Lose].ToString() : "0");
    }

    void SetupGraphs()
    {
        if(PlayerData.ResultsByLevelType.ContainsKey(LevelType.interpersonal))
        {
            float totalInterconnected = PlayerData.ResultsByLevelType[LevelType.interpersonal].Sum();
            interConnected.localScale = new Vector3(interConnected.localScale.x, PlayerData.ResultsByLevelType[LevelType.interpersonal][0] / totalInterconnected, interConnected.localScale.z);
            interNeutral.localScale = new Vector3(interNeutral.localScale.x, PlayerData.ResultsByLevelType[LevelType.interpersonal][1] / totalInterconnected, interNeutral.localScale.z);
            interUnconnected.localScale = new Vector3(interUnconnected.localScale.x, PlayerData.ResultsByLevelType[LevelType.interpersonal][2] / totalInterconnected, interUnconnected.localScale.z);
        }
        else
        {
            interConnected.localScale = new Vector3(interConnected.localScale.x, 0, interConnected.localScale.z);
            interNeutral.localScale = new Vector3(interNeutral.localScale.x, 0, interNeutral.localScale.z);
            interUnconnected.localScale = new Vector3(interUnconnected.localScale.x, 0, interUnconnected.localScale.z);
        }

        if (PlayerData.ResultsByLevelType.ContainsKey(LevelType.intrapersonal))
        {
            float totalIntraconnected = PlayerData.ResultsByLevelType[LevelType.intrapersonal].Sum();
            intraConnected.localScale = new Vector3(intraConnected.localScale.x, PlayerData.ResultsByLevelType[LevelType.intrapersonal][0] / totalIntraconnected, intraConnected.localScale.z);
            intraNeutral.localScale = new Vector3(intraNeutral.localScale.x, PlayerData.ResultsByLevelType[LevelType.intrapersonal][1] / totalIntraconnected, intraNeutral.localScale.z);
            intraUnconnected.localScale = new Vector3(intraUnconnected.localScale.x, PlayerData.ResultsByLevelType[LevelType.intrapersonal][2] / totalIntraconnected, intraUnconnected.localScale.z);
        }
        else
        {
            intraConnected.localScale = new Vector3(intraConnected.localScale.x, 0, intraConnected.localScale.z);
            intraNeutral.localScale = new Vector3(intraNeutral.localScale.x,0, intraNeutral.localScale.z);
            intraUnconnected.localScale = new Vector3(intraUnconnected.localScale.x, 0, intraUnconnected.localScale.z);
        }
    }

    void SetupLabels()
    {

        if (PlayerData.ResultsByLevelType.ContainsKey(LevelType.interpersonal))
        {
            interConnectedLabel.text = PlayerData.ResultsByLevelType[LevelType.interpersonal][0].ToString();
            interNeutralLabel.text = PlayerData.ResultsByLevelType[LevelType.interpersonal][1].ToString();
            interUnconnectedLabel.text = PlayerData.ResultsByLevelType[LevelType.interpersonal][2].ToString();
        }
        else
        {
            interConnectedLabel.text = "0";
            interNeutralLabel.text = "0";
            interUnconnectedLabel.text = "0";
        }


        if (PlayerData.ResultsByLevelType.ContainsKey(LevelType.intrapersonal))
        {

            intraConnectedLabel.text = PlayerData.ResultsByLevelType[LevelType.intrapersonal][0].ToString();
            intraNeutralLabel.text = PlayerData.ResultsByLevelType[LevelType.intrapersonal][1].ToString();
            intraUnconnectedLabel.text = PlayerData.ResultsByLevelType[LevelType.intrapersonal][2].ToString();
        }
        else
        {

            intraConnectedLabel.text = "0";
            intraNeutralLabel.text = "0";
            intraUnconnectedLabel.text = "0";
        }
    }
}

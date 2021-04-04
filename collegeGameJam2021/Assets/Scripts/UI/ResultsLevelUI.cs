using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultsLevelUI : MonoBehaviour
{
    [Header("Canvas Variables")]
    [SerializeField] Image background;

    [Header("Level Info")]
    [SerializeField] TextMeshProUGUI levelNum;
    [SerializeField] TextMeshProUGUI levelName;
    [SerializeField] List<GameObject> results;

    public void Initialize(GalleryLevel level)
    {
        levelNum.text = level.levelNumber < 10 ? string.Format("#0{0}", level.levelNumber) : string.Format("#{0}", level.levelNumber);
        levelName.text = level.name;

        for(int i = 0; i < results.Count; ++i)
        {
            if (level.resultsAchieved[i] == 1)
                results[i].SetActive(true);
            else
                results[i].SetActive(false);
        }
    }
    public void Selected(bool selected)
    {
        if (selected)
        {
            background.color = Color.red;
        }
        else
        {
            background.color = Color.white;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GalleryLevelUI : MonoBehaviour
{
    [Header("Canvas Variables")]
    [SerializeField] Image background;

    [Header("Level Info")]
    [SerializeField] TextMeshProUGUI levelNumber;
    [SerializeField] TextMeshProUGUI levelName;
    [SerializeField] Image thumbnail;
    [SerializeField] List<Image> results;

    GalleryLevel level;

    public void Initialize(int index)
    {
        level = PlayerData.GetLevelByIndex(index);
        if(level == null)
        {
            levelNumber.gameObject.SetActive(false);
            levelName.text = "???";
            levelName.alignment = TextAlignmentOptions.Center;
            thumbnail.transform.parent.gameObject.SetActive(false);
            
            foreach(Image i in results)
            {
                i.transform.parent.gameObject.SetActive(false);
            }
        }
        else
        {
            levelNumber.gameObject.SetActive(true);
            levelNumber.text = level.levelNumber < 10 ? string.Format("#0{0}", level.levelNumber) : string.Format("#{0}", level.levelNumber);
            levelName.text = level.name;
            thumbnail.gameObject.SetActive(true);
            thumbnail.sprite = level.thumbnail;

            switch(level.outcome)
            {
                case LevelOutcome.binary:
                    for(int i = 0; i < results.Count; i += 2)
                    {
                        if(level.resultsAchieved[i] == 1)
                        {
                            results[i].color = results[i].transform.parent.GetComponent<Image>().color;
                        }
                        else
                        {
                            results[i].color = Color.white;
                        }
                    }

                    results[1].transform.parent.gameObject.SetActive(false);
                    break;
                case LevelOutcome.tertiary:
                    for (int i = 0; i < results.Count; ++i)
                    {
                        if (level.resultsAchieved[i] == 1)
                        {
                            results[i].color = results[i].transform.parent.GetComponent<Image>().color;
                        }
                        else
                        {
                            results[i].color = Color.white;
                        }
                    }
                    break;

            }
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
            background.color = Color.black;
        }
    }


    public bool isUnlocked()
    {
        return level != null;
    }
}

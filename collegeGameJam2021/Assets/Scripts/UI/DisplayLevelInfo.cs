using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DisplayLevelInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelNum;
    [SerializeField] TextMeshProUGUI levelName;

    private void Start()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        levelNum.text = string.Format("#{0}{1}", scene < 10 ? "0" : "", scene);

        levelName.text = string.Format("{0}", SceneManager.GetActiveScene().name);
    }
}

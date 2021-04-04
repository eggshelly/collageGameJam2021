using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ResultsLevelDisplay : MonoBehaviour
{
    [SerializeField] GameObject Grid;
    [SerializeField] ScrollRect rect;

    List<ResultsLevelUI> levels;

    Movement controls;

    int currentIndex;
    int maxIndexOnScreen = 9;
    float shift;

    private void Awake()
    {
        levels = new List<ResultsLevelUI>();

        CreateEntries();
        InitializeEntries();

        shift = 1.0f / (levels.Count - (maxIndexOnScreen + 1));

        SetupControls();
    }



    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void SetupControls()
    {
        controls = new Movement();

        controls.Move.Up.started += prev => PreviousLevel();
        controls.Move.Down.started += next => NextLevel();
    }


    void NextLevel()
    {
        if (currentIndex + 1 < levels.Count)
        {
            levels[currentIndex].Selected(false);
            levels[++currentIndex].Selected(true);

            if (currentIndex > maxIndexOnScreen)
            {
                rect.verticalNormalizedPosition += shift;
                maxIndexOnScreen += 1;
            }
        }
    }

    void PreviousLevel()
    {
        if (currentIndex - 1 >= 0)
        {
            levels[currentIndex].Selected(false);
            levels[--currentIndex].Selected(true);

            if (currentIndex < maxIndexOnScreen - 6)
            {
                rect.verticalNormalizedPosition -= shift;
                maxIndexOnScreen -= 1;
            }
        }
    }


    void CreateEntries()
    {
        Debug.Log(PlayerData.LevelsCompletedOnRun.Count);
        GameObject first = Grid.transform.GetChild(0).gameObject;
        levels.Add(first.GetComponent<ResultsLevelUI>());
        for(int i = 1; i < PlayerData.LevelsCompletedOnRun.Count; ++i)
        {
            GameObject newObj = Instantiate(first, Grid.transform);
            levels.Add(newObj.GetComponent<ResultsLevelUI>());
        }
    }

    void InitializeEntries()
    {
        List<int> keys = PlayerData.LevelsCompletedOnRun.Keys.ToList();
        keys.Sort();
        int index = 0;
        foreach (int i in keys)
        {
            levels[index].Initialize(PlayerData.LevelsCompletedOnRun[i]);
            index += 1;
        }

        levels[0].Selected(true);
    }
}

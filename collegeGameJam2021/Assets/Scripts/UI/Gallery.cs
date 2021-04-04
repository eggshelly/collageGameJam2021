using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gallery : MonoBehaviour
{
    static int selectedLevel = -1;

    [SerializeField] GameObject grid;
    [SerializeField] ScrollRect rect;

    List<GalleryLevelUI> levels;

    int currentIndex = 0;
    int maxIndexOnScreen = 6;
    float shift;

    Movement controls;

    private void Awake()
    {
        levels = new List<GalleryLevelUI>();


        GameObject level = grid.transform.GetChild(0).gameObject;
        GalleryLevelUI first = level.GetComponent<GalleryLevelUI>();
        first.Initialize(1);
        levels.Add(first);

        for(int i = 2; i < SceneManager.sceneCountInBuildSettings - 1; ++i)
        {
            GameObject nextLevel = Instantiate(level, grid.gameObject.transform);
            GalleryLevelUI nl = nextLevel.GetComponent<GalleryLevelUI>();
            nl.Initialize(i);
            levels.Add(nl);
        }

        if(selectedLevel == -1 || !GameManager.IsFromGallery())
        {
            levels[currentIndex].Selected(true);
            selectedLevel = -1;
        }
        else
        {
            levels[selectedLevel].Selected(true);
            currentIndex = selectedLevel;
        }

        controls = new Movement();

        shift = 1.0f /(levels.Count - 7);
        Debug.Log(shift);
        SetupControls();

        if (!GameManager.IsFromGallery())
            this.gameObject.SetActive(false);
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
        controls.Move.Left.started += prev => PreviousLevel();
        controls.Move.Right.started += next => NextLevel();
        controls.Move.Select.started += select => PlayLevel();
        controls.Navigation.Escape.started += back => Close();
    }

    void Close()
    {
        Debug.Log("Closing");
        this.gameObject.SetActive(false);
    }

    void NextLevel()
    {
        if (currentIndex + 1 < levels.Count)
        {
            levels[currentIndex].Selected(false);
            levels[++currentIndex].Selected(true);

            if(currentIndex > maxIndexOnScreen)
            {
                rect.horizontalNormalizedPosition += shift;
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
                rect.horizontalNormalizedPosition -= shift;
                maxIndexOnScreen -= 1;
            }
        }
    }

    void PlayLevel()
    {
        if(levels[currentIndex].isUnlocked())
        {
            selectedLevel = currentIndex;
            StaticDelegates.PlaySpecificLevel(currentIndex + 1);
        }
    }
}

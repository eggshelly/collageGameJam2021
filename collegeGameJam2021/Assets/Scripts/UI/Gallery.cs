using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gallery : MonoBehaviour
{
    static int selectedLevel = -1;

    [SerializeField] GameObject grid;

    List<GalleryLevelUI> levels;

    int currentIndex = 0;

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

        SetupControls();

        if (!GameManager.IsFromGallery())
            this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        controls.Move.Enable();
    }

    private void OnDisable()
    {
        controls.Move.Disable();
    }

    void SetupControls()
    {
        controls.Move.Left.started += prev => PreviousLevel();
        controls.Move.Right.started += next => NextLevel();
        controls.Move.Select.started += select => PlayLevel();
    }

    void NextLevel()
    {
        if (currentIndex + 1 < levels.Count)
        {
            levels[currentIndex].Selected(false);
            levels[++currentIndex].Selected(true);
        }
    }

    void PreviousLevel()
    {
        if (currentIndex - 1 >= 0)
        {
            levels[currentIndex].Selected(false);
            levels[--currentIndex].Selected(true);
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

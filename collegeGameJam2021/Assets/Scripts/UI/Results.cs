using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Results : MonoBehaviour
{
    [SerializeField] List<GameObject> panels;

    Movement controls;

    int currentIndex = -1;
    void Awake()
    {
        StaticDelegates.GameState += StartResults;

        controls = new Movement();


        SetupControls();
    }

    private void Start()
    {
        foreach (GameObject g in panels)
            g.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        StaticDelegates.GameState -= StartResults;
    }

    private void OnDisable()
    {
        controls.Move.Disable();
    }

    void SetupControls()
    {
        controls.Move.Select.started += start => NextPanel();
    }

    void StartResults(bool start)
    {
        if (start)
        {
            controls.Move.Enable();

            NextPanel();
        }
        else
        {
            controls.Move.Disable();
        }
    }

    public void NextPanel()
    {
        if(currentIndex + 1 == panels.Count)
        {
            StaticDelegates.UpdateGameState(false);
        }
        else
        {
            Debug.Log(currentIndex);
            currentIndex += 1;
            panels[currentIndex].SetActive(true);
        }
    }
}

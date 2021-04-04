using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static int CurrentLevel;
    static int NextLevel = 0;

    static bool loadedFromGallery = false;

    static ResultType finalResult = ResultType.None;
    static Level level;

    [SerializeField] Level levelToPlay;
    [SerializeField] float TimeToComplete;
    [SerializeField] ResultType ResultIfTimeout = ResultType.Lose;

    float timer = 0f;

    Coroutine timerRoutine;

    private void Awake()
    {
        StaticDelegates.GameState += this.UpdateGameState;

        level = levelToPlay;
    }

    private void Start()
    {
        if(GameManager.NextLevel == 0)
        {
            StaticDelegates.UpdateGameState(true);
        }
    }

    private void OnDestroy()
    {
        StaticDelegates.GameState -= this.UpdateGameState;
    }

    void UpdateGameState(bool start)
    {
        timer = 0f;
        if(start && timer  == 0f)
        {
            Debug.Log("Starting Timer");
            timerRoutine = StartCoroutine(Timer());
        }
        else
        {
            Debug.Log("Timer ended");
            StopCoroutine(timerRoutine);
        }
    }

    IEnumerator Timer()
    {
        timer = 0f;
        while(timer < TimeToComplete)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        GameFinished(ResultIfTimeout);
    }


    public static int GetNextLevel()
    {
        if (loadedFromGallery)
            return 0;

        if (NextLevel + 1 == SceneManager.sceneCountInBuildSettings)
        {
            NextLevel = 0;
            return 0;
        }

        CurrentLevel = NextLevel;
        NextLevel += 1;
        return NextLevel;
    }

    public static void GameFinished(ResultType result)
    {
        finalResult = finalResult == ResultType.None ? result : (ResultType)Mathf.Min((int)finalResult, (int)result);

        PlayerData.UpdateData(result, level);
        StaticDelegates.UpdateGameState(false);
    }

    public static void UpdateFinalResult(ResultType result)
    {
        finalResult = result;
    }

    public static void ResetVariables()
    {
        NextLevel = 0;
        FromGallery(false);
    }

    public static ResultType GetFinalResult()
    {
        return finalResult;
    }

    public static int GetCurrentLevel()
    {
        return CurrentLevel;
    }

    public static void FromGallery(bool b)
    {
        loadedFromGallery = b;
    }

    public static bool IsFromGallery()
    {
        return loadedFromGallery;
    }

}

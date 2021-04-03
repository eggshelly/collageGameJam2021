using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static int CurrentLevel;
    static int NextLevel = 0;
    static GameType type;
    [SerializeField] GameType TypeOfGame;
    [SerializeField] float TimeToComplete;
    [SerializeField] ResultType ResultIfTimeout = ResultType.Lose;

    float timer = 0f;

    Coroutine timerRoutine;

    private void Awake()
    {
        StaticDelegates.GameState += this.UpdateGameState;
        type = TypeOfGame;
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
        PlayerData.UpdateData(ResultIfTimeout);
        StaticDelegates.UpdateGameState(false);
    }


    public static int GetNextLevel()
    {
        if (NextLevel + 1 == SceneManager.sceneCountInBuildSettings)
        {
            NextLevel = 1;
            return 1;
        }

        CurrentLevel = NextLevel;
        NextLevel += 1;
        return NextLevel;
    }

    public static int GetCurrentLevel()
    {
        return CurrentLevel;
    }

    public static GameType GetTypeOfGame()
    {
        return type;
    }
}

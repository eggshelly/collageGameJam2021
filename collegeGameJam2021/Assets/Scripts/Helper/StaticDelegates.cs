using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class StaticDelegates
{

    public delegate void Toggle(bool b);
    public static Toggle UpdateMovement;

    public static void UpdateAllMovement(bool b)
    {
        if (UpdateMovement != null)
            UpdateMovement.Invoke(b);
    }

    public static Toggle GameState;

    public static void UpdateGameState(bool start)
    {
        if(GameState != null)
        {
            if (start)
                Debug.Log("STARTING");
            else
                Debug.Log("GAME FINISHED");

            UpdateAllMovement(start);

            GameState.Invoke(start);

            if (!start)
            {
                PlaySequence(false);
            }


        }
    }

    public static Toggle Sequence;

    public static void PlaySequence(bool pregame)
    {
        if(Sequence != null)
        {
            Sequence.Invoke(pregame);
        }
    }


    public static System.Action Fade;

    public static void FadeOut()
    {
        if(Fade!= null)
            Fade.Invoke();
    }

    public static void TransitionToNextLevel()
    {
        SceneManager.LoadScene(GameManager.GetNextLevel());
    }
}

using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private static GameManager instance;

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------
    // Public accessor for the instance

    public static GameManager Instance
    {
      
        get
        {
            if (instance == null)
            {
                // Find existing instance in the scene
                instance = FindObjectOfType<GameManager>();

                // If no instance exists, create a new one
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    instance = singletonObject.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        PlayerPrefs.DeleteAll();
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    public static event Action<GameState> OnGameStateChanged;

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    public GameState state;

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------


    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.start:
                break;
            case GameState.shopopned:
                break;
            case GameState.settingsopned:
                break;
            case GameState.playing:
                break;
            case GameState.gamepaused:
                break;
            case GameState.respawn:
                break;
            case GameState.restart:
                break;
            case GameState.finish:
                break;
        }

        OnGameStateChanged?.Invoke(newState);

    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    public enum GameState
    {
    start,
    shopopned,
    settingsopned,
    playing,
    gamepaused,
    respawn,
    restart,
    finish,
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

  

}
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    [SerializeField] private float movSpeed = 10f;
    private bool canMove;
    


    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += StartMovingPlayer;
        GameManager.OnGameStateChanged += StopMovingPlayer;
        GameManager.OnGameStateChanged += StopPlayerOnFinishLine;
        GameManager.OnGameStateChanged += StopMovingPlayerWhenPaused;
    
    }

    private void StopMovingPlayerWhenPaused(GameManager.GameState state)
    {
        if (state == GameManager.GameState.gamepaused)
        {
            canMove = false;
        }
    }


    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void StopPlayerOnFinishLine(GameManager.GameState state)
    {
        if (state == GameManager.GameState.finish)
        {
            canMove = false;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void StartMovingPlayer(GameManager.GameState state)
    {
        if(state == GameManager.GameState.playing)
        {
            canMove = true;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void StopMovingPlayer(GameManager.GameState state)
    {
        if(state == GameManager.GameState.restart)
        {
            canMove = false;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= StartMovingPlayer;
        GameManager.OnGameStateChanged -= StopMovingPlayer;
        GameManager.OnGameStateChanged -= StopPlayerOnFinishLine;
        GameManager.OnGameStateChanged -= StopMovingPlayerWhenPaused;
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------
    // Update is called once per frame

    void Update()
    {

        if (canMove)
        {
            MovePlayer();
        }  
        
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void MovePlayer()
    {
        transform.Translate(transform.right * movSpeed * Time.deltaTime);
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    
}

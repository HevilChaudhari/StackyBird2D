using UnityEngine;

public class TapToStart : MonoBehaviour
{
    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private BoxCollider2D eventTriggerArea;

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private bool isStarted;
    private bool isShopOpned;

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        eventTriggerArea = GetComponent<BoxCollider2D>();
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------
    private void OnEnable()
    {

        GameManager.OnGameStateChanged += GameRestart;
        GameManager.OnGameStateChanged += GameFinish;
        GameManager.OnGameStateChanged += ShopIsOpned;
        GameManager.OnGameStateChanged += isShopClosed;
        GameManager.OnGameStateChanged += RespawnPlayer;
    }

    

    private void RespawnPlayer(GameManager.GameState state)
    {
        if (state == GameManager.GameState.respawn)
        {
            isStarted = false;
        }
    }

    private void isShopClosed(GameManager.GameState state)
    {
       
        if(state == GameManager.GameState.start)
        {
            isShopOpned = false;
        }
    }

    private void ShopIsOpned(GameManager.GameState state)
    {
        if(state == GameManager.GameState.shopopned)
        {
            isShopOpned = true;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void GameFinish(GameManager.GameState state)
    {
        if (state == GameManager.GameState.finish)
        {
            isStarted = false;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void GameRestart(GameManager.GameState state)
    {
        if(state == GameManager.GameState.restart)
        {
            isStarted = false;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void OnMouseDown()
    {
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        if (eventTriggerArea.OverlapPoint(mousePos) && !isStarted && !isShopOpned)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.playing);
            isStarted = true;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameRestart;
        GameManager.OnGameStateChanged -= GameFinish;
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------
}

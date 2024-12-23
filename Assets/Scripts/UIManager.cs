using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    public static UIManager instance;

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    [SerializeField] private GameObject startPanel, restartPanel, finishPanel, playingPanel , shopPanel, respawnPanel , settingsPanel, gamePausedPanel;
    [SerializeField] private TextMeshProUGUI coinText , totalCoinText , ShopPanelTotalText;
    [SerializeField] private Canvas screenSpaceOverlayCanvas, worldSpaceCanvas;

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private int addCoins = 5;
    private int totalCoins;

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        instance = this;
        
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += ShowStartPanel;
        GameManager.OnGameStateChanged += ShowRestartPanel; 
        GameManager.OnGameStateChanged += ShowFinishPanel;
        GameManager.OnGameStateChanged += ShowPlayingPanel;
        GameManager.OnGameStateChanged += ShowRespawnPanel;
        GameManager.OnGameStateChanged += CanvasControl;
        GameManager.OnGameStateChanged += ShowSettingPanel;
        GameManager.OnGameStateChanged += ShowPausedPanel;
    }

    private void ShowPausedPanel(GameManager.GameState state)
    {
        gamePausedPanel.SetActive(state == GameManager.GameState.gamepaused);
    }

    private void ShowSettingPanel(GameManager.GameState state)
    {
        settingsPanel.SetActive(state == GameManager.GameState.settingsopned);
    }


    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void ShowRespawnPanel(GameManager.GameState state)
    {
        respawnPanel.SetActive(state == GameManager.GameState.respawn);
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void CanvasControl(GameManager.GameState state)
    {
        shopPanel.SetActive(state == GameManager.GameState.shopopned);
        if(state == GameManager.GameState.shopopned)
        {
            screenSpaceOverlayCanvas.enabled = false;
            worldSpaceCanvas.enabled = true;
            ShopPanelTotalText.text = totalCoins.ToString();

        }
        if (state == GameManager.GameState.start)
        {
            screenSpaceOverlayCanvas.enabled = true;
            worldSpaceCanvas.enabled = false;
            totalCoinText.text = totalCoins.ToString();
            
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void ShowPlayingPanel(GameManager.GameState state)
    {
        playingPanel.SetActive(state == GameManager.GameState.playing || state == GameManager.GameState.respawn);
        if(state == GameManager.GameState.playing)
        coinText.text = totalCoins.ToString();
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void ShowStartPanel(GameManager.GameState state)
    {   
        startPanel.SetActive(state == GameManager.GameState.start);
        totalCoins = PlayerPrefs.GetInt("totalCoins", 0);
        totalCoinText.text = totalCoins.ToString();
        
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void ShowRestartPanel(GameManager.GameState state)
    {   
        restartPanel.SetActive(state == GameManager.GameState.restart);
        
       
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void ShowFinishPanel(GameManager.GameState state)
    {
        finishPanel.SetActive(state == GameManager.GameState.finish);
      
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void Start()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.start);
       
    }

    public void addCoin()
    {
        totalCoins += addCoins;
        coinText.text =totalCoins.ToString();
        PlayerPrefs.SetInt("totalCoins", totalCoins);
        AudioManager.Instance.playSound("CoinCollect");

    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= ShowStartPanel;
        GameManager.OnGameStateChanged -= ShowRestartPanel;
        GameManager.OnGameStateChanged -= ShowFinishPanel;
        GameManager.OnGameStateChanged -= ShowPlayingPanel;
        GameManager.OnGameStateChanged -= ShowRespawnPanel;
        GameManager.OnGameStateChanged -= CanvasControl;
        GameManager.OnGameStateChanged -= ShowSettingPanel;
        GameManager.OnGameStateChanged -= ShowPausedPanel;
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    public void isShopOpen()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.shopopned);
    }

    public void isShopClosed()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.start);
    }

    public void isPlayerRespawn()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.respawn);
    }

    public void ClickToStart()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.playing);
    }

    public void isSettingsOpen()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.settingsopned);
    }
    public void isSettingsClose()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.start);
    }

    public void isGamePaused()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.gamepaused);
    }
    public void isGameResume()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.playing);
    }

    public void QuitApplication()
    {
        Debug.Log("AppQuit");
        Application.Quit();
    }

}

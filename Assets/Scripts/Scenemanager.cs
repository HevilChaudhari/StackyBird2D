using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{

    int lastSceneIndex;
    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void OnEnable()
    { 
        GameManager.OnGameStateChanged += LoadNewLevel;
        SceneManager.sceneLoaded += LoadNewScene;
        GameManager.OnGameStateChanged += SaveIndexofNewLevel;

    }

    private void SaveIndexofNewLevel(GameManager.GameState state)
    {
        if(state == GameManager.GameState.start)
        {
        SaveLastSceneIndex();

        }
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void LoadNewScene(Scene scene, LoadSceneMode mode)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        lastSceneIndex = PlayerPrefs.GetInt("LastSceneIndexKey", 0);
        if(lastSceneIndex > currentSceneIndex)
        {
            LoadLastScene();

        }
        
        
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void LoadNewLevel(GameManager.GameState state)
    {
        if (state == GameManager.GameState.finish)
        {
            StartCoroutine("LoadNewLevelAfterSomeTime");
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------

    private IEnumerator LoadNewLevelAfterSomeTime()
    {
       
        yield return new WaitForSeconds(3f);

        GetNextSceneIndex();
      
        yield return null;
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void GetNextSceneIndex()
    {
       
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);

    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= LoadNewLevel;
        SceneManager.sceneLoaded -= LoadNewScene;
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

   
    private void OnApplicationQuit()
    {
        SaveLastSceneIndex();
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void SaveLastSceneIndex()
    {
        lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("LastSceneIndexKey", lastSceneIndex);
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void LoadLastScene()
    {
        SceneManager.LoadScene(lastSceneIndex);
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------

    public void RestartBtnPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

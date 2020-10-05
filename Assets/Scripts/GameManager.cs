using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameState state = new GameState();

    private void Awake(){
        if(instance == null){
            instance = this;
        }
        else if(instance != this){
            Destroy(this.gameObject);
        }
        //Want this to persist throughout the game
        DontDestroyOnLoad(gameObject);
    }

    #region Events for scenes

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //run transition animations
        //do any scene prep, if applicable
    }

    #endregion

    #region Scene Load Functions

    public void LoadMainMenu(){
        SceneManager.LoadScene(MAIN_MENU_SCENE);
    }

    public void LoadGame(){
        SceneManager.LoadScene(GAME_SCENE);
    }

    public void LoadWin(){
        SceneManager.LoadScene(WIN_SCENE);
    }

    public void LoadLose(){
        SceneManager.LoadScene(LOSE_SCENE);
    }

    public void LoadTutorial(){
        SceneManager.LoadScene(TUTORIAL_SCENE);
    }

    public void Exit(){
        #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    #endregion

    public void ResetGameState(){
        state = new GameState();
    }

    #region Constants

    private const string MAIN_MENU_SCENE = "MainMenu";
    private const string WIN_SCENE = "Win";
    private const string LOSE_SCENE = "Lose";
    private const string GAME_SCENE = "Game";
    private const string TUTORIAL_SCENE = "Tutorial";

    #endregion Constants
    
}

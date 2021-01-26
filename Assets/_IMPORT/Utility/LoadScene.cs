using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class LoadScene : MonoBehaviour
{

    public KeyCode skipKey;
    //public string skipInputButton = "Start";
    public bool restartScene;

    public string goToScene;

    //public bool resetTimeScale;

    public UnityEvent OnLoad;

    //Put this in a global setting later (whenever its we make it)
    void Awake() { Application.targetFrameRate = 60; }

    public void Start()
    {
        //if (skipInputButton == "")
           // skipInputButton = "Start";

        //if (resetTimeScale)
           // Time.timeScale = 1;
    }

    public void Update()
    {
        //if (string.IsNullOrEmpty(goToScene)) return;

        if (Input.GetKeyDown(skipKey))//|| Input.GetButtonDown(skipInputButton))
        {
            if (restartScene)
                ReloadScene();
            else
                SceneManager.LoadScene(goToScene);
        }

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadThisScene(string _sceneName)
    {
        OnLoad.Invoke();
        SceneManager.LoadScene(_sceneName);
    }

    public void LoadThisScene(int _sceneID)
    {
        OnLoad.Invoke();
        SceneManager.LoadScene(_sceneID);
    }

    public void ReloadScene()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
}

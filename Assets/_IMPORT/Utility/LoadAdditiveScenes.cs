using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAdditiveScenes : MonoBehaviour
{
    public bool loadOnStart;
    public string[] scenenames;

    // Start is called before the first frame update
    void Start()
    {

        if (loadOnStart)
            LoadScenes();
    }



    private void LoadScenes()
    {
        for (int i = 0; i < scenenames.Length; i++)
        {
            if (!SceneManager.GetSceneByName(scenenames[i]).isLoaded)
            {
                SceneManager.LoadScene(scenenames[i], LoadSceneMode.Additive);
            }

        }
    }

    public void LoadSceneAdditivly(string name)
    {
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
    }

    public void UnloadScene(string name)
    {
        SceneManager.UnloadSceneAsync(name);
    }
}

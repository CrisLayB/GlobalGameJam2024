using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevetTest : MonoBehaviour
{
    [ContextMenuItem("Load Level", nameof(EditorLoadLevel))];
    [ContextMenuItem("Load Level 2", nameof(EditorLoadLevel))];
    public string levelName; 
    private string loadedLevelName = string.Empty;

    [ContextMenuItem("Load Level - Context Menu")];

    void EditorLoadLevel(){
        StartCoroutine(LoadLevelAsync());
    }

    private IEnumerator LoadLevelAsync(){
        var unloadProgress = SceneManager.UnloadSceneAsync(loadedLevelName);
        while (!progress.isDone){
            yield return null;
        }

        var progress = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        while (!unloadProgress.isDone){
            yield return null;
        }
        Debug.Log("Level Loaded")
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelTest : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad = "TaskList"; // La escena a cargar
    private bool shouldLoadScene = false; // Controla si la escena debe cargarse

    // Método público para establecer el valor de shouldLoadScene desde otros scripts
    public void SetShouldLoadScene(bool value)
    {
        shouldLoadScene = value;
    }

    private void Update()
    {
        if (shouldLoadScene)
        {
            StartCoroutine(LoadLevel(sceneToLoad));
            shouldLoadScene = false; // Restablecer el indicador
        }
    }

    // Corrutina para cargar la escena
    private IEnumerator LoadLevel(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
        Debug.Log("Escena cargada: " + sceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerOpenElevator : MonoBehaviour
{
    private GameObject zombie;
    private string sceneToLoad = "Gameplay"; // La escena a cargar
    private bool shouldLoadScene = false; // Controla si la escena debe cargarse

    void Start()
    {
        // Encuentra el objeto "Zombie" al inicio y lo asigna a la variable zombie
        zombie = GameObject.Find("Zombie");
    }

    void Update()
    {
        // Código de Update si es necesario
    }

    private void OnTriggerEnter(Collider other)
    {
            StartCoroutine(LoadLevel(sceneToLoad));
            shouldLoadScene = false; // Restablecer el indicador    
            
    }

        private IEnumerator LoadLevel(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
        Debug.Log("Escena cargada: " + sceneName);
    }
}

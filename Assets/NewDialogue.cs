using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NewDialogue : MonoBehaviour
{
    private bool isPlayerInRange;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    private string sceneToLoad = "Scene1"; // La escena a cargar
    private bool shouldLoadScene = false; // Controla si la escena debe cargarse


    private bool didDialogueStart;
    private int lineIndex;
    private float typingTime = 0.05f;

    void Start()
    {
        dialoguePanel.SetActive(true); // Asegúrate de que el panel de diálogo esté desactivado al inicio
        StartDialogue();
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Return)) // Cambiado de "Fire1" a "0" para detectar clic izquierdo
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);

            

            // Activa la animación cuando se termina de mostrar el diálogo
            StartCoroutine(LoadLevel(sceneToLoad));
            shouldLoadScene = false; // Restablecer el indicador    
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            StartDialogue(); // Comienza el diálogo automáticamente cuando el jugador entra en el collider
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialoguePanel.SetActive(false); // Desactiva el panel de diálogo cuando el jugador sale del collider
        }
    }

        public bool IsActive()
    {
        return didDialogueStart;
    }

            private IEnumerator LoadLevel(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
        Debug.Log("Escena cargada: " + sceneName);
    }
}



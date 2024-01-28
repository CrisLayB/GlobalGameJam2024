using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityStandardAssets.CrossPlatformInput;

public class Manager : MonoBehaviour
{
    [SerializeField] private GameObject player, cameraEcuationLife;
    [SerializeField] private GameObject uiKeyInput, ui_answers_ecuation_life;
    [SerializeField] private Sprite selectedAnswerSprite, unselectedAnswerSprite;
    [SerializeField] private Image[] uiAnswers;
    private int selected = 0;
    private float _selectAnswerRate = 0.2f;
    private float _canSelectAnswer = -1f;
    [SerializeField] private TMP_Text actionText, keyInputText;
    private TaskList taskList;

    private void Start() 
    {
        InitializeTaskList();

        if(ui_answers_ecuation_life == null)
            Debug.Log("Error: No esta adjunto el Game Object de UI Respuestas de ecuacion de la vida");

        if(player == null)
            Debug.Log("Error: Player no asignado en Manager");

        if(cameraEcuationLife == null)
            Debug.Log("Error: Player no asignado en Manager");

        if(selectedAnswerSprite == null)
            Debug.Log("Error: No se agrego un Sprite de Respuesta seleccionada");

        if(unselectedAnswerSprite == null)
            Debug.Log("Error: No se agrego un Sprite de Respuesta no seleccionada");

        if(uiAnswers.Length == 0)
            Debug.Log("Error: No hay imagenes de ui asignadas");
    }

    private void InitializeTaskList()
    {
        GameObject tasksFound = GameObject.Find("TaskList");

        if(tasksFound == null) 
        {
            Debug.Log("Error: No se encontro el GameObject Tasklist");
            return;
        }

        taskList = tasksFound.GetComponent<TaskList>();

        if(taskList == null) Debug.Log("Error: No se encontro el componente TaskList en el gameobject Tasklist");
    }

    private void Update()
    {
        if(ui_answers_ecuation_life.activeSelf)
        {
            EcuationOfLifeEjecutation();
        }
    }

    private void EcuationOfLifeEjecutation()
    {
        float inputX = Input.GetAxisRaw("Horizontal");

        if(inputX > 0f && Time.time > _canSelectAnswer)
        {
            _canSelectAnswer = Time.time + _selectAnswerRate;
            selected++;
            selected %= uiAnswers.Length;
        }

        if(inputX < 0f && Time.time > _canSelectAnswer)
        {
            _canSelectAnswer = Time.time + _selectAnswerRate;
            selected--;
            if(selected < 0) 
                selected = uiAnswers.Length - 1;
        }

        for (int i = 0; i < uiAnswers.Length; i++)
        {
            uiAnswers[i].sprite = (i == selected) ? selectedAnswerSprite : unselectedAnswerSprite;
        }

        if(Input.GetButtonDown("Enter"))
        {
            WinningEcuationLife();
        }
    }

    public void ShowTaskList(bool showTaskList)
    {
        taskList.ShowTaskList(showTaskList);
    }

    public int TheTaskIsDone(TaskName taskEnum)
    {
        return taskList.TheTaskIsDone(TaskName.EcuationLife);
    }

    public void FinishedTask(TaskName taskEnum)
    {
        AudioManeger.Play(AudioClipName.BellDone);
        taskList.TaskAllreadyDone(taskEnum);
    }

    public void EnterEcuationLife()
    {
        AudioManeger.Play(AudioClipName.Yeso);
        ui_answers_ecuation_life.SetActive(true);
        player.SetActive(false);
        cameraEcuationLife.SetActive(true);
    }

    private void WinningEcuationLife()
    {        
        FinishedTask(TaskName.EcuationLife);
        ui_answers_ecuation_life.SetActive(false);
        player.SetActive(true);
        cameraEcuationLife.SetActive(false);
    }

    public void ShowInputInformation(string pKeyInput, string pActionText)
    {
        uiKeyInput.SetActive(true);
        keyInputText.text = pKeyInput;
        actionText.text = pActionText;
    }

    public void HideInputInformation()
    {
        uiKeyInput.SetActive(false);
        keyInputText.text = "";
        actionText.text = "";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Manager : MonoBehaviour
{
    [SerializeField] private GameObject player, cameraEcuationLife, cameraSnake;
    [SerializeField] private GameObject uiKeyInput, ui_answers_ecuation_life, ui_snake;
    [SerializeField] private Sprite selectedAnswerSprite, unselectedAnswerSprite;
    [SerializeField] private Image[] uiAnswers;
    private int selected = 0;
    private float _selectAnswerRate = 0.2f;
    private float _canSelectAnswer = -1f;
    [SerializeField] private TMP_Text actionText, keyInputText;
    private TaskList taskList;
    [SerializeField] private GameObject lightWorld;

    private void Start() 
    {
        InitializeTaskList();

        if(uiKeyInput == null)
            Debug.Log("Error: No se asigno la UI en donde se muestra la acción a realizar");

        if(ui_answers_ecuation_life == null)
            Debug.Log("Error: No esta adjunto el Game Object de UI Respuestas de ecuacion de la vida");

        if(player == null)
            Debug.Log("Error: Player no asignado en Manager");

        if(cameraEcuationLife == null)
            Debug.Log("Error: No se asigno la camara para la ecuacion de la vida");

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
            FinishedTask(TaskName.EcuationLife);
        }
    }

    public void ShowTaskList(bool showTaskList)
    {
        taskList.ShowTaskList(showTaskList);
    }

    public int TheTaskIsDone(TaskName taskEnum)
    {
        return taskList.TheTaskIsDone(taskEnum);
    }

    public void FinishedTask(TaskName taskEnum)
    {
        AudioManeger.Play(AudioClipName.BellDone);
        taskList.TaskAllreadyDone(taskEnum);
    }

    public void EnterFixPrinter()
    {
        player.SetActive(false);
        ui_snake.SetActive(true);
        cameraSnake.SetActive(true);
    }

    public void OutFixPrinter()
    {
        player.SetActive(true);
        ui_snake.SetActive(false);
        cameraSnake.SetActive(false);
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

    public void FixLight()
    {
        lightWorld.SetActive(true);
    }
}

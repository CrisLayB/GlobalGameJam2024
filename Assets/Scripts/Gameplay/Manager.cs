using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private GameObject player, cameraEcuationLife;
    private TaskList taskList;

    private void Start() 
    {
        InitializeTaskList();

        if(player == null)
            Debug.Log("Error: Player no asignado en Manager");

        if(cameraEcuationLife == null)
            Debug.Log("Error: Player no asignado en Manager");
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
        taskList.TaskAllreadyDone(taskEnum);
    }

    public void EnterEcuationLife()
    {
        player.SetActive(false);
        cameraEcuationLife.SetActive(true);
    }

    public void WinningEcuationLife()
    {
        player.SetActive(true);
        cameraEcuationLife.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TaskList : MonoBehaviour
{
    [SerializeField] private UITaskMark[] uiTasks;
    private bool[] doneTasks;
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        if(uiTasks.Length == 0)
        {
            Debug.Log("Error: Tienes que implementar tasks en la lista");
        }
        else
        {
            doneTasks = new bool[uiTasks.Length];
            for (int i = 0; i < uiTasks.Length; i++)
            {
                doneTasks[0] = false;
            }
        }        

        animator = GetComponent<Animator>();

        if(animator == null) Debug.Log("Error: No se encontro un animator en la TaskList");
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < doneTasks.Length; i++)
        {
            bool marked = doneTasks[i];
            
            if(marked)
                uiTasks[i].ElementMarked();
            else
                uiTasks[i].ElementNotMarked();
        }
    }

    public void TaskAllreadyDone(TaskName taskEnum)
    {
        int taskNum = (int)taskEnum;
        
        if(taskNum < 0 || taskNum > uiTasks.Length)
        {
            Debug.Log("Error: Task Num " + taskNum + " pasa los limites ");
            return;
        }

        doneTasks[taskNum] = true;
    }

    public void ShowTaskList(bool show)
    {
        animator.SetBool("showTaskList", show);
    }
}

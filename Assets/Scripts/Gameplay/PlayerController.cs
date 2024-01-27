///#########################################################################################
/// <author> Cristian Fernando Laynez Bachez </author>
/// <copyright> Copyright 2020, © Cristian Laynez Productions </copyright>
/// <maintainer> Cristian Laynez Productions </maintainer>
/// <email> lay201281@uvg.edu.gt </email>
/// <status> Student of Computer Science </status>
/// 
/// <proyect> Laboratorio #8 </proyect>
/// <p> El objetivo de este laboratorio es aprender a usar las luces en Unity UwU </p>
/// 
/// <class> Player Controller </class>
/// <summary>
/// Script para tener el control del personaje
/// </summary>
///#########################################################################################

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //--------------------------------------------------------------------------------------
    /// -→ Atributos y Campos    
    /// - Linterna
    [SerializeField] private Light flashlight;
    private bool isOn = true;

    // /// - Para mostrar información
    // [SerializeField] private GameObject objectText;
    // [SerializeField] private Text infoText;

    //--------------------------------------------------------------------------------------
    /// -→ Métodos

    /// <summary>
    /// Update es llamado por cada frame
    /// </summary>
    private void Update()
    {
        // + Apagar linterna con un solo click
        if(Input.GetMouseButtonDown(0))
        {           
            // Encender o apagar
            isOn = !isOn;             
            flashlight.intensity = !isOn ? 0 : 2;
            // Audio de encender o apagar
            // AudioClipName name = !isOn ? AudioClipName.OffSound : AudioClipName.OnSound;
            AudioManeger.Play(AudioClipName.FlashLightSound);
        }

        // De Prueba
        if(Input.GetKey(KeyCode.Z))
        {
            MarkTask(TaskName.GreetTheBoss);
        }

         if(Input.GetKey(KeyCode.X))
        {
            MarkTask(TaskName.UnplugTheBath);
        }

         if(Input.GetKey(KeyCode.C))
        { 
            MarkTask(TaskName.CleanTheWindows);
        }

         if(Input.GetKey(KeyCode.V))
        {
            MarkTask(TaskName.PrepareCoffee);
        }

         if(Input.GetKey(KeyCode.B))
        {
            MarkTask(TaskName.EcuationLife);
        }

         if(Input.GetKey(KeyCode.N))
        {
            MarkTask(TaskName.FinalTaskLight);
        }
    }

    private void MarkTask(TaskName taskEnum)
    {
        GameObject tasksFound = GameObject.Find("TaskList");

        if(tasksFound == null) Debug.Log("Error: No se encontro el GameObject Tasklist");

        TaskList taskList = tasksFound.GetComponent<TaskList>();

        if(taskList == null) Debug.Log("Error: No se encontro el componente TaskList en el gameobject Tasklist");

        taskList.TaskAllreadyDone(taskEnum);
    }

    /// <summary>
    /// OnTriggerExit es llamado cuando el collier se matiene tocando el trigger.
    /// </summary>
    /// <param name="other">El otro collider involucrado con esta  colision.</param>
    private void OnTriggerStay(Collider other) 
    {
        // RayCast para tener un mejor control de detección
        Ray camRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;
            
        if(Physics.Raycast(camRay, out hitInfo))
        {           
            // DetectObject(other, hitInfo, "ItemBox", "CAJA");
            // DetectObject(other, hitInfo, "ItemBall", "PELOTA DEFORMADA");
            // DetectObject(other, hitInfo, "ItemPhoto1", "FOTO DE MARIO EN 8BTS");
            // DetectObject(other, hitInfo, "ItemPhoto2", "FOTO DE SONIC EN PNG");                                             
        }          
    }

    // /// <summary>
    // /// Implemente este código para evitar repetir código y aparte porque odio código repetido.
    // /// </summary>
    // /// <param name="other">El otro collider involucrado con esta  colision.</param>
    // /// <param name="hitInfo">La información del hit box del objeto apuntado.</param>
    // /// <param name="theTag">El tag de un objeto que se quiera acceder.</param>
    // /// <param name="theObject">Nombre/Titulo del objeto identificado.</param>
    // private void DetectObject(Collider other, RaycastHit hitInfo, string theTag, string theObject)
    // {
    //     // Para ver que es Caja            
    //     if(other.gameObject.CompareTag(theTag) && hitInfo.collider.CompareTag(theTag))
    //     {
    //         objectText.SetActive(true);
    //         infoText.text = theObject;
    //     }            

    //     if(!hitInfo.collider.CompareTag(theTag) && other.gameObject.CompareTag(theTag))
    //         objectText.SetActive(false);
    // }

    // /// <summary>
    // /// OnTriggerExit es llamado cuando el collier se detubo de tocar el trigger.
    // /// </summary>
    // /// <param name="other">The other Collider involved in this collision.</param>
    // private void OnTriggerExit(Collider other)
    // {
    //     objectText.SetActive(false);                
    // }

}

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

public class PlayerController : MonoBehaviour
{
    //--------------------------------------------------------------------------------------
    /// -→ Atributos y Campos    
    /// - Linterna
    [SerializeField] private Light flashlight;
    private bool isOn = true;    
    private bool showTaskList = false;    
    [SerializeField] private Manager manager;
    [SerializeField] private AgarrarInodoro indoroObj;

    //--------------------------------------------------------------------------------------
    /// -→ Métodos

    private void Start() 
    {
        if(manager == null)
        {
            Debug.Log("Error: El Manager no esta adjunto con el player Controller");
        }
    }

    /// <summary>
    /// Update es llamado por cada frame
    /// </summary>
    private void Update()
    {
        // + Apagar linterna con un solo click
        if(Input.GetButtonDown(InputName.Fire1.ToString()))
        {           
            // Encender o apagar
            isOn = !isOn;             
            flashlight.intensity = !isOn ? 0 : 2;
            AudioManeger.Play(AudioClipName.FlashLightSound);
        }

        // Mostrar o ocultar la lista de tareas
        if(Input.GetButtonDown(InputName.ListButton.ToString()))
        {
            showTaskList = !showTaskList;
            manager.ShowTaskList(showTaskList);
        }

        // De Prueba
        if(Input.GetKey(KeyCode.Z))
        {
            manager.FinishedTask(TaskName.GreetTheBoss);
        }
    }

    private void FixedUpdate() 
    {
        Ray camRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;
        float maxRayDistance = 1f;

        if(Physics.Raycast(camRay, out hitInfo, maxRayDistance))
        {            
            TasksToDo(hitInfo);
        }
        else
        {
            Debug.DrawRay(camRay.origin, camRay.direction * maxRayDistance, Color.green);
            manager.HideInputInformation();
        }
    }

    private void TasksToDo(RaycastHit hitInfo)
    {
        // Bath Task
        if(hitInfo.collider.CompareTag("Inodoro") && manager.TheTaskIsDone(TaskName.UnplugTheBath) == 0 && indoroObj.PickedObject != null)
        {
            if(indoroObj.PickedObject.ToString() == "BananaGrab (UnityEngine.GameObject)")
            {
                manager.ShowInputInformation("F", "Destapar el baño");
                if(Input.GetButtonDown(InputName.InteractionButton.ToString()))
                {
                    StartCoroutine("UnplugTheBathAction");
                }
            }
        }
        
        // Ecuation Life Task
        if(hitInfo.collider.CompareTag("EcuationLife") && manager.TheTaskIsDone(TaskName.EcuationLife) == 0)
        {                
            manager.ShowInputInformation("F", "Resuelve la Ecuación de la Vida");
            if(Input.GetButtonDown(InputName.InteractionButton.ToString()))
            {
                manager.HideInputInformation();
                manager.EnterEcuationLife();
            }
        }

        // Final Mission Light
        if(hitInfo.collider.CompareTag("Cilindro") && manager.TheTaskIsDone(TaskName.FinalTaskLight) == 0)
        {
            manager.ShowInputInformation("F", "Repara la luz");
            if(Input.GetButtonDown(InputName.InteractionButton.ToString()))
            {
                StartCoroutine("TurnonTheLight");
                
            }
        }
    }

    IEnumerator UnplugTheBathAction()
    {
        indoroObj.DropObject();
                
        GameObject bananaToDestroy = GameObject.Find("BananaGrab");

        if(bananaToDestroy != null)
            Destroy(bananaToDestroy);

        GameObject bathObj = GameObject.Find("Toilet");

        Animator animBath = bathObj.GetComponent<Animator>();

        AudioManeger.Play(AudioClipName.Plunger);
        animBath.SetBool("banana", true);
        
        yield return new WaitForSeconds(5f);

        AudioManeger.Play(AudioClipName.BellDone);
        manager.FinishedTask(TaskName.UnplugTheBath);
    }

    IEnumerator TurnonTheLight()
    {
        GameObject turnOn = GameObject.Find("Cylinder");

        if(turnOn != null)
        {
            Animator animTurnOn = turnOn.GetComponent<Animator>();
            animTurnOn.SetBool("Switch", true);
        }
        
        yield return new WaitForSeconds(3f);

        manager.FinishedTask(TaskName.FinalTaskLight);
    }

}

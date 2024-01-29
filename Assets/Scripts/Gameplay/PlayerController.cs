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
    private Manager manager;
    [SerializeField] private AgarrarInodoro indoroObj;

    //--------------------------------------------------------------------------------------
    /// -→ Métodos

    private void Start() 
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();

        if(manager == null)
            Debug.Log("Error: No hay un manager asignado");
        
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
    }

    private void FixedUpdate() 
    {
        Ray camRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;
        float maxRayDistance = 2f;

        if(Physics.Raycast(camRay, out hitInfo, maxRayDistance))
        {            
            Debug.DrawRay(camRay.origin, camRay.direction * maxRayDistance, Color.cyan);
            TasksToDo(hitInfo);
        }
        else
        {
            Debug.DrawRay(camRay.origin, camRay.direction * maxRayDistance, Color.blue);
            manager.HideInputInformation();
        }
    }

    private void TasksToDo(RaycastHit hitInfo)
    {
        // Talk to Boss
        if(hitInfo.collider.CompareTag("BossRabbit") && manager.TheTaskIsDone(TaskName.GreetTheBoss) == 0)
        {
             manager.ShowInputInformation("F", "Habla con tu Jefe");
             if(Input.GetButtonDown(InputName.InteractionButton.ToString()))
             {
                manager.HideInputInformation();
                GameObject finalDialoge = GameObject.Find("BossTalk");

                if(finalDialoge != null)
                {
                    SistemaDialogo sistemaDialogo = finalDialoge.GetComponent<SistemaDialogo>();
                    sistemaDialogo.ActiveDialoge();
                }
             }
        }

        // Fix Printer
        if(hitInfo.collider.CompareTag("Impresora") && manager.TheTaskIsDone(TaskName.FixPrinter) == 0)
        {
            manager.ShowInputInformation("F", "Arregla la impresora");
            if(Input.GetButtonDown(InputName.InteractionButton.ToString()))
            {
                manager.FinishedTask(TaskName.FixPrinter);
                manager.HideInputInformation();
                manager.EnterFixPrinter();
            }
        }
        
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
                manager.FixLight();
                manager.HideInputInformation();
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

        AudioManeger.Play(AudioClipName.PlungerShortened);
        animBath.SetBool("banana", true);
        
        yield return new WaitForSeconds(4f);

        AudioManeger.Play(AudioClipName.ToiletFlushSOund);

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

        // ! Hacer aparecer el asunto del dialogo final
        GameObject finalDialoge = GameObject.Find("FinalDialoge");

        if(finalDialoge != null)
        {
            SistemaDialogo sistemaDialogo = finalDialoge.GetComponent<SistemaDialogo>();
            sistemaDialogo.ActiveDialoge();
        }
    }

}

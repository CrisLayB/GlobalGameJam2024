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
    private bool showTaskList = false;
    
    [SerializeField] private GameObject answers_ecuation_life; 
    [SerializeField] private Manager manager;

    //--------------------------------------------------------------------------------------
    /// -→ Métodos

    private void Start() 
    {
        if(answers_ecuation_life == null)
        {
            Debug.Log("Error: No esta adjunto el Game Object de UI Respuestas de ecuacion de la vida");
        }

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
        if(Input.GetMouseButtonDown(0))
        {           
            // Encender o apagar
            isOn = !isOn;             
            flashlight.intensity = !isOn ? 0 : 2;
            AudioManeger.Play(AudioClipName.FlashLightSound);
        }

        // Mostrar o ocultar la lista de tareas
        if(Input.GetKeyDown(KeyCode.Q))
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
            if(hitInfo.collider.CompareTag("EcuationLife") && manager.TheTaskIsDone(TaskName.EcuationLife) == 0)
            {                
                answers_ecuation_life.SetActive(true);
                manager.EnterEcuationLife();
            }
        }
        else
        {
            Debug.DrawRay(camRay.origin, camRay.direction * maxRayDistance, Color.green);
            answers_ecuation_life.SetActive(false);
        }
    }
}

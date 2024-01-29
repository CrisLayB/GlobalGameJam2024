using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agarrar : MonoBehaviour
{
    public GameObject handPoint;
    private GameObject pickedObject = null;
    private bool interactingWithMachine = false; 
    private bool animationPlayed = false; 
    private Manager manager;

    private void Start() 
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();    

        if(manager == null)
            Debug.Log("Error: No hay un manager asignado");
    }

    void Update()
    {
        if (pickedObject != null)
        {
            // Soltar la taza cuando se presiona la tecla "r"
            if (Input.GetKey("r"))
            {
                DropObject();
            }

            // Llenar la taza "f",
            if(interactingWithMachine)
            {
                manager.ShowInputInformation("F", "Para llenar la taza de café");
                ActivateAnimation();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Recoger la taza al presionar "e" 
        if (other.gameObject.CompareTag("CoffeeCup") && pickedObject == null)
        {
            manager.ShowInputInformation("E", "Agarra la taza para el café");
            if (Input.GetKey("e"))
            {
                manager.HideInputInformation();
                PickUpObject(other.gameObject);
            }
        }

        // Verificar si se está interactuando con la máquina de café
        if (other.gameObject.CompareTag("CoffeeMachine"))
        {
            interactingWithMachine = true;
        }

        // Verificar si la animación del café ya ha ocurrido al tocar el collider
        if (other.gameObject.CompareTag("WinCollider") && animationPlayed)
        {
            GameObject managerFound = GameObject.Find("Manager");
            if(managerFound != null)
            {
                Manager manager = managerFound.GetComponent<Manager>();
                manager.FinishedTask(TaskName.PrepareCoffee);
                GameObject coffeeToDestroy = GameObject.Find("PlasticCupForCoffee");
                if(coffeeToDestroy != null)
                {
                    Destroy(coffeeToDestroy);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Dejar de interactuar con la máquina de café cuando el jugador se aleje
        if (other.gameObject.CompareTag("CoffeeMachine"))
        {
            interactingWithMachine = false;
        }
    }

    private void PickUpObject(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().useGravity = false;
        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.transform.position = handPoint.transform.position;
        obj.transform.SetParent(handPoint.transform);
        pickedObject = obj;
    }

    private void DropObject()
    {
        pickedObject.GetComponent<Rigidbody>().useGravity = true;
        pickedObject.GetComponent<Rigidbody>().isKinematic = false;
        pickedObject.transform.SetParent(null);
        pickedObject = null;
    }

    private void ActivateAnimation()
    {
        Animator cupAnimator = pickedObject.GetComponent<Animator>();
        if (cupAnimator != null)
        {
            cupAnimator.SetBool("fillCoffee", true);
            animationPlayed = true;
        }
        else
        {
            Debug.Log("No se encontro el animator");
        }
    }
}


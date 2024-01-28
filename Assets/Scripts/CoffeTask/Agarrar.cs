using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agarrar : MonoBehaviour
{
    public GameObject handPoint;
    private GameObject pickedObject = null;
    private bool interactingWithMachine = false; 
    private bool animationPlayed = false; 

    void Update()
    {
        if (pickedObject != null)
        {
            // Soltar la taza cuando se presiona la tecla "r"
            if (Input.GetKey("r"))
            {
                DropObject();
            }

            // Llenar la taza "q",
            if (Input.GetKeyDown("q") && interactingWithMachine)
            {
                ActivateAnimation();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Recoger la taza al presionar "e" 
        if (other.gameObject.CompareTag("CoffeeCup") && pickedObject == null)
        {
            if (Input.GetKey("e"))
            {
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
            Debug.Log("¡Ganaste!");
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


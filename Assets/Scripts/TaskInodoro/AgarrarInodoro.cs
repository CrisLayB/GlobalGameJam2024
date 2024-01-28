using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgarrarInodoro : MonoBehaviour
{
    public GameObject handPoint;
    public GameObject targetObject; // Objeto cuyo color será cambiado
    public Color newColor; // Nuevo color para el objeto
    private GameObject pickedObject = null;
    private bool interactingWithMachine = false; 

    public GameObject PickedObject
    {
        get { return pickedObject; }
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

            // Cambiar el color del objeto objetivo "f",
            if (Input.GetKeyDown("f") && interactingWithMachine)
            {
                ChangeObjectColor();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Recoger la taza al presionar "e" 
        if (other.gameObject.CompareTag("Banana") && pickedObject == null)
        {
            if (Input.GetKey("e"))
            {
                PickUpObject(other.gameObject);
            }
        }

        // Verificar si se está interactuando con el inodoro
        if (other.gameObject.CompareTag("Inodoro"))
        {
            interactingWithMachine = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Dejar de interactuar con la máquina de café cuando el jugador se aleje
        if (other.gameObject.CompareTag("Inodoro"))
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

    private void ChangeObjectColor()
    {
        Renderer rend = targetObject.GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = newColor;
        }
        else
        {
            Debug.Log("No se encontró el Renderer en el objeto objetivo.");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskLuz : MonoBehaviour
{
    private GameObject interactingObject = null;
    private bool animationPlayed = false;

    void Update()
    {
        // apagar la luz "f",
        if (Input.GetKeyDown("f") && interactingObject != null)
        {
            ActivateAnimation();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si se está interactuando con la luz
        if (other.gameObject.CompareTag("Cilindro"))
        {
            interactingObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Dejar de interactuar con la máquina de café cuando el jugador se aleje
        if (other.gameObject == interactingObject)
        {
            interactingObject = null;
        }
    }

    private void ActivateAnimation()
    {
        Animator animator = interactingObject.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("Switch", true);
            animationPlayed = true;
        }
        else
        {
            Debug.Log("No se encontró el Animator en el objeto de interacción.");
        }
    }
}

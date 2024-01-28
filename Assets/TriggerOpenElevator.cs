using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOpenElevator : MonoBehaviour
{
    private GameObject zombie;

    void Start()
    {
        // Encuentra el objeto "Zombie" al inicio y lo asigna a la variable zombie
        zombie = GameObject.Find("Zombie");
    }

    void Update()
    {
        // Código de Update si es necesario
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entró en el trigger tiene la etiqueta "Zombie"
        if (other.CompareTag("Zombie"))
        {
            Debug.Log("Entro zombie");
        }
    }
}

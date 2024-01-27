using UnityEngine;

public class CoffeeCup : MonoBehaviour
{
    public bool isBeingCarried = false;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Si la taza está siendo llevada por el jugador, la posición de la taza se actualizará a la posición del jugador
        if (isBeingCarried)
        {
            transform.position = player.position;
        }
    }

    // Método para levantar la taza de café
    public void PickUp()
    {
        isBeingCarried = true;
        // Desactivar gravedad y colisiones mientras la taza esté siendo llevada
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;
    }

    // Método para soltar la taza de café
    public void Drop()
    {
        isBeingCarried = false;
        // Reactivar gravedad y colisiones cuando la taza sea soltada
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().enabled = true;
    }
}

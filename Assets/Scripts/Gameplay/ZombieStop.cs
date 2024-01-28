using UnityEngine;

public class TriggerZombieOnly : MonoBehaviour
{
    [SerializeField] private Animator zombieAnimator; // Referencia al Animator del zombi

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            Debug.Log("Zombi ha activado el trigger.");

            // Asegúrate de que el nombre del parámetro coincida con uno en tu Animator Controller
            if (zombieAnimator != null)
            {
                zombieAnimator.SetBool("playStop", true); // Activa la animación de stop
            }
            else
            {
                Debug.LogError("Animator no asignado al script TriggerZombieOnly");
            }
        }
        else
        {
            Debug.Log("Otro objeto ha entrado al trigger pero no es el zombi: " + other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            // Desactiva la animación de stop cuando el zombi sale del trigger
            if (zombieAnimator != null)
            {
                zombieAnimator.SetBool("playStop", false); // Desactiva la animación de stop
            }
        }
    }
}

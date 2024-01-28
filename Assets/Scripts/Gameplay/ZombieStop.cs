using UnityEngine;

public class TriggerZombieOnly : MonoBehaviour
{
    [SerializeField] private Animator zombieAnimator; // Referencia al Animator del zombi

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            Debug.Log("Zombi ha activado el trigger.");

            // Aseg�rate de que el nombre del par�metro coincida con uno en tu Animator Controller
            if (zombieAnimator != null)
            {
                zombieAnimator.SetBool("playStop", true); // Activa la animaci�n de stop
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
            // Desactiva la animaci�n de stop cuando el zombi sale del trigger
            if (zombieAnimator != null)
            {
                zombieAnimator.SetBool("playStop", false); // Desactiva la animaci�n de stop
            }
        }
    }
}

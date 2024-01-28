using UnityEngine;

public class ZombieStop : MonoBehaviour
{
    [SerializeField] private Animator myAnimationController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            Debug.Log("Entro");
            myAnimationController.SetBool("Stop", true);
        }

    }


}

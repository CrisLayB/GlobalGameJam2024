using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieActions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    [SerializeField] private Animator myAnimationController;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            myAnimationController.SetBool("playHi", true);
        }
        
    }

    /*private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myAnimationController.SetBool("playHi", false);
        }

    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
            Debug.Log("Error: Animator de zombie no encontrado");
    }

    private void Update()
    {
        if (transform.position.x < 8f)
        {
            animator.SetBool("playStop", true);
        }
    }

    // private void OnTriggerEnter(Collider other) 
    // {
    //     if(other.tag == "ParedElevador")
    //     {
    //         animator.SetBool("playStop", true);
    //     }
    // }

}
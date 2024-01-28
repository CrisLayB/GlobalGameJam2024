using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenElevator : MonoBehaviour
{
    private SistemaDialogo sistemaDialogo;
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        sistemaDialogo = FindObjectOfType<SistemaDialogo>();
        myAnimator = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
    if (sistemaDialogo != null)
    {
        if(sistemaDialogo.IsActive()){
            myAnimator.SetBool("DoorClosed", true);

        }
        else {
            myAnimator.SetBool("DoorClosed", false);
        }

    }    
    }
}

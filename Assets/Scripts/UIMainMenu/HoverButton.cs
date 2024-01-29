using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour
{
    [SerializeField] private Sprite selected, unselected;
    private Image buttonSprite;
    
    void Start(){
        // renderer.material.color = Color.black;
        buttonSprite = GetComponent<Image>();

        if(buttonSprite == null)
            Debug.Log("Error: No se encontro un sprite");
    }

    void OnMouseEnter(){
        // renderer.material.color = Color.red;
        buttonSprite.sprite = selected;
    }

    void OnMouseExit() {
        buttonSprite.sprite = unselected;
    }
}

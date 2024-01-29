using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    public string nombreEscena = "Snake";
    private string nombreEscenaAnterior;

    void Start()
    {
        nombreEscenaAnterior = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            InteractuarConImpresora();
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            VolverAEscenaAnterior();
        }
    }

    void InteractuarConImpresora()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.CompareTag("Impresora"))
            {
                SceneManager.LoadScene(nombreEscena);
            }
        }
    }

    void VolverAEscenaAnterior()
    {
        SceneManager.LoadScene(nombreEscenaAnterior);
    }
}

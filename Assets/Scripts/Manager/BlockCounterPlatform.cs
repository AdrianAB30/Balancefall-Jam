using UnityEngine;

public class BlockCounterPlatform : MonoBehaviour
{
    public UILevel1 ui;

    private int bloquesEnPlataforma = 0;
    public int BloquesEnPlataforma => bloquesEnPlataforma;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cubo"))
        {
            bloquesEnPlataforma++;
            ui.ActualizarBloquesEnPlataforma(bloquesEnPlataforma);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cubo"))
        {
            bloquesEnPlataforma--;
            ui.ActualizarBloquesEnPlataforma(bloquesEnPlataforma);
        }
    }
}

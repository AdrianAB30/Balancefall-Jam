using DG.Tweening;
using TMPro;
using UnityEngine;

public class UILevel1 : MonoBehaviour
{
    public TMP_Text txtBloquesRestantes;
    public TMP_Text txtBloquesEnPlataforma;

    private int totalBloques;
    private int minBloquesRequeridos;

    public void Inicializar(int total, int minimo)
    {
        totalBloques = total;
        minBloquesRequeridos = minimo;

        txtBloquesRestantes.text = $"Restantes: {totalBloques}";
        txtBloquesEnPlataforma.text = $"En plataforma: 0/{minBloquesRequeridos}";
    }

    public void ActualizarBloquesRestantes(int restantes)
    {
        txtBloquesRestantes.text = $"Restantes: {restantes}";
    }

    public void ActualizarBloquesEnPlataforma(int actuales)
    {
        txtBloquesEnPlataforma.text = $"En plataforma: {actuales} / {minBloquesRequeridos}";
    }
}

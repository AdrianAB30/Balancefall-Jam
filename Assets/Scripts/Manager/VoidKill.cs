using UnityEngine;

public class VoidKill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cubo"))
        {
            Destroy(this.gameObject);
        }
    }
}

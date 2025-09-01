using UnityEngine;

public class TeleportableObject : MonoBehaviour, ITeleportable
{
    [Tooltip("O objeto com o qual este objeto troca de posição.")]
    public TeleportableObject pair;

    public void Teleport(Vector3 destination)
    {
        transform.position = destination;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void SwapWithPair()
    {
        if (pair == null)
        {
            Debug.LogWarning($"{name} não tem par configurado!");
            return;
        }

        Vector3 myPos = transform.position;
        Vector3 pairPos = pair.transform.position;

        // Troca posições
        Teleport(pairPos);
        pair.Teleport(myPos);
    }
}

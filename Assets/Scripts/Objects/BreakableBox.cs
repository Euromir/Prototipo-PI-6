using UnityEngine;

public class BreakableBox : MonoBehaviour, IBreakable
{
    // [Header("Efeitos")]
    // [Tooltip("Opcional: Um prefab de partículas para instanciar quando a caixa quebrar.")]
    //public GameObject DestructionEffectPrefab;

    public void Break()
    {
        //if (DestructionEffectPrefab != null)
        //{
        //     Instantiate(DestructionEffectPrefab, transform.position, transform.rotation);
        // }

        Destroy(gameObject);
    }
}

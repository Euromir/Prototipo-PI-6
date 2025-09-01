using UnityEngine;

[CreateAssetMenu(fileName = "New Teleport Ability", menuName = "Abilities/Teleport")]
public class TeleportAbility : Ability
{
    [Tooltip("Distância máxima do raycast para encontrar alvo.")]
    public float MaxDistance = 10f;

    [Tooltip("Layer dos objetos teleportáveis.")]
    public LayerMask TeleportableLayer;

    public override void Activate(GameObject caster)
    {
        Ray ray = new Ray(caster.transform.position, caster.transform.forward);
        if (Physics.SphereCast(ray, 0.5f, out RaycastHit hit, MaxDistance, TeleportableLayer))
        {
            TeleportableObject teleportable = hit.collider.GetComponent<TeleportableObject>();
            if (teleportable != null)
            {
                teleportable.SwapWithPair();
            }
        }
    }
}

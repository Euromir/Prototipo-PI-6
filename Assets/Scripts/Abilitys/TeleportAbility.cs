using UnityEngine;

[CreateAssetMenu(fileName = "New Teleport Ability", menuName = "Abilities/Teleport")]
public class TeleportAbility : Ability, ITargetingAbility 
{
    public float MaxDistance = 1f;
    public LayerMask TeleportableLayer;

    public LayerMask GetTargetLayer()
    {
        return TeleportableLayer;
    }

    public override void Activate(GameObject caster)
    {
        Ray ray = new Ray(caster.transform.position, caster.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, MaxDistance, TeleportableLayer))
        {
            TeleportableObject teleportable = hit.collider.GetComponent<TeleportableObject>();
            if (teleportable != null)
            {
                teleportable.SwapWithPair();
            }
        }
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "New Break Ability", menuName = "Abilities/Break")]
public class BreakAbility : Ability, ITargetingAbility
{
    [Header("Configurações de Quebra")]
    public float BreakDistance = 1f;
    [Tooltip("Camada dos objetos quebráveis")]
    public LayerMask BreakableLayer;

    public LayerMask GetTargetLayer()
    {
        return BreakableLayer;
    }

    public override void Activate(GameObject caster)
    {
        RaycastHit hit;
        if (Physics.Raycast(caster.transform.position, caster.transform.forward, out hit, BreakDistance, BreakableLayer))
        {
            IBreakable breakable = hit.collider.GetComponent<IBreakable>();
            if (breakable != null)
            {
                breakable.Break();
            }
        }
    }
}

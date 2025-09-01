using UnityEngine;

[CreateAssetMenu(fileName = "New Break Ability", menuName = "Abilities/Break")]
public class BreakAbility : Ability
{
    [Header("Configurações de Quebra")]
    public float BreakRadius = 3f;
    public LayerMask BreakableLayer;

    public override void Activate(GameObject caster)
    {
        Collider[] colliders = Physics.OverlapSphere(caster.transform.position, BreakRadius, BreakableLayer);
        foreach (var col in colliders)
        {
            IBreakable breakable = col.GetComponent<IBreakable>();
            if (breakable != null)
            {
                breakable.Break();
            }
        }
        Debug.Log("Habilidade de quebrar ativada!");
    }
}

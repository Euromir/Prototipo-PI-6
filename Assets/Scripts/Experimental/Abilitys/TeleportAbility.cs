using UnityEngine;

[CreateAssetMenu(fileName = "New Teleport Ability", menuName = "Abilities/Teleport")]
public class TeleportAbility : Ability
{
    [Header("Configurações de Teleporte")]
    public float TeleportRadius = 5f;
    public Transform Destination; // Ponto para onde os objetos serão teleportados
    public LayerMask TeleportableLayer;

    public override void Activate(GameObject caster)
    {
        if (Destination == null) return;

        Collider[] colliders = Physics.OverlapSphere(caster.transform.position, TeleportRadius, TeleportableLayer);
        foreach (var col in colliders)
        {
            ITeleportable teleportable = col.GetComponent<ITeleportable>();
            if (teleportable != null)
            {
                teleportable.Teleport(Destination.position);
            }
        }
        Debug.Log("Habilidade de teleporte ativada!");
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "New Toggle Light Ability", menuName = "Abilities/Toggle Light")]
public class ToggleLightAbility : Ability
{
    [Header("Configurações da Luz")]
    [Tooltip("Nome exato do GameObject filho que contém a luz.")]
    public string LightObjectName;

    public override void Activate(GameObject caster)
    {
        Transform lightTransform = caster.transform.Find(LightObjectName);

        Light lightComponent = lightTransform.GetComponent<Light>();
        if (lightComponent != null)
        {
            lightComponent.enabled = !lightComponent.enabled;
        }
    }
}

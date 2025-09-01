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
        if (lightTransform == null)
        {
            Debug.LogError($"Objeto de luz '{LightObjectName}' não encontrado como filho de {caster.name}!");
            return;
        }

        Light lightComponent = lightTransform.GetComponent<Light>();
        if (lightComponent != null)
        {
            // Inverte o estado atual da luz (se estiver ligada, desliga, e vice-versa)
            lightComponent.enabled = !lightComponent.enabled;
        }
    }
}

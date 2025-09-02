using UnityEngine;

[CreateAssetMenu(fileName = "New Toggle Light Ability", menuName = "Abilities/Toggle Light")]
public class ToggleLightAbility : Ability
{
    public override void Activate(GameObject caster)
    {
        PlayerLightState lightState = caster.GetComponent<PlayerLightState>();

        if (lightState != null)
        {
            lightState.ToggleLight();
        }
    }
}

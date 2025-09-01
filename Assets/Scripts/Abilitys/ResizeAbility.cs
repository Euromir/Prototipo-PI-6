using UnityEngine;

[CreateAssetMenu(fileName = "New Resize Ability", menuName = "Abilities/Resize")]
public class ResizeAbility : Ability
{
    public override void Activate(GameObject caster)
    {
        PlayerResizer resizer = caster.GetComponent<PlayerResizer>();

        if (resizer != null)
        {
            resizer.ToggleSize();
        }
    }
}

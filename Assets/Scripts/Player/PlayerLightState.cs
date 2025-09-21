using UnityEngine;

public class PlayerLightState : MonoBehaviour
{
    [Tooltip("A luz que será ligada e desligada no jogador.")]
    public Light playerLightObject;
    public Collider lightCollider;

    public bool isLightOn = true;

    void Start()
    {
        if (playerLightObject != null)
        {
            playerLightObject.enabled = isLightOn;
        }
    }

    public void ToggleLight()
    {
        isLightOn = !isLightOn;

        if (playerLightObject != null)
        {
            playerLightObject.enabled = isLightOn;
            lightCollider.enabled = isLightOn;
        }
    }
}

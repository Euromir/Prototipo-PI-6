using UnityEngine;

public class PlayerLightEmitter : MonoBehaviour
{
    [Tooltip("O componente de luz que este script controla.")]
    public Light playerLight;

    [Tooltip("O alcance da luz para interagir com as lâmpadas.")]
    public float interactionRange = 20f;

    [Tooltip("A Layer em que as lâmpadas estão.")]
    public LayerMask lampLayer;

    private ChargeableLamp _lastHitLamp;

    void Update()
    {
        if (!playerLight.enabled)
        {
            if (_lastHitLamp != null)
            {
                _lastHitLamp.StopReceivingLight();
                _lastHitLamp = null;
            }
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange, lampLayer))
        {
            ChargeableLamp currentLamp = hit.collider.GetComponent<ChargeableLamp>();
            if (currentLamp != null)
            {
                if (currentLamp != _lastHitLamp)
                {
                    if (_lastHitLamp != null)
                    {
                        _lastHitLamp.StopReceivingLight();
                    }
                    _lastHitLamp = currentLamp;
                }
                _lastHitLamp.ReceiveLight();
            }
        }
        else
        {
            if (_lastHitLamp != null)
            {
                _lastHitLamp.StopReceivingLight();
                _lastHitLamp = null;
            }
        }
    }
}
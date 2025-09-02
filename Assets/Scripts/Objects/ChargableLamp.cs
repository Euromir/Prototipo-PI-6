using UnityEngine;
using System.Collections;

public class ChargeableLamp : MonoBehaviour
{
    [Tooltip("A luz desta lâmpada.")]
    public Light lampLight;

    [Tooltip("Tempo em segundos que a lâmpada permanece acesa após perder o contato.")]
    public float timeBeforeFading = 3f;

    private Coroutine _fadeCoroutine;

    void Start()
    {
        if (lampLight != null)
        {
            lampLight.enabled = false;
        }
    }

    public void ReceiveLight()
    {
        if (!lampLight.enabled)
        {
            lampLight.enabled = true;
            LightPuzzleManager.Instance.OnLampActivated();
        }

        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = null;
        }
    }

    public void StopReceivingLight()
    {
        if (_fadeCoroutine == null)
        {
            _fadeCoroutine = StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(timeBeforeFading);
        
        lampLight.enabled = false;
        LightPuzzleManager.Instance.OnLampDeactivated();
        _fadeCoroutine = null;
    }
}

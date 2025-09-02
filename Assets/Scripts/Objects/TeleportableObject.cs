using UnityEngine;

public class TeleportableObject : MonoBehaviour, IHighlightable
{
    [Header("Configuração de Troca")]
    public TeleportableObject PairObject;
    public Transform DestinationForPair;

    private Renderer _renderer;
    private Color _originalColor;
    private bool _isHighlighted = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        if (_renderer != null)
        {
            _originalColor = _renderer.material.color;
        }
    }

    public void SwapWithPair()
    {
        Transform thisObjectDestination = PairObject.DestinationForPair;

        PairObject.transform.position = DestinationForPair.position;
        PairObject.transform.rotation = DestinationForPair.rotation;
        PairObject.transform.localScale = DestinationForPair.localScale;

        transform.position = thisObjectDestination.position;
        transform.rotation = thisObjectDestination.rotation;
        transform.localScale = thisObjectDestination.localScale;
    }

    public void Highlight(Color highlightColor)
    {
        if (_renderer != null)
        {
            _renderer.material.color = highlightColor;
        }
    }

    public void ResetColor()
    {
        if (!_isHighlighted)
        {
            return;
        }

        _isHighlighted = false;

        if (_renderer != null)
        {
            _renderer.material.color = _originalColor;
        }

        if (PairObject != null)
        {
            PairObject.ResetColor();
        }
    }
}

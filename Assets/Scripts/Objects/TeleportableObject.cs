using UnityEngine;

public class TeleportableObject : MonoBehaviour, IHighlightable
{
    [Header("Configuração de Troca")]
    [Tooltip("O objeto parceiro para realizar a troca de posição.")]
    public TeleportableObject PairObject;

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
        Vector3 myOriginalPosition = transform.position;
        transform.position = PairObject.transform.position;
        PairObject.transform.position = myOriginalPosition;

        Quaternion myOriginalRotation = transform.rotation;
        transform.rotation = PairObject.transform.rotation;
        PairObject.transform.rotation = myOriginalRotation;
    }

    public void Highlight(Color highlightColor)
    {
        if (_renderer != null)
        {
            _renderer.material.color = highlightColor;
            _isHighlighted = true;
        }
    }

    public void ResetColor()
    {
        if (!_isHighlighted) return;

        _isHighlighted = false;

        if (_renderer != null)
        {
            _renderer.material.color = _originalColor;
        }
    }
}

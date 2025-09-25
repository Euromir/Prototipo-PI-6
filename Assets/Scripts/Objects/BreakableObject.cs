using UnityEngine;

public class BreakableObject : MonoBehaviour, IBreakable, IHighlightable
{
    [Header("Configuração de Quebra")]
    [Tooltip("Marque esta caixa se este objeto for uma ponte.")]
    public bool isBridge = false;

    [Tooltip("O objeto que será ATIVADO quando esta ponte for quebrada.")]
    public GameObject objectToActivateOnBreak;

    private Renderer _renderer;
    private Color _originalColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        if (_renderer != null)
        {
            _originalColor = _renderer.material.color;
        }
    }

    public void Break()
    {
        if (isBridge && objectToActivateOnBreak != null)
        {
            objectToActivateOnBreak.SetActive(true);
        }

        Destroy(gameObject);
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
        if (_renderer != null)
        {
            _renderer.material.color = _originalColor;
        }
    }
}

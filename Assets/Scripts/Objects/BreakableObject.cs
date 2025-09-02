using UnityEngine;

public class BreakableObject : MonoBehaviour, IBreakable, IHighlightable
{
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

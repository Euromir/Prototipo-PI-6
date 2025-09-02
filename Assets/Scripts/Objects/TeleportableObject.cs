using UnityEngine;

public class TeleportableObject : MonoBehaviour, IHighlightable
{
    [Header("Configuração de Troca")]
    public TeleportableObject PairObject;
    public Transform DestinationPoint;

    private static bool _areSwapped = false;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    private Renderer _renderer;
    private Color _originalColor;
    private bool _isHighlighted = false;

    private void Awake()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;

        _renderer = GetComponent<Renderer>();
        if (_renderer != null)
        {
            _originalColor = _renderer.material.color;
        }
    }

    public void SwapWithPair()
    {
        if (this.GetInstanceID() < PairObject.GetInstanceID())
        {
            PerformSwapLogic();
        }
        else
        {
            PairObject.PerformSwapLogic();
        }
    }

    public void PerformSwapLogic()
    {
        if (!_areSwapped)
        {
            this.transform.position = this.PairObject.DestinationPoint.position;
            this.transform.rotation = this.PairObject.DestinationPoint.rotation;

            this.PairObject.transform.position = this.DestinationPoint.position;
            this.PairObject.transform.rotation = this.DestinationPoint.rotation;
        }
        else
        {
            this.transform.position = this._initialPosition;
            this.transform.rotation = this._initialRotation;

            this.PairObject.transform.position = this.PairObject._initialPosition;
            this.PairObject.transform.rotation = this.PairObject._initialRotation;
        }

        _areSwapped = !_areSwapped;
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

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJump : MonoBehaviour
{
    [Header("Data")]
    [Tooltip("Referência ao ScriptableObject com os dados do jogador.")]
    [SerializeField] private PlayerData _playerData;

    [Header("Ground Check")]
    [Tooltip("Ponto de onde a verificação de chão será feita.")]
    [SerializeField] private Transform _groundCheckPoint;
    [Tooltip("Raio da esfera de verificação de chão.")]
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [Tooltip("Quais layers são consideradas 'chão'.")]
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody _rb;
    private bool _isGrounded;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckIfGrounded();
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && _isGrounded)
        {
            Jump();
        }
    }

    private void CheckIfGrounded()
    {
        _isGrounded = Physics.CheckSphere(_groundCheckPoint.position, _groundCheckRadius, _groundLayer);
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * _playerData.JumpForce, ForceMode.Impulse);
        PlayerEventSystem.InvokePlayerJump(_playerData.PlayerID, _rb, _playerData.JumpForce);
    }

    private void OnDrawGizmosSelected()
    {
        if (_groundCheckPoint == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_groundCheckPoint.position, _groundCheckRadius);
    }
}

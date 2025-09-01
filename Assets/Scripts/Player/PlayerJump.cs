using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJump : MonoBehaviour, IPlayerID
{
    [Header("Data")]
    [Tooltip("Referência ao ScriptableObject com os dados do jogador.")]
    [SerializeField] private PlayerData _playerData;

    public int PlayerID { get => _playerData.PlayerID; }

    [Header("Ground Check")]
    [Tooltip("Ponto de onde a verificação de chão será feita.")]
    [SerializeField] private Transform _groundCheckPoint;
    [Tooltip("Raio da esfera de verificação de chão.")]
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [Tooltip("Quais layers são consideradas 'chão'.")]
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody _rb;
    private bool _isGrounded;
    private InputDevice _assignedDevice;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void SetDevice(InputDevice device)
    {
        _assignedDevice = device;
    }

    private void Update()
    {
        CheckIfGrounded();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.control.device != _assignedDevice) return;

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
        PlayerEventSystem.InvokePlayerJump(PlayerID, _rb, _playerData.JumpForce);
    }

    private void OnDrawGizmosSelected()
    {
        if (_groundCheckPoint == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_groundCheckPoint.position, _groundCheckRadius);
    }
}

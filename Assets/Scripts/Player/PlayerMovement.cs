using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IPlayerID
{
    public int PlayerID { get => _playerData.PlayerID; }

    [SerializeField] private PlayerData _playerData;

    [Header("VFX")]
    [Tooltip("Sistema de partículas que será ativado ao andar.")]
    [SerializeField] private ParticleSystem _walkingVFX;

    private Rigidbody _rb;
    private Vector3 _moveInput;
    private Transform _cameraTransform;
    private InputDevice _assignedDevice;
    private Animator _animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    public void SetDevice(InputDevice device)
    {
        _assignedDevice = device;
    }

    public void SetCameraTransform(Transform cameraTransform)
    {
        _cameraTransform = cameraTransform;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.control.device != _assignedDevice) return;

        if (context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>();
            _moveInput = new Vector3(input.x, 0, input.y);
        }
        else if (context.canceled)
        {
            _moveInput = Vector3.zero;
        }
    }

    private void MovePlayer()
    {
        if (_moveInput.magnitude >= 0.1f && _cameraTransform != null)
        {
            _animator.SetBool("isWalking", true);

            if (_walkingVFX != null && !_walkingVFX.isPlaying)
            {
                _walkingVFX.Play();
            }

            Vector3 camForward = _cameraTransform.forward;
            Vector3 camRight = _cameraTransform.right;

            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 desiredMoveDirection = camForward * _moveInput.z + camRight * _moveInput.x;
            Vector3 newLinearVelocity = desiredMoveDirection * _playerData.Speed;
            _rb.linearVelocity = new Vector3(newLinearVelocity.x, _rb.linearVelocity.y, newLinearVelocity.z);

            Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
            _rb.rotation = targetRotation;

            PlayerEventSystem.InvokePlayerMoved(PlayerID, _rb, _rb.linearVelocity);
        }
        else
        {
            _animator.SetBool("isWalking", false);

            if (_walkingVFX != null && _walkingVFX.isPlaying)
            {
                _walkingVFX.Stop();
            }

            _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);
        }
    }
}

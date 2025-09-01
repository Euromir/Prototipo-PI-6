using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerCameraController : MonoBehaviour
{
    public enum CameraState { Following, Overview }

    [Header("Alvos")]
    [SerializeField] private Transform _overviewPoint;
    [SerializeField] private Transform _targetPlayer;

    [Header("Configurações de Suavidade")]
    [Tooltip("Velocidade de suavização do movimento da câmera.")]
    [SerializeField] private float _moveSpeed = 5f;
    [Tooltip("Velocidade de suavização da rotação.")]
    [SerializeField] private float _rotationSpeed = 10f;

    [Header("Modo de Acompanhamento (Following)")]
    [SerializeField] private Vector3 _followOffset = new Vector3(0f, 10f, -8f);
    [Tooltip("Ângulos de inclinação da câmera para o efeito 'bird's eye'.")]
    [SerializeField] private Vector3 _followRotationAngles = new Vector3(45f, 0f, 0f);

    [Header("Modo Visão Geral (Overview)")]
    [Tooltip("Offset da câmera em relação ao Ponto Central.")]
    [SerializeField] private Vector3 _overviewOffset = new Vector3(0f, 20f, -15f);

    private int _currentRotationStep = 0;
    private CameraState _currentState = CameraState.Following;

    void LateUpdate()
    {
        HandlePosition();
        HandleRotation();
    }

    private void HandlePosition()
    {
        Quaternion worldRotation = Quaternion.Euler(0, _currentRotationStep * 90f, 0);
        Vector3 targetPosition;

        switch (_currentState)
        {
            case CameraState.Following:
                if (_targetPlayer == null) return;
                targetPosition = _targetPlayer.position + (worldRotation * _followOffset);
                break;

            case CameraState.Overview:
                targetPosition = _overviewPoint.position + (worldRotation * _overviewOffset);
                break;

            default:
                return;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _moveSpeed);
    }

    private void HandleRotation()
    {
        Quaternion targetRotation;

        switch (_currentState)
        {
            case CameraState.Following:
                Quaternion yawRotation = Quaternion.Euler(0, _currentRotationStep * 90f, 0);

                Quaternion tiltRotation = Quaternion.Euler(_followRotationAngles);

                targetRotation = yawRotation * tiltRotation;
                break;

            case CameraState.Overview:
                targetRotation = Quaternion.LookRotation(_overviewPoint.position - transform.position);
                break;

            default:
                targetRotation = transform.rotation;
                break;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
    }

    public void OnToggleCamera(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _currentState = (_currentState == CameraState.Following) ? CameraState.Overview : CameraState.Following;
        }
    }

    public void OnRotateCamera(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float rotationValue = context.ReadValue<float>();
            if (rotationValue > 0) _currentRotationStep++;
            else if (rotationValue < 0) _currentRotationStep--;
        }
    }

    public void SetFollowTarget(Transform target)
    {
        _targetPlayer = target;
    }
}

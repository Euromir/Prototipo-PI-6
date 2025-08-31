using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private MultiplayerCameraController _assignedCamera;

    public void SetCamera(MultiplayerCameraController camera)
    {
        _assignedCamera = camera;
    }
    public void OnToggleCamera(InputAction.CallbackContext context)
    {
        if (_assignedCamera != null)
        {
            _assignedCamera.OnToggleCamera(context);
        }
    }
    public void OnRotateCamera(InputAction.CallbackContext context)
    {
        if (_assignedCamera != null)
        {
            _assignedCamera.OnRotateCamera(context);
        }
    }
}

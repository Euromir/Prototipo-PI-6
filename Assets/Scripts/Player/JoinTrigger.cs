using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class JoinTrigger : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private InputAction joinAction;

    private void Awake()
    {
        joinAction.Enable();
    }

    private void OnEnable()
    {
        joinAction.performed += OnJoinPerformed;
    }

    private void OnDisable()
    {
        joinAction.performed -= OnJoinPerformed;
    }

    private void OnJoinPerformed(InputAction.CallbackContext context)
    {
        if (InputUser.FindUserPairedToDevice(context.control.device) == null)
        {
            playerManager.JoinPlayer(context.control.device);
        }
    }
}

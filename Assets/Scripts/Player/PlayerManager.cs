using UnityEngine;
using System.Collections; // Adicione esta linha no topo
using System.Collections.Generic;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayerManager : MonoBehaviour
{
    [Header("Configuração dos Personagens")]
    [Tooltip("Arraste seus 4 PREFABS de jogador únicos aqui, na ordem (P1, P2, P3, P4).")]
    [SerializeField] private List<GameObject> playerPrefabs;

    [Tooltip("A lista de locais onde os jogadores irão aparecer.")]
    [SerializeField] private List<Transform> spawnPoints;

    [Header("Configuração das Câmeras")]
    [Tooltip("Arraste as 4 CÂMERAS da sua cena para esta lista, na ordem.")]
    [SerializeField] private List<MultiplayerCameraController> sceneCameras;

    private PlayerInputManager inputManager;
    private int playersJoined = 0;

    private List<InputDevice> _joinedDevices = new List<InputDevice>();

    private void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
    }

    public void JoinPlayer(InputDevice device)
    {
        if (playersJoined >= playerPrefabs.Count) return;

        PlayerInput.Instantiate(
            prefab: playerPrefabs[playersJoined],
            playerIndex: playersJoined,
            controlScheme: null,
            splitScreenIndex: -1,
            pairWithDevice: device
        );
    }

    private void OnEnable()
    {
        inputManager.onPlayerJoined += HandlePlayerJoined;
    }

    private void OnDisable()
    {
        inputManager.onPlayerJoined -= HandlePlayerJoined;
    }

    private void HandlePlayerJoined(PlayerInput playerInput)
    {
        var devices = playerInput.devices;

        playerInput.SwitchCurrentControlScheme(devices.ToArray());

        int playerIndex = playerInput.playerIndex;

        if (playerIndex < spawnPoints.Count && spawnPoints[playerIndex] != null)
        {
            Transform spawnPoint = spawnPoints[playerIndex];
            StartCoroutine(TeleportPlayer(playerInput.transform, spawnPoint));
        }

        if (playerIndex < sceneCameras.Count)
        {
            PlayerController controller = playerInput.GetComponent<PlayerController>();
            MultiplayerCameraController targetCamera = sceneCameras[playerIndex];

            if (controller != null)
            {
                controller.SetCamera(targetCamera);
                targetCamera.SetFollowTarget(playerInput.transform);
            }

            PlayerMovement movement = playerInput.GetComponent<PlayerMovement>();
            if (movement != null)
            {
                movement.SetCameraTransform(targetCamera.transform);

                movement.SetDevice(devices[0]);
            }

            PlayerJump jump = playerInput.GetComponent<PlayerJump>();
            if (jump != null)
            {
                jump.SetDevice(devices[0]);
            }
        }
        playersJoined++;
    }

    public void AddJoinedDevice(InputDevice device)
    {
        if (!_joinedDevices.Contains(device))
        {
            _joinedDevices.Add(device);
        }
    }

    public bool IsDeviceJoined(InputDevice device)
    {
        return _joinedDevices.Contains(device);
    }

    private IEnumerator TeleportPlayer(Transform playerTransform, Transform spawnPoint)
    {
        Rigidbody playerRb = playerTransform.GetComponent<Rigidbody>();
        if (playerRb != null)
        {
            playerRb.MovePosition(spawnPoint.position);
            playerRb.MoveRotation(spawnPoint.rotation);
        }
        yield return null;
    }
}

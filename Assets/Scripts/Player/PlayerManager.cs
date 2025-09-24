using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayerManager : MonoBehaviour
{
    [Header("Configuração dos Personagens")]
    [Tooltip("Arraste seus 4 PREFABS de jogador únicos aqui, na ordem (P1, P2, P3, P4).")]
    [SerializeField] private List<GameObject> playerPrefabs;

    [Tooltip("A lista de locais onde os jogadores irão aparecer INICIALMENTE.")]
    [SerializeField] private List<Transform> initialSpawnPoints;

    [Header("Configuração das Câmeras")]
    [Tooltip("Arraste as 4 CÂMERAS da sua cena para esta lista, na ordem.")]
    [SerializeField] private List<MultiplayerCameraController> sceneCameras;

    private Checkpoint currentCheckpoint;

    private List<PlayerInput> players = new List<PlayerInput>();

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
        players.Add(playerInput);

        var devices = playerInput.devices;
        playerInput.SwitchCurrentControlScheme(devices.ToArray());

        int playerIndex = playerInput.playerIndex;

        RespawnPlayer(playerInput);

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

    public void SetCurrentCheckpoint(Checkpoint newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
    }

    public void RespawnPlayer(PlayerInput playerInput)
    {
        int playerIndex = playerInput.playerIndex;
        Transform spawnPoint = null;

        if (currentCheckpoint != null)
        {
            spawnPoint = currentCheckpoint.GetRespawnPoint(playerIndex);
        }
        else if (playerIndex < initialSpawnPoints.Count && initialSpawnPoints[playerIndex] != null)
        {
            spawnPoint = initialSpawnPoints[playerIndex];
        }

        if (spawnPoint != null)
        {
            StartCoroutine(TeleportPlayer(playerInput.transform, spawnPoint));
        }
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
            playerRb.isKinematic = true;
        }

        playerTransform.position = spawnPoint.position;
        playerTransform.rotation = spawnPoint.rotation;

        yield return null;

        if (playerRb != null)
        {
            playerRb.isKinematic = false;
            playerRb.linearVelocity = Vector3.zero;
        }
    }
}
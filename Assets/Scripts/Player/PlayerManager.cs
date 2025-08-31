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
            }
        }

        playersJoined++;
    }

    private IEnumerator TeleportPlayer(Transform playerTransform, Transform spawnPoint)
    {
        // Espera o próximo ciclo de física para garantir que o Rigidbody esteja pronto.
        yield return new WaitForFixedUpdate();

        // Pega a referência do Rigidbody no jogador
        Rigidbody playerRb = playerTransform.GetComponent<Rigidbody>();
        if (playerRb != null)
        {
            // Usa o método correto para mover um objeto com física
            playerRb.MovePosition(spawnPoint.position);
            playerRb.MoveRotation(spawnPoint.rotation);
        }
        else // Caso não encontre o Rigidbody, usa o método antigo como segurança
        {
            playerTransform.position = spawnPoint.position;
            playerTransform.rotation = spawnPoint.rotation;
        }

        Debug.Log($"SUCESSO: Jogador movido via física para {spawnPoint.name} na posição {playerTransform.position}");
    }
}

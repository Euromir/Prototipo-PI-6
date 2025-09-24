using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private static PlayerManager playerManager;

    [Tooltip("Pontos de respawn para os jogadores (P1, P2, P3, P4) dentro deste checkpoint.")]
    [SerializeField] private Transform[] respawnPoints;

    private static Checkpoint lastActiveCheckpoint;

    private void Awake()
    {
        if (playerManager == null)
        {
            playerManager = FindObjectOfType<PlayerManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (lastActiveCheckpoint != this)
            {
                playerManager.SetCurrentCheckpoint(this);
                lastActiveCheckpoint = this;
            }
        }
    }

    public Transform GetRespawnPoint(int playerIndex)
    {
        if (playerIndex >= 0 && playerIndex < respawnPoints.Length)
        {
            return respawnPoints[playerIndex];
        }
        return transform;
    }
}

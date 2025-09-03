using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public int RequiredPlayersToCheckpoint = 4;

    private GameObject _checkPointWall;
    private List<GameObject> _playersOnCheckpoint = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody playerRigidbody = other.attachedRigidbody;

        if (playerRigidbody != null && playerRigidbody.CompareTag("Player"))
        {
            GameObject playerObject = playerRigidbody.gameObject;
            PlayerEventSystem.InvokePlayerCheckpointReached(playerObject);
        }
    }
}

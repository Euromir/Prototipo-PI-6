using UnityEngine;
using UnityEngine.InputSystem;

public class PatrolEnemy : MonoBehaviour
{
    [Header("Configuração da Patrulha")]
    [Tooltip("A velocidade com que o inimigo se move.")]
    public float speed = 2f;

    [Tooltip("Os pontos (Transforms) pelos quais o inimigo irá patrulhar em ordem.")]
    public Transform[] patrolPoints;

    [Header("Configuração de Dano")]
    [Tooltip("O peso exato que o jogador precisa ter para ser eliminado.")]
    public float targetWeight = 0.5f;

    private int _currentPointIndex = 0;
    private PlayerManager _playerManager;

    void Awake()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
    }

    void Update()
    {
        if (patrolPoints.Length == 0)
        {
            return;
        }

        Transform targetPoint = patrolPoints[_currentPointIndex];

        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            _currentPointIndex = (_currentPointIndex + 1) % patrolPoints.Length;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Pista 1: A colisão aconteceu?
        Debug.Log("OnTriggerEnter ativado por: " + other.name);

        PlayerWeight playerWeight = other.GetComponentInParent<PlayerWeight>();

        if (playerWeight != null)
        {
            // Pista 2: Encontramos o componente de peso?
            Debug.Log("Componente PlayerWeight encontrado no objeto " + playerWeight.name);

            // Pista 3: Qual é o peso ATUAL do jogador no momento da colisão?
            Debug.Log("Peso atual do jogador: " + playerWeight.Weight + " | Peso alvo: " + targetWeight);

            if (Mathf.Approximately(playerWeight.Weight, targetWeight))
            {
                // Pista 4: A condição de peso foi atendida?
                Debug.Log("CONDIÇÃO DE PESO ATENDIDA!");

                if (_playerManager != null)
                {
                    PlayerInput playerInput = other.GetComponentInParent<PlayerInput>();
                    if (playerInput != null)
                    {
                        Debug.Log("PlayerManager e PlayerInput encontrados. CHAMANDO RESPAWN!");
                        _playerManager.RespawnPlayer(playerInput);
                    }
                    else
                    {
                        Debug.LogError("FALHA: PlayerInput não foi encontrado no jogador!", other.gameObject);
                    }
                }
                else
                {
                    Debug.LogError("FALHA: PlayerManager não foi encontrado na cena!", this.gameObject);
                }
            }
        }
    }
}
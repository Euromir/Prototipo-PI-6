using UnityEngine;

/// <summary>
/// Aplica um efeito de flutuação (bobbing) e rotação contínua a um objeto.
/// Ideal para itens coletáveis como chaves, moedas e power-ups.
/// </summary>
public class EfeitoFlutuacao : MonoBehaviour
{
    [Header("Configurações de Flutuação")]
    [Tooltip("A altura máxima que o objeto alcançará para cima e para baixo a partir do ponto inicial.")]
    [SerializeField] private float amplitude = 0.25f;

    [Tooltip("A velocidade com que o objeto sobe e desce.")]
    [SerializeField] private float frequencia = 1f;

    [Header("Configurações de Rotação")]
    [Tooltip("A velocidade da rotação em graus por segundo.")]
    [SerializeField] private float velocidadeRotacao = 50f;

    [Tooltip("O eixo de rotação local do objeto. Use (1,0,0), (0,1,0) ou (0,0,1) para testar.")]
    [SerializeField] private Vector3 eixoRotacao = Vector3.right; // << MUDANÇA AQUI

    // Posição inicial do objeto para referência
    private Vector3 posicaoInicial;

    void Start()
    {
        // Salva a posição inicial do objeto.
        posicaoInicial = transform.position;
    }

    void Update()
    {
        // --- Lógica de Flutuação (Bobbing) ---
        float offsetVertical = Mathf.Sin(Time.time * frequencia) * amplitude;
        transform.position = posicaoInicial + new Vector3(0, offsetVertical, 0);

        // --- Lógica de Rotação ---
        // Gira o objeto em torno do 'eixoRotacao' definido, no seu próprio espaço local (Space.Self).
        transform.Rotate(eixoRotacao, velocidadeRotacao * Time.deltaTime, Space.Self);
    }
}
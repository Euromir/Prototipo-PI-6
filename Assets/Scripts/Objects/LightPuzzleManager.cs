using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LightPuzzleManager : MonoBehaviour
{
    [Header("Configuração do Puzzle")]
    [Tooltip("A sequência correta de IDs das tochas.")]
    [SerializeField] private int[] sequenciaCorreta = { 1, 0, 2 };

    [Tooltip("Todas as tochas que fazem parte deste puzzle.")]
    [SerializeField] private LightableTorch[] tochasDoPuzzle;

    [Tooltip("O GameObject que será desativado ao completar o puzzle.")]
    [SerializeField] private GameObject objetoParaDesativar;

    private List<int> sequenciaAtual = new List<int>();

    public void RegistrarTocha(LightableTorch tocha)
    {
        sequenciaAtual.Add(tocha.ID);
        tocha.Acender();

        if (sequenciaAtual.Count == sequenciaCorreta.Length)
        {
            VerificarSequencia();
        }
    }

    private void VerificarSequencia()
    {
        if (sequenciaAtual.SequenceEqual(sequenciaCorreta))
        {
            if (objetoParaDesativar != null)
            {
                objetoParaDesativar.SetActive(false);
            }
        }
        else
        {
            Invoke(nameof(ResetarPuzzle), 0.5f);
        }
    }

    private void ResetarPuzzle()
    {
        sequenciaAtual.Clear();

        foreach (LightableTorch tocha in tochasDoPuzzle)
        {
            tocha.Apagar();
        }
    }
}

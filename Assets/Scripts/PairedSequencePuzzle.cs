using UnityEngine;
using System.Collections.Generic;

public class PairedSequencePuzzle : MonoBehaviour
{
    [Header("1. Conecte as Placas e Indicadores")]
    [Tooltip("Crie um par para cada placa/indicador do puzzle. A ordem aqui não importa.")]
    [SerializeField] private List<PuzzlePair> puzzlePairs;

    [Header("2. Defina a Sequência Correta")]
    [Tooltip("Arraste as PRESSURE PLATES para esta lista NA ORDEM CORRETA da solução.")]
    [SerializeField] private List<PressurePlate> correctSequence;

    [Header("3. Configure os Materiais e Ações")]
    [Tooltip("Material para o indicador no estado INATIVO.")]
    [SerializeField] private Material inactiveMaterial;

    [Tooltip("Material para o indicador no estado ATIVO.")]
    [SerializeField] private Material activeMaterial;

    [Tooltip("Objetos que serão LIGADOS quando o puzzle for resolvido.")]
    [SerializeField] private List<GameObject> objectsToActivate;

    [Tooltip("Objetos que serão DESLIGADOS quando o puzzle for resolvido.")]
    [SerializeField] private List<GameObject> objectsToDeactivate;

    private int _currentStep = 0;

    private void OnEnable()
    {
        foreach (var pair in puzzlePairs)
        {
            if (pair.plate != null) pair.plate.OnStateChanged += OnPlateTriggered;
        }
    }

    private void OnDisable()
    {
        foreach (var pair in puzzlePairs)
        {
            if (pair.plate != null) pair.plate.OnStateChanged -= OnPlateTriggered;
        }
    }

    private void Start()
    {
        ResetPuzzle();
    }

    private void OnPlateTriggered(PressurePlate triggeredPlate)
    {
        if (!triggeredPlate._isActivated) return;

        if (triggeredPlate == correctSequence[_currentStep])
        {
            HandleCorrectStep(triggeredPlate);
        }
        else
        {
            ResetPuzzle();
        }
    }

    private void HandleCorrectStep(PressurePlate correctPlate)
    {
        Renderer indicator = GetIndicatorForPlate(correctPlate);
        if (indicator != null)
        {
            indicator.material = activeMaterial;
        }

        _currentStep++;

        if (_currentStep >= correctSequence.Count)
        {
            SolvePuzzle(true);
        }
    }

    public void ResetPuzzle()
    {
        _currentStep = 0;

        foreach (var pair in puzzlePairs)
        {
            if (pair.indicator != null)
            {
                pair.indicator.material = inactiveMaterial;
            }
        }
        SolvePuzzle(false);
    }

    private void SolvePuzzle(bool isSolved)
    {
        foreach (var obj in objectsToActivate) { if (obj != null) obj.SetActive(isSolved); }
        foreach (var obj in objectsToDeactivate) { if (obj != null) obj.SetActive(!isSolved); }
    }

    private Renderer GetIndicatorForPlate(PressurePlate plateToFind)
    {
        foreach (var pair in puzzlePairs)
        {
            if (pair.plate == plateToFind)
            {
                return pair.indicator;
            }
        }
        return null;
    }
}

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MultiPlatePuzzle : MonoBehaviour
{
    [Header("Plataformas do Puzzle")]
    [Tooltip("Arraste todas as Pressure Plates que fazem parte deste puzzle para esta lista.")]
    [SerializeField] private List<PressurePlate> puzzlePlates;

    [Header("Objetos a serem Controlados")]
    [Tooltip("Objetos que serão LIGADOS quando o puzzle for resolvido.")]
    [SerializeField] private List<GameObject> objectsToActivate;

    [Tooltip("Objetos que serão DESLIGADOS quando o puzzle for resolvido.")]
    [SerializeField] private List<GameObject> objectsToDeactivate;

    private bool _isPuzzleSolved = false;

    private void OnEnable()
    {
        foreach (var plate in puzzlePlates)
        {
            if (plate != null)
            {
                plate.OnStateChanged += CheckPuzzleState;
            }
        }
    }

    private void OnDisable()
    {
        foreach (var plate in puzzlePlates)
        {
            if (plate != null)
            {
                plate.OnStateChanged -= CheckPuzzleState;
            }
        }
    }

    private void Start()
    {
        CheckPuzzleState(null);
    }

    private void CheckPuzzleState(PressurePlate plate)
    {
        if (_isPuzzleSolved) return;

        int activatedPlatesCount = puzzlePlates.Count(p => p != null && p._isActivated);

        if (puzzlePlates.Count == 0) return;

        if (activatedPlatesCount == puzzlePlates.Count)
        {
            if (!_isPuzzleSolved)
            {
                _isPuzzleSolved = true;
                ApplyPuzzleState(true);
            }
        }
        else
        {
            if (_isPuzzleSolved)
            {
                _isPuzzleSolved = false;
                ApplyPuzzleState(false);
            }
        }
    }

    private void ApplyPuzzleState(bool solved)
    {
        foreach (var obj in objectsToActivate)
        {
            if (obj != null)
            {
                obj.SetActive(solved);
            }
        }

        foreach (var obj in objectsToDeactivate)
        {
            if (obj != null)
            {
                obj.SetActive(!solved);
            }
        }
    }
}

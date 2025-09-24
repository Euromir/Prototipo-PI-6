using System.Collections.Generic;
using UnityEngine;

public class MultiStepsPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject Door;

    public List<PressurePlate> Plates = new List<PressurePlate>();
    public List<LightableTorch> Torches = new List<LightableTorch>();

    private void OnEnable()
    {
        foreach (PressurePlate plate in Plates)
        {
            if (plate != null)
            {
                plate.OnStateChanged += HandlePuzzleStateChanged;
            }
        }

        foreach (LightableTorch torch in Torches)
        {
            if (torch != null)
            {
                torch.OnStateChanged += HandlePuzzleStateChanged;
            }
        }

        CheckPuzzleConditions();
    }

    private void OnDisable()
    {
        foreach (PressurePlate plate in Plates)
        {
            if (plate != null)
            {
                plate.OnStateChanged -= HandlePuzzleStateChanged;
            }
        }

        foreach (LightableTorch torch in Torches)
        {
            if (torch != null)
            {
                torch.OnStateChanged -= HandlePuzzleStateChanged;
            }
        }
    }

    private void HandlePuzzleStateChanged(object sender)
    {
        CheckPuzzleConditions();
    }

    public void CheckPuzzleConditions()
    {
        foreach (PressurePlate plate in Plates)
        {
            if (plate != null && !plate._isActivated)
            {
                Door.SetActive(true);
                return;
            }
        }

        foreach (LightableTorch torch in Torches)
        {
            if (torch != null && !torch.foiAcesa)
            {
                Door.SetActive(true);
                return;
            }
        }

        Door.SetActive(false);
    }
}

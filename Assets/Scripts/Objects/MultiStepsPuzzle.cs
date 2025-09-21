using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultiStepsPuzzle : MonoBehaviour
{
    public UnityEvent CheckTheDoor;
    [SerializeField] GameObject Door;

    public List<PressurePlate> Plates = new List<PressurePlate>();
    public List<LightableTorch> Torches = new List<LightableTorch>();

    public void OpenTheDoor()
    {
        Debug.Log("Done");
        if (Plates.Count != 0)
        {
            foreach (PressurePlate plate in Plates)
                if (!plate._isActivated)
                {
                    Door.SetActive(true);
                    return;
                }
        }
        if (Torches.Count != 0)
            foreach (LightableTorch torch in Torches)
            {
                if (!torch.foiAcesa)
                {
                    Door.SetActive(true);
                    return;
                }
            }
        Door.SetActive(false);
    }
}

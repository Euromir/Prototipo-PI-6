using System.Collections.Generic;
using UnityEngine;

public class MultiStepsPuzzle : MonoBehaviour
{
    [Header("Objetos Controlados pelo Puzzle")]
    [Tooltip("OPCIONAL: Arraste aqui o objeto que será DESATIVADO ao resolver o puzzle (ex: uma porta que se abre).")]
    [SerializeField] private GameObject objetoParaDesativar;

    [Tooltip("OPCIONAL: Arraste aqui o objeto que será ATIVADO ao resolver o puzzle (ex: uma ponte que surge).")]
    [SerializeField] private GameObject objetoParaAtivar;


    [Header("Condições do Puzzle")]
    public List<PressurePlate> Plates = new List<PressurePlate>();
    public List<LightableTorch> Torches = new List<LightableTorch>();

    private void OnEnable()
    {
        // O restante do código de OnEnable e OnDisable permanece o mesmo
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
        // Primeiro, verifica se todas as condições foram atendidas
        bool puzzleResolvido = true;
        foreach (PressurePlate plate in Plates)
        {
            if (plate != null && !plate._isActivated)
            {
                puzzleResolvido = false;
                break; // Se uma placa falhar, não precisa checar o resto
            }
        }

        if (puzzleResolvido) // Só checa as tochas se as placas estiverem OK
        {
            foreach (LightableTorch torch in Torches)
            {
                if (torch != null && !torch.foiAcesa)
                {
                    puzzleResolvido = false;
                    break; // Se uma tocha falhar, não precisa checar o resto
                }
            }
        }

        // Agora, atualiza os GameObjects com base no estado do puzzle
        if (puzzleResolvido)
        {
            // O PUZZLE FOI RESOLVIDO
            if (objetoParaDesativar != null)
            {
                objetoParaDesativar.SetActive(false);
            }
            if (objetoParaAtivar != null)
            {
                objetoParaAtivar.SetActive(true);
            }
        }
        else
        {
            // O PUZZLE AINDA NÃO FOI RESOLVIDO (ou foi desfeito)
            if (objetoParaDesativar != null)
            {
                objetoParaDesativar.SetActive(true);
            }
            if (objetoParaAtivar != null)
            {
                objetoParaAtivar.SetActive(false);
            }
        }
    }
}

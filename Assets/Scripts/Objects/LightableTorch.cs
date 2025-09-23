using UnityEngine;

public class LightableTorch : MonoBehaviour
{
    [Header("Configuração da Tocha")]
    [Tooltip("ID único para esta tocha (ex: 0, 1, 2).")]
    public int ID;

    [Tooltip("Referência para o Gerenciador do Puzzle.")]
    public LightPuzzleManager gerenciador;

    private GameObject luzDaTocha;
    private MultiStepsPuzzle LockedDoor;
    public bool foiAcesa { get; private set; }

    private void Start()
    {
        Light luzComponent = GetComponentInChildren<Light>(true);

        if (luzComponent != null)
        {
            luzDaTocha = luzComponent.gameObject;
        }


        LockedDoor = GetComponentInParent<MultiStepsPuzzle>();
        Apagar();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player") && gerenciador != null)
        {
            PlayerLightState playerLight = other.transform.root.GetComponent<PlayerLightState>();
            if (playerLight != null && playerLight.isLightOn && !foiAcesa)
            {
                gerenciador.RegistrarTocha(this);
            }
        }

        if (other.transform.root.CompareTag("Player") && gerenciador == null)
        {
            Acender();
        }
    }

    public void Acender()
    {
        if (luzDaTocha != null) luzDaTocha.SetActive(true);
        foiAcesa = true;
        if (LockedDoor != null)
        {
            LockedDoor.CheckTheDoor.Invoke();
        }
    }

    public void Apagar()
    {
        if (luzDaTocha != null) luzDaTocha.SetActive(false);
        foiAcesa = false;
        if (LockedDoor != null)
        {
            LockedDoor.CheckTheDoor.Invoke();
        }
    }
}

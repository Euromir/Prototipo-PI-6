using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Become Platform Ability", menuName = "Abilities/Become Platform")]
public class BecomePlatformAbility : Ability
{
    [Header("Configurações da Plataforma")]
    public float Duration = 5f;
    public GameObject PlatformPrefab; // O visual da plataforma

    public override void Activate(GameObject caster)
    {
        AbilityHolder holder = caster.GetComponent<AbilityHolder>();
        if (holder != null)
        {
            // Pede para o AbilityHolder iniciar a Coroutine
            holder.StartAbilityCoroutine(PlatformRoutine(caster));
        }
    }

    private IEnumerator PlatformRoutine(GameObject caster)
    {
        // Desativa os componentes do jogador
        caster.GetComponent<PlayerMovement>().enabled = false;
        // ... desative outros componentes como MeshRenderer, etc.

        // Cria a plataforma
        GameObject platformInstance = Instantiate(PlatformPrefab, caster.transform.position, caster.transform.rotation);
        
        Debug.Log("Jogador virou uma plataforma!");

        // Espera pela duração definida
        yield return new WaitForSeconds(Duration);

        // Destrói a plataforma
        Destroy(platformInstance);

        // Reativa os componentes do jogador
        caster.GetComponent<PlayerMovement>().enabled = true;
        // ... reative outros componentes
        
        Debug.Log("Jogador voltou ao normal!");
    }
}

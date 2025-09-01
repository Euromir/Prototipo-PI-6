using System.Collections;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    [Tooltip("Array com 6 posições para as habilidades (slots 1 a 6)")]
    public Ability[] abilities = new Ability[6];

    private float[] _cooldowns;

    private void Awake()
    {
        _cooldowns = new float[abilities.Length];
    }

    private void Update()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                if (abilities[i] != null && Time.time >= _cooldowns[i])
                {
                    abilities[i].Activate(this.gameObject);
                    _cooldowns[i] = Time.time + abilities[i].CooldownTime;
                }
                else if (abilities[i] != null)
                {
                    Debug.Log($"Habilidade '{abilities[i].Name}' está em recarga!");
                }
            }
        }
    }
    
    public void StartAbilityCoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }
}

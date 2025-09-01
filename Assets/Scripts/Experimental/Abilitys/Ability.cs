using UnityEngine;

// A classe base abstrata para todas as habilidades
public abstract class Ability : ScriptableObject
{
    [Header("Informações da Habilidade")]
    public string Name;
    public Sprite Icon;
    public float CooldownTime = 1f;

    // O método que cada habilidade específica deverá implementar
    public abstract void Activate(GameObject caster);
}
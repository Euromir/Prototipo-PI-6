using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [Header("Informações da Habilidade")]
    public string Name;
    public Sprite Icon;
    public float CooldownTime = 1f;
    public abstract void Activate(GameObject caster);
}

using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [Header("Informações da Habilidade")]
    public float CooldownTime = 1f;
    public abstract void Activate(GameObject caster);
}

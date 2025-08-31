using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public float JumpForce = 10f;

    public int PlayerID;

    public float Speed = 5f;
}

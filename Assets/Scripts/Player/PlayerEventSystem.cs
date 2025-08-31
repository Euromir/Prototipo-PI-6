using UnityEngine;
using System;

public static class PlayerEventSystem
{
    public static event Action<int, Rigidbody, Vector3> OnPlayerMoved;
    public static void InvokePlayerMoved(int PlayerID, Rigidbody playerRb, Vector3 newPosition) => OnPlayerMoved?.Invoke(PlayerID, playerRb, newPosition);

    public static event Action<int, Rigidbody, Vector3> OnPlayerInteracted;
    public static void InvokePlayerInteracted(int PlayerID, Rigidbody playerRb, Vector3 interactPosition) => OnPlayerInteracted?.Invoke(PlayerID, playerRb, interactPosition);

    public static event Action<int, Rigidbody, float> OnPlayerJump;
    public static void InvokePlayerJump(int PlayerID, Rigidbody playerRb, float jumpForce) => OnPlayerJump?.Invoke(PlayerID, playerRb, jumpForce);
}

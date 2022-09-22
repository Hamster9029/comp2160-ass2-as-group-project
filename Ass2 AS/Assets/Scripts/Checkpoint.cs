using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        // Return the other collider
        GameObject other = collider.gameObject;

        // Check if the collider is player
        if (Layers.Instance.player.Contains(other))
        {
            // Send the checkpoint and time information when the player entered checkpoint
            PlayerThroughCheckpoint(this, Time.time);
        }
    }

    // Method for when player is through the checkpoint
    private void PlayerThroughCheckpoint(Checkpoint checkpoint, float time)
    {
        CheckpointList.Instance.IsPlayerThroughCheckpoint(checkpoint, time);
    }
}

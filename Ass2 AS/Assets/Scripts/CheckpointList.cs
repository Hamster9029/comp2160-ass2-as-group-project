using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointList : MonoBehaviour
{
    static private CheckpointList instance;
    static public CheckpointList Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("There is no Layers instance in the scene.");
            }
            return instance;
        }
    }

    private Transform checkpointGroup;

    private List<Checkpoint> checkpointList;

    private int checkpointNextIndex;
    private int checkpointIndex;

    private bool isActive = true;

    void Awake()
    {
        if (instance != null)
        {
            // destroy duplicates
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        // Get the component this script is assigned to
        checkpointGroup = GetComponent<Transform>();

        // Create new checkpoint list
        checkpointList = new List<Checkpoint>();

        // Increment through all children in this component
        foreach (Transform checkpointTransform in checkpointGroup)
        {
            // Add each checkpoint to the list
            Checkpoint checkpoint = checkpointTransform.GetComponent<Checkpoint>();
            checkpointList.Add(checkpoint);
        }
    }

    void Update()
    {
        // Check if the checkpoint is active and if it's index is indeed in the list
        if (isActive && checkpointNextIndex <= checkpointList.Count)
        {
            // Call the method and apply to the checkpoint in the current index
            CheckpointLight(checkpointList[checkpointNextIndex]);
        }
        else
        {
            // Call the method and apply to the check in the last index
            CheckpointDim(checkpointList[checkpointIndex]);
            checkpointIndex = (checkpointIndex + 1) % checkpointList.Count;
            isActive = true;
        }
    }

    // Method to call if player is through the checkpoint
    public void IsPlayerThroughCheckpoint(Checkpoint checkpoint, float time)
    {
        if (checkpointList.IndexOf(checkpoint) == checkpointNextIndex)
        {
            // Send checkpoint information and time to GameManager
            GameManager.Instance.Checkpoint(checkpoint.transform.name, time);
            checkpointNextIndex = (checkpointNextIndex + 1) % checkpointList.Count;
            isActive = false;
            // If the checkpointNextIndex is 0 then the player as entered through all checkpoints and call the GameManager to log GameOver
            if (checkpointNextIndex == 0)
            {
                GameManager.Instance.GameOver(true);
            }
        }
    }

    // Turn on the emission and change color to green when method is called
    private void CheckpointLight(Checkpoint checkpoint)
    {
        checkpoint.GetComponentInChildren<Renderer>().material.EnableKeyword("_EMISSION");
        checkpoint.GetComponentInChildren<Renderer>().material.SetColor("_EmissionColor", Color.green);
    }

    // Turn off the emission when method is called
    private void CheckpointDim(Checkpoint checkpoint)
    {
        checkpoint.GetComponentInChildren<Renderer>().material.DisableKeyword("_EMISSION");
    }
}

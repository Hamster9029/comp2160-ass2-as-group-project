using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour
{
    // Singleton
    public PlayerHealth player;
    static private GameManager instance;
    static public GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("There is no GameManager instance in the scene.");
            }
            return instance;
        }
    }

    private float playerHealth;
    public float amountWithNoDamage = 5f;
    public float healAmount = 10f;

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

    // Start is called before the first frame update
    void Start()
    {
        AnalyticsEvent.GameStart();
        playerHealth = player.maxHealth;
    }

    public float getHealth
    {
        get
        {
            return playerHealth;
        }
    }

    public void PlayerTakeDamage(float damage)
    {
        if (damage >= amountWithNoDamage)
        {
            playerHealth -= damage;
        }
    }
    public void HealPlayer()
    {
        if (playerHealth < player.maxHealth)
        {
            playerHealth += healAmount;
        }
    }

    public void Checkpoint(string checkpoint, float time)
    {
        Debug.Log(checkpoint);
        Debug.Log(time);
        Analytics.CustomEvent("checkpoint", new Dictionary<string, object>
        {
            { "time", Time.time },
            { "playerHealth", playerHealth },
        });
        HealPlayer();
        UIManager.Instance.CheckpointTime(checkpoint, time);
    }

    public void GameOver(bool win)
    {
        //Implement UI Manager show win 
        AnalyticsEvent.GameOver();
        UIManager.Instance.ShowGameOver(win);
        Time.timeScale = 0;
        Analytics.CustomEvent("death", new Dictionary<string, object>
        {
            { "time", Time.time },
            { "playerPosition", player.transform.position },
            { "collidedObject", player.collisionObjectName },
        });
    }
}

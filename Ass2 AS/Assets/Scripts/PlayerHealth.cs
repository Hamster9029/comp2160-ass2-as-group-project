using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float maxHealth = 100f;
    private float health;
    private float halfHealth = 50f;
    public GameManager gameManager;
    public ParticleSystem smoke;
    public ParticleSystem explosion;
    public string collisionObjectName;

    // Start is called before the first frame update
    void Start()
    {
        smoke.Stop();
        explosion.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        health = gameManager.getHealth;
        if (health <= halfHealth)
        {
            if (!smoke.isPlaying)
            {
                smoke.Play();
            }
        }
        else
        {
            smoke.Stop();
        }
        if (health <= 0)
        {
            if (!explosion.isPlaying)
            {
                explosion.Play();
                Debug.Log(!explosion.isPlaying);
            }
            Destroy(gameObject);
            GameManager.Instance.GameOver(false);
        }
        else
        {
            explosion.Stop();
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        collisionObjectName = collision.transform.name;
        GameObject other = collision.gameObject;
        //Player takes damage when colliding with objects that is not a checkpoint and a ground layer
        if (!Layers.Instance.checkpoint.Contains(other) && !Layers.Instance.ground.Contains(other))
        {
            GameManager.Instance.PlayerTakeDamage(collision.impulse.magnitude / 100);
        }
        else
        {

        }
    }
}

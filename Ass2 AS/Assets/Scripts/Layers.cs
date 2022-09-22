using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layers : MonoBehaviour
{
    static private Layers instance;
    static public Layers Instance
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

    // layers which can be assigned and called
    public LayerMask player;
    public LayerMask ground;
    public LayerMask checkpoint;

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
}

using System;
using UnityEngine;

public class StopBirdSpawningEvent : MonoBehaviour
{
    public static StopBirdSpawningEvent Instance;

    public static event Action OnPlayerCross;


    private void Awake()
    {
        Instance = this;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 0)
        {
            OnPlayerCross();
        }
    }
}

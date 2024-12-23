using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBird : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += StopBirds;
        GameManager.OnGameStateChanged += DestroyBirds;
    }

    private void DestroyBirds(GameManager.GameState state)
    {
        if (state == GameManager.GameState.respawn || state == GameManager.GameState.finish)
        {
            Destroy(gameObject);
        }
    }

    private void StopBirds(GameManager.GameState state)
    {
        if (state == GameManager.GameState.restart || state == GameManager.GameState.gamepaused) ;
        {
            rigidbody2D.bodyType = RigidbodyType2D.Static;
        }
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= StopBirds;
        GameManager.OnGameStateChanged -= DestroyBirds;
    }
}

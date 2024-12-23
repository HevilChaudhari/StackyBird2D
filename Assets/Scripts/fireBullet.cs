using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class fireBullet : MonoBehaviour
{
    public static fireBullet Instance;

    public static event Action OnBonusLevel;

    [SerializeField] private GameObject bulletprefab;
    [SerializeField] private Transform bulletSpawnPoint;
    private float bulletSpeed = 50f;
    private float nextFireTime;
    private float fireRate = 8f;
    private bool isFiring;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        
        GameManager.OnGameStateChanged += StartFiring;
    }

    private void StartFiring(GameManager.GameState state)
    {
        isFiring = state == GameManager.GameState.playing;
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("SetFireRate", .5f);
       
    }

    private void SetFireRate()
    {
        if (isFiring && Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + 1f / fireRate; // Calculate next allowed fire time
            OnBonusLevel?.Invoke();
        }
    }



    private void FireBullet()
    {
        GameObject block = Instantiate(bulletprefab, bulletSpawnPoint.position, Quaternion.identity);
        block.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.right * bulletSpeed;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= StartFiring;
    }
}

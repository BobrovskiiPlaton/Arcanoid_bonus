using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallScript : MonoBehaviour
{
    public Vector2 ballInitialForce;
    public float minSpeed = 3f; // Минимальная скорость
    Rigidbody2D rb;
    GameObject playerObj;
    float deltaX;
    private AudioSource audioSrc;
    public AudioClip hitSound;
    public AudioClip loseSound;
    public GameDataScript gameData;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        deltaX = transform.position.x;
        audioSrc = Camera.main.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (rb.isKinematic)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                rb.isKinematic = false;
                rb.AddForce(ballInitialForce);
            }
            else
            {
                var pos = transform.position;
                pos.x = playerObj.transform.position.x + deltaX;
                transform.position = pos;
            }
        }
        else
        {
            // Проверяем минимальную скорость
            CheckMinSpeed();
            
            if (Input.GetKeyDown(KeyCode.J))
            {
                var v = rb.velocity;
                if (Random.Range(0, 2) == 0)
                    v.Set(v.x - 0.1f, v.y + 0.1f);
                else
                    v.Set(v.x + 0.1f, v.y - 0.1f);
                rb.velocity = v;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(gameData.sound)
            audioSrc.PlayOneShot(loseSound, 5);
        Destroy(gameObject);
        playerObj.GetComponent<PlayerScript>().BallDestroyed();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameData.sound)
            audioSrc.PlayOneShot(hitSound, 5);
        
        // Проверяем скорость после столкновения
        CheckMinSpeed();
    }

    // Метод для проверки минимальной скорости
    private void CheckMinSpeed()
    {
        if (rb.isKinematic || rb.velocity.magnitude == 0)
            return;
            
        float currentSpeed = rb.velocity.magnitude;
        
        if (currentSpeed < minSpeed)
        {
            // Нормализуем текущее направление и умножаем на минимальную скорость
            Vector2 newVelocity = rb.velocity.normalized * minSpeed;
            rb.velocity = newVelocity;
        }
    }

    // Альтернативный вариант: фиксированная проверка в FixedUpdate
    private void FixedUpdate()
    {
        CheckMinSpeed();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    Renderer materialRenderer;
    [SerializeField] int invincibilityLifetime = 4;
    const int playerLayer = 7;
    const int enemyLayer = 9; 

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    private void Start()
    {
        materialRenderer = GetComponent<Renderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent(out EnemyState enemyState))
            {
                enemyState.DestroyEnemy();
            }
            LifeManager.instance.LifeLostEvent();
        }
    }

    public void Respawn()
    {
        StartCoroutine(Invincibility());
        ResetPosition();
    }

    public void ResetPosition()
    {
        transform.position = Vector2.zero;
    }

    IEnumerator Invincibility()
    {
        float flickerRate = .4f;
        for (int i = 0; i < invincibilityLifetime; i++)
        {
            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer);
            yield return new WaitForSeconds(flickerRate);
            materialRenderer.enabled = false;
            yield return new WaitForSeconds(flickerRate);
            materialRenderer.enabled = true;
            flickerRate /= 1.5f;
        }
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        yield break;
    }
}

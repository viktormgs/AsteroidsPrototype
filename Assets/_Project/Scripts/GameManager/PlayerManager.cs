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
        transform.position = Vector2.zero;
        StartCoroutine(Invincibility());
    }

    IEnumerator Invincibility()
    {
        for (int i = 0; i < invincibilityLifetime; i++)
        {
            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer);
            materialRenderer.enabled = false;
            yield return new WaitForSeconds(.3f);
            materialRenderer.enabled = true;
            yield return new WaitForSeconds(.3f);
        }
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        yield break;
    }
}

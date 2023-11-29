using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [SerializeField] BoxCollider2D boxCollider;
    Renderer materialRenderer;
    [SerializeField] int invincibilityLifetime = 4;


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
            other.gameObject.SetActive(false);
            //if (other.TryGetComponent(out EnemyState enemyState))
            //{
            //    gameObject.SetActive(false);
            //    enemyState.DestroyEnemy();
            //}

            LifeManager.instance.LifeLostEvent();
            Debug.Log("Player collided with enemy");
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
            boxCollider.enabled = false;
            materialRenderer.enabled = false;
            yield return new WaitForSeconds(.3f);
            materialRenderer.enabled = true;
            yield return new WaitForSeconds(.3f);
        }
        boxCollider.enabled = true;
        yield break;
    }
}

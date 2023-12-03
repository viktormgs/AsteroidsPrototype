using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileState : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyState enemyState))
        {
            gameObject.SetActive(false);
            enemyState.DestroyEnemy();
            ScoreManager.instance.EnemyIsDestroyedEvent();
        }
    }
}

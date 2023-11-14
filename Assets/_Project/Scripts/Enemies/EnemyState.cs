using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    [SerializeField] float randomSpeed;
    int randomOrientation;
    Vector3 randomRotation = new();

    private void Start()
    {
        randomSpeed = Random.Range(0, randomSpeed);
        randomOrientation = Random.Range(0, 1) * 2 - 1; 
        randomRotation = new Vector3(0, 0, randomOrientation);
    }

    void FixedUpdate() => Rotation();

    void Rotation() => transform.Rotate(randomRotation, randomSpeed * Time.deltaTime);
}

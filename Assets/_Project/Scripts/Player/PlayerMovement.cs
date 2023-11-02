using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float movementSpeed;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        var playerPosition = new Vector2( PlayerReadInput.horizontalInput, PlayerReadInput.verticalInput);
        rb.AddForce(movementSpeed * Time.deltaTime * playerPosition);

    }
}

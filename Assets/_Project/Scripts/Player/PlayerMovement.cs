using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float movementSpeed;
    [SerializeField] float rotateSpeed;

    void Start() => rb = gameObject.GetComponent<Rigidbody2D>();
    void FixedUpdate() => Movement();
    void Movement()
    {
        var playerPosition = new Vector2(PlayerReadInput.horizontalInput, PlayerReadInput.verticalInput);
        rb.AddForce(movementSpeed * playerPosition);

        if (playerPosition != Vector2.zero) //Rotate player towards direction
        {
            var toRotation = Quaternion.LookRotation(transform.forward, playerPosition);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
    }
}

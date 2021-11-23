using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public float maxVelocity;
    public float acceleration;
    public float jumpStrength;
    public float speedLoss;
    public Rigidbody2D body;
    public Collider2D playerCollider;

    private bool isJumping = false;

    private Vector2 directionalInput;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movement = directionalInput.normalized * acceleration * Time.fixedDeltaTime;
        MovePlayer(movement);
    }

    public void OnJumpPressed()
    {
        if (!isJumping)
        {
            isJumping = true;
            body.AddForce(jumpStrength * Vector2.up);
        }
    }

    public void OnJumpReleased()
    {
        
    }

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    private void MovePlayer(Vector2 movement)
    {
        if (Mathf.Abs(body.velocity.x) * acceleration <= maxVelocity)
        {
            body.AddForce(movement);            
        }

        if (movement.x == 0 && Mathf.Abs(body.velocity.x) > 0)
        {
            body.AddForce(body.velocity.normalized * -1 * speedLoss * Time.fixedDeltaTime);
        }
    }
}

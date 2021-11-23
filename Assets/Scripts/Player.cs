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
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movement = directionalInput * acceleration * Time.fixedDeltaTime;
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
        if (body.velocity.x * acceleration <= maxVelocity)
        {
            body.AddForce(movement);            
        }
    }
}

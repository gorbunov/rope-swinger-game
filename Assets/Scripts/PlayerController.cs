using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private const float JumpStrength = 200f;
    private const float RunStrength = 200f;
    private Vector2 directionalInput;
    private Rigidbody2D player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    public void doJump()
    {
        player.AddForce(Vector2.up * JumpStrength);
    }

    public void FixedUpdate()
    {
        player.AddForce(directionalInput * RunStrength);
    }
}
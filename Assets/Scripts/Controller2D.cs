using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private const float JumpStrength = 200f;
    private const float RunStrength = .2f;
    private Vector2 directionalInput;
    public Rigidbody2D player;
    public Collider2D playerCollider;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

}
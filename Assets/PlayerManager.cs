using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerInput playerInput;
    public PlayerMovement playerMovement;
    public PlayerCollision playerCollision;

    internal Rigidbody rb;

    [SerializeField]
    internal Transform visual;
    [SerializeField]
    internal Transform opponent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] float runSpeed = 100f;
    Rigidbody2D myrigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal"); // the value is between -1 to 1
        Debug.Log(controlThrow);
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed , myrigidbody.velocity.y);
        myrigidbody.velocity = playerVelocity;
    }
}

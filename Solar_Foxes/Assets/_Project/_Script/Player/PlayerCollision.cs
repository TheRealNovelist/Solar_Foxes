using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Star"))
        {
            Debug.Log("Collided " + collision);
            collision.gameObject.SetActive(false);
        }
    }
}

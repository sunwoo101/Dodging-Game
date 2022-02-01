using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    #region Collision

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnCollision(GameObject collider)
    {
        // Makes sure the coin isnt in the ground
        if (collider.CompareTag("Coin"))
        {
            Destroy(collider.gameObject);
        }
    }

    #endregion
}

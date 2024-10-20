using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePlanetController : MonoBehaviour
{
    // Putting the responsilility of the collision to the Depot collider because the
    // player can also collide with the Planets and Trash
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
            return;

        // Controller is in the parent of the collider GameObject
        PlayerController player = collision.GetComponentInParent<PlayerController>();
        if (player == null)
            return;

        player.DepositTrash();
    }
}

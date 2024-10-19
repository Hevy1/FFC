using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour
{

    private void Awake()
    {
        if (GetComponent<Collider2D>() == null)
            Debug.LogWarning("Trash collider is null");
    }

    // Putting the responsilility of the collision to the Trash object because the
    // player can also collide with the Planets
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
            return;

        // Controller is in the parent of the collider GameObject
        PlayerController player = collision.GetComponentInParent<PlayerController>();
        if (player == null)
            return;

        player.InteractWithTrash(this);
    }
}

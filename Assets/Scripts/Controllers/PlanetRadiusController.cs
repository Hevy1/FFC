using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRadiusController : MonoBehaviour
{
    [SerializeField] private PlanetController _parent = null;

    private void Awake()
    {
        if (GetComponent<Collider2D>() == null)
            Debug.LogWarning("PlanetRadius collider is null");

        if (_parent == null)
            Debug.LogWarning("Planet radius missing reference to PlanetController");
    }

    // Putting the responsilility of the collision to the Planet because the
    // player can also collide with Trash objects
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
            return;

        // Controller is in the parent of the collider GameObject
        PlayerController player = collision.GetComponentInParent<PlayerController>();
        if (player == null)
            return;

        player.AddNearPlanet(_parent);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == null)
            return;

        // Controller is in the parent of the collider GameObject
        PlayerController player = collision.GetComponentInParent<PlayerController>();
        if (player == null)
            return;

        player.RemoveNearPlanet(_parent);
    }
}

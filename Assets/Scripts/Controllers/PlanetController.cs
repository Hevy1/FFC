using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField] private float _planetWeight = 1.0f;
    [SerializeField] private float _planetRadius = 1.0f;
    [SerializeField] private float _planetColliderRadius = 5.0f;

    // Accessors
    public float GetWeight()
    {
        return _planetWeight;
    }

    private void Awake()
    {
        transform.localScale = Vector3.one * _planetRadius;

        if (GetComponent<Collider2D>() == null)
            Debug.LogWarning("Planet collider is null");
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

        player.CollideWithPlanet(this);
    }
}

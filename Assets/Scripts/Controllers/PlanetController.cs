using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{

    [SerializeField] private List<Sprite> _sprites;

    private SpriteRenderer _renderer = null;

    [SerializeField] private float _planetWeight = 1.0f;

    // Accessors
    public float GetWeight()
    {
        return _planetWeight;
    }

    private void Awake()
    {
        if (GetComponent<Collider2D>() == null)
            Debug.LogWarning("Planet collider is null");

        // At awake, choose a sprite randomly
        _renderer = GetComponent<SpriteRenderer>();
        if (_renderer != null)
            _renderer.sprite = RandomSpritePicker(_sprites);
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

        player.PlayerDeath();
    }


    private Sprite RandomSpritePicker(List<Sprite> sprites)
    {
        // Ensure the list is not empty to avoid errors
        if (sprites == null || sprites.Count == 0)
        {
            Debug.LogError("The sprite list is empty!");
            return null;
        }

        // Pick a random index between 0 and the length of the list
        int randomIndex = Random.Range(0, sprites.Count);

        // Return the sprite at the random index
        return sprites[randomIndex];
    }


}

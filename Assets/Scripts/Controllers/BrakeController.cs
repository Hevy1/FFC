using UnityEngine;

public class BrakeController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer = null;  // Reference to the SpriteRenderer component

    [SerializeField] public Sprite normalSprite;             // Sprite for when the brake is not applied
    [SerializeField] public Sprite brakeSprite;              // Sprite for when the brake is applied (down button pressed)

    void Awake()
    {
        // Get the SpriteRenderer component attached to this GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Check if the SpriteRenderer is assigned
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on this GameObject.");
        }

        // Ensure both sprites are assigned in the Inspector
        if (normalSprite == null || brakeSprite == null)
        {
            Debug.LogError("Please assign both normalSprite and brakeSprite in the Inspector.");
        }

        // Set the initial sprite to the normal one
        spriteRenderer.sprite = normalSprite;
    }

    void Update()
    {
        // Check if the down button (S key or Down arrow) is pressed
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            // Change to the brake sprite
            spriteRenderer.sprite = brakeSprite;
        }
        else
        {
            // Change back to the normal sprite
            spriteRenderer.sprite = normalSprite;
        }
    }
}

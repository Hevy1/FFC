using UnityEngine;

public class PropellerSpeedController : MonoBehaviour
{
    private SpriteRenderer _propellerRenderer =null ; // Reference to the propeller's SpriteRenderer
    [SerializeField] Sprite[] propellerSprites;        // Array to store the 4 propeller sprites (based on speed)
    [SerializeField] private PlayerMovement player = null;                    // Reference to the player script (containing current_speed)

    // Speed thresholds for changing the propeller sprite
    private float speedThreshold1 = 0.005f;
    private float speedThreshold2 = 0.015f;
    private float speedThreshold3 = 0.030f;
    private float speedThreshold4 = 0.050f;

    private void Awake()
    {
        // At awake, choose a sprite randomly
        _propellerRenderer = GetComponent<SpriteRenderer>();
        if (_propellerRenderer != null)
            _propellerRenderer.sprite = propellerSprites[0];
    }


    void Update()
    {
        // Get the current speed from the player's script
        float currentSpeed = player.current_speed.magnitude;

        // Change the propeller sprite based on the current speed
        if (currentSpeed < speedThreshold1)
        {
            // Low speed (use the first sprite)
            _propellerRenderer.sprite = propellerSprites[0];
        }
        else if (currentSpeed < speedThreshold2)
        {
            // Medium-low speed (use the second sprite)
            _propellerRenderer.sprite = propellerSprites[1];
        }
        else if (currentSpeed < speedThreshold3)
        {
            // Medium-high speed (use the third sprite)
            _propellerRenderer.sprite = propellerSprites[2];
        }
        else if (currentSpeed < speedThreshold4)
        {
            // Medium-high speed (use the third sprite)
            _propellerRenderer.sprite = propellerSprites[3];
        }
        else
        {
            // High speed (use the fourth sprite)
            _propellerRenderer.sprite = propellerSprites[4];
        }
    }
}

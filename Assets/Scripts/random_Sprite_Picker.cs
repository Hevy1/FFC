using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_Sprite_Picker : MonoBehaviour
{

    [SerializeField] private List<Sprite> _sprites;
    

    public Sprite Random_Picker(List<Sprite> sprites)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonController : MonoBehaviour
{

    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private int _capacity_max = 4;
    private int _cargo = 0;

    private SpriteRenderer _renderer = null;

    private void Awake()
    {
        DrawSprite();
    }

    public void DrawSprite()
    {
        // Ensure the list is not empty to avoid errors
        if (_sprites == null || _sprites.Count == 0)
        {
            Debug.LogError("The sprite list is empty!");
            return;
        }

        _cargo += 1;
        int index = 3;
        if (_cargo / _capacity_max < 0.33f)
        {
            index = 0;
        }
        else if (_cargo / _capacity_max < 0.66f)
        {
            index = 1;
        }
        else if (_cargo / _capacity_max < 0.99f)
        {
            index = 2;
        }

        _renderer = GetComponent<SpriteRenderer>();
        if (_renderer != null)
            _renderer.sprite = _sprites[index];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField] private float _planetWeight = 1.0f;
    [SerializeField] private float _planetRadius = 1.0f;
    [SerializeField] private float _planetColliderRadius = 5.0f;

    private Collider2D _planetCollider = null;

    // Accessors
    public float GetWeight()
    {
        return _planetWeight;
    }

    private void Awake()
    {
        transform.localScale = Vector3.one * _planetRadius;

        _planetCollider = GetComponent<Collider2D>();
        if (_planetCollider == null)
            Debug.Log("Collider is null");
    }

    private void Update()
    {
        
    }
}

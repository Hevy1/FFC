using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField] private float _planetRadius = 1.0f;

    private void Awake()
    {
        transform.localScale = Vector3.one * _planetRadius;
    }

    private void Update()
    {
        
    }
}

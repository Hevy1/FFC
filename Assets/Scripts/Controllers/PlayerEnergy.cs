using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour
{
    [SerializeField] private float _maxEnergy = 200.0f;
    [SerializeField] private float _deplationRatio = 0.1f;

    private Slider _slider = null;

    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour
{
    [SerializeField] private float _maxEnergy = 200.0f;
    [SerializeField] private float _deplationRatio = 1.0f;
    [SerializeField] private Slider _slider = null;

    [SerializeField] private AudioSource energyAudioSource;

    private float _currentEnergy = 0.0f;

    private void Awake()
    {
        _slider.minValue = 0.0f;
        _slider.maxValue = _maxEnergy;
        _currentEnergy = _maxEnergy;
    }

    public void ResetEnergy()
    {
        _currentEnergy = _maxEnergy;
    }

    // Method returns fals while there is energy left, and true is the energy is depleted
    public bool UpdateEnergy()
    {
        // Taking Time.deltaTime to avoid depending on Update rate
        _currentEnergy -= _deplationRatio * Time.deltaTime;

        if(_currentEnergy < 50 && !energyAudioSource.isPlaying){
            energyAudioSource.Play();
        }

        if(_currentEnergy > 50 && energyAudioSource.isPlaying){
            energyAudioSource.Pause();
        }

        if (_currentEnergy < 0)
        {
            return true;
        }
        else
        {
            if (_slider != null)
                _slider.value = _currentEnergy;

            return false;
        }

    }
}

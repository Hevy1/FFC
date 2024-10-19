using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab = null;
    [SerializeField] private GameObject _cameraGO = null;


    private GameObject _playerInstance = null;
    private PlayerController _player = null;

    private void Awake()
    {
        // For now, player instance is the _playerPrefab,
        // later it should be instantiated manually
        _playerInstance = _playerPrefab;
        // _playerInstance = Instantiate(_playerPrefab);

        if (_playerInstance == null)
        {
            Debug.LogError("Player couldn't be found");
            return;
        }

        _player = _playerInstance.GetComponent<PlayerController>();
        if (_player == null)
        {
            Debug.LogError("PlayerController couldn't be found");
            return;
        }

        _player.PlayerManager = this;


        // TEMP EW : camera is currently set manually, will change with scene changes
        SetCamera(_cameraGO);
    }

    // Public Methods
    public void SetCamera(GameObject cameraGO)
    {
        if (cameraGO == null || _player == null || _player.CameraPosition == null)
            return;

        cameraGO.transform.SetParent(_player.CameraPosition);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPoint = null;
    [SerializeField] private GameObject _player = null;
    [SerializeField] private GameObject _cameraGO = null;

    [SerializeField] private GameObject _trashPrefab = null;
    [SerializeField] private float _respawnDelay = 1.0f;

    private PlayerController _playerController = null;

    private void Awake()
    {
        if (_player == null)
        {
            Debug.LogError("Player couldn't be found");
            return;
        }

        _playerController = _player.GetComponent<PlayerController>();
        if (_playerController == null)
        {
            Debug.LogError("PlayerController couldn't be found");
            return;
        }

        _playerController.PlayerManager = this;
        _playerController.transform.position = _spawnPoint.transform.position;

        // TEMP EW : camera is currently set manually, will change with scene changes
        SetCamera(_cameraGO);
    }

    // Public Methods
    public void SetCamera(GameObject cameraGO)
    {
        if (cameraGO == null || _playerController == null || _playerController.CameraPosition == null)
            return;

        cameraGO.transform.SetParent(_playerController.CameraPosition);
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnPlayerCoroutine());
    }

    public IEnumerator RespawnPlayerCoroutine()
    {
        // Not destroying old player, just creating a trash at player place
        // and teleporting player to spawn
        if (_trashPrefab == null)
        {
            Debug.LogWarning("No Trash prefab found");
            yield break;
        }


        // Waiting a short time to let the player understand they died
        yield return new WaitForSeconds(_respawnDelay);


        Vector3 oldPos = _player.transform.position;
        _player.transform.position = _spawnPoint.transform.position;

        GameObject playerTrash = Instantiate(_trashPrefab);
        if (playerTrash == null)
            yield break;

        playerTrash.transform.position = oldPos;
    }
}

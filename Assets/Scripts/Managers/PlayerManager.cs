using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPoint = null;
    [SerializeField] private GameObject _player = null;
    [SerializeField] private GameObject _cameraGO = null;
    [SerializeField] private GameObject _map = null;

    [SerializeField] private GameObject _trashPrefab = null;
    [SerializeField] private GameObject _wagonPrefab = null;
    [SerializeField] private float _respawnDelay = 1.0f;

    private PlayerController _playerController = null;
    private PlayerMovement _playerMovement = null;
    private Transform _lastTail = null;

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
        _playerMovement = _player.GetComponent<PlayerMovement>();
        if (_playerMovement == null)
        {
            Debug.LogError("PlayerMovement couldn't be found");
            return;
        }

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
        if (_player == null || _playerController == null)
            return;

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
        // Player position is not at the same z value
        oldPos.z = _map.transform.position.z;

        // CanMove was set to true at the moment of the collision
        _playerController.CanMove = true;
        _player.transform.position = _spawnPoint.transform.position;

        Quaternion oldRot = _playerController.ResetRotation(
            _spawnPoint.transform.rotation);

        GameObject playerTrash = Instantiate(_trashPrefab);
        if (playerTrash == null)
            yield break;

        playerTrash.transform.position = oldPos;
        playerTrash.transform.rotation = oldRot;
    }

    public WagonController NewWagon()
    {
        GameObject wagon = Instantiate(_wagonPrefab);
        WagonMovement wagon_move = wagon.GetComponent<WagonMovement>();
        WagonController wagon_control = wagon.GetComponent<WagonController>();
        if (_lastTail == null)
        {
            wagon_move.Following_tail = _playerMovement.GetTail();
        }
        else
        {
            wagon_move.Following_tail = _lastTail;
        }
        wagon_move.Spawn();
        _lastTail = wagon_move.GetTail();
        return wagon_control;
    }
}

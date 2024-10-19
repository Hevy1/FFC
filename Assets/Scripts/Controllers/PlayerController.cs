using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _cameraPosition = null;
    // Référence à l'AudioSource spécifique pour la collision
    [SerializeField] private AudioSource collisionAudioSource;
    [SerializeField] private AudioSource trashAudioSource;


    // Fields
    private PlayerMovement _movement = null;
    private PlayerEnergy _energy = null;
    private List<PlanetController> _nearPlanets = null;

    private bool _canMove = true;

    // Properties
    public PlayerManager PlayerManager { get; set; }
    public Transform CameraPosition { get { return _cameraPosition; } }
    public bool CanMove { set { _canMove = value; } }
    public bool IsMoving { get; set; }

    // Accessors
    public List<PlanetController> GetNearPlanets()
    {
        return _nearPlanets;
    }


    // Unity Interface
    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _energy = GetComponent<PlayerEnergy>();
        _nearPlanets = new List<PlanetController>();
        _canMove = true;
    }

    private void Update()
    {
        if (_movement == null)
            return;

        if (_canMove)
            _movement.UpdateMovement(_nearPlanets);
    }

    public void AddNearPlanet(PlanetController planet)
    {
        if (planet == null || _nearPlanets == null)
            return;

        if (_nearPlanets.Contains(planet))
        {
            Debug.Log("Planet " + planet.name + " is already in the list");
            return;
        }

        _nearPlanets.Add(planet);
    }

    public void RemoveNearPlanet(PlanetController planet)
    {
        if (planet == null || _nearPlanets == null)
            return;

        if (_nearPlanets.Contains(planet))
        {
            _nearPlanets.Remove(planet);
        }
        else
        {
            Debug.Log("Trying to remove " + planet.name
                + " planet that was already removed");
        }
    }

    public void CollideWithPlanet(PlanetController planet)
    {
        _canMove = false;
        PlayerManager.RespawnPlayer();
        _movement.CancelMovement();
        
        // Jouer le son de collision
        if (collisionAudioSource != null)
        {
            collisionAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("Collision AudioSource not assigned in the Inspector.");
        }
    }

    public void InteractWithTrash(TrashController trash)
    {
        // Jouer le son de ramassage de trash
        if (trashAudioSource != null)
        {
            trashAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("Trash AudioSource not assigned in the Inspector.");
        }
        // TODO EW : Put trash in inventory
        return;
    }

    public Quaternion ResetRotation(Quaternion rotation)
        => _movement != null ? _movement.ResetRotation(rotation) : rotation;
}

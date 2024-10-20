using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _cameraPosition = null;
    // Référence à l'AudioSource spécifique pour la collision
    [SerializeField] private AudioSource collisionAudioSource;
    [SerializeField] private AudioSource trashAudioSource;
    [SerializeField] private AudioSource depositAudioSource;


    // Fields
    private PlayerMovement _movement = null;
    private PlayerEnergy _energy = null;
    private List<PlanetController> _nearPlanets = null;
    private WagonController _lastWagon = null;
    private int _score = 0;
    private int _TotalCargo = 0;
    private List<WagonController> _wagonList = null;


    private bool _canMove = true;

    // Properties
    public PlayerManager PlayerManager { get; set; }
    public Transform CameraPosition { get { return _cameraPosition; } }
    public bool CanMove { set { _canMove = value; } }

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
        _wagonList = new List<WagonController>();
        _canMove = true;
    }

    private void Update()
    {
        if (_movement == null)
            return;

        if (_canMove)
            _movement.UpdateMovement(_nearPlanets);

        // Deplete energy only if player is moving
        if (_movement.IsMoving && _energy != null)
        {
            if (_energy.UpdateEnergy())
                PlayerDeath();
        }

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

    public void PlayerDeath()
    {
        _canMove = false;
        PlayerManager.RespawnPlayer();
        _movement.CancelMovement();

        // Parcourir et détruire chaque wagon
        if (_wagonList != null && _wagonList.Count > 0)
        {
            foreach (WagonController wagon in _wagonList)
            {
                if (wagon != null)
                {
                    // Détruire le GameObject associé au wagon
                    Destroy(wagon.gameObject);
                }
            }
            // Vider la liste des wagons
            _wagonList.Clear();
            _lastWagon = null;
            _TotalCargo = 0;
        }

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
        _score += 1;
        _TotalCargo += 1;
        Destroy(trash.gameObject);
        if (_TotalCargo % 4 == 1)
        {
            _lastWagon = PlayerManager.NewWagon();
            _wagonList.Add(_lastWagon);
        }
        else
        {
            _lastWagon.DrawSprite();
        }
        return;
    }

    public void DepositTrash()
    {
        _score += _TotalCargo * 2;

        // Parcourir et détruire chaque wagon
        if (_wagonList != null && _wagonList.Count > 0)
        {
            foreach (WagonController wagon in _wagonList)
            {
                if (wagon != null)
                {
                    // Détruire le GameObject associé au wagon
                    Destroy(wagon.gameObject);
                }
            }
            // Vider la liste des wagons
            _wagonList.Clear();
            _lastWagon = null;
            _TotalCargo = 0;
            if(!depositAudioSource.isPlaying){
                depositAudioSource.Play();
            }
        }

    }

    public Quaternion ResetRotation(Quaternion rotation)
        => _movement != null ? _movement.ResetRotation(rotation) : rotation;

    public void ResetEnergy() => _energy?.ResetEnergy();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _cameraPosition = null;

    // Fields
    private List<PlanetController> _nearPlanets = null;

    // Properties
    public PlayerManager PlayerManager { get; set; }
    public Transform CameraPosition { get { return _cameraPosition; } }

    // Accessors
    public List<PlanetController> GetNearPlanets()
    {
        return _nearPlanets;
    }


    // Unity Interface
    private void Awake()
    {
        _nearPlanets = new List<PlanetController>();
    }

    private void Update()
    {

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

    public void InteractWithTrash(TrashController trash)
    {
        // TODO EW : Put trash in inventory
        return;
    }
}

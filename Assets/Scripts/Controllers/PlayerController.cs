using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Fields
    private List<PlanetController> _nearPlanets = null;

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

    // Event management
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_nearPlanets == null || collision == null)
            return;

        PlanetController pController = collision.GetComponent<PlanetController>();
        if (pController == null)
            return;

        if (_nearPlanets.Contains(pController))
        {
            Debug.Log("Planet " + pController.name + " is already in the list");
            return;
        }

        _nearPlanets.Add(pController);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_nearPlanets == null || collision == null)
            return;

        PlanetController pController = collision.GetComponent<PlanetController>();
        if (pController == null)
            return;

        if (_nearPlanets.Contains(pController))
        {
            _nearPlanets.Remove(pController);
        }
        else
        {
            Debug.Log("Trying to remove " + pController.name
                + " planet that was already removed");
        }
    }
}

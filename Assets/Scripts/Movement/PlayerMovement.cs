using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float gravity_speed = 0.001f;
    [SerializeField] private float translation_speed = 0.005f;
    [SerializeField] private float rotation_speed = 80.0f;

    // Rotation is only on the body GameObject
    [SerializeField] private GameObject _body = null;
    [SerializeField] private float maxSpeed = 0.1f;


    private Vector3 rotation_delta;
    private Vector3 translation_delta;
    private Vector3 current_speed;
    private Quaternion new_rotation;

    public AudioSource movementSound; // Référence à l'AudioSource pour le son de mouvement

    private void Awake()
    {
        if (_body == null)
        {
            Debug.LogWarning("Please fill in 'Body' field");
            return;
        }
    }

    public void CancelMovement()
    {
        current_speed = new Vector3();
        return;
    }

    public void UpdateMovement(List<PlanetController> nearPlanets)
    {
        if (_body == null || nearPlanets == null)
            return;

        bool isMoving = false; // Pour savoir si le joueur est en train de bouger
        rotation_delta = rotation_speed * Time.deltaTime * Vector3.forward;

        // Moving forward considering the current rotation of the body
        translation_delta = translation_speed * Time.deltaTime * _body.transform.up;

        foreach (PlanetController planet in nearPlanets)
        {
            Vector3 planet_position = planet.transform.position;
            float dist_planet = Vector3.Distance(planet_position, transform.position);
            float gravity = gravity_speed * planet.GetWeight() / Mathf.Pow(dist_planet, 2);
            Vector3 direction = (planet_position - transform.position).normalized;
            current_speed += gravity * direction;
        }

        // Getting inputs
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // Rotation is applied to the body only
            new_rotation.eulerAngles = _body.transform.rotation.eulerAngles - rotation_delta;
            _body.transform.rotation = new_rotation;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Rotation is applied to the body only
            new_rotation.eulerAngles = _body.transform.rotation.eulerAngles + rotation_delta;
            _body.transform.rotation = new_rotation;
        }
        if (Input.GetKey(KeyCode.UpArrow)){
			current_speed += translation_delta;
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.DownArrow)){
            // Apply strong braking with Time.deltaTime for frame-rate independence
            float brakeFactor = 0.65f; // Make this smaller for stronger braking (adjust if needed)
            float brakeMultiplier = Mathf.Pow(brakeFactor, Time.deltaTime * 10f); // 10x multiplier for stronger braking
            current_speed *= brakeMultiplier; // Stronger and frame-rate independent braking
            isMoving = true;
        }

        // Clamp the current speed 
            current_speed = Vector3.ClampMagnitude(current_speed, maxSpeed);  // Limit the speed to maxSpeed


        // Si une touche est pressée et le son n'est pas déjà en train de jouer, on joue le son
        if (isMoving && !movementSound.isPlaying)
        {
            movementSound.Play();
        }
        // Si aucune touche n'est pressée et le son est en train de jouer, on met en pause le son
        else if (!isMoving && movementSound.isPlaying)
        {
            movementSound.Pause();
        }

        transform.position += current_speed;
    }
}
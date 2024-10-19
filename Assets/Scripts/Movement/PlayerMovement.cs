using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float gravity_speed = 0.01f;
    [SerializeField] private float translation_speed = 0.05f;
    [SerializeField] private float rotation_speed = 80.0f;

    // Rotation is only on the body GameObject
    [SerializeField] private GameObject _body = null;
    [SerializeField] private Transform _tail = null;

    private Vector3 rotation_delta;
    private Vector3 translation_delta;
    private Vector3 current_speed;
    private Quaternion new_rotation;

    public AudioSource movementSound; // Référence à l'AudioSource pour le son de mouvement

    private void Awake()
    {
        if (_body == null || _tail == null)
        {
            Debug.LogWarning("Please fill in 'Body' and 'Tail' field");
            return;
        }
        SetTailPosition();
        Debug.Log("player_body = "+transform.position);
        Debug.Log("player_tail = "+_tail.position);
    }

    public void CancelMovement()
    {
        current_speed = new Vector3();
        return;
    }

    public void UpdateMovement(List<PlanetController> nearPlanets)
    {
        if (_body == null || _tail == null || nearPlanets == null)
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
			current_speed -= translation_delta;
            isMoving = true; 
        }

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
        SetTailPosition();
    }

    private void SetTailPosition()
    {
        if (_body == null || _tail == null)
            return;

        _tail.SetPositionAndRotation(_body.transform.position - 1.3f * _body.transform.up, _body.transform.rotation);
    }
}
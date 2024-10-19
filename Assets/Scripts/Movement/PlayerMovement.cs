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

    private Vector3 rotation_delta;
    private Vector3 translation_delta;
    private Vector3 current_speed;
    private Quaternion new_rotation;

	private PlayerController _controller = null;
	private List<PlanetController> _nearPlanets = null;

    public AudioSource movementSound; // Référence à l'AudioSource pour le son de mouvement


    private void Awake()
    {
        _controller = GetComponent<PlayerController>();

        if (_body == null)
        {
            Debug.LogWarning("Please fill in 'Body' field");
            return;
        }
    }

    private void Update()
    {   
        bool isMoving = false; // Pour savoir si le joueur est en train de bouger

		if (_controller == null || _body == null)
			return;

		_nearPlanets = _controller.GetNearPlanets();
		if (_nearPlanets == null)
			return;

        rotation_delta = rotation_speed * Time.deltaTime * Vector3.forward;

        // Moving forward considering the current rotation of the body
        translation_delta = translation_speed * Time.deltaTime * _body.transform.up;

		for (int i = 0; i < _nearPlanets.Count; i++)
        {
			Vector3 planet_position = _nearPlanets[i].transform.position;
			float dist_planet = Vector3.Distance(planet_position, transform.position);
			float gravity = gravity_speed * _nearPlanets[i].GetWeight() / Mathf.Pow(dist_planet, 2);
			Vector3 direction = (planet_position - transform.position).normalized;
			current_speed += gravity * direction;
        }

        if (Input.GetKey(KeyCode.RightArrow)){
            // Rotation is applied to the body only
            new_rotation.eulerAngles = _body.transform.rotation.eulerAngles - rotation_delta;
        	_body.transform.rotation = new_rotation;
        }
        if (Input.GetKey(KeyCode.LeftArrow)){
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
		Debug.Log("speed:"+current_speed);
    }
}
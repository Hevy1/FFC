using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOnArrows : MonoBehaviour
{
	// Fields
    [SerializeField] private float gravity_speed = 0.01f;
    [SerializeField] private float translation_speed = 0.05f;
    [SerializeField] private float rotation_speed = 80.0f;

    private Vector3 rotation_delta;
    private Vector3 translation_delta;
    private Vector3 current_speed;
    private Quaternion new_rotation;

	private PlayerController _controller = null;
	private List<PlanetController> _nearPlanets = null;


    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
		if (_controller == null)
			return;

		_nearPlanets = _controller.GetNearPlanets();
		if (_nearPlanets == null)
			return;

        rotation_delta = rotation_speed * Time.deltaTime * Vector3.forward;
        translation_delta = translation_speed * Time.deltaTime * transform.up;

		for (int i = 0; i < _nearPlanets.Count; i++)
        {
			Vector3 planet_position = _nearPlanets[i].transform.position;
			float dist_planet = Vector3.Distance(planet_position, transform.position);
			float gravity = gravity_speed * _nearPlanets[i].GetWeight() / Mathf.Pow(dist_planet, 2);
			Vector3 direction = (planet_position - transform.position).normalized;
			current_speed += gravity * direction;
        }

        if (Input.GetKey(KeyCode.RightArrow)){
            new_rotation.eulerAngles = transform.rotation.eulerAngles - rotation_delta;
        	transform.rotation = new_rotation;
        }
        if (Input.GetKey(KeyCode.LeftArrow)){
            new_rotation.eulerAngles = transform.rotation.eulerAngles + rotation_delta;
        	transform.rotation = new_rotation;
        }
        if (Input.GetKey(KeyCode.UpArrow)){
			current_speed += translation_delta;
        }
        if (Input.GetKey(KeyCode.DownArrow)){
			current_speed -= translation_delta;
        }

		transform.position += current_speed;
		Debug.Log("speed:"+current_speed);
    }
}
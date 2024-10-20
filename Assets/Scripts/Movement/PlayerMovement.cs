using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float gravity_speed = 0.0005f;
    [SerializeField] private float translation_speed = 0.02f;
    [SerializeField] private float rotation_speed = 80.0f;

    // Rotation is only on the body GameObject
    [SerializeField] private GameObject _body = null;
    [SerializeField] private Transform _tail = null;
    [SerializeField] private float maxSpeed = 0.1f;


    private Vector3 rotation_delta;
    private Vector3 translation_delta;
    public Vector3 current_speed;
    private Quaternion new_rotation;

    public bool IsMoving { get; private set; }

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
        IsMoving = false;
        return;
    }

    public void UpdateMovement(List<PlanetController> nearPlanets)
    {
        if (_body == null || _tail == null || nearPlanets == null)
            return;

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
            IsMoving = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow)){
            // Apply strong braking with Time.deltaTime for frame-rate independence
            float brakeFactor = 0.65f; // Make this smaller for stronger braking (adjust if needed)
            float brakeMultiplier = Mathf.Pow(brakeFactor, Time.deltaTime * 10f); // 10x multiplier for stronger braking
            current_speed *= brakeMultiplier; // Stronger and frame-rate independent braking
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }

        // Clamp the current speed 
        current_speed = Vector3.ClampMagnitude(current_speed, maxSpeed);  // Limit the speed to maxSpeed
        current_speed.z = 0;

        // Si une touche est pressée et le son n'est pas déjà en train de jouer, on joue le son
        if (IsMoving && !movementSound.isPlaying)
        {
            movementSound.Play();
        }
        // Si aucune touche n'est pressée et le son est en train de jouer, on met en pause le son
        else if (!IsMoving && movementSound.isPlaying)
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

    public Quaternion ResetRotation(Quaternion rotation)
    {
        Quaternion oldRot = _body.transform.rotation;
        _body.transform.rotation = rotation;
        return oldRot;
    }

    public Transform GetTail()
    {
        return _tail;
    }
}
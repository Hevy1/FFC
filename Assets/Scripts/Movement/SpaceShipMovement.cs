using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOnArrows : MonoBehaviour
{
    [SerializeField] private float translation_speed = 4.0f;
    [SerializeField] private float rotation_speed = 80.0f;
    Vector3 rotation_delta;
    Vector3 translation_delta;
    Quaternion new_rotation;
    public GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotation_delta = rotation_speed * Time.deltaTime * Vector3.forward;
        translation_delta = translation_speed * Time.deltaTime * transform.up;

        if (Input.GetKey(KeyCode.RightArrow)){
            new_rotation.eulerAngles = transform.rotation.eulerAngles - rotation_delta;
        	transform.rotation = new_rotation;
        }
        if (Input.GetKey(KeyCode.LeftArrow)){
            new_rotation.eulerAngles = transform.rotation.eulerAngles + rotation_delta;
        	transform.rotation = new_rotation;
        }
        if (Input.GetKey(KeyCode.UpArrow)){
            transform.position += translation_delta;
        }
        if (Input.GetKey(KeyCode.DownArrow)){
            transform.position -= translation_delta;
        }
    }
}
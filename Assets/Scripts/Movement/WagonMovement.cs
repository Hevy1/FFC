using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonMovement : MonoBehaviour
{
    [SerializeField] private Transform _head = null;
    [SerializeField] private Transform _own_tail = null;
    [SerializeField] private float _min_dist = 0.1f;
    [SerializeField] private float _max_dist = 1.3f;

    private Vector3 translation_delta;

    public Transform Following_tail {get; set;}
    
    public void Awake() {}

    public void Spawn()
    {
        if (Following_tail == null || _head == null || _own_tail == null)
            return;

        SetPosition();
        SetHeadPosition();
        SetTailPosition();
    }

    public Transform GetTail()
    {
        return _own_tail;
    }

    private void Update()
    {
        if (Following_tail == null || _head == null || _own_tail == null)
            return;
        
        float dprim = Vector3.Distance(Following_tail.position, _head.transform.position);
        if (dprim > _max_dist) {
            Vector3 tail_direction = (Following_tail.position - _head.transform.position).normalized;
            float alpha = Vector3.Angle(Following_tail.up, tail_direction);
            // if (alpha > 90)
            //     alpha = 180 - alpha;
            Debug.Log("angle = "+alpha);
            float beta = Vector3.Angle(transform.up, tail_direction);
            // if (beta > 90)
            //     beta = 180 - beta;
            translation_delta = (dprim - _max_dist) * tail_direction;
            _head.transform.position += translation_delta;
            _head.RotateAround(Following_tail.position, Vector3.back, alpha * Time.deltaTime);

            transform.position = _head.position - 1.3f * _head.up;
            transform.RotateAround(_head.transform.position, Vector3.back, beta * Time.deltaTime);
            SetHeadPosition();
            SetTailPosition();
        }
        // if (dprim > _max_dist) {
        //     Vector3 tail_direction = (Following_tail.position - _head.transform.position).normalized;
        //     float alpha = Vector3.Angle(Following_tail.up, tail_direction) * Mathf.Deg2Rad;
        //     float alpha_deg = Vector3.Angle(Following_tail.up, tail_direction);
        //     Vector3 tail_orthogonal = Vector3.Cross(tail_direction, Following_tail.forward);
        //     translation_delta = (dprim - _max_dist*Mathf.Cos(alpha/2)) * tail_direction;
        //     translation_delta -= _max_dist * Mathf.Sin(alpha/2) * tail_orthogonal;
        //     translation_delta = (dprim - _max_dist) * tail_direction;
            
        //     //Debug.Log("dprim = "+dprim);
        //     //Debug.Log("max = "+_max_dist);
        //     //Debug.Log("tail_direction = "+tail_direction);
        //     Debug.Log("alpha = "+alpha_deg);
        //     //Debug.Log("tail_orthogonal = "+tail_orthogonal);
        //     //Debug.Log("delta = "+translation_delta);

        //     transform.position += translation_delta;
        //     transform.RotateAround(Following_tail.position, Vector3.back, alpha_deg * Time.deltaTime);
        //     transform.RotateAround(transform.position + 1.3f * transform.up, Vector3.back, alpha_deg * Time.deltaTime);
        //     SetHeadPosition();
        //     SetTailPosition();
        // }
    }
    
    private void SetPosition()
    {
        transform.position = Following_tail.transform.position - 1.6f * Following_tail.up;
        transform.rotation = Following_tail.rotation;
    }
    
    
    private void SetHeadPosition()
    {
        _head.SetPositionAndRotation(transform.position + 1.3f * transform.up, transform.rotation);
    }
    private void SetTailPosition()
    {
        _own_tail.SetPositionAndRotation(transform.position - 1.3f * transform.up, transform.rotation);
    }
}
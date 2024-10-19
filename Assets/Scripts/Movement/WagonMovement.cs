using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonMovement : MonoBehaviour
{
    [SerializeField] private Transform _following_tail = null;
    [SerializeField] private Transform _head = null;
    [SerializeField] private Transform _own_tail = null;
    [SerializeField] private float _min_dist = 0.1f;
    [SerializeField] private float _max_dist = 1.3f;

    private Vector3 rotation_delta;
    private Vector3 translation_delta;

    private void Awake()
    {
        if (_following_tail == null || _head == null || _own_tail == null)
            return;

        SetHeadPosition();
        SetBodyPosition();
        SetTailPosition();
    }

    private void Update()
    {
        if (_following_tail == null || _head == null || _own_tail == null)
            return;

        //Debug.Log("tail_pos = "+_following_tail.position);
        //Debug.Log("tail_rot = "+_following_tail.rotation);
        /*
        float dprim = Vector3.Distance(_following_tail.position, _head.transform.position);
        if (dprim > _max_dist) {
            Vector3 tail_direction = (_following_tail.position - _head.transform.position).normalized;
            float alpha = Vector3.Angle(_following_tail.up, tail_direction) * Mathf.Deg2Rad;
            Vector3 tail_orthogonal = Vector3.Cross(tail_direction, _following_tail.forward);
            translation_delta = (dprim - _max_dist*Mathf.Cos(alpha/2)) * tail_direction;
            translation_delta -= _max_dist * Mathf.Sin(alpha/2) * tail_orthogonal;
            translation_delta = (dprim - _max_dist) * tail_direction;
            
            Debug.Log("dprim = "+dprim);
            Debug.Log("max = "+_max_dist);
            Debug.Log("tail_direction = "+tail_direction);
            Debug.Log("alpha = "+alpha);
            Debug.Log("tail_orthogonal = "+tail_orthogonal);
            Debug.Log("delta = "+translation_delta);

            _head.transform.position += translation_delta;
            _head.transform.Rotate(0.0f, 0.0f, Vector3.Angle(transform.up, tail_direction));
            SetBodyPosition();
            SetTailPosition();
        }*/
    }
    
    private void SetHeadPosition()
    {
        _head.SetPositionAndRotation(_following_tail.position + 0.3f * _following_tail.up, _following_tail.rotation);
        Debug.Log("tail = "+_following_tail.position);
        Debug.Log("head = "+_head.position);
    }
    
    private void SetBodyPosition()
    {
        transform.position = _head.transform.position - 1.3f * _head.up;
        transform.rotation = _head.rotation;
    }
    
    private void SetTailPosition()
    {
        _own_tail.SetPositionAndRotation(transform.position - 1.3f * transform.up, transform.rotation);
    }
}
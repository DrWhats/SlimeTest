using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCanvas : MonoBehaviour
{
    [SerializeField] private Transform _mainCameraTransform;


    private void Awake()
    {
        _mainCameraTransform = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
    }

    /*void Update()
    {
        transform.LookAt(transform.position + _mainCameraTransform.rotation * Vector3.forward,
            _mainCameraTransform.rotation * Vector3.up);
    }*/
}

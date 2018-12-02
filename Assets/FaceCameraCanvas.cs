﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCameraCanvas : MonoBehaviour {

    private Camera m_Camera;
    private void Start()
    {
        m_Camera = (Camera)FindObjectOfType(typeof(Camera));
    }
    void Update () {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
    }
}

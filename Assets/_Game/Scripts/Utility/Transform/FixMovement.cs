﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixMovement : MonoBehaviour
{
    public Vector3 direction;

    void Update()
    {
        transform.position = transform.position + direction * Time.deltaTime;
    }
}

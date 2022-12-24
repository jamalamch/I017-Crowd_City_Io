using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeBrid : MonoBehaviour
{
    [SerializeField] float _rbSpeed = 0.5f;
    [SerializeField] float _noiseSteep,_noiseMagh;
    float _noisX, _noisY;
    Vector3 _noiseNormal;

    void Start()
    {
        _noisX = Random.value * 10;
        _noisX = Random.value * 10;
    }

    void Update()
    {
        _noisX += _noiseSteep * Time.deltaTime;
        _noisX += _noiseSteep * Time.deltaTime;
        _noiseNormal.x = Mathf.PerlinNoise(_noisX, _noisY) - 0.5f;
        _noiseNormal.z = Mathf.PerlinNoise(_noisY, _noisX) - 0.5f;

        transform.position = transform.position + _noiseNormal.normalized * _rbSpeed * Time.deltaTime;
    }
}

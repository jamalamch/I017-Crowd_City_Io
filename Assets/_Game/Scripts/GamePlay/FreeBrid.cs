using UnityEngine;

public class FreeBrid : MonoBehaviour
{
    [SerializeField] float _rbSpeed = 0.5f;
    [SerializeField] float _noiseSteep,_noiseMagh;
    float _noisX, _noisY;
    Vector3 _noiseNormal;

    [SerializeField] Collider _collider;
    [SerializeField] MeshRenderer _meshRenderer;
    [SerializeField] Material _freeMatrila, _inCrowdMarial;

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

        Vector3 target = transform.position + _noiseNormal.normalized * _rbSpeed * Time.deltaTime;

        int indexX = (int)(target.x / 50);
        int indexY = (int)(target.z / 50);

        if(!FreeBridSpawner.instance.grid.matrix[indexY*20 + indexX])
        {
            transform.position = target;
        }
    }

    public void Free(bool free)
    {
        enabled = free;
        _collider.enabled = free;
        if (free) 
        {
            tag = "FreeBrid";
            _meshRenderer.sharedMaterial = _freeMatrila;
        }
        else
        {
            tag = "Brid";
            _meshRenderer.sharedMaterial = _inCrowdMarial;
        }
    }
}

using UnityEngine;

public class FreeBrid : MonoBehaviour
{
    [SerializeField] float _rbSpeed = 0.5f;
    [SerializeField] float _noiseSteep,_noiseMagh;
    float _noisX;
    float _deg;

    Vector3 _noiseNormal;

    [SerializeField] Collider _collider;
    [SerializeField] MeshRenderer _meshRenderer;
    [SerializeField] Material _freeMatrila, _inCrowdMarial;

    void Start()
    {
        _noisX = Random.value * 100;
        _deg = Random.value * 2 * Mathf.PI;
    }

    void Update0()
    {
        _noisX += _noiseSteep * Time.deltaTime;
        _deg += (Mathf.PerlinNoise(_noisX, 0) - 0.5f) * _noiseMagh;
        _noiseNormal.x = Mathf.Cos(_deg);
        _noiseNormal.z = Mathf.Sin(_deg);

        Vector3 target = transform.position + _noiseNormal * _rbSpeed * Time.deltaTime;

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

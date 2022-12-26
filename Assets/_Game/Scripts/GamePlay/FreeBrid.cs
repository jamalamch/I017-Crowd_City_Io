using UnityEngine;

public class FreeBrid : MonoBehaviour
{
    [SerializeField] Collider _collider;
    [SerializeField] MeshRenderer _meshRenderer;
    [SerializeField] Material _freeMatrila, _inCrowdMarial;

    public void Free(bool free)
    {
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

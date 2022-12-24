using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour
{
    public bool initOnStart;

    public int count;
    public GPUFlock gPUFlockBrid;
    [SerializeField] SphereCollider _sphereCollider;

    protected void Start()
    {
        if (initOnStart)
            Init();
    }

    public virtual void Init()
    {
        gPUFlockBrid.FirstSpawn(count);
        ResetSphereColliderRaduis();
    }

    public void Collect(FreeBrid freeBrid)
    {
        freeBrid.tag = "Brid";
        GPUBoid gPUBoid = gPUFlockBrid.CreateBoidDataAtPosition(freeBrid.transform.position);
        gPUFlockBrid.AddBoidsGo(gPUBoid, freeBrid.gameObject);
        Destroy(freeBrid);
        ResetSphereColliderRaduis();
    }

    public void StartFight(Crowd crowd)
    {
        
    }

    protected virtual void UpdateSphereTriger()
    {
        _sphereCollider.transform.position = gPUFlockBrid.centreBoids;
    }

    void ResetSphereColliderRaduis() => _sphereCollider.radius = Math.Interpolation(10, 7f, 1000, 36.5f, count);

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "FreeBrid")
        {
            Collect(other.GetComponent<FreeBrid>());
        }
    }

}

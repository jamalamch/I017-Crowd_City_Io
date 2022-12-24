using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Crowd
{
    [SerializeField] float _rbSpeed = 0.5f;

    TouchPad _touchPad;
    bool _canMove = true;

    public override void Init()
    {
        base.Init();
        _touchPad = TouchPad.instance;
    }

    void Update()
    {
        if(_canMove)
            MovementUpdate();

        base.UpdateSphereTriger();
    }

    private void MovementUpdate()
    {
        if (_touchPad.velocityDirection.magnitude > Mathf.Epsilon)
        {
            Vector3 newForward = Quaternion.Euler(0, 45, 0) * _touchPad.velocityDirection;
            transform.forward = Vector3.Lerp(transform.forward, newForward, 0.5f);
            transform.position = transform.position + newForward * _rbSpeed * Time.deltaTime;
        }
    }
}

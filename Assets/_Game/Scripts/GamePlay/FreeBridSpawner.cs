using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeBridSpawner : MonoBehaviour
{
    public static FreeBridSpawner instance;

    [SerializeField] FreeBrid _freeBridGenerator;
    [SerializeField] CityGrid _cityGrid;
    [SerializeField] float _timeToSpwan;
    [SerializeField] int _targetCount;
    [SerializeField] float _spawnRadius;

    float _timer;
    int _count;

    public IPool<FreeBrid> poolBrid { get; set;}
    public CityGrid grid => _cityGrid;
    void Awake()
    {
        instance = this;
        poolBrid = new PoolImpl<FreeBrid>(_freeBridGenerator, 50, this);
    }

    void Update()
    {
        if(_timer < Time.time)
        {
            _timer += _timeToSpwan;
            if (_count < _targetCount)
            {
                _count++;
                FreeBrid freeBrid = poolBrid.Pick();
                var pos = transform.position + Random.insideUnitSphere * _spawnRadius;
                pos.y = 0;
                freeBrid.transform.position = pos;
            }
        }
    }
}

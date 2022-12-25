﻿//
//  Created by jiadong chen    
//  https://www.jiadongchen.com
//
//
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GPUFlock : MonoBehaviour {

    #region Fields

    [SerializeField] private ComputeShader cshader;
    [SerializeField] private List<FreeBrid> boidsGo;
    [SerializeField] private int boidsCount;
    [SerializeField] private float spawnRadius;
    [SerializeField] private float flockSpeed;
    [SerializeField] private float nearbyDis;
    private GPUBoid[] _boidsData;
    private Vector3 _targetPos = Vector3.zero;
    private int _kernelHandle;

    private Vector3 _centre;
    Texture2D gridMapArray;

    [SerializeField] RawImage rawTexture;
    public Vector3 centreBoids => _centre;

    #endregion

    #region Methods

    public void FirstSpawn(int firstCount)
    {
        this.boidsCount = firstCount;
        boidsGo = new List<FreeBrid>(boidsCount);
        _boidsData = new GPUBoid[boidsCount];
        _kernelHandle = cshader.FindKernel("CSMain");

        for (var i = 0; i < boidsCount; i++)
        {
            _boidsData[i] = CreateBoidData();
            FreeBrid freeBrid = FreeBridSpawner.instance.poolBrid.Pick();
            freeBrid.Free(false);
            freeBrid.transform.SetPositionAndRotation(_boidsData[i].pos, Quaternion.Euler(_boidsData[i].rot));
            _boidsData[i].rot = freeBrid.transform.forward;
            boidsGo.Add(freeBrid);
        }
        enabled = true;

        CityGrid cityGrid = FreeBridSpawner.instance.grid;

        gridMapArray = cityGrid.GeTextureArray();
        int rows = cityGrid.matrix.rows;
        int cols = cityGrid.matrix.cols;
        float gridSize = cityGrid.gridSize;

        cshader.SetTexture(_kernelHandle, "grids", gridMapArray);
        cshader.SetInt("rows", rows);
        cshader.SetInt("cols", cols);
        cshader.SetFloat("gridSize", gridSize);

        rawTexture.texture = gridMapArray;
    }

    public void AddBoidsGo(GPUBoid gPUBoid,FreeBrid boidGo)
    {
        boidsGo.Add(boidGo);
        System.Array.Resize(ref _boidsData, _boidsData.Length + 1);
        _boidsData[_boidsData.Length - 1] = gPUBoid;
        boidsCount++;

        cshader.SetFloat("boidsCount", boidsCount);
    }

    private GPUBoid CreateBoidData()
    {
        var pos = transform.position + Random.insideUnitSphere * spawnRadius;
        return CreateBoidDataAtPosition(pos);
    }

    public GPUBoid CreateBoidDataAtPosition(Vector3 pos)
    {
        var boidData = new GPUBoid();
        pos.y = 0f;
        boidData.pos = pos;
        boidData.flockPos = transform.position;
        boidData.nearbyDis = nearbyDis + Random.Range(-0.2f, 0.5f);
        boidData.speed = flockSpeed + Random.Range(-0.5f, 0.5f);
        return boidData;
    }

    private void Update()
    {
        var buffer = new ComputeBuffer(boidsCount, 44);
        for (int i = 0; i < _boidsData.Length; i++)
        {
            _boidsData[i].flockPos = this.transform.position;
        }

        buffer.SetData(_boidsData);

        cshader.SetBuffer(_kernelHandle, "boidBuffer", buffer);
        cshader.SetFloat("deltaTime", Time.deltaTime);
        cshader.SetFloat("boidsCount", boidsCount);
        cshader.Dispatch(_kernelHandle, this.boidsCount, 1, 1);

        buffer.GetData(_boidsData);

        buffer.Release();

        _centre = Vector3.zero;
        for (int i = 0; i < _boidsData.Length; i++)
        {
            _centre += _boidsData[i].pos;
            boidsGo[i].transform.position = _boidsData[i].pos;

            if (!_boidsData[i].rot.Equals(Vector3.zero))
            {
                boidsGo[i].transform.rotation = Quaternion.LookRotation(_boidsData[i].rot);
            }
        }
        _centre /= _boidsData.Length;

        //HashSet<int> toDestroy = new HashSet<int>(); 
        //if(toDestroy.Count > 0)
        //{
        //    int cout = 0;

        //    for (int i = 0; i < _boidsData.Length; i++)
        //    {
        //        if (!toDestroy.Contains(i))
        //        {
        //            _boidsData[cout] = _boidsData[i];
        //            cout++;
        //        }
        //    }

        //    System.Array.Resize(ref _boidsData, _boidsData.Length - toDestroy.Count);
        //    cout = 0;
        //    foreach (var item in toDestroy)
        //    {
        //        Destroy(boidsGo[item + cout]);
        //        boidsGo.RemoveAt(item + cout);
        //        cout--;
        //    }
        //}
    }

    #endregion
}
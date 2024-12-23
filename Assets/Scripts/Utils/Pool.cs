using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[Serializable]
public class Pool<T> where T : Component
{
    private ObjectPool<T> _objectPool;
    private T _objToInstantiate;
    private List<T> _instantiatedObjects = new List<T>();

    [SerializeField] private int _defaultCapacity = 5;
    [SerializeField] private int _maxCapacity = 15;

    public void Initialize(T objToInstantiate)
    {
        _objToInstantiate = objToInstantiate;
        _objectPool = new ObjectPool<T>(OnObjectCreate, OnObjectRequest, OnObjectReturn, OnObjectDestroy, false, _defaultCapacity, _maxCapacity);
    }

    private T OnObjectCreate()
    {
        return GameObject.Instantiate(_objToInstantiate);
    }

    private void OnObjectRequest(T obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnObjectReturn(T obj)
    {
        if (obj == null) return;

        obj.gameObject.SetActive(false);
    }

    private void OnObjectDestroy(T obj)
    {
        GameObject.Destroy(obj);
    }

    private void ReturnObject(T gameObject)
    {
        _objectPool.Release(gameObject);
    }

    public void ResetPool()
    {
        foreach (var obj in _instantiatedObjects)
        {
            ReturnObject(obj);
        }
    }

    public T GetObject()
    {
        var obj = _objectPool.Get();
        _instantiatedObjects.Add(obj);

        return obj;
    }
}
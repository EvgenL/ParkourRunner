﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Assets.Scripts.Pick_Ups;
using Invector.CharacterController;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    #region Singleton

    public static PoolManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    #endregion

    [SerializeField] private float DisableDistance = 2f;
    [SerializeField] private List<GameObject> _pool = new List<GameObject>();

    private Transform _player;

    private void Start()
    {
        StartCoroutine(PoolTick());
        _player = FindObjectOfType<vThirdPersonController>().transform;
    }

    private IEnumerator PoolTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            List<GameObject> itemsToDestroy = new List<GameObject>();
            foreach (var item in _pool)
            {
                if (_player.position.z > item.transform.position.z + DisableDistance)
                {
                    itemsToDestroy.Add(item);
                }
            }

            foreach (var item in itemsToDestroy)
            {
                Remove(item);
            }


        }
    }

    public GameObject Spawn(GameObject original, Vector3 position, Quaternion rotation)
    {
        var obj = Instantiate(original, position, rotation);
        _pool.Add(obj);
        return obj;
    }

    public void Remove(GameObject obj)
    {
        _pool.Remove(obj);
        Destroy(obj);
        //item.SetActive(false);
    }
}

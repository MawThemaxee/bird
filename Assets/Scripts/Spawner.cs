using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _spawnRate = 1f;
    [SerializeField] private float _minHeight = -1f;
    [SerializeField] private float _maxHeight = 1f;
    private void OnEnable() {
        InvokeRepeating(nameof(Spawn), _spawnRate, _spawnRate);
    }
    private void OnDisable() {
        CancelInvoke(nameof(Spawn));
    }
    private void Spawn() {
        GameObject pipes = Instantiate(_prefab, transform.position, Quaternion.identity, transform);
        pipes.transform.position += Vector3.up * Random.Range(_minHeight, _maxHeight);
    }
}

using Ashsvp;
using UnityEngine;

/// <summary>
/// Скрипт управления теневой машиной.
/// </summary>
public class ShadowCar : MonoBehaviour
{
    private Rigidbody _rb;
    private Collider _bodyCollider;
    private LayerMask _layerMask;
    private PlayerRecord[] _records;
    private int _currentRecordIndex;
    private float _startTime;
    private bool _isMoving;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _bodyCollider = GetComponentInChildren<Collider>();
        _layerMask = LayerMask.NameToLayer("ShadowCar");
        _rb.gameObject.layer = _layerMask;
        _bodyCollider.gameObject.layer = _layerMask;
    }

    public void SetPosition(Vector3 position, Quaternion rotation)
    {
        _rb.position = position;
        _rb.rotation = rotation;
    }

    public void SetRecords(PlayerRecord[] records)
    {
        _records = records;
        Debug.Log(records.Length);
    }

    public void StartMove()
    {
        _currentRecordIndex = 0;
        _startTime = Time.time;
        _isMoving = true;
    }

    public void StopMove()
    {
        _isMoving = false;
    }

    private void Update()
    {
        if (!_isMoving)
            return;
        float elapsedTime = Time.time - _startTime;
        while (_currentRecordIndex < _records.Length - 1 && _records[_currentRecordIndex + 1].Timestamp < elapsedTime)
            _currentRecordIndex++;
        var record = _records[_currentRecordIndex];
        _rb.position = record.Position;
        _rb.rotation = record.Rotation;
        if (_currentRecordIndex == _records.Length - 1)
            StopMove();
    }
}
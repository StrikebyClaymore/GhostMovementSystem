using Ashsvp;
using UnityEngine;

/// <summary>
/// Скрипт управления теневой машиной.
/// </summary>
public class ShadowCar : MonoBehaviour, IFixedUpdate
{
    private SimcadeVehicleController _controller;
    private Rigidbody _rb;
    private Collider _bodyCollider;
    private LayerMask _layerMask;
    private InputRecord[] _records;
    private int _currentRecordIndex;
    private bool _isMoving;

    private void Awake()
    {
        _controller = GetComponent<SimcadeVehicleController>();
        _rb = GetComponent<Rigidbody>();
        _bodyCollider = GetComponentInChildren<Collider>();
        _layerMask = LayerMask.NameToLayer("ShadowCar");
        _controller.CanDrive = false;
        _rb.gameObject.layer = _layerMask;
        _bodyCollider.gameObject.layer = _layerMask;
    }

    public void SetPosition(Vector3 position, Quaternion rotation)
    {
        _rb.position = position;
        _rb.rotation = rotation;
    }

    public void SetRecords(InputRecord[] records)
    {
        _records = records;
        Debug.Log(records.Length);
    }

    public void StartMove()
    {
        _currentRecordIndex = 0;
        _isMoving = true;
    }

    private void StopMove()
    {
        _controller.SetInput(new InputRecord(0, 0, 1));
        _isMoving = false;
    }

    public void CustomFixedUpdate()
    {
        if (!_isMoving)
        {
            _controller.CustomFixedUpdate();
            return;
        }
        
        _controller.SetInput(_records[_currentRecordIndex]);
        _controller.CustomFixedUpdate();
        
        _currentRecordIndex++;
        
        if (_currentRecordIndex == _records.Length)
        {
            StopMove();
        }
    }
}
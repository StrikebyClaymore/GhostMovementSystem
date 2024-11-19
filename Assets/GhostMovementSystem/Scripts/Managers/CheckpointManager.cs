using System;
using UnityEngine;

/// <summary>
/// Проверяет прошёл ли игрок все чекпоинты до финиша.
/// </summary>
public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Collider[] _checkpoints;
    private int _checkpointIndex = -1;
    public event Action OnAllCheckpointsReached;

    public void Initialize()
    {
        _checkpointIndex = 0;
        foreach (var checkpoint in _checkpoints)
        {
            checkpoint.enabled = false;
        }
        _checkpoints[_checkpointIndex].enabled = true;
    }
        
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.TryGetComponent<PlayerCar>(out _))
        {
            CheckpointReached();
        }
    }

    private void CheckpointReached()
    {
        if(_checkpointIndex > -1)
            _checkpoints[_checkpointIndex].enabled = false;

        if (_checkpointIndex == _checkpoints.Length - 1)
        {
            OnAllCheckpointsReached?.Invoke();
            return;
        }
        _checkpointIndex++;
        _checkpoints[_checkpointIndex].enabled = true;
    }
}

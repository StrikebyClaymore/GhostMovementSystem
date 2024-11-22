using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Записывает игровые инпуты с привязкой ко времени для последующдей передачи в теневую машину.
/// </summary>
public class PlayerRecorder
{
    private readonly PlayerCar _player;
    private readonly List<PlayerRecord> _records = new List<PlayerRecord>();
    private float _startTime;
    private bool _isRecording;
    
    public PlayerRecorder(PlayerCar player)
    {
        _player = player;
    }

    public void Update()
    {
        if(!_isRecording)
            return;
        _records.Add(new PlayerRecord(_player.transform.position, _player.transform.rotation, Time.time - _startTime));
    }

    public void StartRecording()
    {
        _startTime = Time.time;
        _isRecording = true;
    }
    
    public void StopRecording()
    {
        _isRecording = false;
    }

    public PlayerRecord[] GetRecords()
    {
        return _records.ToArray();
    }
}
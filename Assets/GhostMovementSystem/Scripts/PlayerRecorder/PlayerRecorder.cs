using System.Collections.Generic;

/// <summary>
/// Записывает игровые инпуты с привязкой ко времени для последующдей передачи в теневую машину.
/// </summary>
public class PlayerRecorder : IFixedUpdate
{
    private readonly PlayerCar _player;
    private readonly List<InputRecord> _records = new ();
    private bool _isRecording;
    
    public PlayerRecorder(PlayerCar player)
    {
        _player = player;
    }

    public void CustomFixedUpdate()
    {
        if(!_isRecording)
            return;
        _records.Add(_player.GetInput());
    }
    
    public void StartRecording()
    {
        _isRecording = true;
    }
    
    public void StopRecording()
    {
        _isRecording = false;
    }

    public InputRecord[] GetRecords()
    {
        return _records.ToArray();
    }
}
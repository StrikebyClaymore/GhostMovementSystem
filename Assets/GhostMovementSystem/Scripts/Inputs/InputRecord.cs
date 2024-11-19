/// <summary>
/// Запись игрового ввода для машины.
/// </summary>
public struct InputRecord
{
    public float AccelerationInput;
    public float SteerInput;
    public float BrakeInput;
    
    public InputRecord(float acceleration, float steer, float brake)
    {
        AccelerationInput = acceleration;
        SteerInput = steer;
        BrakeInput = brake;
    }
}
/// <summary>
/// Запись игрового ввода для машины с привязкой ко времени.
/// </summary>
public struct PlayerRecord
{
    public InputRecord InputRecord;
    public float Timestamp;

    public PlayerRecord(InputRecord inputRecord, float timestamp)
    {
        InputRecord = inputRecord;
        Timestamp = timestamp;
    }
}
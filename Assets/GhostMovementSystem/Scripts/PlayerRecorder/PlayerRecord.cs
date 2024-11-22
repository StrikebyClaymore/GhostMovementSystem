using UnityEngine;

/// <summary>
/// Запись игрового ввода для машины с привязкой ко времени.
/// </summary>
public struct PlayerRecord
{
    public Vector3 Position;
    public Quaternion Rotation;
    public float Timestamp;

    public PlayerRecord(Vector3 position, Quaternion rotation, float timestamp)
    {
        Position = position;
        Rotation = rotation;
        Timestamp = timestamp;
    }
}
using Ashsvp;
using Cinemachine;
using UnityEngine;

/// <summary>
/// Скрипт управления машиной игрока.
/// </summary>
public class PlayerCar : MonoBehaviour, IFixedUpdate
{
    private SimcadeVehicleController _controller;
    private CinemachineVirtualCamera _camera;
    public Rigidbody RB { get; private set; }
    private InputRecord _cachedInput;

    private void Awake()
    {
        _controller = GetComponent<SimcadeVehicleController>();
        _camera = _controller.GetComponentInChildren<CinemachineVirtualCamera>();
        RB = GetComponent<Rigidbody>();
        Enable(false);
    }

    private void Update()
    {
        if(!_controller.CanDrive)
            return;
        _cachedInput.AccelerationInput = Input.GetAxis("Vertical");
        _cachedInput.SteerInput = Input.GetAxis("Horizontal");
        _cachedInput.BrakeInput = Input.GetAxis("Jump");
        _controller.SetInput(_cachedInput);
    }

    public void SetPosition(Vector3 position, Quaternion rotation)
    {
        RB.position = position;
        RB.rotation = rotation;
    }

    public void ResetVehicle()
    {
        RB.velocity = Vector3.zero;
    }
    
    public void Enable(bool enable)
    {
        _controller.CanDrive = enable;
        _controller.SetInput(new InputRecord(0, 0, 1));
    }

    public InputRecord GetInput()
    {
        return _cachedInput;
    }

    public void CustomFixedUpdate()
    {
        _controller.CustomFixedUpdate();
    }
}
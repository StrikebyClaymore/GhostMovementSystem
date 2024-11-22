using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Основной скрипт который управляет логикой игры.
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private GameObject _carPrefab;
    [SerializeField] private GameObject _shadowCarPrefab;
    [SerializeField] private GameObject _playerCameraPrefab;
    [SerializeField] private CheckpointManager _checkpointManager;
    [SerializeField] private MonoTimer _startTimer;
    [SerializeField] private TextMeshProUGUI _startTimerText;
    [SerializeField] private TextMeshProUGUI _roundText;
    [SerializeField] private Button _resetButton;
    private const string FirstRoundMsg = "First Round"; 
    private const string SecondRoundMsg = "Second Round"; 
    private PlayerCar _playerCar;
    private ShadowCar _shadowCar;
    private PlayerRecorder _playerRecorder;
    private List<IFixedUpdate> _fixedUpdates = new List<IFixedUpdate>();
    private bool _firstRound;

    private void Start()
    {
        _checkpointManager.OnAllCheckpointsReached += PlayerAllCheckpointsReached;
        _startTimer.OnCompleted.AddListener(StartTimerCompleted);
        _startTimer.OnTimeTick.AddListener(StartTimerTick);
        _resetButton.onClick.AddListener(ResetScene);
        InitRace();
    }

    private void FixedUpdate()
    {
        for (int i = _fixedUpdates.Count - 1; i >= 0; i--)
        {
            _fixedUpdates[i].CustomFixedUpdate();
        }
    }

    private void InitRace()
    {
        _firstRound = true;
        _checkpointManager.Initialize();
        SpawnPlayer();
        _playerCar.SetPosition(_startPoint.position, _startPoint.rotation);
        _roundText.SetText(FirstRoundMsg);
        _startTimerText.gameObject.SetActive(true);
        _startTimer.Enable();
    }
    
    private void InitSecondRound()
    {
        _firstRound = false;
        _checkpointManager.Initialize();
        _playerCar.ResetVehicle();
        _playerCar.SetPosition(_startPoint.position, _startPoint.rotation);
        SpawnShadow();
        _shadowCar.SetPosition(_startPoint.position, _startPoint.rotation);
        _roundText.SetText(SecondRoundMsg);
        _startTimerText.gameObject.SetActive(true);
        _startTimer.Enable();
    }
    
    private void StartTimerCompleted()
    {
        _startTimerText.gameObject.SetActive(false);
        if (_firstRound)
        {
            StartRace();
        }
        else
        {
            StartSecondRound();
        }
    }
    
    private void StartTimerTick(float timeLeft)
    {
        int intTimeLeft = Mathf.CeilToInt(timeLeft);
        _startTimerText.SetText(intTimeLeft.ToString());
    }
    
    private void StartRace()
    {
        _playerCar.Enable(true);
        _playerRecorder.StartRecording();
    }

    private void StartSecondRound()
    {
        _playerCar.Enable(true);
        _shadowCar.SetRecords(_playerRecorder.GetRecords());
        _shadowCar.StartMove();
    }

    private void SpawnPlayer()
    {
        var car = Instantiate(_carPrefab);
        Instantiate(_playerCameraPrefab, car.transform);
        _playerCar = car.AddComponent<PlayerCar>();
        _playerRecorder = new PlayerRecorder(_playerCar);
        _fixedUpdates.Add(_playerCar);
        _fixedUpdates.Add(_playerRecorder);
    }
    
    private void SpawnShadow()
    {
        _shadowCar = Instantiate(_shadowCarPrefab).AddComponent<ShadowCar>();
        _fixedUpdates.Add(_shadowCar);
    }
    
    private void PlayerAllCheckpointsReached()
    {
        _playerCar.Enable(false);
        if (_firstRound)
        {
            _playerRecorder.StopRecording();
            InitSecondRound();
        }
        else
        {
           _playerCar.Enable(false);
        }
    }
    
    private void ResetScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}

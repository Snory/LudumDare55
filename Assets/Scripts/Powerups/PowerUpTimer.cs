using UnityEngine;

public class PowerUpTimer : MonoBehaviour
{
    [SerializeField]
    private float _powerUpDuration;

    [SerializeField]
    private GeneralEvent _eventToRaiseEnd;
    
    private float _powerUpTimer;
    private bool _played;

    [SerializeField]
    private BoolVariable _powerUpActive;

    [SerializeField]
    private Material _turnedOffMaterial, _turnedOnMaterial;

    [SerializeField]
    private MeshRenderer _iconMeshRenderer;

    private void Awake()
    {
        _powerUpActive.Value = false;
        _iconMeshRenderer.material = _turnedOffMaterial;
    }

    private void Update()
    {
        if (!_played)
        {
            return;
        }

        if (Time.time > _powerUpTimer)
        {
            _played = false;
            _eventToRaiseEnd.Raise();
        }
    }

    public void OnPowerupCollected()
    {
        _iconMeshRenderer.material = _turnedOnMaterial;
    }

    public void OnPowerUpPlayed()
    {
        _played = true;
        _powerUpTimer = Time.time + _powerUpDuration;
        _powerUpActive.Value = true;
        _iconMeshRenderer.material = _turnedOffMaterial;
    }

    public void OnPowerupEnded()
    {
        _powerUpActive.Value = false;
    }

}

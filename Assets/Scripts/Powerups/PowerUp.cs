using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _powerUpLifeTimeLimit;
    private float _powerUpLifeTime;

    [SerializeField]
    private GeneralEvent _eventToRaiseCollected;

    [SerializeField]
    private GeneralEvent _eventToRaisedDestroyed;

    private void Start()
    {
        _powerUpLifeTime = Time.time + _powerUpLifeTimeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _powerUpLifeTime)
        {
            _eventToRaisedDestroyed.Raise();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Creature")
        {
            _eventToRaiseCollected.Raise();
        }
    }

    public void OnOtherCollected()
    {
        Destroy(gameObject);
    }
}

using UnityEngine;

public class GhostHealth : MonoBehaviour
{
    [SerializeField]    
    private float _ghostLifeTime;
    private float _ghostLifeTimeLimit;

    private void Start()
    {
        _ghostLifeTimeLimit = Time.time + _ghostLifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > _ghostLifeTimeLimit)
        {
            Destroy(gameObject);
        }
    }
}

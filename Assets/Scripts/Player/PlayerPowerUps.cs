using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUps : Singleton<PlayerPowerUps>
{
    private Dictionary<string, int> _powerUps = new Dictionary<string, int>();

    [SerializeField]
    private GeneralEvent _ignoreOrderPowerupPlayed, _stopMovementPowerupPlayed;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(_powerUps.ContainsKey("IgnoreOrder") && _powerUps["IgnoreOrder"] > 0)
            {
                _powerUps["IgnoreOrder"]--;

                // Activate powerup
                _ignoreOrderPowerupPlayed.Raise();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (_powerUps.ContainsKey("StopMovement") && _powerUps["StopMovement"] > 0)
            {
                _powerUps["StopMovement"]--;

                // Activate powerup
                _stopMovementPowerupPlayed.Raise();
            }
        }
    }

    public void OnIgnoreOrderCollected()
    {
        if (!_powerUps.ContainsKey("IgnoreOrder"))
        {
            _powerUps.Add("IgnoreOrder", 1);
        }
        else
        {
            _powerUps["IgnoreOrder"] = 1;
        }
    }

    public void OnStopMovementCollected()
    {
        if (!_powerUps.ContainsKey("StopMovement"))
        {
            _powerUps.Add("StopMovement", 1);
        }
        else
        {
            _powerUps["StopMovement"] = 1;
        }
    }

}

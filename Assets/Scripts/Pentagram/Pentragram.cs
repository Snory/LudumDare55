using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pentragram : Singleton<Pentragram>
{
    [SerializeField]
    private List<ConjecturePoint> _conjecuturePoints;

    [SerializeField]
    private int _currentConjecturePointIndex = 0;

    [SerializeField]
    private Image _fullFillmentImage;

    [SerializeField]
    private float _maxFullFillment = 100;
    private float _fullFilmentPerSacrifice = 1;
    private float _currentFullFillment = 0;

    private void Start()
    {
        _fullFillmentImage.fillAmount = 0;
    }


    // Update is called once per frame
    void Update()
    {
        RotatePentagram();
    }

    private void RotatePentagram()
    {
        transform.Rotate(Vector3.forward, 10 * Time.deltaTime);
    }

    public ConjecturePoint GetCurrentConjecturePoint()
    {
        if (_conjecuturePoints[_currentConjecturePointIndex].IsOccupied)
        {
            return null;
        }

        return _conjecuturePoints[_currentConjecturePointIndex];
    }

    public void OnCorrectSacrifice()
    {
        _currentFullFillment += _fullFilmentPerSacrifice;
        UpdateFullfillment();
    }

    public void UpdateFullfillment()
    {
       _fullFillmentImage.fillAmount = _currentFullFillment / _maxFullFillment;
    }

    public void GeneratePentagram()
    {

    }
}

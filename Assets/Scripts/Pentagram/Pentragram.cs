using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class Pentragram : Singleton<Pentragram>
{
    private List<ConjecturePoint> _conjecuturePoints;

    [SerializeField]
    private LevelSetting _levelSetting;

    [SerializeField]
    private Image _fullFillmentImage;

    [SerializeField]
    private GameObject _conjecturePoint;

    [SerializeField]
    private float _conjecturePointradius = 1;

    [SerializeField]
    private GeneralEvent _newFullFillment, _levelCleared, _wrongSacrifice, _correctSacrifice;


    private int _currentFullFillmentAmount = 0;
    private FullFillment _currentFullfillment;
    private int _currentFullfillmentIndex = -1;
    private void Start()
    {
        NextFullfillment();
    }

    public int GetCurrentFullFillmentIndex()
    {
        return _currentFullfillmentIndex;
    }

    public LevelSetting GetLevelSetting()
    {
        return _levelSetting;
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

    public void OnSacrifice(EventArgs sacrificeEvnetArgs)
    {
        SacrificeEventArgs sacrificeEventArgs = (SacrificeEventArgs)sacrificeEvnetArgs;

        if(sacrificeEventArgs.ConjecturePoint is null)
        {
            _wrongSacrifice.Raise();
            return;
        }

        var nextSacrifice = _currentFullfillment.GetNextSacrifice(_currentFullFillmentAmount);

        if (
            (nextSacrifice.CreatureType == sacrificeEventArgs.CreatureType
            && nextSacrifice.CreatureType == sacrificeEventArgs.ConjecturePoint.SacrificeType)
            || nextSacrifice is null
            )
        {
            UpdateFullfillment(1);
            _correctSacrifice.Raise();
        }
        else
        {
            UpdateFullfillment(-1);
            _wrongSacrifice.Raise();
        }
    }

    public void UpdateFullfillment(int valueAddition)
    {
        _currentFullFillmentAmount += valueAddition;

        if (_currentFullFillmentAmount < 0)
        {
            _currentFullFillmentAmount = 0;
        }

        if (_currentFullFillmentAmount >= _currentFullfillment.GetFullFillmentValue())
        {           
            NextFullfillment();
        }

        _fullFillmentImage.fillAmount = _currentFullFillmentAmount / (float) _currentFullfillment.GetFullFillmentValue();
    }

    public void GeneratePentagram()
    {
        _conjecuturePoints = new List<ConjecturePoint>();

        var countOfConjecturePoints = _currentFullfillment.GetUniqueCreatureTypes();

        for (int i = 0; i < countOfConjecturePoints.Count; i++)
        {
            var radians = 2 * Mathf.PI / countOfConjecturePoints.Count * i;

            var vertical = Mathf.Sin(radians) * _conjecturePointradius;
            var horizontal = Mathf.Cos(radians) * _conjecturePointradius;

            var spawnPosition = new Vector3(horizontal, this.transform.position.y, vertical) + this.transform.position;
            var newConjecturePointGameObject = Instantiate(_conjecturePoint, transform);
            newConjecturePointGameObject.transform.position = spawnPosition;

            _conjecuturePoints.Add(newConjecturePointGameObject.GetComponent<ConjecturePoint>());
            _conjecuturePoints[i].Init(countOfConjecturePoints[i]);
        }
    }

    public void DestroyConjecturePoints()
    {
        if(_conjecuturePoints is null)
        {
            return;
        }   

        foreach (var conjecturePoint in _conjecuturePoints)
        {
            Destroy(conjecturePoint.gameObject);
        }
    }

    private void NextFullfillment()
    {
        _currentFullfillmentIndex++;

        if(_currentFullfillmentIndex >= _levelSetting.FullFillmentBank.FullFillments.Count)
        {
            _levelCleared.Raise();
        }

        _fullFillmentImage.fillAmount = 0;
        _currentFullFillmentAmount = 0;
        _currentFullfillment = _levelSetting.FullFillmentBank.FullFillments[_currentFullfillmentIndex];

        // Events
        DestroyConjecturePoints();
        GeneratePentagram();
        _newFullFillment.Raise(new FullFillmentEventArgs(_currentFullfillment));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _conjecturePointradius);
    }
}

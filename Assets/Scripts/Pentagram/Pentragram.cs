using System;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField]
    private List<FullFillmentItem> _currentFullFillments;
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


        // Sacrifice from Destination
        if (sacrificeEventArgs.ConjecturePoint is null)
        {
            _wrongSacrifice.Raise();
            return;
        }
        FullFillmentItem nextSacrifice = GetNextSacrifice();


        // Nothing to sacrifice
        if(nextSacrifice is null)
        {
            return;
        }

        if (
            (nextSacrifice.CreatureType == sacrificeEventArgs.CreatureType
            && nextSacrifice.CreatureType == sacrificeEventArgs.ConjecturePoint.SacrificeType)
            )
        {

            Debug.Log("Correct Sacrifice");
            UpdateFullfillment(1);
            _correctSacrifice.Raise();
        }
        else
        {
            Debug.Log("Bad Sacrifice");
            UpdateFullfillment(-1);
            _wrongSacrifice.Raise();
        }
    }

    public FullFillmentItem GetNextSacrifice()
    {
        return _currentFullFillments.Where(x => x.CurrentAmount < x.Amount).OrderBy(x => x.Order).FirstOrDefault();
    }

    public void UpdateFullfillment(int valueAddition)
    {
        if(valueAddition > 0) // Success 
        {
            var fullfillment = _currentFullFillments.Where(x => x.CurrentAmount < x.Amount).OrderBy(x => x.Order).FirstOrDefault();

            if(fullfillment is not null)
            {
                fullfillment.CurrentAmount +=valueAddition;
            }
        } else
        {
            var fullfillment = _currentFullFillments.Where(x => x.CurrentAmount > 0).OrderByDescending(x => x.Order).FirstOrDefault();
            
            if(fullfillment is not null)
            {
                fullfillment.CurrentAmount+= valueAddition;
            }            
        }


        if (_currentFullfillment.GetCurrentFilling() >= _currentFullfillment.GetMaxFullFillment())
        {           
            NextFullfillment();
        }

        _fullFillmentImage.fillAmount = _currentFullfillment.GetCurrentFilling() / (float) _currentFullfillment.GetMaxFullFillment();
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
            return;
        }

        _fullFillmentImage.fillAmount = 0;

        _currentFullfillment = _levelSetting.FullFillmentBank.FullFillments[_currentFullfillmentIndex];

        _currentFullFillments = _currentFullfillment.FullfillmentItems;

        for (int i = 0; i < _currentFullFillments.Count; i++)
        {
            _currentFullFillments[i].Order = i;
            _currentFullFillments[i].CurrentAmount = 0;
        }

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

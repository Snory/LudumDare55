using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

public class CreatureNudgeMovement : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private Transform _creatureTransform;
    private Vector3 _middlePosition;
    private Camera _main;

    private NavMeshAgent _agent;

    private RectTransform _directionRectTransform;

    [SerializeField]
    private GameObject _directionPointerObject;

    [SerializeField]
    private Image _forcePointer;

    [SerializeField]
    private LayerMask _creatureLayerMask;

    [Header("Nudge movement speed settings")]
    [SerializeField]
    private float _minForwardForce;

    [SerializeField]
    private float _maxForwardForce;

    [SerializeField]
    private float _maxMagnitudeInPercent;

    [SerializeField]
    private float _degradationForce;

    [SerializeField]
    private float _minSpeed;

    [Header("Random movement settings")]
    [SerializeField]    
    private float _randomMovementCoolDown;
    private float _randomMovementTime;

    [Header("Bounce settings")]
    [SerializeField]
    private float _bounceForceDecrease;


    float _maxMagnitude;

    public UnityEvent NudgeMovementStarted;
    public UnityEvent NudgeMovementEnded;

    [Header("Process")]
    private bool _isDragging;
    private bool _nudgeMovement;
    private Vector3 _lastVelocity;

    private bool _isBanished;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();

        if (_rigidBody == null)
        {
            Debug.LogError("Rigidbody component not found");
        }

        _creatureTransform = transform;
        _main = Camera.main;
        _directionRectTransform = _directionPointerObject.GetComponent<RectTransform>();
        _directionPointerObject.SetActive(false);
        _maxMagnitude = Mathf.Sqrt(Screen.width * Screen.width + Screen.height + Screen.height) * _maxMagnitudeInPercent;
        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            Debug.LogError("NavMeshAgent component not found");
        }
        _nudgeMovement = false;

        _randomMovementTime = Time.time + _randomMovementCoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isBanished)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.SphereCast(ray, 1, out hit, Mathf.Infinity, _creatureLayerMask))
            {
                return;
            }

            if (hit.collider.gameObject != gameObject)
            {
                return;
            }

            InitNudgeMovement();
        }

        StartNudgeMovement();
        KeepNudgeMovement();
    }

    private void KeepNudgeMovement()
    {
        if (!_nudgeMovement)
        {
            return;
        }

        _rigidBody.velocity = _rigidBody.velocity / (1 + (_degradationForce * Time.deltaTime));

        if (_rigidBody.velocity.magnitude < _minSpeed)
        {
            _rigidBody.velocity = Vector3.zero;
        }
        else
        {
            _randomMovementTime = Time.time + _randomMovementCoolDown;
        }

        if (Time.time > _randomMovementTime)
        {
            Debug.Log("Invoked");

            _nudgeMovement = false;
            NudgeMovementEnded?.Invoke();
        }

        _lastVelocity = _rigidBody.velocity;
    }

    private void StartNudgeMovement()
    {
        if (!_isDragging)
        {
            return;
        }

        Vector3 directionMouseToMiddle = Input.mousePosition - _middlePosition;

        float angle = (Mathf.Atan2(directionMouseToMiddle.y, directionMouseToMiddle.x) * Mathf.Rad2Deg);
        angle = 270 - (angle) % 360;
        _directionRectTransform.localRotation = Quaternion.Euler(0, 0, angle - _creatureTransform.rotation.eulerAngles.y);

        float magnitude = directionMouseToMiddle.magnitude;

        if (magnitude > _maxMagnitude)
        {
            magnitude = _maxMagnitude;
        }

        _forcePointer.fillAmount = magnitude / _maxMagnitude;

        if (Input.GetMouseButtonUp(0))
        {
            _rigidBody.velocity = Vector3.zero;
            _creatureTransform.rotation = Quaternion.Euler(0, angle, 0);
            _directionRectTransform.localRotation = Quaternion.Euler(0, 0, 0);

            float force = Mathf.Lerp(_minForwardForce, _maxForwardForce, magnitude / _maxMagnitude);

            _rigidBody.AddForce(_creatureTransform.forward * force, ForceMode.VelocityChange);
            _isDragging = false;
            _agent.isStopped = true;
            _nudgeMovement = true;
            _randomMovementTime = Time.time + _randomMovementCoolDown;
            NudgeMovementStarted?.Invoke();
            _directionPointerObject.SetActive(false);
        }
    }

    private void InitNudgeMovement()
    {
        _middlePosition = _main.WorldToScreenPoint(_creatureTransform.position);
        _directionPointerObject.SetActive(true);
        _isDragging = true;
    }

    private void OnCollisionEnter(Collision collision)
    {        
        Vector3 normal = collision.contacts[0].normal;
        Vector3 reflection = Vector3.Reflect(_creatureTransform.forward, normal);
        _rigidBody.velocity = reflection * _lastVelocity.magnitude * _bounceForceDecrease;
    }

    public void OnBanished()
    {
        _isBanished = true;

        _rigidBody.velocity = Vector3.zero;
    }
}

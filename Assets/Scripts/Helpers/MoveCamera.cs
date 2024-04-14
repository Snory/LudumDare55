using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    [SerializeField]
    private float _panSpeed = 20f;

    [SerializeField]
    private Vector2 _panLimit;

    [SerializeField]
    private float _scrollSpeed = 50;

    [SerializeField]
    private float _minY = 25f;

    [SerializeField]
    private float _maxY = 100f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0)
        {
            pos.x += horizontal * _panSpeed * Time.deltaTime;
        }

        if (vertical != 0)
        {
            pos.z += vertical * _panSpeed * Time.deltaTime;
        }

        /*
        pos.x = Mathf.Clamp(pos.x, -_panLimit.x, _panLimit.x);
        pos.z = Mathf.Clamp(pos.z, -_panLimit.y, _panLimit.y);
        */
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * 100f * _scrollSpeed * Time.deltaTime;

        pos.y = Mathf.Clamp(pos.y, _minY, _maxY);


        this.transform.position = pos;

    }
}

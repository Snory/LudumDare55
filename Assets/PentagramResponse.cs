using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PentagramResponse : MonoBehaviour
{

    [SerializeField]
    private Image _image;

    [SerializeField]    
    private Color _wrongSacrificeColor;

    [SerializeField]
    private float _flickeringCooldown;

    private Color _originalColor;

    private void Awake()
    {
        _originalColor = _image.color;
    }

    public void OnWrongSacrifice()
    {
        StopAllCoroutines();
        // change color of pentagram between its original one and _wrongSacrificeColor for 0.5 seconds
        StartCoroutine(ChangeColorForSeconds());
    }

    private IEnumerator ChangeColorForSeconds()
    {
        int counts = 5;

        while(counts > 0)
        {
            _image.color = _wrongSacrificeColor;
            yield return new WaitForSeconds(_flickeringCooldown);
            _image.color = _originalColor;
            yield return new WaitForSeconds(_flickeringCooldown);

            counts--;
        }
    }
}

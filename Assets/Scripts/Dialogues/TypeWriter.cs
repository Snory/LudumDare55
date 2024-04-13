using System;
using System.Collections;
using UnityEngine;

public class TypeWriter : MonoBehaviour
{
    [SerializeField]
    private float _minSpeedInSec;
    [SerializeField]
    private float _maxSpeedInSec;

    public void Type(string text, Action<string> targetCallBack)
    {
        StartCoroutine(TypeText(text, targetCallBack));
    }

    private IEnumerator TypeText(string text, Action<string> targetCallback)
    {
        string typedText = "";

        float speed = CalculateSpeed(text.Length);

        foreach (char letter in text.ToCharArray())
        {
            typedText += letter;
            targetCallback(typedText);

            yield return new WaitForSeconds(speed);
        }

        yield return null;
    }

    private float CalculateSpeed(int textLength)
    {
        float speed = Mathf.Lerp(_maxSpeedInSec, _minSpeedInSec, textLength / 100f);
        return speed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FullFillmentBank", menuName = "FullFillment/FullFillmentBank")]
public class FullFillmentBank : ScriptableObject 
{
    public List<FullFillment> FullFillments;
}

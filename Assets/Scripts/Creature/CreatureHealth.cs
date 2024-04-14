using UnityEngine;
using UnityEngine.Events;

public class CreatureHealth : MonoBehaviour
{
    public SacrificeTypeBank TypeBank;

    [SerializeField]
    private GeneralEvent _wrongSacrifice;

    [SerializeField]
    private GeneralEvent _correctSacrifice;


    public SacrificeType SacrificeType { get; private set; }
    public UnityEvent OnDeath;

    private void Start()
    {
        if(TypeBank == null)
            Debug.LogError("TypeBank is not set in CreatureHealth");

        SacrificeType = TypeBank.SacrificeTypes[Random.Range(0, TypeBank.SacrificeTypes.Count)];
        GetComponent<Renderer>().material.color = SacrificeType.Material.color;
    }

    private void OnDestroy()
    {
        OnDeath?.Invoke();
    }

    internal void Die()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Destination")
        {
            _wrongSacrifice.Raise();
            Die();
        }

        if(other.tag == "ConjecturePoint")
        {
            if(other.GetComponent<ConjecturePoint>().SacrificeType != SacrificeType)
            {
                _wrongSacrifice.Raise();
            }
            else
            {
                _correctSacrifice.Raise();
            }

            Die();
        }   
    }
}

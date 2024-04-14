using UnityEngine;
using UnityEngine.Events;

public class CreatureHealth : MonoBehaviour
{
    [SerializeField]
    private GeneralEvent _sacrifice, _spawned, _died;
    public CreatureType CreatureType { get; private set; }
    public UnityEvent OnDeath;

    public void Init(CreatureType sacrificeType, bool ghostSpawn = false)
    {
        CreatureType = sacrificeType;
        GetComponent<Renderer>().material.color = CreatureType.Material.color;

        _spawned.Raise(new CreatureEventArgs(CreatureType, ghostSpawn));
    }

    private void OnDestroy()
    {
        OnDeath?.Invoke();
    }

    internal void Die()
    {
        _died.Raise(new CreatureEventArgs(CreatureType));
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ConjecturePoint" || other.tag == "Destination")
        {
            _sacrifice.Raise(new SacrificeEventArgs(CreatureType, other.GetComponent<ConjecturePoint>()));
            Die();
        }   
    }
}

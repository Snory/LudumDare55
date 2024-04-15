using UnityEngine;
using UnityEngine.Events;

public class CreatureHealth : MonoBehaviour
{
    [SerializeField]
    private GeneralEvent _sacrifice, _spawned, _died;
    public CreatureType CreatureType { get; private set; }
    public UnityEvent OnDeath;

    [SerializeField]
    private MeshRenderer _bodyRenderer;

    [SerializeField]
    private Material _defaultMaterial;

    [SerializeField]
    private BoolVariable _ignoreOrderPlayed;

    public void Init(CreatureType sacrificeType, bool ghostSpawn = false)
    {
        CreatureType = sacrificeType;

        if (!_ignoreOrderPlayed.Value)
        {
            _bodyRenderer.material.color = CreatureType.Material.color;
        } else
        {
            OnIgnoreOrderPowerupPlayer();
        }

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

    public void OnIgnoreOrderPowerupPlayer()
    {
        _bodyRenderer.material.color = _defaultMaterial.color;
    }

    public void OnIgnoreOrderPowerupEnded()
    {
        _bodyRenderer.material.color = CreatureType.Material.color;
    }

}

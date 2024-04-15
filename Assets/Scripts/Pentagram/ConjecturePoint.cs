using UnityEngine;

public class ConjecturePoint : MonoBehaviour
{
    public CreatureType SacrificeType;
    private SpriteRenderer _spriteRenderer;
    private Material _defaultMaterial;

    [SerializeField]
    private BoolVariable _ignoreOrderPlayed;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();        
    }

    public void Init(CreatureType sacrificeType, Material defaultMaterial)
    {
        SacrificeType = sacrificeType;
        _spriteRenderer.sprite = SacrificeType.Sprite;
        _defaultMaterial = defaultMaterial;

        if (!_ignoreOrderPlayed.Value)
        {
            _spriteRenderer.color = SacrificeType.Material.color;
        } else
        {
            _spriteRenderer.color = _defaultMaterial.color;
        }
    }

    public void SetDefaultColor()
    {
        _spriteRenderer.color = _defaultMaterial.color;
    }

    public void ResetColor()
    {
        _spriteRenderer.color = SacrificeType.Material.color;
    }

}

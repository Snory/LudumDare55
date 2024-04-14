using UnityEngine;

public class ConjecturePoint : MonoBehaviour
{
    public CreatureType SacrificeType;

    // Start is called before the first frame update
    void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = SacrificeType.Sprite;
        spriteRenderer.color = SacrificeType.Material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(CreatureType sacrificeType)
    {
        SacrificeType = sacrificeType;
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = SacrificeType.Sprite;
        spriteRenderer.color = SacrificeType.Material.color;
    }
}

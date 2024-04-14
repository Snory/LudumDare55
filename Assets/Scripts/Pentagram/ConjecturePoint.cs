using UnityEngine;

public enum ConjecturePointType
{
    First = 0,
    Second = 1,
    Third = 2,
    Fourth = 3,
    Fifth = 4
}

public class ConjecturePoint : MonoBehaviour
{
    public SacrificeType SacrificeType;

    public bool IsOccupied { get; private set; }
    public bool IsActivated { get; private set; }

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
}

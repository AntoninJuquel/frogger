using UnityEngine;

public class RoadEntity : Entity
{
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer _sr;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _sr.sprite = sprites[Mathf.Abs((int) Transform.position.y) - 1];
    }

    public override void Action(Frogger other)
    {
        other.Die();
    }
}
using UnityEngine;

public class WaterEntity : Entity
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Vector2Int sizeRange;
    [SerializeField] private Sprite start, end;
   
    private void Start()
    {
        var size = Random.Range(sizeRange.x, sizeRange.y);
        for (var i = 1; i < size; i++)
        {
            var position = Transform.position - (Transform.right * i);
            Instantiate(prefab, position, Quaternion.identity, Transform);
        }

        Transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = start;
        Transform.GetChild(size - 1).GetComponent<SpriteRenderer>().sprite = end;
        Box.size = new Vector2(size, 1);
        Box.offset = new Vector2(((size - 1) / 2f) * Mathf.Sign(Transform.position.x), 0);
    }

    public override void Action(Frogger other)
    {
        other.transform.parent = Transform;
    }
}
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private float speed;
    protected Transform Transform;
    protected BoxCollider2D Box;

    private void OnEnable()
    {
        Box = GetComponent<BoxCollider2D>();
        Transform = transform;
        Transform.right = Transform.position.x < 0 ? Vector3.right : Vector3.left;
    }

    private void FixedUpdate()
    {
        Transform.position += Transform.right * (speed * Time.fixedDeltaTime);
    }

    public abstract void Action(Frogger other);
}
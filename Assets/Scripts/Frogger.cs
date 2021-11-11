using System.Collections;
using UnityEngine;

public class Frogger : MonoBehaviour
{
    [SerializeField] private LayerMask layerToHit;
    [SerializeField] private Sprite deathSprite, scoreSprite;
    [SerializeField] private float leapDuration = .125f;
    [SerializeField] private int lives;
    private SpriteController _sc;
    private SpriteRenderer _sr;
    private Vector3 _computedPosition, _spawnPosition;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _spawnPosition = _transform.position;
        _computedPosition = _spawnPosition;
        _sc = GetComponent<SpriteController>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Move(Vector3.up);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            Move(Vector3.down);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            Move(Vector3.right);
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            Move(-Vector3.right);
        
        var hit = Physics2D.OverlapCircle(_transform.position, .25f, layerToHit);
        if(!hit) return;
    }
    
    private void Move(Vector3 direction)
    {
        var destination = _computedPosition + direction;
        StartCoroutine(Leap(destination));
        _transform.up = direction;
    }

    private IEnumerator Leap(Vector3 destination)
    {
        var startPosition = _computedPosition;
        _computedPosition = destination;
        var elapsed = 0f;
        _sc.NextSprite();
        while (elapsed < leapDuration)
        {
            var percentage = elapsed / leapDuration;
            _transform.position = Vector3.Lerp(startPosition, destination, percentage);
            elapsed += Time.deltaTime;
            yield return null;
        }

        _transform.position = destination;
        _sc.NextSprite();
    }

    private void NewFrogger()
    {
        Instantiate(gameObject, _spawnPosition, Quaternion.identity);
        Destroy(_sc);
        Destroy(this);
    }

    public void Die()
    {
        StopAllCoroutines();
        lives--;
        if (lives == 0)
            Debug.Log("DEAD");
        else
            NewFrogger();
        _sr.sprite = deathSprite;
    }

    public void Score()
    {
        StopAllCoroutines();
        NewFrogger();
        _sr.sprite = scoreSprite;
    }
}
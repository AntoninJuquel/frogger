using System.Collections;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    [SerializeField] private bool animate;
    [SerializeField] private float delay;
    [SerializeField] private Sprite[] sprites;

    private SpriteRenderer _sr;
    private int _index;
    private YieldInstruction _waitInstruction;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _waitInstruction = new WaitForSeconds(delay);
        if (animate) StartCoroutine(Animation());
        _sr.sprite = sprites[0];
    }

    private IEnumerator Animation()
    {
        while (gameObject.activeSelf)
        {
            yield return _waitInstruction;
            NextSprite();
        }
    }

    public void NextSprite()
    {
        _index = (_index + 1) % sprites.Length;
        _sr.sprite = sprites[_index];
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _sprites;
    private int _spriteIndex;

    private Vector3 direction;
    public float _gravity = -9.8f;
    public float _jumpForce = 5f;
    void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start() {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }
    void Update() {

        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {
                direction = Vector3.up * _jumpForce;
            }
        }
        direction.y += _gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }
    private void AnimateSprite() {
        _spriteIndex++;
        if (_spriteIndex >= _sprites.Length) {
            _spriteIndex = 0;
        }
        _spriteRenderer.sprite = _sprites[_spriteIndex];
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Colition")) {
            Manager.Instance.GameOver();
        } else if (other.gameObject.CompareTag("Point")) {
            Manager.Instance.AddScore(1);
        }
    }
}

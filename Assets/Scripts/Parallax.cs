using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    public float animationSpeed = 1f;
    void Awake() {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    void Update() {
        _meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
    }
}
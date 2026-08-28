using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ObstacleTextureChanger : MonoBehaviour
{
    [Header("Sprite Pool")]
    [SerializeField] private Sprite[] _obstacleSprites;

    [Header("Collider Settings")]
    [SerializeField] private bool _useExactPolygonCollider = true;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        RandomizeSpriteAndCollider();
    }

    private void RandomizeSpriteAndCollider()
    {
        if (_obstacleSprites == null || _obstacleSprites.Length == 0) return;

        // 1. Ubah Sprite
        int randomIndex = Random.Range(0, _obstacleSprites.Length);
        _spriteRenderer.sprite = _obstacleSprites[randomIndex];

        // 2. Update Collider
        UpdateCollider();
    }

    private void UpdateCollider()
    {
        // Hapus collider lama jika ada (sekarang aman karena [RequireComponent] sudah dihapus)
        Collider2D existingCollider = GetComponent<Collider2D>();
        if (existingCollider != null)
        {
            DestroyImmediate(existingCollider);
        }

        // Buat collider baru yang presisi dengan sprite baru
        if (_useExactPolygonCollider)
        {
            PolygonCollider2D polyCol = gameObject.AddComponent<PolygonCollider2D>();
            polyCol.isTrigger = true;
        }
        else
        {
            BoxCollider2D boxCol = gameObject.AddComponent<BoxCollider2D>();
            boxCol.size = _spriteRenderer.sprite.bounds.size;
            boxCol.offset = _spriteRenderer.sprite.bounds.center;
            boxCol.isTrigger = true;
        }
    }
}
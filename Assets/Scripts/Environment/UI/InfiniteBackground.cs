using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class InfiniteBackground : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 5f;

    private float _localWidth;
    private float _startX;

    private void Awake()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        // Hitung lebar murni sprite dalam unit 2D (tanpa terpengaruh Scale GameObject)
        _localWidth = sr.sprite.rect.width / sr.sprite.pixelsPerUnit;

        // Buat clone sambungan secara internal
        GameObject childBG = new GameObject("BG_Clone");
        childBG.transform.SetParent(transform, false); // False menjaga koordinat lokal presisi 1:1
        childBG.transform.localPosition = new Vector3(_localWidth, 0, 0);

        // Samakan atribut render
        SpriteRenderer childSR = childBG.AddComponent<SpriteRenderer>();
        childSR.sprite = sr.sprite;
        childSR.sortingLayerID = sr.sortingLayerID;
        childSR.sortingOrder = sr.sortingOrder;
        childSR.color = sr.color;
    }

    private void Start()
    {
        _startX = transform.position.x;
    }

    private void Update()
    {
        // Geser ke kiri berdasarkan delta time
        transform.Translate(Vector3.left * (_scrollSpeed * Time.deltaTime));

        // Hitung lebar aktual di layar (mengakomodasi Scale GameObject di Inspector)
        float worldWidth = _localWidth * transform.lossyScale.x;

        // Saat posisi bergeser sejauh 1 lebar sprite dari titik awal
        if (transform.position.x <= _startX - worldWidth)
        {
            // Snap relatif untuk mencegah jeda/stutter akibat sisa pergeseran frame
            transform.position += new Vector3(worldWidth, 0, 0);
        }
    }
}
using UnityEngine;

public class CameraSpeedShake : MonoBehaviour
{
    public static CameraSpeedShake Instance { get; private set; }

    [Header("Impact Shake (Trauma)")]
    [Tooltip("Exponent for non-linear decay. Higher values make high trauma feel punchier.")]
    [SerializeField] private float _traumaExponent = 2f;
    [SerializeField] private float _traumaDecay = 1.5f;
    [SerializeField] private Vector3 _maxPositionOffset = new Vector3(0.5f, 0.5f, 0f);
    [SerializeField] private float _maxRotationRoll = 5f; // Z-axis rotation adds high-speed tension

    [Header("Speed Wobble (Continuous)")]
    [SerializeField] private float _frequency = 25f;
    [Range(0f, 1f)]
    [SerializeField] private float _speedTraumaBaseline = 0.05f; // Minimum shake at normal speed

    private float _trauma = 0f;
    private Vector3 _initialLocalPosition;
    private Quaternion _initialLocalRotation;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        _initialLocalPosition = transform.localPosition;
        _initialLocalRotation = transform.localRotation;
    }

    private void LateUpdate()
    {
        // Total shake strength combines instant trauma and continuous speed baseline
        float currentTrauma = Mathf.Clamp01(_trauma + _speedTraumaBaseline);

        if (currentTrauma > 0)
        {
            // Squaring trauma creates a punchy, realistic falloff
            float shakeIntensity = Mathf.Pow(currentTrauma, _traumaExponent);
            float time = Time.time * _frequency;

            // Generate smooth Perlin noise offsets (-1 to 1)
            float offsetX = (Mathf.PerlinNoise(time, 0f) * 2f - 1f) * _maxPositionOffset.x * shakeIntensity;
            float offsetY = (Mathf.PerlinNoise(0f, time) * 2f - 1f) * _maxPositionOffset.y * shakeIntensity;
            float offsetRoll = (Mathf.PerlinNoise(time, time) * 2f - 1f) * _maxRotationRoll * shakeIntensity;

            // Apply relative offsets to local transform
            transform.localPosition = _initialLocalPosition + new Vector3(offsetX, offsetY, 0f);
            transform.localRotation = _initialLocalRotation * Quaternion.Euler(0f, 0f, offsetRoll);

            // Decay impact trauma over time
            _trauma = Mathf.Clamp01(_trauma - Time.deltaTime * _traumaDecay);
        }
        else
        {
            // Reset to original local position when zero shake
            transform.localPosition = _initialLocalPosition;
            transform.localRotation = _initialLocalRotation;
        }
    }

    /// <summary>
    /// Call this from any script to trigger an impact shake (e.g., hitting obstacles or speed boosts).
    /// </summary>
    /// <param name="amount">Trauma strength from 0.0 to 1.0</param>
    public void AddTrauma(float amount)
    {
        _trauma = Mathf.Clamp01(_trauma + amount);
    }

    /// <summary>
    /// Adjust the baseline shake strength dynamically as the runner speeds up over time.
    /// </summary>
    public void SetSpeedBaseline(float normalizedSpeed)
    {
        _speedTraumaBaseline = Mathf.Clamp(normalizedSpeed * 0.2f, 0f, 0.25f);
    }
}
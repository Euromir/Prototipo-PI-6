using UnityEngine;

public class LightIntensityRandomizer : MonoBehaviour
{
    [SerializeField] private Light _light;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _minIntensity = 5f;
    [SerializeField] private float _maxIntensity = 30f;

    private float _initialIntensity;

    private void Start()
    {
        _light = GetComponent<Light>();
        _initialIntensity = _light.intensity;
    }

    private void Update()
    {
        float noise = Mathf.PerlinNoise(Time.time * _speed, 0f);
        _light.intensity = Mathf.Lerp(_minIntensity, _maxIntensity, noise);
    }
}

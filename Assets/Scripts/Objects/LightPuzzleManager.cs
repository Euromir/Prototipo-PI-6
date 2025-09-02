using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;
using System.Collections;

public class LightPuzzleManager : MonoBehaviour
{
    [Header("Configuração do Puzzle")]
    public int requiredLamps = 3;
    public List<GameObject> objectsToToggle;
    
    [Header("Controle de Claridade da Cena")]
    [Tooltip("Arraste para cá o GameObject que contém seu Global Volume.")]
    public Volume globalVolume;
    
    [Tooltip("Defina os valores de exposição. O tamanho deve ser 4 (para 0, 1, 2 e 3 luzes).")]
    public float[] exposureValues = new float[4] { 0f, 0.5f, 1f, 1.5f };

    [Tooltip("A velocidade da transição de claridade.")]
    public float brightnessTransitionSpeed = 1f;
    private ColorAdjustments colorAdjustments;
    private Coroutine _brightnessCoroutine;

    private int _activeLampsCount = 0;
    private bool _isConditionMet = false;

    public static LightPuzzleManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    void Start()
    {
        if (globalVolume != null)
        {
            globalVolume.profile.TryGet(out colorAdjustments);
        }

        UpdateSceneBrightness();
    }

    public void OnLampActivated()
    {
        _activeLampsCount++;
        UpdatePuzzleState();
    }

    public void OnLampDeactivated()
    {
        _activeLampsCount--;
        UpdatePuzzleState();
    }
    
    private void UpdatePuzzleState()
    {
        UpdateSceneBrightness();

        bool conditionShouldBeActive = (_activeLampsCount >= requiredLamps);
        if (conditionShouldBeActive != _isConditionMet)
        {
            _isConditionMet = conditionShouldBeActive;
            ToggleObjects(_isConditionMet);
        }
    }
    
    private void UpdateSceneBrightness()
    {
        if (colorAdjustments == null) return;

        int clampedLamps = Mathf.Clamp(_activeLampsCount, 0, 3);
        float targetExposure = exposureValues[clampedLamps];

        if (_brightnessCoroutine != null)
        {
            StopCoroutine(_brightnessCoroutine);
        }
        _brightnessCoroutine = StartCoroutine(LerpBrightness(targetExposure));
    }

    private IEnumerator LerpBrightness(float targetValue)
    {
        float startValue = colorAdjustments.postExposure.value;
        float time = 0;

        while (time < 1)
        {
            colorAdjustments.postExposure.value = Mathf.Lerp(startValue, targetValue, time);
            time += Time.deltaTime * brightnessTransitionSpeed;
            yield return null;
        }

        colorAdjustments.postExposure.value = targetValue;
    }

    private void ToggleObjects(bool isActive)
    {
        foreach (var obj in objectsToToggle)
        {
            if (obj != null) obj.SetActive(isActive);
        }
    }
}

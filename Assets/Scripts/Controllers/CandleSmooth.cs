/**
 * @class CandleSmoot
 * @brief Simulates a candle's flame effect by adjusting the intensity and position of a Point Light in Unity.
 * 
 * The CandleSmoot class simulates a realistic candle effect using smooth intensity variations and slight positional 
 * changes to the light, creating a natural flicker. The effect is achieved through the use of Perlin Noise to create smooth, 
 * random variations over time.
 */
using System.Collections;
using UnityEngine;

public class CandleSmoot : MonoBehaviour
{
    /**
     * @brief The Point Light representing the candle's flame.
     * 
     * This is the Light component that will be manipulated to create the candle effect.
     */
    [SerializeField]
    [Tooltip("The Point Light representing the candle's flame.")]
    private Light _pointLight = null;

    /**
     * @brief Minimum intensity of the light.
     * 
     * The light's intensity will vary between _minIntensity and _maxIntensity to simulate the flicker of the candle.
     */
    [SerializeField]
    [Tooltip("Minimum intensity of the light.")]
    private float _minIntensity = 0.8f;

    /**
     * @brief Maximum intensity of the light.
     */
    [SerializeField]
    [Tooltip("Maximum intensity of the light.")]
    private float _maxIntensity = 1.2f;

    /**
     * @brief Speed of the intensity variation.
     * 
     * This value controls how fast the intensity of the light changes.
     */
    [SerializeField]
    [Tooltip("Speed of the intensity variation.")]
    private float _intensityVariationSpeed = 2.0f;

    /**
     * @brief Range of the light's position fluctuation.
     * 
     * This controls how much the position of the light will move to simulate the flicker of a candle's flame.
     */
    [SerializeField]
    [Tooltip("Range of the light's position fluctuation.")]
    private float _positionRange = 0.02f;

    /**
     * @brief Speed of the position variation.
     * 
     * This value controls how fast the light's position fluctuates.
     */
    [SerializeField]
    [Tooltip("Speed of the position variation.")]
    private float _positionVariationSpeed = 1.0f;

    /**
     * @brief Whether the candle starts lit.
     * 
     * If true, the candle will start lit and flickering when the game begins.
     */
    [SerializeField]
    [Tooltip("Whether the candle starts lit.")]
    bool _startLit = true;

    /**
     * @brief Offset value used for Perlin Noise to simulate smooth, random variations over time.
     */
    private float _noiseOffset = 0.0f;

    /**
     * @brief The initial position of the light.
     * 
     * Stores the starting position of the Point Light to enable subtle movements.
     */
    private Vector3 _initialPosition;

    /**
     * @brief Whether the candle is currently lit.
     */
    private bool _isLit;

    /**
     * @brief Property to get or set whether the candle is lit.
     * 
     * When set to true, the candle starts flickering. When set to false, it stops flickering.
     */
    public bool IsLit
    {
        get { return _isLit; }
        set
        {
            if (_isLit != value)
            {
                _isLit = value;
                if (_isLit)
                    StartFlickerDelay();
                else
                    StopFlickerDelay();
            }
        }
    }

    /**
     * @brief Ensures the Light component is assigned on Awake.
     * 
     * If the _pointLight is not set in the editor, this method will attempt to find a Light component on the GameObject.
     */
    private void Awake()
    {
        if (_pointLight == null)
            _pointLight = GetComponent<Light>();
    }

    /**
     * @brief Initializes the candle's position and lighting state on Start.
     * 
     * The candle's initial position is recorded, and the light is set to the starting state defined by _startLit.
     */
    private void Start()
    {
        _initialPosition = _pointLight.transform.position;
        IsLit = _startLit;
    }

    /**
     * @brief Holds the reference to the currently running flicker Coroutine.
     */
    private Coroutine _flickerDelayRoutine = null;

    /**
     * @brief Starts the flicker effect by initiating the Coroutine.
     */
    private void StartFlickerDelay()
    {
        if (_pointLight == null)
            return;

        StopFlickerDelay();
        _pointLight.enabled = true;
        _flickerDelayRoutine = StartCoroutine(FlickerDelay());
    }

    /**
     * @brief Stops the flicker effect by halting the Coroutine.
     */
    private void StopFlickerDelay()
    {
        if (_flickerDelayRoutine != null)
            StopCoroutine(_flickerDelayRoutine);

        _flickerDelayRoutine = null;
        _pointLight.enabled = false;
    }

    /**
     * @brief Coroutine that handles the flicker effect.
     * 
     * This Coroutine repeatedly adjusts the light's intensity and position to simulate the behavior of a candle's flame.
     */
    private IEnumerator FlickerDelay()
    {
        while (_pointLight.enabled)
        {
            SimulateCandleLight();
            yield return new WaitForEndOfFrame();
        }
    }

    /**
     * @brief Simulates the behavior of a candle by adjusting the light's intensity and position.
     * 
     * The intensity and position of the light are modified using Perlin Noise to create a smooth and random flicker effect.
     */
    private void SimulateCandleLight()
    {
        // Simulate smooth intensity change using Perlin Noise.
        float noise = Mathf.PerlinNoise(Time.time * _intensityVariationSpeed, _noiseOffset);
        _pointLight.intensity = Mathf.Lerp(_minIntensity, _maxIntensity, noise);

        // Simulate flame movement by slightly shifting the light position.
        float noiseX = Mathf.PerlinNoise(Time.time * _positionVariationSpeed, _noiseOffset) * 2 - 1;
        float noiseY = Mathf.PerlinNoise(_noiseOffset, Time.time * _positionVariationSpeed) * 2 - 1;
        float noiseZ = Mathf.PerlinNoise(Time.time * _positionVariationSpeed, _noiseOffset * 2) * 2 - 1;

        Vector3 offset = new Vector3(noiseX, noiseY, noiseZ) * _positionRange;
        _pointLight.transform.position = _initialPosition + offset;

        // Increment noise offset to create more variation over time.
        _noiseOffset += Time.deltaTime;
    }

    /**
     * @brief Extinguishes the candle, stopping the flicker effect.
     * 
     * This method can be called to turn off the candle light.
     */
    public void ExtinguishCandle()
    {
        IsLit = false;
    }

    /**
     * @brief Lights the candle, starting the flicker effect.
     * 
     * This method can be called to turn on the candle light.
     */
    public void LightCandle()
    {
        IsLit = true;
    }
}

/**
 * @class CandleFlicker
 * @brief Simulates the flickering effect of a candle's flame by adjusting the intensity and position of a Point Light in Unity.
 * 
 * The CandleFlicker class controls the flickering effect of a light, simulating the behavior of a candle flame. It allows the light 
 * to flicker randomly in terms of intensity and position. The flicker can be turned on or off programmatically.
 */
using System.Collections;
using UnityEngine;

public class CandleFlicker : MonoBehaviour
{
    /**
     * @brief The Point Light representing the candle's flame.
     * 
     * This is the Light component that will be manipulated to create the flicker effect.
     */
    [SerializeField]
    [Tooltip("The Point Light representing the candle's flame.")]
    private Light _pointLight = null;

    /**
     * @brief Minimum intensity of the light.
     * 
     * The light's intensity will fluctuate between _minIntensity and _maxIntensity to simulate the flicker.
     */
    [SerializeField]
    [Tooltip("Minimum intensity of the light.")]
    private float _minIntensity = 0.5f;

    /**
     * @brief Maximum intensity of the light.
     */
    [SerializeField]
    [Tooltip("Maximum intensity of the light.")]
    private float _maxIntensity = 1.5f;

    /**
     * @brief Speed of the flicker effect.
     * 
     * This value controls the delay between flickers.
     */
    [SerializeField]
    [Tooltip("The Speed (between 0 and the selected value) of the flicker effect.")]
    private float _flickerRangeSpeed = 0.1f;

    /**
     * @brief Range of the light's position fluctuation.
     * 
     * This controls how much the position of the light will move to simulate the flame's movement.
     */
    [SerializeField]
    [Tooltip("Range of the light's position fluctuation.")]
    private float _positionRange = 0.05f;

    /**
     * @brief Whether the candle starts lit.
     * 
     * If true, the candle will start flickering when the game begins.
     */
    [SerializeField]
    [Tooltip("Whether the candle starts lit.")]
    bool _startLit = true;

    /**
     * @brief The initial position of the light.
     * 
     * This stores the starting position of the Point Light so it can be manipulated for the flicker effect.
     */
    private Vector3 _initialLocalPosition;

    /**
     * @brief Whether the candle is currently lit.
     */
    bool _isLit = false;

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
        _initialLocalPosition = _pointLight.transform.localPosition;
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
     * Randomly changes the light's intensity and position, and waits for the specified flicker speed before repeating.
     */
    private IEnumerator FlickerDelay()
    {
        while (_isLit)
        {
            _pointLight.intensity = Random.Range(_minIntensity, _maxIntensity);
            _pointLight.transform.localPosition = _initialLocalPosition + Random.insideUnitSphere * _positionRange;

            yield return new WaitForSeconds(Random.Range(0, _flickerRangeSpeed));
        }
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

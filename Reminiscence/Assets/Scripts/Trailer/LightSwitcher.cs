using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitcher : MonoBehaviour
{
    public float switchOffTime;
    private float _timeStartedCountingOffTime;
    public AnimationCurve switchOffCurve;
    public Light ToSwitchOff;
    private float _lightIntensityOff;
    private bool isEnteredTrigger;

    private bool startingDelay;
    public float delayToActivate;
    private float _timeStartedCountingDelay;
    public AnimationCurve activatingCurve;
    public Light toActivate;
    private float _lightIntensityDelay;
    // Start is called before the first frame update
    void Start()
    {
        _timeStartedCountingOffTime = Time.time;
        startingDelay = false;
        _lightIntensityOff = ToSwitchOff.intensity;
        _lightIntensityDelay = toActivate.intensity;
        isEnteredTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnteredTrigger) return;
        if(!startingDelay)
        {
            _timeStartedCountingOffTime += Time.deltaTime;
            ToSwitchOff.intensity = (1 - switchOffCurve.Evaluate(_timeStartedCountingOffTime / switchOffTime)) * _lightIntensityOff;
        }
        

        if(_timeStartedCountingOffTime > switchOffTime && !startingDelay)
        {
            startingDelay = true;
            _timeStartedCountingDelay = Time.time;
        }

        if(startingDelay)
        {
            _timeStartedCountingDelay += Time.deltaTime;

            toActivate.intensity = activatingCurve.Evaluate(_timeStartedCountingDelay / delayToActivate) * _lightIntensityDelay;

            if (_timeStartedCountingDelay > delayToActivate)
            {
                AkSoundEngine.PostEvent("Intro_Unlocked", Camera.main.gameObject);
                ToSwitchOff.gameObject.SetActive(false);
                toActivate.gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
                
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isEnteredTrigger = true;
    }
}

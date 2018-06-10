using System.Collections.Generic;
using UnityEngine;

public class WaterfallController : MonoBehaviour
{
    ParticleSystem thisParticleSystem;
    ParticleSystem.EmissionModule thisEmission;
    ParticleSystem.EmissionModule splashParticleSystemEmission;
    List<ParticleSystem.Particle> particlesOnEnter = new List<ParticleSystem.Particle>();
    const int defaultRateConstant = 20000;
    const int defaultRateConstantForSplashParticleSystem = 200;

    void OnEnable()
    {
        thisParticleSystem = GetComponent<ParticleSystem>();
        thisEmission = thisParticleSystem.emission;
        splashParticleSystemEmission = GameObject.FindWithTag("Waterfall Splash").GetComponent<ParticleSystem>().emission;

        // Waterfall turned off by default.
        thisEmission.rateOverTime = new ParticleSystem.MinMaxCurve(0);

        // Splash effect will be turned on when needed (see OnParticleTrigger method).
        splashParticleSystemEmission.rateOverTime = new ParticleSystem.MinMaxCurve(0);
    }

    void OnParticleTrigger()
    {
        // Get the particles which matched the trigger conditions this frame.
        int numEnter = thisParticleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particlesOnEnter);

        // Count the entering particles. If there are none, make the splash particle system stop working by setting its rate over time to 0. Otherwise, set the rate to 200 so we can enjoy the splashing of the waterfall!
        if (numEnter > 0)
        {
            splashParticleSystemEmission.rateOverTime = new ParticleSystem.MinMaxCurve(defaultRateConstantForSplashParticleSystem);
        }
        else
        {
            splashParticleSystemEmission.rateOverTime = new ParticleSystem.MinMaxCurve(0);
        }

        // Re-assign the modified particles back into the particle system.
        thisParticleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particlesOnEnter);
    }

    // Update is called once per frame
    void Update()
    {
        // Enabling/disabling support with the Q key for the waterfall.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            int newRateConstant = defaultRateConstant;
            if (thisEmission.rateOverTime.constant > 0)
            {
                newRateConstant = 0;
            }
            thisEmission.rateOverTime = new ParticleSystem.MinMaxCurve(newRateConstant);
        }
    }
}

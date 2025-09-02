using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace Gamekit3D
{
    [RequireComponent(typeof(Slider))]
    public class MixerSliderLink : MonoBehaviour
    {
        public string volumeParameter;

        protected Slider m_Slider;


        void Awake ()
        {
            m_Slider = GetComponent<Slider>();

            float value;
            FMOD.RESULT result = FMODUnity.RuntimeManager.StudioSystem.getParameterByName(volumeParameter, out value);
            if (result == FMOD.RESULT.OK)
            {
                m_Slider.value = value;
            }
            else
            {
                Debug.LogWarning("Failed to get parameter for Mixer: " + result);
            }

            m_Slider.onValueChanged.AddListener(SliderValueChange);
        }


        void SliderValueChange(float value)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName(volumeParameter, 0.5f);
        }
    }
}
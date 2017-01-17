using UnityEngine;
using System.Collections;

namespace Mayhem.World
{
    public class LightFlicker : MonoBehaviour
    {
        private Light m_Light;

        private float[] m_IntensitySmoothing = new float[20];
        private float[] m_RangeSmoothing = new float[20];
        public float IntensityMax;
        public float IntensityMin;
        public float RangeMax;
        public float RangeMin;

        void Start()
        {
            m_Light = GetComponent<Light>();

            for (int i = 0; i < m_IntensitySmoothing.Length; i++)
            {
                m_IntensitySmoothing[i] = 0.0f;
                m_RangeSmoothing[i] = m_Light.range;
            }
        }

        void Update()
        {
            float intensitySum = 0.0f;
            float rangeSum = 0.0f;

            for (int i = 1; i < m_IntensitySmoothing.Length; i++)
            {
                m_IntensitySmoothing[i - 1] = m_IntensitySmoothing[i];
                intensitySum += m_IntensitySmoothing[i - 1];

                m_RangeSmoothing[i - 1] = m_RangeSmoothing[i];
                rangeSum += m_RangeSmoothing[i - 1];
            }

            // Add the new value at the end of the array.
            m_IntensitySmoothing[m_IntensitySmoothing.Length - 1] = Random.Range(IntensityMin, IntensityMax);
            intensitySum += m_IntensitySmoothing[m_IntensitySmoothing.Length - 1];

            m_RangeSmoothing[m_RangeSmoothing.Length - 1] = Random.Range(RangeMin, RangeMax);
            rangeSum += m_RangeSmoothing[m_RangeSmoothing.Length - 1];

            // Compute the average of the array and assign it to the
            // light intensity.
            m_Light.intensity = intensitySum / m_IntensitySmoothing.Length;

            m_Light.range = rangeSum / m_RangeSmoothing.Length;
        }
    }
}
using UnityEngine;

namespace ProjectBlue.LightBeamPerformance
{

    public class Tilt : MonoBehaviour
    {

        Quaternion defaultRotation;

        public Range movableRange = new Range(-70, 70);

        [SerializeField] float lowPassWeight = 0.05f;
        QuaternionLowPassFilter lowPassFilter;

        private void Awake()
        {
            defaultRotation = transform.localRotation;
            lowPassFilter = new QuaternionLowPassFilter(lowPassWeight, transform.localRotation);
        }

        public void SetDefault()
        {
            
            transform.localRotation = lowPassFilter.Append(defaultRotation);
        }

        public void SetRotation(float degree)
        {
            transform.localRotation = lowPassFilter.Append(Quaternion.Euler(degree, 0, 0));

        }

    }
}
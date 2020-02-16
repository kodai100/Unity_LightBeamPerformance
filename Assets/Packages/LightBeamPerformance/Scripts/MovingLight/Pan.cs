using UnityEngine;

namespace ProjectBlue.LightBeamPerformance
{

    public class Pan : MonoBehaviour
    {

        Quaternion defaultRotation;

        public Range movableRange = new Range(-180, 180);

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
            
            transform.localRotation = lowPassFilter.Append(Quaternion.Euler(0, degree, 0));
        }

    }
}
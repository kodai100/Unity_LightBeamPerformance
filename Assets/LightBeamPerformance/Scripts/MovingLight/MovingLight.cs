using UnityEngine;

namespace ProjectBlue.LightBeamPerformance
{
    public class MovingLight : MonoBehaviour
    {
        [SerializeField] private LightHead head;

        [SerializeField] private Pan pan;

        [SerializeField] private Tilt tilt;

        public int group;
        public int address;

        public int globalAddress;

        // own 0 - 1 address offset value in light group
        public float LocalAddressOffset;
        
        public float GetAddressOffset()
        {
            return LocalAddressOffset;
        }

        public void Process()
        {
            head.Process();
        }

        public void SetColor(Color color)
        {
            head.color = color;
        }

        public void SetIntensity(float intensity)
        {
            head.intensity = intensity;
        }

        public void MultiplyIntensity(float intensityMultiplier)
        {
            head.intensity *= intensityMultiplier;
        }

        public void SetPanDefaultAngle()
        {
            if(pan) pan.SetDefault();
        }

        public void SetTiltDefaultAngle()
        {
            if(tilt) tilt.SetDefault();
        }

        public void SetTiltAngle(float angle)
        {
            if(tilt) tilt.SetRotation(angle);
        }

        public void SetPanAngle(float angle)
        {
            if(pan) pan.SetRotation(angle);
        }

        public void LookAt(Vector3 focusPosition)
        {

            var aimVector = focusPosition - transform.position;
            var rotation = Quaternion.LookRotation(aimVector);

            SetPanAngle(rotation.eulerAngles.y);
            SetTiltAngle(rotation.eulerAngles.x);

        }

    }

}
using UnityEngine;

namespace ProjectBlue.LightBeamPerformance
{

    public enum AddressType
    {
        Global, Group
    }

    public class MovingLight : MonoBehaviour
    {
        [SerializeField] LightHead head;

        [SerializeField]
        Pan pan;

        [SerializeField]
        Tilt tilt;

        public int group;
        public int address;

        public int globalAddress;

        // own 0 - 1 address offset value in light group
        public float LocalAddressOffset { get; set; }
        public float GlobalAddressOffset { get; set; }

        public float GetAddressOffset(AddressType addressType)
        {
            if(addressType == AddressType.Global)
            {
                return GlobalAddressOffset;
            }
            else
            {
                return LocalAddressOffset;
            }
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
            pan?.SetDefault();
        }

        public void SetTiltDefaltAngle()
        {
            tilt?.SetDefault();
        }

        public void SetTiltAngle(float angle)
        {
            tilt?.SetRotation(angle);
        }

        public void SetPanAngle(float angle)
        {
            pan?.SetRotation(angle);
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
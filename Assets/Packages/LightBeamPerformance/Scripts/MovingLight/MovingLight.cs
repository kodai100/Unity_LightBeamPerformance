using System.Collections;
using System.Collections.Generic;
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

        public Pan pan;
        public Tilt tilt;

        public int group;
        public int address;
        public float addressOffset;   // own 0 - 1 address offset value in light group

        public int globalAddress;
        public float globalAddressOffset;

        GameObject dummyObj;
        Transform dummyTrans;

        public LightHead Head => head;

        public float GetAddressOffset(AddressType addressType)
        {
            if(addressType == AddressType.Global)
            {
                return globalAddressOffset;
            }
            else
            {
                return addressOffset;
            }
        }

        public void Start()
        {
            dummyObj = new GameObject();
            dummyObj.transform.parent = transform;
            dummyObj.transform.localPosition = Vector3.zero;

            dummyTrans = dummyObj.transform;
        }

        public void DefaultTransform()
        {
            pan.SetDefault();
            tilt.SetDefault();
        }

        public void SetTiltDegree()
        {
        }

        public void LookAt(Transform trans)
        {
            dummyTrans.LookAt(trans);

            Vector3 gimbal = dummyTrans.localRotation.eulerAngles;

            pan.SetRotation(gimbal.y);
            tilt.SetRotation(gimbal.x);

        }

    }

}
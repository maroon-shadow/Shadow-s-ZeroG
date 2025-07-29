using BepInEx;
using GorillaLocomotion;
using Photon.Pun;
using UnityEngine;

namespace ZeroG
{
    [BepInPlugin("Shadow.ZeroG", "Zero Gravity", "1.0.0")]
    internal class Plugin : BaseUnityPlugin
    {
        bool canZerog = true;
        public float cooldownTime = 0.3f;
        private float nextAllowedTime = 0f;
        bool zg = false;
        public void FixedUpdate()
        {
            if (ControllerInputPoller.instance.leftControllerPrimaryButton && Time.time >= nextAllowedTime && NetworkSystem.Instance.GameModeString.Contains("MODDED"))
            {
                zg = !zg;
                nextAllowedTime = Time.time + cooldownTime;
            }
            if (canZerog && zg)
            {
                Rigidbody rb = GTPlayer.Instance.GetComponent<Rigidbody>();
                rb.AddForce(-Physics.gravity, ForceMode.Acceleration);
            }
            if (PhotonNetwork.InRoom == false)
            {
                zg = false;
            }
        }
        void OnEnable()
        {
            canZerog = true;
        }
        void OnDisable()
        {
            canZerog = false;
            zg = false;
        }
    }
}
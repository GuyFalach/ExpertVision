using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsDebugInfo : MonoBehaviour
{
    public OVRHand hand;
    [SerializeField] private HandInfoFrequency handInfoFrequency = HandInfoFrequency.Once;
    private bool pauseDisplay = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (!hand) hand = GetComponent<OVRHand>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space)) pauseDisplay = !pauseDisplay;
#endif
        if(hand.IsTracked && !pauseDisplay)
        {
            
            //Logger.Instance.LogInfo($"{OVRHand.HandFinger.Index} is pinching: {hand.GetFingerIsPinching(OVRHand.HandFinger.Index)}");
            //Logger.Instance.LogInfo($"{OVRHand.HandFinger.Middle} is pinching: {hand.GetFingerIsPinching(OVRHand.HandFinger.Middle)}");
            //Logger.Instance.LogInfo($"{OVRHand.HandFinger.Ring} is pinching: {hand.GetFingerIsPinching(OVRHand.HandFinger.Ring)}");
            //Logger.Instance.LogInfo($"{OVRHand.HandFinger.Pinky} is pinching: {hand.GetFingerIsPinching(OVRHand.HandFinger.Pinky)}");

             //Logger.Instance.LogInfo($"{OVRHand.HandFinger.Index} is pinching: {hand.GetFingerIsPinching(OVRHand.HandFinger.Middle)}");
            
            //Logger.Instance.LogInfo($"{OVRHand.HandFinger.Index} is pinch strenght is: {hand.GetFingerPinchStrength(OVRHand.HandFinger.Index)}");
            //Logger.Instance.LogInfo($"{OVRHand.HandFinger.Index} is pinch strenght is: {hand.GetFingerConfidence(OVRHand.HandFinger.Middle)}");


        }
    }
}

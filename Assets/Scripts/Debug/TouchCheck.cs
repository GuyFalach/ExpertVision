using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class TouchCheck : MonoBehaviour
{
    [SerializeField] private OVRHand hand;
    [SerializeField] private Transform position;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Watch")
        {
            Logger.Instance.LogInfo($"Watch touched");
        }
        else
        {
            Logger.Instance.LogInfo($"{other.gameObject.name} Location is: {other.transform.position}");
        }
    }

}

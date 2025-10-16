using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Avatar2;

public class SwitchAvatar : MonoBehaviour
{
    [SerializeField] private GameObject avatar;
    // Start is called before the first frame update
    void Start()
    {
        avatar.GetComponent<SampleAvatarEntity>().LoadPreset(Random.Range(1, 31));
        
    }
}

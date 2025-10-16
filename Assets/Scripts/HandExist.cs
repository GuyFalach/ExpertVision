using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandExist : MonoBehaviour
{
    [SerializeField] private GameObject visuals;
    [SerializeField] private GameObject watch;

    private void Update()
    {
        if(visuals.activeInHierarchy == true && watch.activeInHierarchy == false)
        {
            watch.SetActive(true);
        }
        else if(visuals.activeInHierarchy == false && watch.activeInHierarchy == true)
        {
            watch.SetActive(false);
        }
    }

    public void DeactivateWatch()
    {
        watch.SetActive(false);
    }

    public void ActivateWatch()
    {
        watch.SetActive(true);
    }
}

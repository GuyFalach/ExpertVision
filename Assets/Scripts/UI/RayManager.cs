using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Core.Singletons;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using System;

enum RayTypes
{
    X,
    V,
    Reset,
    Trash,
    RightArrow,
    LeftArrow,
    Part
}

public class RayManager : MonoBehaviour
{
    [SerializeField] private RayTypes type;
    [SerializeField] private GameObject watch;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    public GameObject gameManager;
    private RayInteractable rayIteractable;
    // Start is called before the first frame update
    void Start()
    {
        rayIteractable = GetComponent<RayInteractable>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(rayIteractable.State == InteractableState.Select)
        {
            InteractionSwitch(type);
        }
    }
    
    private void InteractionSwitch(RayTypes type)
    {
        switch (type)
        {
            case RayTypes.V:
                
                if (gameManager.GetComponent<ChoiceControl>().IsPartExist() == true)
                {
                    gameManager.GetComponent<ChoiceControl>().ClosePart();
                }
                string activePartName = gameManager.GetComponent<ChoiceControl>().GetActivePartName();
                if (activePartName == "")
                {
                    break;
                }
                gameManager.GetComponent<ChoiceControl>().SelectPart(activePartName);
                watch.GetComponent<WatchManager>().CloseWatch();
                break;

            case RayTypes.X:
                watch.GetComponent<WatchManager>().CloseWatch();
                break;

            case RayTypes.Reset:
                if(gameManager.GetComponent<ChoiceControl>().IsPartExist() == true)
                {
                    gameManager.GetComponent<ChoiceControl>().ClosePart();
                    InteractionSwitch(RayTypes.V);
                }
                else
                {
                    Logger.Instance.LogError("Part dont exist!");
                }
                watch.GetComponent<WatchManager>().CloseWatch();
                break;

            case RayTypes.Trash:
                if(gameManager.GetComponent<ChoiceControl>().IsPartExist() == true)
                {
                    gameManager.GetComponent<ChoiceControl>().ClosePart();
                }
                else
                {
                    Logger.Instance.LogError("Part don't exist!");
                }
                watch.GetComponent<WatchManager>().CloseWatch();
                break;

            case RayTypes.Part:
                Debug.LogWarning("Entered Part ray");
                string rayName = rayIteractable.gameObject.name;
                GameObject displayContainer = GameObject.Find(rayName + "Display");
                string partName = GetActiveChildName(displayContainer.transform.Find("PartName").gameObject);
                Debug.LogWarning("Ray PartName: " + partName);
                gameManager.GetComponent<ChoiceControl>().SetActivePart(partName, rayName[4] - '1', watch);
                break;

            case RayTypes.LeftArrow:
                watch.GetComponent<WatchManager>().SwitchDisplayed(false);
                break;

            case RayTypes.RightArrow:
                watch.GetComponent <WatchManager>().SwitchDisplayed(true);
                break;

            default:
                Logger.Instance.LogInfo($"Something unexpected happened!");
                watch.GetComponent<WatchManager>().CloseWatch();
                break;
        }
    }

    private string GetActiveChildName( GameObject parent )
    {
        if (parent != null)
        {
            // Loop through each child of the parent GameObject
            foreach (Transform child in parent.transform)
            {
                // Check if the child is active
                if (child.gameObject.activeSelf)
                {
                    // Remove the last char
                    return child.gameObject.name[..^1];
                }
            }
        }
        return null;
    }
}

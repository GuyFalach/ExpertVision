using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singletons;
using TMPro;
using Unity.VisualScripting;

public enum AnimationType
{
    START,
    IDLE,
    CLOSE
}
public class WatchManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private Camera _camera;
    [SerializeField] private TextMeshProUGUI textContainer; 
    private GameObject[] renderContainers = null; //Keep in mind, after the models menu is operational switch to private.
    private int maxPart = 0;
    private int displayIndex = 5;
    private float elapsedTime = 0f;
    private bool switchRowActivated = false; //Determain whether the left or right menu arrow where chosen
    
    public void ReactivateModelMenu()
    {
        menu.transform.GetChild(2).gameObject.SetActive(false); //PartsMenu
        menu.transform.GetChild(1).gameObject.SetActive(true); //ModelsMenu
        maxPart = 0;
    }

    public void ActivateModelPartSet(GameObject partSetPrefab)
    {
        GameObject displayRenderer = menu.transform.GetChild(2).Find("DisplayRenderers").gameObject;
        if (displayRenderer.transform.childCount == 1)
        {
            Destroy(displayRenderer.transform.GetChild(0).gameObject);
        }

        GameObject newSet = Instantiate(partSetPrefab, displayRenderer.transform); //The partant is Menu -> PartsMenu -> DislayRenderers 
        InitPartSet(newSet);
        maxPart = renderContainers[0].transform.Find("PartName").childCount; //Getting the child count of the first and longest container
        displayIndex = maxPart - 1;
    }

    private void InitPartSet(GameObject partSets)
    {
        if (renderContainers == null)
        {
            renderContainers = new GameObject[partSets.transform.childCount];
        }
        for (int i = 0;i < renderContainers.Length;i++)
        {
            renderContainers[i] = partSets.transform.GetChild(i).gameObject;
        }
    }

    /**
     * The funciton switch the set displayed object in the top manu
     * @param next: Determine whether to switch to the next or pervious set. If next set to true switches next otherwise switches pervious
     */
    public void SwitchDisplayed(bool next)
    {
        if (switchRowActivated == false)
        {
            int added = (next == true) ? 1 : -1;
            int originalIndex = displayIndex;
            displayIndex = (displayIndex + added + maxPart) % maxPart;
            //Make the  switch base on the caluclated index
            for (int i = 0; i < renderContainers.Length; i++)
            {
                Transform partContain = renderContainers[i].transform.Find("PartName");
                partContain.GetChild(originalIndex).gameObject.SetActive(false);
                partContain.GetChild(displayIndex).gameObject.SetActive(true);
            }
            switchRowActivated = true;
        }
    }

    private void Start()
    {
        CloseWatch();
    }

    private void Update()
    {
        if (switchRowActivated == true)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 0.5f) // Check if half a second has passed
            {
                switchRowActivated = false;
                elapsedTime = 0;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            Logger.Instance.LogWarning("Whatch tag: " + other.tag + " Parant Name: " + other.gameObject.transform.parent.name + " Name:" + other.gameObject.name + "... ");
        }
        catch
        {
            Logger.Instance.LogWarning("Whatch tag: " + other.tag + " Name: " + other.gameObject.name + "... ");

        }

        if (other.gameObject.CompareTag("Player"))
        {
            Logger.Instance.LogWarning("Watch was triggered!");
            WatchTouched();
        }
    }

    /**
     * The funciton relplave the previous highlited part to a new given part
     * @param renderTexture: The image of the new part to highlite
     * @param textMeshPro: The new part's name and information
     */
    public void IntialPartMenu(RenderTexture renderTexture, TextMeshProUGUI textMeshPro)
    {
        MeshRenderer meshRenderer = menu.transform.GetChild(1).Find("ObjectPreview").GetComponent<MeshRenderer>();
        meshRenderer.material.mainTexture = renderTexture;
        textContainer.text = textMeshPro.text;
    }


    /**
     * Function change the watch state, from open to close and from close to open. 
     */
    private void WatchTouched()
    {
        Logger.Instance.LogWarning("Watch opened...");
        OpenWatch();
    }

    /**
     * Open the menu. 
     */
    private void OpenWatch() 
    {
        OpenMenu();
    }

    /**
     * Close the menu
     */
    public void CloseWatch()
    {
        Logger.Instance.LogInfo("Watch was Closed!!...");
        CloseMenu();
    }

    /**
     * Open the menu list 
     */
    private void OpenMenu()
    {
        menu.SetActive(true);
        menu.transform.position = _camera.GetComponent<Camera>().transform.position + _camera.GetComponent<Camera>().transform.forward;
        menu.transform.LookAt(_camera.GetComponent<Camera>().transform);
    }

    /**
     * Close the menu list
     */
    private void CloseMenu()
    {
        menu.SetActive(false);
    }
}

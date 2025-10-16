using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class ChoiceControl : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private GameObject grabInteractable;
    [SerializeField] private RenderTexture[] renderTextures;
    private bool partExist = false;
    private GameObject part;
    private Vector3 originalScale;
    private Quaternion _originalRotation;
    private readonly string INTERACTABLE = "HandGrabInteractable";
    private string activePartName = "";
    public string modelName = "Server";
    
    /**
     * @return: The name of the part displayed on the main menu
     */
    public string GetActivePartName()
    {
        return activePartName;
        
    }
    
    /**
     * The function sets the highlighter part in the menu and save it
     * @param partName: The name of the part to highlight
     * @param partContainer: The object where the part's render is stored
     * @param watch: The menu controller
     */
    public void SetActivePart(string partName, int partIndex, GameObject watch)
    {
        Debug.LogWarning("Original PartName: " +  partName);
        activePartName = partName;
        partName += "/";
        GameObject partDisplay = GameObject.Find(partName + "/");
        Debug.LogWarning("active part name: " + activePartName);
        Transform partTextDisplay = partDisplay.transform.Find("Canvas").GetChild(0); ;
        watch.GetComponent<WatchManager>().IntialPartMenu(renderTextures[partIndex], partTextDisplay.gameObject.GetComponent<TextMeshProUGUI>());
    }

    /**
     * Check if a part exist
     * @return: Wheter the part exist of not
     */
    public bool IsPartExist()
    {
        return partExist;
    }

    /**
     * The function display the given part in the user hand 
     * @parm: partName - The name of the part to display (the function assums that theres only one object with this name)
     */
    public void SelectPart(string partName)
    {
        GameObject originalPart = GameObject.Find(partName);
        if (originalPart != null)
        {
            Logger.Instance.LogWarning("Instating the object...");

            Vector3 eyePos = new(_camera.position.x + 0.5f, _camera.position.y, _camera.position.z);
            Quaternion originalRotation = originalPart.transform.rotation;
            CreatePart(originalPart, originalRotation);
        }
        else
        {
            Debug.LogWarning(partName + "Object not found!");
        }
    }

    /**
     * The function adds the interactions components and activate the part
     * @Param originalPart: the part exist in the model
     * @Param originalRotation: The rotation of the part in the model
     */
    private void CreatePart(GameObject originalPart, Quaternion originalRotation)
    {
        //part = Instantiate(originalPart, handPos, originalRotation);
        Vector3 partNawPos = _camera.GetComponent<Camera>().transform.position + _camera.GetComponent<Camera>().transform.forward;
        string partName = "Servers/" + originalPart.name;
        part = PhotonNetwork.Instantiate(partName, partNawPos, originalRotation);
        part.SetActive(false);
        part.transform.localScale = originalPart.transform.lossyScale;
        //part.GetComponent<Grabbable>().enabled = true;
        //part.tag = "Untagged";
        GameObject handInteractable = part.transform.Find(INTERACTABLE).gameObject;
        if (handInteractable != null) { handInteractable.SetActive(true); }
        else { Debug.LogWarning("Hand interactable not found!"); }

        //part.transform.position = _camera.GetComponent<Camera>().transform.position + _camera.GetComponent<Camera>().transform.forward;
        _originalRotation = part.transform.rotation;
        originalScale = originalPart.transform.lossyScale;
        //Destroy(part.GetComponent<Rotating>());
        //DetectRigidbody(part);
        part.SetActive(true);
        partExist = true;
    }

    private void DetectRigidbody(GameObject part)
    {
        for (int i=0;i<part.transform.childCount;i++)
        {
            if (part.transform.GetChild(i).gameObject.GetComponent<Rigidbody>() != null)
            {
                Destroy(part.transform.GetChild(i).gameObject.GetComponent<Rigidbody>());
            }
        }
    }

    /**
     * The function destroy the active object
     */
    public void ClosePart()
    {
        if (part != null)
        {
            PhotonNetwork.Destroy(part);
           //Destroy(part);
            part = null;
            partExist = false;
        }
    }

    /**
     * The function reset the part transform to its original scale and rotation
     */
    public void ResetTrans()
    {
        part.transform.SetPositionAndRotation(_camera.GetComponent<Camera>().transform.position + _camera.GetComponent<Camera>().transform.forward, _originalRotation);
        part.transform.localScale = originalScale;
    }
}

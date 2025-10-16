using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

enum BoardButtonType
{
    Play
}

public class BoardController : MonoBehaviour
{
    [SerializeField] private BoardButtonType type;
    private GameObject board;
    private RayInteractable rayIteractable;

    private bool videoPlaying = false;
    private bool videoPaused = false;
    private float elapsedTime = 0f;
    private bool touchPause = false;
    // Start is called before the first frame update
    void Start()
    {
        rayIteractable = GetComponent<RayInteractable>();
        board = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (touchPause == true)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 0.5f) // Check if half a second has passed
            {
                touchPause = false;
                elapsedTime = 0;
            }
        }
        if (rayIteractable.State == InteractableState.Select)
        {
            InteractionSwitch(type);
        }
    }

    private void InteractionSwitch( BoardButtonType type )
    {
        switch (type)
        {
            case BoardButtonType.Play:
                if(videoPlaying == true)
                {
                    ActivateVidieo();
                    return;
                }
                board.GetComponent<VideoPlayer>().Play();
                GameObject playButton = board.transform.GetChild(0).gameObject;
                playButton.GetComponent<Animator>().enabled = true;
                Debug.LogWarning("Animation is playing");
                videoPlaying = true;
                break;
            default:
                Debug.LogError("Action not found");
                Logger.Instance.LogError("Action not found");
                break;
        }
    }

    private void ActivateVidieo()
    {
        if(touchPause == true)
        {
            return;
        }
        if (videoPaused == false)
        {
            board.GetComponent<VideoPlayer>().playbackSpeed = 0;
            videoPaused = true;
        }
        else
        {
            board.GetComponent<VideoPlayer>().playbackSpeed = 1;
            videoPaused = false;
        }
        touchPause = true;
    }
}

using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

enum AvatarJoints //The exact place in the highary of those elements in avatar gameobject (Exist only at runtime)
{
    Chest,
    Head,
    LeftHand,
    RightHand
}

public class EyeSync : MonoBehaviourPun
{
    private EyeState eyeState = new EyeState();
    private GameObject centerEye;

    private void Start()
    {
        centerEye = GameObject.Find("CenterEyeAnchor");
        if (centerEye == null )
        {
            Debug.LogError("Couldnt find eyes");
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            // Update the avatar state based on the player's body movements
            SendPositionRotaion();
            UpdateAvatarState();
        }
        else
        {
            // Apply the received avatar state to the remote avatar
            ApplyRemoteAvatarState();
        }
    }

    private void UpdateAvatarState()
    {
       transform.SetPositionAndRotation(centerEye.transform.position, centerEye.transform.rotation);
    }

    private void ApplyRemoteAvatarState()
    {
        transform.SetPositionAndRotation(eyeState.position, eyeState.rotation);
    }

    public void SendPositionRotaion()
    {
        if(PhotonNetwork.CurrentRoom.Players.Count >= 2)
        {
            Player otherPlayer = GetOtherPlayer();
            photonView.RPC("GetPositionRotaion", otherPlayer, transform.position, transform.rotation);
        }
    }

    [PunRPC]
    public void GetPositionRotaion(Vector3 position, Quaternion rotation)
    {
        eyeState.position = position;
        eyeState.rotation = rotation;
    }

    private Player GetOtherPlayer()
    {
        Player localPlayer = PhotonNetwork.LocalPlayer;
        foreach (var playerEntry in  PhotonNetwork.CurrentRoom.Players)
        {
            Player player = playerEntry.Value;
            if(player.Equals(localPlayer) == false)
            {
                return player;
            }
        }
        return null;
    }
}
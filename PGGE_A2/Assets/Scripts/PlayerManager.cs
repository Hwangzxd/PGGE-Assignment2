using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public string mPlayerPrefabName;
    public PlayerSpawnPoints mSpawnPoints;

    [HideInInspector]
    public GameObject mPlayerGameObject;
    [HideInInspector]
    private ThirdPersonCamera mThirdPersonCamera;

    private void Start()
    {
        Transform randomSpawnTransform = mSpawnPoints.GetSpawnPoint();
        mPlayerGameObject = PhotonNetwork.Instantiate(mPlayerPrefabName,
            randomSpawnTransform.position,
            randomSpawnTransform.rotation,
            0);

        // use PhotonView to call the "SetNickname" RPC method on all buffered clients, updating their player nickname
        mPlayerGameObject.GetComponent<PhotonView>().RPC("SetNickname", RpcTarget.AllBuffered, PhotonNetwork.NickName);

        mThirdPersonCamera = Camera.main.gameObject.AddComponent<ThirdPersonCamera>();

        //mPlayerGameObject.GetComponent<PlayerMovement>().mFollowCameraForward = false;
        mThirdPersonCamera.mPlayer = mPlayerGameObject.transform;
        mThirdPersonCamera.mDamping = 20.0f;
        mThirdPersonCamera.mCameraType = CameraType.Follow_Track_Pos_Rot;
    }

    public void LeaveRoom()
    {
        Debug.LogFormat("LeaveRoom");
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        //Debug.LogFormat("OnLeftRoom()");
        SceneManager.LoadScene("Menu");
    }

}

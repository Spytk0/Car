using UnityEngine;
using Photon.Pun;

public class OnlinePlayer : MonoBehaviourPunCallbacks
{
    public static GameObject LocalPlayerInstance;

    private void Awake()
    {
        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
        }
        else
        {
            string playerName = null;
            Color playerColor = Color.white;

            if (photonView.InstantiationData != null)
            {
                playerName = (string)photonView.InstantiationData[0];
                playerColor = ColorCar.IntToColor((int)photonView.InstantiationData[1], (int)photonView.InstantiationData[2], (int)photonView.InstantiationData[3]);
            }

            if (playerName != null)
            {
                GetComponent<CarApperance>().SetNameAndColor(playerName, playerColor);
            }

        }

    }
}

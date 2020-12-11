using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleView2D : MonoBehaviour
{


    public void OnClickStartBtn()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }
}

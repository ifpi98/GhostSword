using UnityEngine;
using System.Collections;

public class CIntroScene : MonoBehaviour {

    public GameObject _IntroAni;


    void Awake()
    {
        Time.timeScale = 1;
        if (Application.loadedLevelName == "Main")
        {
            Debug.Log("메인 이벤트");
            _IntroAni.SetActive(true);
        }
        if (Application.loadedLevelName == "Game")
        {
            Debug.Log("게임 이벤트");
            _IntroAni.SetActive(true);
        }
    }


	// Use this for initialization
	void Start () {
        GetPrefsIdPwd();  
	}

	
	// Update is called once per frame
	void Update () {
        
	
	}


    public void GetPrefsIdPwd()
    {
        CLogin.user_id = PlayerPrefs.GetString("id");
        CLogin.user_passwd = PlayerPrefs.GetString("pwd");
    }
}

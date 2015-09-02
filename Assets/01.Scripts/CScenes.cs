using UnityEngine;
using System.Collections;

public class CScenes : MonoBehaviour {

    public GameObject _popUp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GoMain()
    {
        Application.LoadLevel("Main");

    }

    public void GoGame()
    {
        Application.LoadLevel("Game");

      CGameManager._enemyCount = 0;
    CGameManager._killCount = 0;
    CGameManager._comboCount = 0;
    CGameManager._maxComboCount = 0;
    CGameManager._ComboResetCount = 3.5f;
    CGameManager._balance = 1f;
    CGameManager._comboSucess = false;



    CPlayer._playerLv = 1;
    CPlayer._nowExp = 0f;
    CPlayer._nowMaxExp = 10;
    CPlayer._maxHp = 100;
    CPlayer._hp = 100;
    CPlayer._maxMp = 100;
    CPlayer._mp = 0;
    CPlayer._pointCount = 0;
    CPlayer._skillOn = false;
    CPlayer._playerBalance = 1;
    }

    public void GoResult()
    {
        Time.timeScale = 1;
        Application.LoadLevel("End");
    }

    public void Pause()
    {
        Time.timeScale = 0;
        _popUp.SetActive(true);
        
    }


    public void UnPause()
    {
        Time.timeScale = 1;
        _popUp.SetActive(false);
    }

    public void Open()
    {
        
        _popUp.SetActive(true);

    }


    public void Close()
    {
        
        gameObject.SetActive(false);
    }

}

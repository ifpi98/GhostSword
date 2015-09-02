using UnityEngine;
using System.Collections;

public class CUIDissable : MonoBehaviour {

    
    public GameObject _skillFire;
    public CPlayer _player;

	// Use this for initialization
	void Start () {

     
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void DissMe()
    {        
        gameObject.SetActive(false);
    }

    public void SetActiveMe()
    {
        gameObject.SetActive(true);
    }

    public void ShakeMe()
    {
        iTween.ShakePosition(Camera.main.gameObject, iTween.Hash("x", 0.2f, "y", 0.2f, "time", 1f));
    }
    public void DelSkillFire()
    {
        _skillFire.SetActive(false);
    }
    public void SkillFire()
    {
        _skillFire.SetActive(true);
    }

    public void SkillAniEnd()
    {
        
        _player.SkillEnd();
    }

}

using UnityEngine;
using System.Collections;

public class CShootE : MonoBehaviour {

    public float _damage = 20;
    public GameObject _crashEff;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        
        if(collider.tag == "SHOOT")
        {
            _crashEff.SetActive(true);     
            
        }   

    }

}

using UnityEngine;
using System.Collections;

public class CItem : MonoBehaviour {

    public float _hpValue;
    public float _mpValue;
    public float _pointValue;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
                    
            Vector2 pos = transform.position;         

                if (pos.x > 7.5f)
                {
                    pos.x = 7.5f;
                    transform.position = pos;

                }
                if (pos.x < -7.2f)
                {
                    pos.x = -7.2f;
                    transform.position = pos;

                }
	
	}
       
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "PLAYER")
        {
            Destroy(gameObject);
        } 
    }

}






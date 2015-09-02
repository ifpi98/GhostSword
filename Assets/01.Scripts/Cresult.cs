using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Cresult : MonoBehaviour {

    public Text killCountText;
    public Text MaxComboText;
    public Text PointCountText;
    public Text TotalCountText;


	// Use this for initialization
	void Start () {

        killCountText.text = CGameManager._killCount.ToString();
        MaxComboText.text = CGameManager._maxComboCount.ToString();
        PointCountText.text = CPlayer._pointCount.ToString();

        float _totalValue = (CGameManager._killCount * 10) + (CGameManager._maxComboCount * 3) + (CPlayer._pointCount * 20);
        TotalCountText.text = _totalValue.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

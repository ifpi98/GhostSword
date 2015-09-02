using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Boomlagoon.JSON;

public class CJoin : MonoBehaviour {

    CScenes _cscenes;

    public GameObject _errPopup;
    public InputField _idText;
    public InputField _pwdText1;
    public InputField _pwdText2;
    


    private string url = "http://ec2-52-69-158-14.ap-northeast-1.compute.amazonaws.com/index.php/ghostctrl/insert_game_user";


    // Use this for initialization
    void Start()
    {
        _cscenes = gameObject.GetComponent<CScenes>();
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void JoinGo()
    {
        StartCoroutine(GameJoin());
    }


    IEnumerator GameJoin()
    {

        if (!(_idText.text == "" || _pwdText1.text == "" || _pwdText2.text == ""))
        {

            WWWForm wwwSend = new WWWForm();

            wwwSend.AddField("user_id", _idText.text);
            wwwSend.AddField("user_passwd", _pwdText1.text);
            WWW wwwGet = new WWW(url, wwwSend);

            yield return wwwGet;

            if (wwwGet.error == null)
            {
                JSONObject json = JSONObject.Parse(wwwGet.text);
                string resultCode = json.GetString("d_result_code");

                if (resultCode.Trim() == "1")
                {
                    Debug.Log("ok " + resultCode.Trim());
                    PlayerPrefs.SetString("id", _idText.text);
                    PlayerPrefs.SetString("pwd", _pwdText1.text);
                    
                    _errPopup.SetActive(true);
                    _errPopup.GetComponentInChildren<Text>().text = "Create Account Success!!";
                    _cscenes.UnPause();
                    
                    
                }
                else // resultcode -1.
                {
                    Debug.Log("-1 = " + resultCode.Trim());
                    _errPopup.SetActive(true);
                    _errPopup.GetComponentInChildren<Text>().text = "Account Is Being Used";
                }
            }
            else // wwwGet.error !null.
            {
                Debug.Log("err = null");
                _errPopup.SetActive(true);
                _errPopup.GetComponentInChildren<Text>().text = "Can't Connect Server";
            }
        }
        else // not input id or pwd.
        {
            Debug.Log("not input");
            _errPopup.SetActive(true);
            _errPopup.GetComponentInChildren<Text>().text = "Check ID And Password";
        }

    }
}


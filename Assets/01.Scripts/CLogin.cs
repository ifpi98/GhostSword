using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Boomlagoon.JSON;

public class CLogin : MonoBehaviour
{

    public static string user_id;
    public static string user_passwd;
    public static string user_kill;
    public static string user_combo;
    public static string user_total;
    CScenes _cscenes;

    public GameObject _errPopup;
    public InputField _idText;
    public InputField _pwdText;


    private string url = "http://ec2-52-69-158-14.ap-northeast-1.compute.amazonaws.com/index.php/ghostctrl/select_user_info";


    // Use this for initialization
    void Start()
    {            
        _cscenes = gameObject.GetComponent<CScenes>();
        if (user_id != null && user_passwd != null)
        {
            _idText.text = user_id;
            _pwdText.text = user_passwd;
        }
    }



    // Update is called once per frame
    void Update()
    {

    }

    public void LoginGo()
    {
        StartCoroutine(GameLogin());
    }


    IEnumerator GameLogin()
    {

        if (!(_idText.text == "" || _pwdText.text == ""))
        {

            WWWForm wwwSend = new WWWForm();
            
            wwwSend.AddField("user_id", _idText.text);
            wwwSend.AddField("user_passwd", _pwdText.text);
            WWW wwwGet = new WWW(url, wwwSend);

            yield return wwwGet;

            if (wwwGet.error == null)
            {
                JSONObject json = JSONObject.Parse(wwwGet.text);
                string resultCode = json.GetString("d_result_code");

                if (resultCode.Trim() == "1")
                {                    
                    user_id = json.GetString("d_user_id");
                    user_kill = json.GetString("d_user_kill");
                    user_combo = json.GetString("d_user_combo");
                    user_total = json.GetString("d_user_total");

                    PlayerPrefs.SetString("id", _idText.text);
                    PlayerPrefs.SetString("pwd", _pwdText.text);
                    _cscenes.GoGame();
                }
                else // resultcode 0 or -1.
                {                    
                    _errPopup.SetActive(true);
                    _errPopup.GetComponentInChildren<Text>().text = "Check ID And Password";
                }
            }
            else // wwwGet.error !null.
            {                
                _errPopup.SetActive(true);
                _errPopup.GetComponentInChildren<Text>().text = "Can't Connect Server";
            }
        }
        else // not input id or pwd.
        {            
            _errPopup.SetActive(true);
            _errPopup.GetComponentInChildren<Text>().text = "Check ID And Password";
        }

    }
}


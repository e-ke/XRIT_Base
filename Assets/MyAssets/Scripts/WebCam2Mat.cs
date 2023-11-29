using UnityEngine;

// Webカメラ
public class WebCam2Mat : MonoBehaviour
{
    [SerializeField] private Material mat;
    
    WebCamTexture webCamTex;
    WebCamDevice[] devices;

    // カメラの選択
    int selectCamera = 0;
    // getter
    public int GetSelectCamera() { return selectCamera; }

    // スタート時に呼ばれる
    void Start()
    {
        devices = WebCamTexture.devices;
        if (this.webCamTex == null)
        {
            // this.webCamTex = new WebCamTexture(devices[selectCamera].name, 3840, 1920); // Resolution you want
            // this.webCamTex = new WebCamTexture(devices[selectCamera].name);
            foreach(var device in devices)
            {
                Debug.Log("device.name:"+device.name);
                if(device.name == "RICOH THETA UVC")
                {
                    // this.webCamTex = new WebCamTexture(device.name); // 動作確認済み
                    this.webCamTex = new WebCamTexture(device.name, 3840, 1920);
                    break;
                }
            }
        }
        // 再生
        if (!this.webCamTex.isPlaying) webCamTex.Play();
        // マテリアルのテクスチャにwebカメラテクスチャを適用

        // if (webCamTex.width>100 && mat != null) mat.mainTexture = webCamTex;
        if (mat != null) mat.mainTexture = webCamTex;
    }

    // カメラの変更
    public void SwitchCamera()
    {
        // カメラの取得
        devices = WebCamTexture.devices;

        // カメラが1個の時は無処理
        if (devices.Length <= 1) return;

        // カメラの切り替え
        selectCamera++;
        if (selectCamera >= devices.Length) selectCamera = 0;
        if(this.webCamTex.isPlaying) this.webCamTex.Stop();  // 再生中なら停止
        this.webCamTex = new WebCamTexture(devices[selectCamera].name);
        // this.mat.mainTexture = this.webCamTex;
        if (mat != null)
        {
            mat.mainTexture = webCamTex;
            this.webCamTex.Play();
        }
    }
}


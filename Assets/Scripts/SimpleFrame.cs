using UnityEngine;

public class SimpleFrame : MonoBehaviour
{
    public Vector2 frameSize = new Vector2(1f, 1f); // 枠のサイズ
    public float thickness = 0.05f;                 // 枠線の太さ
    public Color frameColor = Color.white;          // 枠の色

    void Start()
    {
        // LineRendererを使って枠を描画
        LineRenderer line = gameObject.AddComponent<LineRenderer>();

        line.useWorldSpace = false; // 親（Empty）に追従させる
        line.loop = true;           // 線を閉じて四角形にする
        line.positionCount = 4;

        // 線の太さを設定
        line.startWidth = thickness;
        line.endWidth = thickness;

        // 色とマテリアルの設定
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = frameColor;
        line.endColor = frameColor;

        // 四隅の座標を計算
        float hw = frameSize.x / 2f;
        float hh = frameSize.y / 2f;

        line.SetPosition(0, new Vector3(-hw, hh, 0)); // 左上
        line.SetPosition(1, new Vector3(hw, hh, 0)); // 右上
        line.SetPosition(2, new Vector3(hw, -hh, 0)); // 右下
        line.SetPosition(3, new Vector3(-hw, -hh, 0)); // 左下
    }
}

using UnityEngine;

public class Car : MonoBehaviour
{
    [Tooltip("汽車品牌")]
    public string brand = "Suzuki";
    [Tooltip("CC數"), Range(0, 9999)]
    public int ccAmount = 1000;
    [Range(0, 200)]
    public float speed = 60.5f;
    [Tooltip("重量(kg)"), Range(0, 9999)]
    public float weight = 980f;
    [Tooltip("是否有天窗")]
    public bool hasSunroof = false;
    public Color color = new Color32(100,100,255,255);
}

using UnityEngine;

public class LimitFramerate : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
    }
}

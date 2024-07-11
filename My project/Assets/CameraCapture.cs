using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CameraCapture : MonoBehaviour
{
    public Camera mainCamera;
    public Camera fisheyeCamera;
    public Transform cubeTransform; 

    private void Start()
    {
        // 输出摄像头位置
        Debug.Log($"Main Camera Position: {mainCamera.transform.position}");
        Debug.Log($"Fisheye Camera Position: {fisheyeCamera.transform.position}");

        // 捕获RGB图片
        CaptureRGB(mainCamera, "mainCamera_RGB.png");
        CaptureRGB(fisheyeCamera, "fisheyeCamera_RGB.png");

        // 捕获深度图
        CaptureDepth(mainCamera, "mainCamera_Depth.png");
        CaptureDepth(fisheyeCamera, "fisheyeCamera_Depth.png");
    }

    private void CaptureRGB(Camera camera, string filename)
    {
        RenderTexture rt = new RenderTexture(camera.pixelWidth, camera.pixelHeight, 24);
        camera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(camera.pixelWidth, camera.pixelHeight, TextureFormat.RGB24, false);
        camera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, camera.pixelWidth, camera.pixelHeight), 0, 0);
        camera.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, filename), bytes);
    }

    private void CaptureDepth(Camera camera, string filename)
    {

    }
}
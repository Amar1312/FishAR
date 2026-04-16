using System.Collections;
using System.Collections.Generic;
using NatSuite.Recorders;
using NatSuite.Recorders.Clocks;
using NatSuite.Recorders.Inputs;
using UnityEngine;
using UnityEngine.Events;

public class ARRecorder : MonoBehaviour
{
    [Header(@"Recording")]
    public Camera videoCamera;
    public int videoWidth = 720;

    private MP4Recorder recorder;
    private CameraInput cameraInput;
    private string lastVideoPath;
    //public VideoPlayerManager _VideoPlayerManager;
    public GameObject VideoMessage,_coinAnimation,logoParent,logodisable,_recordingText;


    public void StartRecording()
    {
        //BehaviousController.Instance.Recording = true;
        // Compute the video width dynamically to match the screen's aspect ratio
        var videoHeight = (int)(videoWidth / videoCamera.aspect);
        videoHeight = videoHeight >> 1 << 1;
        //_VideoPlayerManager.VideoWidth = videoWidth;
        //_VideoPlayerManager.VideoHeight = videoHeight;

        var clock = new RealtimeClock();
        recorder = new MP4Recorder(videoWidth, videoHeight, 30);
        cameraInput = new CameraInput(recorder, clock, videoCamera);
        // Attach an optimized frame input to the camera input for better performance
        //        if (Application.platform == RuntimePlatform.Android)
        //            cameraInput.frameInput = new GLESRenderTextureInput(recorder, multithreading: true);
        //        else if (Application.platform ==  RuntimePlatform.IPhonePlayer)
        //            cameraInput.frameInput = new MTLRenderTextureInput(recorder, multithreading: true);
        _recordingText.SetActive(true);
        if (Screen.orientation == ScreenOrientation.Portrait)
            Screen.orientation = ScreenOrientation.Portrait;
        else if (Screen.orientation == ScreenOrientation.LandscapeLeft)
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        else if (Screen.orientation == ScreenOrientation.LandscapeLeft)
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        else if (Screen.orientation == ScreenOrientation.LandscapeRight)
            Screen.orientation = ScreenOrientation.LandscapeRight;

    }

    public async void StopRecording()
    {
        // Stop camera input and recorder
        cameraInput.Dispose();
        _recordingText.SetActive(false);
        lastVideoPath = await recorder.FinishWriting();
        NativeGallery.SaveVideoToGallery(lastVideoPath, Application.productName,
            "ScreenRecord_" + Application.productName + Random.Range(1000, 10000));

        //BehaviousController.Instance.Recording = false;
        // VideoMessage.SetActive(true);
        logoParent.SetActive(true);
        logodisable.SetActive(false);
        _coinAnimation.SetActive(true);
        Invoke(nameof(OffVideoMessage), 3.1f);
        //_VideoPlayerManager.VideoPath = lastVideoPath;
        //VideoRecordingDone.Invoke();
    }

    void OffVideoMessage()
    {
        //VideoMessage.SetActive(false);
        PointManager.Instance.AddPoint(100);
        UIManager.Instance.DisplayPoint();
        _coinAnimation.SetActive(false);
        logoParent.SetActive(false);
        logodisable.SetActive(true);
        Invoke ("Message",0.2f);

    }
    void Message()
    {
        VideoMessage.SetActive(true);
        Invoke("OffMessage", 2f);
    }
    void OffMessage()
    {
        VideoMessage.SetActive(false);
    }

    //public UnityEvent VideoRecordingDone;
}

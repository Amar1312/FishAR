using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Amar
{
    public class TakePicture : MonoBehaviour
    {
        public GameObject Screeshot,_logoPanel,_coinAnimation;
        public Image _logoImage;
        public RawImage _image;
        public List<GameObject> _DeactiveCanvas;
        public int _pointAmount;
        


        // Start is called before the first frame update
        void Start()
        {
            PointManager _pointManager = PointManager.Instance;

            _pointManager.Loadimage(_pointManager._shopOwnerResponce.result.logo, _logoImage, 250f);
            _logoPanel.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void TakeCameraPicture()
        {
            /*			//ScreenshotHandler.TakeScreenshot_Static(Screen.width, Screen.height);
			#if UNITY_ANDROID
						if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
						{
							Permission.RequestUserPermission(Permission.ExternalStorageWrite);
						}
			#endif
						//NativeToolkit.SaveScreenshot(DateTime.Now.ToString(CultureInfo.InvariantCulture), Application.productName);*/



            //	StartCoroutine(Screenshotsceen());

            StartCoroutine(TakeScreenshotAndSave());
            PointManager.Instance.AddPoint(_pointAmount);
            UIManager.Instance.DisplayPoint();
        }

        private IEnumerator TakeScreenshotAndSave()
        {
            for (int i = 0; i < _DeactiveCanvas.Count; i++)
            {
                _DeactiveCanvas[i].SetActive(false);
            }
            _logoPanel.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            yield return new WaitForEndOfFrame();

            Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            ss.Apply();
            _image.texture = ss;
            _coinAnimation.SetActive(true);

            StartCoroutine(IenumStartScreenshot());
            NativeGallery.SaveImageToGallery(ss, "Toll", "Toll.png");
            _image.texture = ss;

            // Save the screenshot to Gallery/Photos
            //Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(ss, "GalleryTest", "Image.png"));

            yield return new WaitForSeconds(5.0f);
            // To avoid memory leaks
            Destroy(ss);
            
        }


        IEnumerator IenumStartScreenshot()
        {
            
            Screeshot.SetActive(true);
            yield return new WaitForSeconds(4.0f);
            Screeshot.SetActive(false);
            _logoPanel.SetActive(false);
            
            for (int i = 0; i < _DeactiveCanvas.Count; i++)
            {
                _DeactiveCanvas[i].SetActive(true);
            }
            

        }
    }
}


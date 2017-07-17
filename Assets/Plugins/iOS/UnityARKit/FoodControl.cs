using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

namespace UnityEngine.XR.iOS
{
	public class FoodControl : MonoBehaviour {

		public GameObject foodTemplate;
		private UnityARCameraManager m_ARCameraManager;

		void Start()
		{
			m_ARCameraManager = GameObject.Find("ARCameraManager").GetComponent<UnityARCameraManager>();
		}

        bool HitTestWithResultType (ARPoint point, ARHitTestResultType resultTypes)
        {
            List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, resultTypes);
            if (hitResults.Count > 0) {
                foreach (var hitResult in hitResults) {
                    Debug.Log ("Got hit!");
					GameObject  newFood = GameObject.Instantiate(foodTemplate);
                    newFood.transform.position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
                    return true;
                }
            }
            return false;
        }

		// Update is called once per frame
		void Update () {
			if (Input.touchCount > 0 && foodTemplate != null)
			{
				var touch = Input.GetTouch(0);
				if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(0))
				{
					//transform.localPosition = Vector3.zero;

					var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
					ARPoint point = new ARPoint {
						x = screenPosition.x,
						y = screenPosition.y
					};

                    // prioritize reults types
                    ARHitTestResultType[] resultTypes = {
                        ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
                        // if you want to use infinite planes use this:
                        //ARHitTestResultType.ARHitTestResultTypeExistingPlane,
                        ARHitTestResultType.ARHitTestResultTypeHorizontalPlane, 
                        ARHitTestResultType.ARHitTestResultTypeFeaturePoint
                    }; 
					
                    foreach (ARHitTestResultType resultType in resultTypes)
                    {
                        if (HitTestWithResultType (point, resultType))
                        {
                            return;
                        }
                    }
				}
			}
		}
	
	}
}

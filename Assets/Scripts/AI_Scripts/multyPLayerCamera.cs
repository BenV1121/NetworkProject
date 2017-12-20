using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class multyPLayerCamera : MonoBehaviour {

    public List<Transform> targets;
    public Vector3 offset;

    public float smooth = .5f;
    public Vector3 velocity;

    public float maxZoom = 30f;
    public float minZoom = 2f;
    public float zoomLimit = 50f;

    private Camera cam;
   
	// Use this for initialization
	void Start () {

        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {

        if (targets.Count == 0) { return; }
        Fallow();
        Zoom();

	}

    void Fallow()
    {


        Vector3 centerPint = getcenterPoint();
        Vector3 newPosition = centerPint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smooth);


    }

    void Zoom()
    {
        Debug.Log(getGreatestD());

        float newZoom = Mathf.Lerp(maxZoom, minZoom, getGreatestD()/zoomLimit);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    float getGreatestD()
    {
        
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++) { bounds.Encapsulate(targets[i].position); }
        return bounds.size.x;
    }

    Vector2 getcenterPoint()
    {
        if (targets.Count == 1) { return targets[0].position; }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++) {bounds.Encapsulate(targets[i].position); }
        return bounds.center;
    }
}

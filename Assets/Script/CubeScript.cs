using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour {

    public float rotateSpeed = 1.0f;
    public Vector3 spinSpeed = Vector3.zero;
    public Vector3 spinAxis = new Vector3(0, 1, 0);

	// Use this for initialization, called when first fired up
	void Start () {
        //spinSpeed = new Vector3(Random.value, Random.value, Random.value);

        //spinAxis = Vector3.up;
        //spinAxis.x = (Random.value - Random.value) *.1f; //Gives us value between range of -1 to 1 //Gives it a small tilt  in the x axis everytime it spins
	}
	

    public void SetSize(float size)
    {
        this.transform.localScale = new Vector3(size, size, size);
    }

	// Update is called once per frame
	void Update () {

        //this.transform.Rotate(spinSpeed);
        //this.transform.RotateAround(Vector3.zero,spinAxis, rotateSpeed);
	}
}

using UnityEngine;
using UnityEditor;
using System.Collections;
using Pathfinding.Serialization.JsonFx;
using UnityEngine.UI;
using System;

public class PlayerRaycasting : MonoBehaviour {

    //How far the person can see
    public float distanceToSee = 7f;

    RaycastHit whatIHit;
    public bool InReach;

    //The tag that is attached to each cube
    public string TriggerTag = "TreeObject";

    public string _WebsiteURL = "http://labweek3.azurewebsites.net/tables/TreeSurvey?zumo-api-version=2.0.0";

    //Variables for dataset
    private string treeID;
    private string location;
    private string ecologicalValue;
    private string historicalSignificance;
    private string whenReadingRecorded;
    private float x;
    private float y;
    private float z;

    //to be configured in Unity
    public GameObject myPrefab;
    public Material hoverColor;


    // Use this for initialization
    void Start () {

        //The response produce is a JSON string
        string jsonResponse = Request.GET(_WebsiteURL);

        if (string.IsNullOrEmpty(jsonResponse))
        {
            return;
        }

        //We can now deserialize into an array of objects - in this case the class we created. The deserializer is smart enough to instantiate all the classes and populate the variables based on column name.
        Tree[] trees = JsonReader.Deserialize<Tree[]>(jsonResponse);

        foreach (Tree tree in trees)
        {
            treeID = tree.TreeID;
            location = tree.Location;
            ecologicalValue = tree.EcologicalValue;
            historicalSignificance = tree.HistoricalSignificance;
            whenReadingRecorded = tree.WhenReadingRecorded;
            x = float.Parse(tree.X);
            y = float.Parse(tree.Y);
            z = float.Parse(tree.Z);

            GameObject newCube = (GameObject)Instantiate(myPrefab, new Vector3(x, y, z), Quaternion.identity);
            newCube.name = tree.TreeID;
        }

	}
	
	// Update is called once per frame
	void Update () {



        Debug.DrawRay(this.transform.position, this.transform.forward * distanceToSee, Color.magenta);

        if (Physics.Raycast(this.transform.position, this.transform.forward, out whatIHit, distanceToSee))
            {
                if (whatIHit.collider.tag == TriggerTag)
                {
                    InReach = true;
                    whatIHit.transform.gameObject.GetComponent<Renderer>().material = hoverColor;

            }
            else
                {
                    InReach = false;
                }
            }

            else
            {
                InReach = false;
            }
    }

    //the GUI box which will provide contextual information depending on what is being displayed
    void OnGUI()
    {
        if (InReach == true)
        {

            //popup.SetActive(true);



            //nameHolder.gameObject.GetComponent<Text>().text = objectName;
            //dateHolder.gameObject.GetComponent<Text>().text = objectDate;
            //classificationHolder.gameObject.GetComponent<Text>().text = objectClassification;

            //The response produce is a JSON string
            string jsonResponse = Request.GET(_WebsiteURL);

            if (string.IsNullOrEmpty(jsonResponse))
            {
                return;
            }

            //We can now deserialize into an array of objects - in this case the class we created. The deserializer is smart enough to instantiate all the classes and populate the variables based on column name.
            Tree[] trees = JsonReader.Deserialize<Tree[]>(jsonResponse);


            foreach (Tree tree in trees)
            {
                if(whatIHit.collider.name == tree.TreeID)
                {
                    treeID = tree.TreeID;
                    location = tree.Location;
                    ecologicalValue = tree.EcologicalValue;
                    historicalSignificance = tree.HistoricalSignificance;
                    whenReadingRecorded = tree.WhenReadingRecorded;
                    x = float.Parse(tree.X);
                    y = float.Parse(tree.Y);
                    z = float.Parse(tree.Z);
                }
            }

            GUI.color = Color.white;
            GUI.Box(new Rect(Screen.width/2, Screen.height/2, 400, 80), "The ecological value of this tree is " + ecologicalValue + "\n" + "The Location is " + location + "\n" + "Data is recorded at " + whenReadingRecorded + "\n" + "Historical significance level is " + historicalSignificance);
            Debug.Log("The ID for this object tree is " + treeID);

        }

        else
        {
            //popup.SetActive(false);
        }
    }

    //public void Wait(GameObject objectHit)
    //{
    //    objectHit.GetComponent<Renderer> = hoverColor;
    //}
}

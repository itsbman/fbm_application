using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class createShape : MonoBehaviour
{
    bool drawStart = false;
    private ShapeMesh createMesh;
    public Vector3 startVertex;
    public Vector3 startPoint;
    public Vector3 endPoint;
    public Vector3 curVertex;
    public Material mtl;
    int numLine;
    private int active = 0;
    private int shapeOption = 0;
    private int rectCount = 0;
    private int circleCount = 0;
    private int triangleCount = 0;
    LineRenderer lr;
    Vector3[] lineVertices;
    public Camera cam;
    private float zinit;
    public Text par1;
    public Text par2;
  


    public void shapeHandle(int shape)
    {
        shapeOption = shape;
        par1 = GameObject.Find("parameter1").GetComponent<Text>();
        par2 = GameObject.Find("parameter2").GetComponent<Text>();
        lr.material = new Material(Shader.Find("Diffuse"));
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.loop = true;
        if (shapeOption == 1) //rectangle
        {
            numLine = 4;
            lr.positionCount = numLine;
            

            lineVertices = new Vector3[numLine];
   
        }
        if (shapeOption == 2) //circle
        {
            numLine = 20;
            lr.positionCount = numLine;
          
            lineVertices = new Vector3[numLine];
        }
        if (shapeOption == 3) //triangle
        {
            numLine = 3;
            lr.positionCount = numLine;
           
            lineVertices = new Vector3[numLine];
        }
    }
    public void activate(int state)
    {
        //createdshapes = GameObject.FindGameObjectsWithTag("shape");
        //Debug.Log("here");
        active = state;
        //Debug.Log(active);
        if (active == 0)
        {
            lr.positionCount = 0;

        }
        if (active == 1)
        {

        }
        if (active == 2) //for confirmation
        {
            
            par1.text = string.Empty;
            par2.text = string.Empty;
            if (shapeOption == 1)
            {
                
                if (lineVertices[0] != lineVertices[2])
                {
                    Debug.Log("here");
                    rectCount = rectCount + 1;
                    GameObject shapePlane = new GameObject();
                    shapePlane.name = "rectangle" + rectCount.ToString();
                    //shapePlane.tag = "shape";
                    shapePlane.AddComponent<ShapeMesh>();
                    //shapePlane.AddComponent<PlaneExtrude>().extrude_state(1, lineVertices, rectCount);
                    //shapePlane.AddComponent<ShapeMeshProperties>();
                    shapePlane.AddComponent<LineRenderer>();
                    LineRenderer shapeLine = shapePlane.GetComponent<LineRenderer>();
                    shapeLine.positionCount = numLine;
                    //shapeLine.material = new Material(Shader.Find("Particles/Additive"));
                    shapeLine.material.color = Color.white;
                    shapeLine.startWidth = 0.05f;
                    shapeLine.endWidth = 0.05f;
                    shapeLine.loop = true;
                    shapeLine.enabled = false;
                    shapePlane.GetComponent<ShapeMesh>().drawMesh(lineVertices, rectCount, numLine, shapeOption, startVertex);
                }
                else
                {
                    lineVertices[0] = new Vector3(0, 0, 0);
                    lineVertices[1] = new Vector3(0, 0, 0);
                    lineVertices[2] = new Vector3(0, 0, 0);
                    lineVertices[3] = new Vector3(0, 0, 0);

                }
            }
            if (shapeOption == 2)
            {
                //something
                circleCount = circleCount + 1;
                GameObject shapePlane = new GameObject();
                shapePlane.name = "circle" + circleCount.ToString();
                shapePlane.AddComponent<ShapeMesh>();
                shapePlane.AddComponent<LineRenderer>();
                LineRenderer shapeLine = shapePlane.GetComponent<LineRenderer>();
                shapeLine.positionCount = numLine;
                shapeLine.material.color = Color.white;
                //shapePlane.tag = "shape";
                shapeLine.startWidth = 1f;
                shapeLine.endWidth = 1f;
                shapeLine.loop = true;
                shapeLine.enabled = false;
                shapePlane.GetComponent<ShapeMesh>().drawMesh(lineVertices, circleCount, numLine, shapeOption, startPoint);
            }
            if (shapeOption == 3)
            {
                //something
                triangleCount = triangleCount + 1;
                GameObject shapePlane = new GameObject();
                shapePlane.name = "triangle" + triangleCount.ToString();
                shapePlane.AddComponent<ShapeMesh>();
                //shapePlane.tag = "shape";

                shapePlane.AddComponent<LineRenderer>();
                LineRenderer shapeLine = shapePlane.GetComponent<LineRenderer>();
                shapeLine.positionCount = numLine;
                shapeLine.material.color = Color.white;
                shapeLine.startWidth = 1f;
                shapeLine.endWidth = 1f;
                shapeLine.loop = true;
                shapeLine.enabled = false;
                shapePlane.GetComponent<ShapeMesh>().drawMesh(lineVertices, triangleCount, numLine, shapeOption, startPoint);
            }
            shapeOption = 0;
            

        }

    }

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        cam = Camera.main;
        zinit = cam.nearClipPlane+5;
        

    }
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (active == 1 && shapeOption != 0)
        {
            

            if (shapeOption == 1) //rectangle
            {

                
                if (Input.GetMouseButtonDown(0) && !drawStart && !IsPointerOverUIObject())
                //if (Input.GetTouch(0).phase == TouchPhase.Began && !drawStart && !EventSystem.current.IsPointerOverGameObject(0))
                {
                    //par1.text = "triggered";
                    drawStart = true;
                    Vector2 mousePos = Input.mousePosition;
                    //Vector2 mousePos = Input.GetTouch(0).position;
                    startVertex = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, zinit));
                    lineVertices[0] = startVertex;
                }
                //Debug.Log(drawStart);
                
                if (Input.GetMouseButton(0) && drawStart)
                //if(Input.touchCount > 0 && drawStart)
                {
                    //bool test = EventSystem.current.IsPointerOverGameObject(0);
                    //par1.text = test.ToString();
                    Vector2 mousePos = Input.mousePosition;
                    //bool test2 = Input.GetMouseButton(0);
                    //par2.text = mousePos.x.ToString();
                    //Vector2 mousePos = Input.GetTouch(0).position;

                    curVertex = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, zinit));
                    lineVertices[1] = new Vector3(startVertex.x, curVertex.y, curVertex.z);
                    lineVertices[2] = new Vector3(curVertex.x, curVertex.y, curVertex.z);
                    lineVertices[3] = new Vector3(curVertex.x, startVertex.y, curVertex.z);
                    //lineVertices[4] = startVertex;
                    float length = Mathf.Abs(curVertex.x - startVertex.x);
                    float width = Mathf.Abs(curVertex.y - startVertex.y);
                    par1.text = "L:"+length.ToString();
                    par2.text = "W:"+width.ToString();
                    //Debug.Log(lineVertices[0]);
                    //Debug.Log(lineVertices[2]);
                    lr.SetPositions(lineVertices);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    drawStart = false;
                }
            }
            //Debug.Log(startVertex);
            if (shapeOption == 2 || shapeOption ==3) //circle or triangle
            {
                //Debug.Log(drawStart);
                if (Input.GetMouseButtonDown(0) && !drawStart && !IsPointerOverUIObject())
                {
                    drawStart = true;
                    Vector2 mousePos = Input.mousePosition;
                    startPoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, zinit ));
                    //lineVertices[0] = startVertex;
                }
                if (Input.GetMouseButton(0) && drawStart)
                {

                    Vector2 mousePos = Input.mousePosition;
                    endPoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, zinit ));
                    Vector3 radius = new Vector3(endPoint.x - startPoint.x, endPoint.y - startPoint.y, endPoint.z - startPoint.z);
                    for (int i = 0; i < numLine ; i++ ){
                        float ratio = (float)i / (numLine);
                        float rad = ratio * Mathf.PI * 2f;
                        float cos = Mathf.Cos(rad), sin = Mathf.Sin(rad);
                        float x = cos * radius.x - sin * radius.y, y = sin * radius.x + cos * radius.y; //only works in x-y plane
                        lineVertices[i] = new Vector3(startPoint.x+x, startPoint.y + y , endPoint.z); ;
                    }
                    if (shapeOption == 2)
                    {
                        float absrad = Mathf.Sqrt(radius.x * radius.x + radius.y * radius.y);
                        par2.text = "R:" + absrad.ToString();
                    }
                    if (shapeOption == 3)
                    {
                        float absrad = Mathf.Sqrt(lineVertices[0].x * lineVertices[0].x + lineVertices[0].y * lineVertices[0].y);
                        par2.text = "L:" + absrad.ToString();
                    }

                    lr.SetPositions(lineVertices);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    drawStart = false;
                }
            }
        }
    }
}

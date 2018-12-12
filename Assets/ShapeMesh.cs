using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeMesh : MonoBehaviour {
    //public Material mtl;
    public GameObject shapePlane;
    private float minExtLength;
    private Vector3[] varVertices;
    //private bool openEnded = true;
    MeshFilter extrudedmesh;
    private int shapeopt = 0;
    public int rectct;
    public int trict;
    public int circct;
    private int side;
    //MeshCollider mc;
    public Slider height_slider;
    public Button upButton;
    public Button downButton;
    public Button rightButton;
    public Button leftButton;
    public Button forButton;
    public Button backButton;
    public Button extrude_cfm;
    //public Button extrude_cnl;
    private Vector3 startpt;
    private Vector3 tr;
    private bool tranStart = false;
    //public Text debug;
    
    // Use this for initialization
    private void editingHandle(int edit_mode)
    {
        if (edit_mode == 0)
        {
            if(shapeopt == 1)
            {
                GameObject.Find("rectangle" + rectct.ToString()).GetComponent<ShapeMesh>().enabled = false;
            }
            if (shapeopt == 2)
            {
                GameObject.Find("circle" + circct.ToString()).GetComponent<ShapeMesh>().enabled = false;
            }
            if (shapeopt == 3)
            {
                GameObject.Find("triangle" + trict.ToString()).GetComponent<ShapeMesh>().enabled = false;
            }

        }
        
    }
    private void tranHandle(int dir)
    {
        float deltapos = 0.5f;
        if (dir == 1)
        {
            tr = new Vector3(0, 0, -deltapos);
        }
        if (dir == 2)
        {
            tr = new Vector3(0, 0, deltapos);
        }
        if (dir == 3)
        {
            tr = new Vector3(-deltapos, 0, 0);
        }
        if (dir == 4)
        {
            tr = new Vector3(deltapos, 0, 0);
        }
        if (dir == 5)
        {
            tr = new Vector3(0, deltapos, 0);
        }
        if (dir == 6)
        {
            tr = new Vector3(0, -deltapos, 0);
        }
        tranStart = true;
    }
    private void Startinit()
    {
        height_slider = GameObject.Find("extd_slider").GetComponent<Slider>();
        extrude_cfm = GameObject.Find("extrude_confirm").GetComponent<Button>();
        upButton = GameObject.Find("upButton").GetComponent<Button>();
        downButton = GameObject.Find("downButton").GetComponent<Button>();
        leftButton = GameObject.Find("leftButton").GetComponent<Button>();
        rightButton = GameObject.Find("rightButton").GetComponent<Button>();
        forButton = GameObject.Find("forButton").GetComponent<Button>();
        backButton = GameObject.Find("backButton").GetComponent<Button>();
        extrude_cfm.onClick.AddListener(delegate { editingHandle(0); });
        upButton.onClick.AddListener(delegate { tranHandle(1); });
        downButton.onClick.AddListener(delegate { tranHandle(2); });
        leftButton.onClick.AddListener(delegate { tranHandle(3); });
        rightButton.onClick.AddListener(delegate { tranHandle(4); });
        forButton.onClick.AddListener(delegate { tranHandle(5); });
        backButton.onClick.AddListener(delegate { tranHandle(6); });
        //debug = GameObject.Find("debug").GetComponent<Text>();
    }

    public void drawMesh(Vector3[] lineVertices,int Count, int segments, int shapeOption, Vector3 startPoint) {
        shapeopt = shapeOption;
        //vertices = lineVertices;
        side = segments;
        Startinit();
        //height_slider = GameObject.Find("extd_slider").GetComponent<Slider>();
        minExtLength = 0.2f;
        varVertices = lineVertices;
        startpt = startPoint;
        //debug.text = "activvaated";
        if (shapeOption == 1)
        {
            rectct = Count;
            shapePlane = GameObject.Find("rectangle" + Count.ToString()); //change for other shape
            //rectObj.Add(shapePlane);
            //mtl = Resources.Load("extrude-plane-lambert2.mat", typeof(Material)) as Material;
            int nvertex = lineVertices.Length;
            //float extrude_length = 5.0f;
            Vector2[] pos = new Vector2[nvertex];

            ////new///
            shapePlane.AddComponent<MeshFilter>();
            shapePlane.AddComponent<MeshRenderer>();
            extrudedmesh = shapePlane.GetComponent<MeshFilter>();

            
            extrudedmesh.mesh = capPlane(lineVertices, minExtLength, segments); //extrude the plane mesh
            MeshRenderer mcol = shapePlane.GetComponent<MeshRenderer>();
 
            mcol.material = new Material(Resources.Load("extrude", typeof(Material)) as Material);
       


        }
        if (shapeOption == 2)
        {
            circct = Count;
            shapePlane = GameObject.Find("circle" + Count.ToString()); //change for other shape
            shapePlane.AddComponent<MeshFilter>();
            shapePlane.AddComponent<MeshRenderer>();
            MeshFilter shapemesh = shapePlane.GetComponent<MeshFilter>();
            shapemesh.sharedMesh = Build(lineVertices, minExtLength, segments, startPoint);
            MeshRenderer mcol = shapePlane.GetComponent<MeshRenderer>();
            mcol.material = new Material(Resources.Load("extrude", typeof(Material)) as Material);

        }
        if (shapeOption == 3)
        {
            trict = Count;
            shapePlane = GameObject.Find("triangle" + Count.ToString()); //change for other shape
            shapePlane.AddComponent<MeshFilter>();
            shapePlane.AddComponent<MeshRenderer>();
            MeshFilter shapemesh = shapePlane.GetComponent<MeshFilter>();
            shapemesh.sharedMesh = Build(lineVertices, minExtLength, segments, startPoint);
            MeshRenderer mcol = shapePlane.GetComponent<MeshRenderer>();
            mcol.material = new Material(Resources.Load("extrude", typeof(Material)) as Material);

        }


        
        //extrusion_state = 1;

        ////



    }
    void Start()
    {
        
        //rectct = 0;
    }

    Mesh capPlane(Vector3[] lineVertices, float extrudelength, int segments)
    {
        var capmesh = new Mesh();
        
        MeshCollider mc = shapePlane.AddComponent<MeshCollider>();
        Vector2[] pos = new Vector2[lineVertices.Length];
        pos[0] = new Vector2(lineVertices[0].x, lineVertices[0].y);
        pos[1] = new Vector2(lineVertices[1].x, lineVertices[1].y);
        pos[2] = new Vector2(lineVertices[2].x, lineVertices[2].y);
        pos[3] = new Vector2(lineVertices[3].x, lineVertices[3].y);

        // Create the Vector3 vertices
        Vector3[] vertices = new Vector3[2 * segments];
        for (int j = 0; j < segments; j++)
        {
            vertices[2 * j] = new Vector3(pos[j].x, pos[j].y, lineVertices[j].z);
            vertices[2 * j + 1] = new Vector3(pos[j].x, pos[j].y, lineVertices[j].z - extrudelength);
        }
        var triangles = new int[] { //create triangle; need to create a formula for this
                //side faces
                1, 2, 0,
                3, 2, 1,
                4, 2, 3,
                5, 4, 3,
                5, 7, 4,
                7, 6, 4,
                1, 0, 6,
                7, 1, 6,
                //top and bottom faces
                0, 2, 4,
                4, 6, 0,
                5, 3, 1,
                5, 1, 7,
            };
        // Create the mesh
        capmesh.Clear();
        capmesh.vertices = vertices;
        capmesh.triangles = triangles;
        capmesh.RecalculateNormals();
        capmesh.RecalculateBounds();
        return capmesh;

    }
    Mesh Build(Vector3[] lineVertices, float extrudelength, int segments, Vector3 startPoint)
    {
        var mesh = new Mesh();

        var vertices = new List<Vector3>();
        //var normals = new List<Vector3>();
        //var uvs = new List<Vector2>();
        var triangles = new List<int>();
        float bottom = lineVertices[0].z;
        //float radius = Mathf.Sqrt(Mathf.Pow(endPoint.x - startPoint.x, 2) + Mathf.Pow(endPoint.y - startPoint.y, 2) + Mathf.Pow(endPoint.z - startPoint.z, 2));
        //GenerateCap(segments + 1, top, bottom, radius, vertices, uvs, normals, true);
        
        var len = (segments) * 2;
        for (int i = 0; i < segments; i++)
        {
            //float ratio = (float)i / (segments - 1);
            vertices.Add(lineVertices[i]); //make this the bottom
            //uvs.Add(new Vector2(ratio, 1f));
            vertices.Add(lineVertices[i]-new Vector3(0f,0f,extrudelength));
            //uvs.Add(new Vector2(ratio, 0f));
            
        }

        for (int i = 0; i < segments; i++) //sides
        {
            int idx = i * 2;
            int a = idx, b = idx + 1, c = (idx + 2) % len, d = (idx + 3) % len;
            triangles.Add(b);
            triangles.Add(c);
            triangles.Add(a);

            triangles.Add(c);
            triangles.Add(b);
            triangles.Add(d);

        }

        //normals.Add(new Vector3(0f, 1f, 0f));
        vertices.Add(new Vector3(startPoint.x, startPoint.y, bottom)); //bottom center
        //uvs.Add(new Vector2(0.5f, 1f));
        vertices.Add(new Vector3(startPoint.x, startPoint.y, bottom-extrudelength)); // top center
        //uvs.Add(new Vector2(0.5f, 0f));
        //normals.Add(new Vector3(0f, -1f, 0f));
        

        //var offset = len;
        var ib = vertices.Count - 2;
        var it = vertices.Count - 1;

        //upper
        for (int i = 0; i < len; i += 2)
        {
            triangles.Add(ib);
            triangles.Add(i);
            triangles.Add((i + 2) % len);
            
            
        }

        //down
        for (int i = 1; i < len; i += 2)
        {
            triangles.Add(it);
            triangles.Add((i + 2) % len);
            triangles.Add(i);
            
            Debug.Log("bottomtriangle");
            Debug.Log(ib);
            Debug.Log(i);
            Debug.Log((i + 2) % len);
            
        }

        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        //mesh.uv = uvs.ToArray();
        //mesh.normals = normals.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        return mesh;
    }
    private void Update()
    {
        if (shapeopt == 1)
        {
            
            minExtLength = 0.2f + height_slider.value * 5.8f;
            shapePlane.GetComponent<MeshFilter>().mesh = capPlane(varVertices, minExtLength, side);
            //mc.sharedMesh = extrudedmesh.sharedMesh;
        }
        if (shapeopt == 2)
        {
            minExtLength = 0.2f + height_slider.value * 5.8f;
            shapePlane.GetComponent<MeshFilter>().mesh = Build(varVertices, minExtLength, side, startpt);
            //mc.sharedMesh = extrudedmesh.sharedMesh;
        }
        if (shapeopt == 3)
        {
            minExtLength = 0.2f + height_slider.value * 5.8f;
            shapePlane.GetComponent<MeshFilter>().mesh = Build(varVertices, minExtLength, side, startpt);
            //mc.sharedMesh = extrudedmesh.sharedMesh;
        }
        if (tranStart)
        {
            shapePlane.transform.position += tr;
            tranStart = false;
        }
        
        
    }


}

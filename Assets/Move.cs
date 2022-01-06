using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class Move : MonoBehaviour
{
    private Dictionary<Color, string> colorMap = new Dictionary<Color, string>();
    private Color currentColor;
    private int randomSpot;
    private GameObject[] _wayPoints;
    [SerializeField] private float speed;
    private float waitTime;
    [SerializeField] private float startWaitTime;
    [SerializeField] private float distance = 0.3f;
    public bool pause;
    // Start is called before the first frame update
    void Start()
    {
        currentColor = GetComponent<Renderer>().material.color;
        _wayPoints = GameObject.FindGameObjectsWithTag("WayPoint");
        randomSpot = Random.Range(0, _wayPoints.Length);
        waitTime = startWaitTime;
        InitializeColorMap();
    }

    public void Setpause()
    {
        pause = !pause;
    }

    public void DebugInfo()
    {
        Debug.Log("Hallo, ik ben een " + GetComponent<MeshFilter>().mesh.name + " en ik ben " + colorMap[currentColor]);
    }

    public void ChangeToRandomColor()
    {
        Color randomColor = colorMap.ElementAt(Random.Range(0, colorMap.Count)).Key;
        var renderer = GetComponent<Renderer>();
        //Call SetColor using the shader property name "_Color" and setting the color to red
        renderer.material.SetColor("_Color", randomColor);
        currentColor = randomColor;
    }

    public void InitializeColorMap()
    {
        Color orange = new Color(255f, 165f, 0f);
        colorMap.Add(Color.white, "wit");
        colorMap.Add(Color.green, "groen");
        colorMap.Add(Color.yellow, "geel");
        colorMap.Add(Color.blue, "blauw");
        colorMap.Add(Color.red, "rood");
        colorMap.Add(orange, "oranje");
    }
    void Update()
    {
        if (pause) return;
        transform.position = Vector3.MoveTowards(transform.position, _wayPoints[randomSpot].transform.position, speed * Time.deltaTime);
        //  Debug.Log("Moving towards: " + transform.position);
        //  Debug.Log("Extra debugs: " + transform.position + " Object name: " + transform.name);
        if (Vector3.Distance(transform.position, _wayPoints[randomSpot].transform.position) < distance)
        {

            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, _wayPoints.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;

            }
        }
    }
}

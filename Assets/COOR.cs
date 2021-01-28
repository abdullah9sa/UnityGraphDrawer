using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class COOR : MonoBehaviour {
    public LineRenderer lr;
    [Range (-30, 30)]
    public float speed;
    public Transform go;
    public Vector3 sz;
    public InputField feld;

    [Range (-8, 8)]
    public int minDomain;
    public float some;

    [Range (-8, 8)]
    public int maxDomain;
    public Transform trail;
    public Transform xLine;
    public Transform yLine;
    float EQN;

    Mesh nMesh;
    public GameObject g;

    public int[] tri;
    void Start () {
        EnterEQN ();
    }

    public void Draw (float f, float p) {
        for (float i = -8; i <= 8; i += 0.01f) {
            float y = 0;
            Vector3 pos = new Vector3 (i, y, 0);
            print (y);
            if (pos.y <= maxDomain && pos.y >= minDomain) {
                GameObject gb = Instantiate (go.gameObject, pos, Quaternion.identity, this.transform);
                gb.transform.localScale = sz;
            }
        }
    }

    public void EnterEQN () {

        lr.positionCount = 0; //   float x = 0;
        string str = feld.text;
        int op = 0;
        for (float x = -8; x <= 8; x += 0.07f) {


            if (!str.Contains ("x")) //or !contains sin >>> etc...
            {
                EQN = float.Parse (str);
            }
            if (str.Contains ("x^")) //or contains sin >>> etc...
            {
                float power = float.Parse (str.Substring (str.IndexOf ("x^") + 2));

                if (char.IsDigit (str[0])) {
                    float num = float.Parse (str.Substring (0, str.IndexOf ('x')));
                    EQN = num * Mathf.Pow (x, power);
                } else {
                    EQN = Mathf.Pow (x, power);
                }
            }
            if (str.Contains ("x") && !str.Contains ("^")) {
                if (char.IsDigit (str[0])) {
                    float num = float.Parse (str.Substring (0, str.IndexOf ('x')));
                    EQN = num * x;
                } else {
                    EQN = x;
                }
            }

            if (str.Contains ("x+") && char.IsDigit (str[2])) {
                if (char.IsDigit (str[0])) {
                    float num = float.Parse (str.Substring (0, str.IndexOf ('x')));
                    EQN = num * x + float.Parse (str[2].ToString ());
                } else {
                    EQN = x + float.Parse (str[-1].ToString ());
                }
            }

            if (str.Contains ("x-") && char.IsDigit (str[2])) {
                if (char.IsDigit (str[0])) {
                    float num = float.Parse (str.Substring (0, str.IndexOf ('x')));
                    EQN = num * x - float.Parse (str[2].ToString ());
                } else {
                    EQN = x - float.Parse (str[-1].ToString ());
                }
            }

            if (str.Contains ("sin(")) {
                if (char.IsDigit (str[0])) {
                    float num = float.Parse (str.Substring (0, str.IndexOf ('x')));
                    EQN = num * Mathf.Sin (x); // - float.Parse (str[2].ToString ());
                } else {
                    EQN = Mathf.Sin (x); // - float.Parse (str[2].ToString ());
                }

            }
            if (str.Contains ("cos(")) {
                if (char.IsDigit (str[0])) {
                    float num = float.Parse (str.Substring (0, str.IndexOf ('x')));
                    EQN = num * Mathf.Cos (x); // - float.Parse (str[2].ToString ());
                } else {
                    EQN = Mathf.Cos (x); // - float.Parse (str[2].ToString ());
                }

            }
            // EQN = float.Parse (Evaluate(str).ToString());
            float y = EQN;
            Vector3 pos = new Vector3 (x, y, 0);
            if (pos.y <= maxDomain && pos.y >= minDomain) {
                //   print(op);
                lr.positionCount++;
                lr.SetPosition (op, pos);
                op++;
                //       GameObject gb = Instantiate(go.gameObject, pos, Quaternion.identity,this.transform);
                //        gb.transform.localScale = sz;       
            }
            //  op++;
        }
        //   Mesh nMesh = new Mesh() ;
        nMesh = new Mesh ();
        GetComponent<MeshFilter> ().mesh = nMesh;

        Vector3[] v = new Vector3[op];
        lr.GetPositions (v);
        nMesh.vertices = v;
        print (nMesh.vertices[30]);
    }

    void Update () {
        float a = Input.GetAxisRaw ("Vertical");
        float s = Input.GetAxisRaw ("Horizontal");

        if (Camera.main.transform.position.z < 0)
            Camera.main.transform.RotateAround (Vector3.zero, Vector3.right, Time.deltaTime * (a) * 1 * speed);
        else {
            Camera.main.transform.RotateAround (Vector3.zero, Vector3.right, Time.deltaTime * (a) * -1 * speed);
        }
        Camera.main.transform.RotateAround (Vector3.zero, Vector3.up, Time.deltaTime * (s) * -1 * speed);
        Camera.main.transform.LookAt (Vector3.zero);

        if (Input.GetKey (KeyCode.E))
            Camera.main.transform.Translate (Vector3.forward * 1 * some);
        if (Input.GetKey (KeyCode.Q))
            Camera.main.transform.Translate (Vector3.forward * -1 * some);
    }

}

//   string xpo = str.Substring(str[str.IndexOf("x^") + 2], str.Length-1);
//   float power = float.Parse(str[str.IndexOf("x^") + 2].ToString());
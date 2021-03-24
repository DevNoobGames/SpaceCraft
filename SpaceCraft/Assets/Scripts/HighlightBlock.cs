using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightBlock : MonoBehaviour
{
    public Material lineMat;


    void OnRenderObject()
    {
        RenderOutline();// call the render in the first place, has to be in this function as far as I'm aware
    }
    void RenderOutline()
    {
        Vector3 pos = transform.position; // just makes it easier to call the position
        float distfromcenter = 0.501f; // this value is fine tuned to be just bigger than a normal block, but change it as you wish!
        GL.Begin(GL.LINES); //start the lines
        lineMat.SetPass(0);
        GL.Color(lineMat.color);
        //Draw the outline box
        GL.Vertex3(pos.x + distfromcenter, pos.y + distfromcenter, pos.z + distfromcenter);
        GL.Vertex3(pos.x + distfromcenter, pos.y - distfromcenter, pos.z + distfromcenter);
        GL.Vertex3(pos.x - distfromcenter, pos.y + distfromcenter, pos.z + distfromcenter);
        GL.Vertex3(pos.x - distfromcenter, pos.y - distfromcenter, pos.z + distfromcenter);
        GL.Vertex3(pos.x + distfromcenter, pos.y + distfromcenter, pos.z - distfromcenter);
        GL.Vertex3(pos.x + distfromcenter, pos.y - distfromcenter, pos.z - distfromcenter);
        GL.Vertex3(pos.x - distfromcenter, pos.y + distfromcenter, pos.z - distfromcenter);
        GL.Vertex3(pos.x - distfromcenter, pos.y - distfromcenter, pos.z - distfromcenter);
        GL.Vertex3(pos.x + distfromcenter, pos.y - distfromcenter, pos.z + distfromcenter);
        GL.Vertex3(pos.x + distfromcenter, pos.y - distfromcenter, pos.z - distfromcenter);
        GL.Vertex3(pos.x - distfromcenter, pos.y - distfromcenter, pos.z + distfromcenter);
        GL.Vertex3(pos.x - distfromcenter, pos.y - distfromcenter, pos.z - distfromcenter);
        GL.Vertex3(pos.x + distfromcenter, pos.y - distfromcenter, pos.z + distfromcenter);
        GL.Vertex3(pos.x - distfromcenter, pos.y - distfromcenter, pos.z + distfromcenter);
        GL.Vertex3(pos.x + distfromcenter, pos.y - distfromcenter, pos.z - distfromcenter);
        GL.Vertex3(pos.x - distfromcenter, pos.y - distfromcenter, pos.z - distfromcenter);
        GL.Vertex3(pos.x + distfromcenter, pos.y + distfromcenter, pos.z + distfromcenter);
        GL.Vertex3(pos.x + distfromcenter, pos.y + distfromcenter, pos.z - distfromcenter);
        GL.Vertex3(pos.x - distfromcenter, pos.y + distfromcenter, pos.z + distfromcenter);
        GL.Vertex3(pos.x - distfromcenter, pos.y + distfromcenter, pos.z - distfromcenter);
        GL.Vertex3(pos.x + distfromcenter, pos.y + distfromcenter, pos.z + distfromcenter);
        GL.Vertex3(pos.x - distfromcenter, pos.y + distfromcenter, pos.z + distfromcenter);
        GL.Vertex3(pos.x + distfromcenter, pos.y + distfromcenter, pos.z - distfromcenter);
        GL.Vertex3(pos.x - distfromcenter, pos.y + distfromcenter, pos.z - distfromcenter);
        GL.End(); // now draw it!
    }

}

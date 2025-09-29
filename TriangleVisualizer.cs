using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace XRC.Assignments.Meshes
{
    /// <summary>
    /// This script visualizes the mesh of its game object by drawing lines between each triangle vertex, using a line renderer
    /// </summary>
    [RequireComponent(typeof(MeshFilter))]
    public class TriangleVisualizer : MonoBehaviour
    {
        private List<LineRenderer> m_LineRenderers;
        private Mesh m_Mesh;

        void Start()
        {
            m_Mesh = gameObject.GetComponent<MeshFilter>().mesh;
            m_LineRenderers = new List<LineRenderer>();
        }

        void LateUpdate()
        {
            // By doing the following in Update(), changes to the mesh during runtime are represented by the visualizer
            VisualizeTriangles(m_Mesh.vertices, m_Mesh.triangles);
        }

        /// <summary>
        /// This method is called by Update() every frame.
        /// The method calls SetupLineRenderer() and SetLineRendererPositions()
        /// </summary>
        /// <param name="vertices">Vertices</param>
        /// <param name="triangles">Triangle indices</param>
        private void VisualizeTriangles(Vector3[] vertices, int[] triangles)
        {
            // TODO - Implement method according to summary and instructions
            // <solution>]
            // Loop through the triangles array (steps of 3)
            for (int i = 0; i < triangles.Length / 3; i++)
            {
                int lineRendererIndex = i;
                
                if (lineRendererIndex >= m_LineRenderers.Count)
                    SetupLineRenderer();

                int idx0 = triangles[i * 3 + 0];
                int idx1 = triangles[i * 3 + 1];
                int idx2 = triangles[i * 3 + 2];

                Vector3 v0 = vertices[idx0];
                Vector3 v1 = vertices[idx1];
                Vector3 v2 = vertices[idx2];

                SetLineRendererPositions(v0, v1, v2, lineRendererIndex);
            }
            // </solution>
        }

        /// <summary>
        /// Instantiates a new game object and adds a LineRenderer component to it.
        /// The new game object is set as a child of the current game object.
        /// Each line renderer is named "TriangleLineRenderer".
        /// The LineRenderer has width 0.02 and the appropriate number of positions to draw a closed triangle
        /// The line renderer component is added to the m_LineRenderer list.
        /// </summary>
        private void SetupLineRenderer()
        {
            // TODO - Implement method according to summary and instructions
            // <solution>
            GameObject go = new GameObject("TriangleLineRenderer");
            go.transform.SetParent(transform, false); // Best practice for parenting

            LineRenderer lr = go.AddComponent<LineRenderer>();
            lr.positionCount = 4;
    
            // Line width
            lr.startWidth = 0.02f;
            lr.endWidth = 0.02f;
    
            // Material and color
            lr.material = new Material(Shader.Find("Unlit/Color")); 
            lr.material.color = Color.red; 
            lr.useWorldSpace = false;

            m_LineRenderers.Add(lr);  
        }

        /// <summary>
        /// This method sets the positions for the line renderer used to visualize the triangle
        /// </summary>
        /// <param name="vertex1">First vertex</param>
        /// <param name="vertex2">Second vertex</param>
        /// <param name="vertex3">Third vertex</param>
        /// <param name="lineRendererIndex">The index for the line renderer in the list of line renderers</param>
        private void SetLineRendererPositions(Vector3 vertex1, Vector3 vertex2, Vector3 vertex3, int lineRendererIndex)
        {
            // TODO - Implement method according to summary and instructions
            // <solution>
            LineRenderer lr = m_LineRenderers[lineRendererIndex];
    
            // Set triangle positions
            lr.SetPosition(0, vertex1); 
            lr.SetPosition(1, vertex2);
            lr.SetPosition(2, vertex3);
            lr.SetPosition(3, vertex1);
            // </solution>
        }
    }
}
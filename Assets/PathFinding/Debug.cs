using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Debug : MonoBehaviour
    {
        [SerializeField] private List<Edge> _listEdge = new List<Edge>();
        [SerializeField] private Vector2 startPoint;
        [SerializeField] private Vector2 endPoint;
        [SerializeField] private bool debug;
        [SerializeField] private bool drawPath;

        private void OnDrawGizmos()
        {
            ///Only Gizmos
            for (var i = 0; i < _listEdge.Count; i++)
            {
                var temp = _listEdge[i];
                temp.UpdateCenter();
                _listEdge[i] = temp;
            }
            ///

            var pathService = new RectPathService();
            var path = pathService.GetPath(startPoint, endPoint, _listEdge);
            if (debug)
                pathService.GizmoDebug(0.1f);

            Gizmos.color = Color.yellow;
            
            foreach (var edge in _listEdge)
            {
                Gizmos.DrawWireCube(edge.First.min + (edge.First.max - edge.First.min) / 2f, (edge.First.max - edge.First.min));
                Gizmos.DrawWireCube(edge.Second.min + (edge.Second.max - edge.Second.min) / 2f, (edge.Second.max - edge.Second.min));
            }

            Gizmos.color = Color.blue;
            Gizmos.DrawCube(startPoint, Vector2.one * 0.1f);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(endPoint, Vector2.one * 0.1f);
            
            Gizmos.color = Color.cyan;
            var list = path.GetEnumerator();

            if (drawPath && list.MoveNext())
            {
                while (true)
                {
                    var last = list.Current;
                    if (!list.MoveNext()) break;
                    var first = list.Current;
                    Gizmos.DrawLine(first, last);
                }
            }
        }
    }
}
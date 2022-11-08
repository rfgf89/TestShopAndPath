using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public class RectPathService : IPathFinder
    {
        private readonly Dictionary<Rectangle, HashSet<Edge>> _rectNodes = new Dictionary<Rectangle, HashSet<Edge>>();
        private readonly Dictionary<Vector2, HashSet<Edge>> _graph = new Dictionary<Vector2, HashSet<Edge>>();
        private readonly Dictionary<Vector2, KeyValuePair<int, bool>> _graphState = new Dictionary<Vector2, KeyValuePair<int, bool>>();
        private List<Vector2> _path = new List<Vector2> ();
        private IEnumerable<Edge> _edges;
        private Rectangle _startNode;
        private Rectangle _endNode;
        private Vector2 _startPos;
        private Vector2 _endPos;

        private void Clear()
        {
            _graphState.Clear();
            _path = new List<Vector2>();
            _graph.Clear();
            _rectNodes.Clear();
        }

        private void BuildGraph()
        {
            foreach (var edgeList in _rectNodes)
            {
                foreach (var edge in edgeList.Value)
                {
                    var posStart = edge.Center;

                    if (_rectNodes.TryGetValue(edge.First, out var pos))
                        foreach (var t in pos)
                            if (t.Center != edge.Center)
                                AddToDicHash(_graph, posStart, t);

                    if (_rectNodes.TryGetValue(edge.Second, out var pos2))
                        foreach (var t in pos2)
                            if (t.Center != edge.Center)
                                AddToDicHash(_graph, posStart, t);
                }
            }
        }

        private void BuildEntryNode(ref Rectangle node, Vector2 pos)
        {
            foreach (var edgeList in _rectNodes)
            {
                if (Extension.PointInRect(edgeList.Key, pos))
                {
                    AddToDicHash(_rectNodes, edgeList.Key, new Edge(default, default, pos, pos));
                    node = edgeList.Key;
                    break;
                }
            }
        }
        public IEnumerable<Vector2> GetPath(Vector2 A, Vector2 C, IEnumerable<Edge> edges)
        {
            var enumeratorEdges = edges.GetEnumerator();
            if (!enumeratorEdges.MoveNext())
                return Enumerable.Empty<Vector2>();
            
            Clear();
            _edges = edges;
            _startNode = default;
            _startPos = A;
            _endPos = C;
            
            while (true)
            {
                AddToDicHash(_rectNodes, enumeratorEdges.Current.First, enumeratorEdges.Current);
                AddToDicHash(_rectNodes, enumeratorEdges.Current.Second, enumeratorEdges.Current);
                if (!enumeratorEdges.MoveNext()) break;
            }

            BuildEntryNode(ref _startNode, _startPos);
            if (_startNode == default)
                return Enumerable.Empty<Vector2>();
            
            BuildEntryNode(ref _endNode, _endPos);
            if (_endNode == default)
                return Enumerable.Empty<Vector2>();
            
            BuildGraph();
            
            foreach (var val in _graph)
                _graphState.Add(val.Key, default);

            _graphState[_startPos] = new KeyValuePair<int, bool>(int.MaxValue, false);
            _graphState[_endPos] = new KeyValuePair<int, bool>(0, false);
            
            FindPath(0, _rectNodes[_endNode]);
            BuildPath(_startPos, int.MaxValue, _rectNodes[_startNode]);
            _path.Add(_endPos);
            
            return _path;
        }

        public void GizmoDebug(float scale)
        {
            if (_path.Count > 1)
            {
                Gizmos.color = Color.magenta;
                foreach (var state in _graphState)
                    Handles.Label(state.Key + Vector2.up * scale, state.Value.ToString());
            }

            Gizmos.color = Color.magenta;
            foreach (var val in _graph)
            foreach (var edge in val.Value)
                Gizmos.DrawLine(val.Key, edge.Center);
            
            Gizmos.color = Color.green;
            foreach (var edge in _edges) 
                Gizmos.DrawLine(edge.Start, edge.End);

            Gizmos.color = Color.white;
        }

        private void FindPath(int posNode, HashSet<Edge> nodes)
        {
            posNode++;
            foreach (var node in nodes)
            {
                if ((_graphState[node.Center].Key > posNode || _graphState[node.Center].Key == 0)
                    && _graph.TryGetValue(node.Center, out var value))
                {
                    _graphState[node.Center] = new KeyValuePair<int, bool>(posNode, true);
                    FindPath(posNode, value);
                }
            }
        }
        private void BuildPath(Vector2 currPos, int posNode, HashSet<Edge> nodes)
        {
            _path.Add(currPos);
            Vector2? minNode = null;
            int minPos = posNode;
            foreach (var node in nodes)
            {
                if (_graphState[node.Center].Key < minPos)
                {
                    minNode = node.Center;
                    minPos = _graphState[node.Center].Key;
                }
            }

            if (minNode !=null && _graph.TryGetValue(minNode.Value, out var value))
                BuildPath(minNode.Value, _graphState[minNode.Value].Key, value);
        }
        private void AddToDicHash<T,U>(Dictionary<T, HashSet<U>> dictionary, T to, params U[] addVal)
        {
            if (dictionary.TryGetValue(to, out var values1))
            {
                foreach (var val in addVal)
                    values1.Add(val);
            }
            else
            {
                var values2 = new HashSet<U>(addVal);
                dictionary.Add(to, values2);
            }
        }
        
    }
}
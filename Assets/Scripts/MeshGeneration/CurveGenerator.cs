using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yansubaev.MeshGeneration
{
    [RequireComponent(typeof(LineRenderer))]
    public class CurveGenerator : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private float _startPoint = -3f;
        private float _endPoint = 3f;
        private float _step = 0.2f;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            var points = new List<Vector3>();
            for (float i = _startPoint; i < _endPoint; i += _step)
            {
                points.Add(new Vector3(i, Mathf.Pow(i, 2)));
            }

            _lineRenderer.positionCount = points.Count;
            _lineRenderer.SetPositions(points.ToArray());
        }
    }
}
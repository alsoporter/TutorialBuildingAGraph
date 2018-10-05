using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Graph : MonoBehaviour {
    public Transform pointPrefab;
    
    [Range(10, 100)]
    public int resolution = 10;

    Transform[] points;
    
    void Awake() {
        var step = 2f / resolution;
        var scale = Vector3.one * step;
        var position = Vector3.zero;
        points = new Transform[resolution];
        for (var i = 0; i < points.Length; i++) {
            var point = Instantiate(pointPrefab);
            position.x = (i + 0.5f) * step - 1f;
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }

    void Update() {
        var i = 0;
        for (; i < points.Length; i++) {
            var point = points[i];
            var position = point.localPosition;
            position.y = Mathf.Sin(Mathf.PI * (position.x + Time.time));
            point.localPosition = position;
        }
    }
}

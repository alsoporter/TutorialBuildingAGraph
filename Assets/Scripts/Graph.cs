using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Graph : MonoBehaviour {
    public Transform pointPrefab;
    
    [Range(10, 100)]
    public int resolution = 10;

    Transform[] _points;
    
    void Awake() {
        var step = 2f / resolution;
        var scale = Vector3.one * step;
        var position = Vector3.zero;
        _points = new Transform[resolution];
        for (var i = 0; i < _points.Length; i++) {
            var point = Instantiate(pointPrefab);
            position.x = (i + 0.5f) * step - 1f;
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);
            _points[i] = point;
        }
    }

    void Update() {
        var t = Time.time;
        foreach (var point in _points) {
            var position = point.localPosition;
            position.y = MultiSineFunction(position.x, t);
            point.localPosition = position;
        }
    }

    static float SineFunction(float x, float t) {
        return Mathf.Sin(Mathf.PI * (x + t));
    }
    
    static float MultiSineFunction(float x, float t) {
        var y = Mathf.Sin(Mathf.PI * (x + t));
        y += Mathf.Sin(2f * Mathf.PI * (x + t)) / 2f;
        y *= 2f / 3f;
        return y;
    }
}

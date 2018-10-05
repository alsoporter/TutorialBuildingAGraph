using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Graph : MonoBehaviour {
    public Transform pointPrefab;

    [Range(10, 100)] public int resolution = 10;

    public GraphFunctionName function;

    Transform[] _points;

    static GraphFunction[] functions = {
        SineFunction, Sine2DFunction, MultiSineFunction, MultiSine2DFunction,
        Ripple
    };

    void Awake() {
        var step = 2f / resolution;
        var scale = Vector3.one * step;
        var position = Vector3.zero;
        _points = new Transform[resolution * resolution];

        for (int i = 0, z = 0; z < resolution; z++) {
            position.z = (z + 0.5f) * step - 1f;
            for (var x = 0; x < resolution; x++, i++) {
                var point = Instantiate(pointPrefab);
                position.x = (x + 0.5f) * step - 1f;
                point.localPosition = position;
                point.localScale = scale;
                point.SetParent(transform, false);
                _points[i] = point;
            }
        }
    }

    void Update() {
        var t = Time.time;
        var f = functions[(int) function];

        foreach (var point in _points) {
            var position = point.localPosition;
            position.y = f(position.x, position.z, t);
            point.localPosition = position;
        }
    }

    const float pi = Mathf.PI;

    static float SineFunction(float x, float z, float t) {
        return Mathf.Sin(pi * (x + t));
    }

    static float Sine2DFunction(float x, float z, float t) {
        var y = Mathf.Sin(pi * (x + t));
        y += Mathf.Sin(pi * (z + t));
        y *= 0.5f;
        return y;
    }

    static float MultiSineFunction(float x, float z, float t) {
        var y = Mathf.Sin(pi * (x + t));
        y += Mathf.Sin(2f * pi * (x + t)) * 0.5f;
        y *= 2f / 3f;
        return y;
    }

    static float MultiSine2DFunction(float x, float z, float t) {
        var y = 4f * Mathf.Sin(pi * (x + z + t * 0.5f));
        y += Mathf.Sin(2f * pi * (z + 2f * t)) * 0.5f;
        y *= 2f / 5.5f;
        return y;
    }

    static float Ripple(float x, float z, float t) {
        var d = Mathf.Sqrt(x * x + z * z);
        var y = Mathf.Sin(pi * (4f * d - t));
        y /= 1f + 10f * d;
        return y;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamMaterial : MonoBehaviour
{
    public enum ValueType
    {
        Color,
        Float
    }

    public AudioPeer audioPeer;
    public int band;
    public bool useBuffer;
    public float sensitivity;

    public Color Tint;
    public ValueType valueType = new ValueType();
    public string ShaderProperty = "_EmissionColor";

    private Material material;

	void Start ()
    {
        material = GetComponent<MeshRenderer>().materials[0];

        switch (valueType)
        {
            case ValueType.Color: valueType = ValueType.Color; break;
            case ValueType.Float: valueType = ValueType.Float; break;
        }
	}
	
	void Update ()
    {
        var value = useBuffer ? audioPeer.AudioBandBuffer[band] : audioPeer.AudioBand[band];
        value *= sensitivity;

        Color color = new Color(value, value, value) * Tint + Tint;

        if (valueType == ValueType.Color)
        {
            material.SetColor(ShaderProperty, color);
        }
        if(valueType == ValueType.Float)
        {
            material.SetFloat(ShaderProperty, value);
        }
    }
}
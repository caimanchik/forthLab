using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class TrainingSet
{
	public double[] input;
	public double output;
}

public class Perceptron : MonoBehaviour {

	public TrainingSet[] ts;
	private readonly double[] _weights = {0,0};
	private double _bias;
	private double _totalError;

	private double DotProductBias(IReadOnlyCollection<double> v1, IReadOnlyList<double> v2) 
	{
		if (v1 == null || v2 == null)
			return -1;
	 
		if (v1.Count != v2.Count)
			return -1;
	 
		var d = v1.Select((t, x) => t * v2[x]).Sum();

		d += _bias;
	 
		return d;
	}

	private double CalcOutput(int i)
	{
		var dp = DotProductBias(_weights, ts[i].input);
		return dp > 0 ? 1 : 0;
	}

	private void InitialiseWeights()
	{
		for(var i = 0; i < _weights.Length; i++) 
			_weights[i] = Random.Range(-1.0f, 1.0f);
		
		_bias = Random.Range(-1.0f,1.0f);
	}

	private void UpdateWeights(int j)
	{
		var error = ts[j].output - CalcOutput(j);
		_totalError += Mathf.Abs((float)error);
		
		for(var i = 0; i < _weights.Length; i++) 
			_weights[i] += error * ts[j].input[i];
		
		_bias += error;
	}

	private double CalcOutput(double i1, double i2)
	{
		var inp = new[] {i1, i2};
		var dp = DotProductBias(_weights,inp);
		
		return dp > 0 ? 1 : 0;
	}

	private void Train(int epochs)
	{
		InitialiseWeights();
		
		for(var e = 0; e < epochs; e++)
		{
			_totalError = 0;
			
			for(var t = 0; t < ts.Length; t++)
			{
				UpdateWeights(t);
				Debug.Log("W1: " + _weights[0] + " W2: " + _weights[1] + " B: " + _bias);
			}
			
			Debug.Log("TOTAL ERROR: " + _totalError);
		}
	}

	private void Start () {
		Train(8);
		Debug.Log("Test 0 0: " + CalcOutput(0,0));
		Debug.Log("Test 0 1: " + CalcOutput(0,1));
		Debug.Log("Test 1 0: " + CalcOutput(1,0));
		Debug.Log("Test 1 1: " + CalcOutput(1,1));		
	}
	
	void Update () {
		
	}
}
using UnityEngine;
using System.Collections;

public class laserScript : MonoBehaviour {

	[SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
	[SerializeField] private Color color;
	[SerializeField] private float laserWidth = 1.0f;
	[SerializeField] private new ParticleSystem particleSystem;
	private LineRenderer laserLine;


    Light[] lights;



	void Start () {
		laserLine = GetComponentInChildren<LineRenderer> ();
		lights = GetComponentsInChildren<Light> ();
		//particleSystem = GetComponent<ParticleSystem> ();
        laserLine.startWidth = laserWidth;
		laserLine.endWidth = laserWidth;
		laserLine.SetPosition(0, startPoint.position);
		laserLine.SetPosition(1, endPoint.position);
	}

    // Update is called once per frame
 //   void Update () {
	//	//laserLine.SetPosition (0, startPoint.position);
	//	//laserLine.SetPosition (1, endPoint.position);
	//}


	public Color getColor()
	{
		return color;
    }


	public void setColor(Color newColor)
	{
		color = newColor;
		laserLine.startColor = color;
		laserLine.endColor = color;
		foreach (Light light in lights)
		{
			light.color = color;
		}
		var main = particleSystem.main;
		main.startColor = color;
    }

}

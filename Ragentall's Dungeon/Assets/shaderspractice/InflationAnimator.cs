using UnityEngine;
using System.Collections;

public class InflationAnimator : ShaderAnimator
{

	protected override void RunAnimateShader ()
	{
		StartCoroutine (AnimateShader ());
	}

	protected override void ResetAnimateShader ()
	{
		base.ResetAnimateShader ();
	}
}


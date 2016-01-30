using UnityEngine;
using UnityEditor;

public class EditorSnaps : EditorWindow
{
	private Vector3 prevPosition;
	private bool doSnap = true;
	private float snapValue = 3;
	private bool enforceYValue = true;
	private float yValue = 0;

	[MenuItem( "Edit/Auto Snap %_l" )]

	static void Init()
	{
		var window = (EditorSnaps)EditorWindow.GetWindow( typeof( EditorSnaps ) );
		window.maxSize = new Vector2( 200, 100 );
	}

	public void OnGUI()
	{
		doSnap = EditorGUILayout.Toggle( "Auto Snap", doSnap );
//		snapValue = EditorGUILayout.FloatField( "Snap Value", snapValue );
		enforceYValue = EditorGUILayout.Toggle( "Enforce Y", enforceYValue );
		yValue = EditorGUILayout.FloatField( "Y Value", yValue );
		if (GUILayout.Button ("Rotate")) {
			Rotate ();
		}
	}

	public void Update()
	{
		if ( doSnap
			&& !EditorApplication.isPlaying
			&& Selection.transforms.Length > 0
			&& Selection.transforms[0].position != prevPosition )
		{
			Snap();
			prevPosition = Selection.transforms[0].position;
		}
	}

	private void Snap()
	{
		foreach ( var transform in Selection.transforms )
		{
			var t = transform.transform.position;
			t.x = Round( t.x );
			if (enforceYValue) {
				t.y = yValue;
			} else {
				t.y = Round( t.y );
			}
			t.z = Round( t.z );
			transform.transform.position = t;
		}
	}

	private float Round( float input )
	{
		return snapValue * Mathf.Round( ( input / snapValue ) );
	}

	private void Rotate() {
		foreach (var transform in Selection.transforms) {
			transform.transform.RotateAround (transform.transform.position, Vector3.up, 90);
		}
	}
}
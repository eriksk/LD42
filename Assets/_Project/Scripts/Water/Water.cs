using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skoggy.LD42
{
	public class Water : MonoBehaviour 
	{
		public Vector2 Main = new Vector2();
		public Vector2 Secondary = new Vector2();
		public Vector2 Bump = new Vector2();
		public Vector2 Bump2 = new Vector2();

		private Material _material;
		private Vector2 _offset1, _offset2, _offset3, _offset4;

		void Start()
		{
			_material = GetComponent<MeshRenderer>().material;
		}

		void Update()
		{
			_offset1 += Main * Time.deltaTime;
			_offset2 += Secondary * Time.deltaTime;
			_offset3 += Bump * Time.deltaTime;
			_offset4 += Bump2 * Time.deltaTime;

			_material.SetTextureOffset("_MainTex", _offset1);
			_material.SetTextureOffset("_SecondaryTex", _offset2);
			_material.SetTextureOffset("_BumpMap", _offset3);
			_material.SetTextureOffset("_BumpMap2", _offset4);
		}
	}
}

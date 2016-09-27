using System;
using Lidgren.Network;
using UnityEngine;

namespace GameZServer
{
	public class PlayerEntity
	{
		public PlayerEntity(int id, NetConnection connection, string name)
		{
			this.ID = id;
			this.Connection = connection;
			this.Name = name;
			this.Position = Vector3.zero;
			this.Velocity = Vector3.zero;
		}

		public NetConnection Connection { get; set; }
		public int ID { get; set; }

		public string Name { get; set; }

		public Vector3 Position { get; set; }
		public Vector3 Velocity { get; set; }
	}

	public struct Vector3
	{
		public float X;
		public float Y;
		public float Z;

		public static Vector3 zero { get { return new Vector3() { X = 0.0f, Y = 0.0f, Z = 0.0f }; } }
	}

}

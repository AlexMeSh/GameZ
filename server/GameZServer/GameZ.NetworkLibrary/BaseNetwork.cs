using System;
using System.Collections.Generic;

namespace GameZ.NetworkLibrary
{
	public abstract class BaseNetwork
	{
		#region Private variables
		Dictionary<int, Delegate> _callbacksStorage;
		Dictionary<int, Type> _typeStorage;
		#endregion

		#region Delegates
		public delegate void RegisterPacketCallback<T>(T packet) where T : BasePacket;
		#endregion

		#region Callbacks
		public void RegisterCallback<T>(RegisterPacketCallback<T> callback) where T : BasePacket, new()
		{
			var p = Activator.CreateInstance<T>();
			_callbacksStorage.Add(p.UniquePacketIdentifier, (Delegate)callback);
			_typeStorage.Add(p.UniquePacketIdentifier, p.GetType());
		}
		#endregion

		public abstract void SendPacket<T>(T packet) where T : NetworkPacket;
	}
}

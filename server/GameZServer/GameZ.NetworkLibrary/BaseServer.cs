using System;
using System.Collections.Generic;
using Lidgren.Network;

namespace GameZ.NetworkLibrary
{
	public class BaseServer
	{
		#region Variables
		NetServer _netServer;
		#endregion

		#region Properties
		public List<NetConnection> Connections
		{
			get
			{
				return _netServer.Connections;
			}
		}
		#endregion

		#region Delegates
		public delegate void NewConnectionDelegate(NetConnection conn);
		#endregion

		#region Constructor

		public BaseServer(int gamePort = 34567, int maxPlayer = 20)
		{
			var config = new NetPeerConfiguration("gameZ")
			{
				MaximumConnections = maxPlayer,
				Port = gamePort,
				ConnectionTimeout = 10,
				PingInterval = 3
			};
			config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
			_netServer = new NetServer(config);
			_netServer.RegisterReceivedCallback(new SendOrPostCallback(RecieveMsg));
		}

		#endregion

		#region Public methods

		public void Start()
		{
			_netServer.Start();
		}

		public override void SendPacket<T>(T packet)
		{
			var msg = _netServer.CreateMessage();
			packet.Write(msg);
			_netServer.SendToAll(msg, NetDeliveryMethod.Unreliable);
		}

		public void SendPacket<T>(T packet, NetConnection connection) where T : NetworkPacket
		{
			var msg = _netServer.CreateMessage();
			packet.Write(msg);
			_netServer.SendMessage(msg, connection, NetDeliveryMethod.Unreliable);
		}

		public void SendPacket<T>(T packet, NetConnection[] connection) where T : NetworkPacket
		{
			var msg = _netServer.CreateMessage();
			packet.Write(msg);
			_netServer.SendMessage(msg, connection, NetDeliveryMethod.Unreliable, 0);
		}

		#endregion

		#region Private methods

		private static void RecieveMsg(object peer)
		{
			var msg = instance._netServer.ReadMessage();
			switch (msg.MessageType)
			{
				case NetIncomingMessageType.ConnectionApproval:
					{
						break;
					}
				case NetIncomingMessageType.Data:
					{
						var msgPacketId = msg.ReadInt32();
						var type = instance._typeStorage[msgPacketId];
						var obj = Activator.CreateInstance(type) as NetworkPacket;
						var callback = instance._callbacksStorage[msgPacketId] as RegisterPacketCallback<NetworkPacket>;                                               >;
						callback(obj);
						break;
					}
				default:
					break;
			}
		}

		#endregion
	}
}

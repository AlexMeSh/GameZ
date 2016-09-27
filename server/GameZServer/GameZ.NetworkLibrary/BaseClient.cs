using System;
using Lidgren.Network;

namespace GameZ.NetworkLibrary
{
	public class BaseClient : BaseNetwork
	{
		private NetClient _client;

		public BaseClient(int port = 34567)
		{
			_client = new NetClient(new NetPeerConfiguration("gameZ") { Port = port });
			_client.Start();
		}

		public void Connect(string ipHost)
		{
			_client.Connect(ipHost, _client.Configuration.Port);
		}

		public override void SendPacket<T>(T packet)
		{
			var msg = _client.CreateMessage();
			packet.Write(msg);
			_client.SendMessage(msg, NetDeliveryMethod.Unreliable);
		}
	}
}

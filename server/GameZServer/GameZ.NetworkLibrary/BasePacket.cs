using System;
using Lidgren.Network;

namespace GameZ.NetworkLibrary
{
	public abstract class BasePacket
	{
		public abstract int UniquePacketIdentifier { get; }

		internal void Write(NetOutgoingMessage msg)
		{
			msg.WriteAllProperties(this);
		}

		internal void Read(NetIncomingMessage msg)
		{
			msg.ReadAllProperties(this);
		}
	}
}

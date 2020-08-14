using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsVoIP
{
	public class NetConvert
	{
		public static UInt16 DoReverseEndian(UInt16 x)
		{
			return Convert.ToUInt16((x << 8 & 0xff00) | (x >> 8));
		}

		public static uint DoReverseEndian(uint x)
		{
			return (x << 24 | (x & 0xff00) << 8 | (x & 0xff0000) >> 8 | x >> 24);
		}

		public static ulong DoReverseEndian(ulong x)
		{
			return (x << 56 | (x & 0xff00) << 40 | (x & 0xff0000) << 24 | (x & 0xff000000) << 8 | (x & 0xff00000000) >> 8 | (x & 0xff0000000000) >> 24 | (x & 0xff000000000000) >> 40 | x >> 56);
		}
	}
}

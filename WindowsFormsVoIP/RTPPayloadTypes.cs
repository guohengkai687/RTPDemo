using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsVoIP
{
    /// <summary>
    //0 -- AUDIO/PCMU       ITU G.711 u-law audio       RFC1890
    //3 -- AUDIO/GSM        GSM full-rate audio         RFC1890
    //8 -- ADUIO/PCUMA      ITU G.711 A-law audio       RFC1890
    //12 -- ADUIO/QCELP     PureVoice QCELP audio       RFC2658
    //14 -- ADUIO/MPA       MPEG audio(e.g mp3)         RFC2250
    /// </summary>
    public enum RTPPayloadTypesEnum
    {
        PCMU = 0,
        PCMA = 1,
        Dynamic = 96,
    }

    public class RTPPayloadTypes
    {
        /// <summary>
        /// Attempts to get the sampling frequency of a payload type.
        /// </summary>
        /// <param name="payloadType">The payload type to get the frequency for.</param>
        /// <returns>An integer representing the payload type's sampling frequency or 0 if it's
        /// dynamic or can't be determined.</returns>
		public static int GetSamplingFrequency(RTPPayloadTypesEnum payloadType)
        {
            switch (payloadType)
            {
                case RTPPayloadTypesEnum.PCMU:
                case RTPPayloadTypesEnum.PCMA:
                    return 8000;
                default:
                    return 0;
            }
        }
    }
}

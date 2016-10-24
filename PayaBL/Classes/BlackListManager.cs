using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PayaBL.Classes
{
    public class BlackListManager
    {
        #region Field

        #endregion

        #region Properties

        #region Base

        #endregion

        #region Related

        #endregion

        #endregion

        #region Constrauctor

        #endregion

        #region Method

        #region Insttance metod:

        #endregion

        #region Static Method:

        public static bool AreEqual(string ipAddress1, string ipAddress2)
        {
            return (IpAddressToLongBackwards(ipAddress1) == IpAddressToLongBackwards(ipAddress2));
        }

        public static uint IpAddressToLong(string ipAddress)
        {
            IPAddress oIp = IPAddress.Parse(ipAddress);
            if (oIp != null)
            {
                byte[] byteIp = oIp.GetAddressBytes();
                uint ip = (uint)(byteIp[3] << 0x18);
                ip += (uint)(byteIp[2] << 0x10);
                ip += (uint)(byteIp[1] << 8);
                return (ip + byteIp[0]);
            }
            return 0;
        }

        private static uint IpAddressToLongBackwards(string ipAddress)
        {
            IPAddress oIp = IPAddress.Parse(ipAddress);
            if (oIp != null)
            {
                byte[] byteIp = oIp.GetAddressBytes();
                uint ip = (uint)(byteIp[0] << 0x18);
                ip += (uint)(byteIp[1] << 0x10);
                ip += (uint)(byteIp[2] << 8);
                return (ip + byteIp[3]);
            }
            return 0;
        }

        public static bool IsEqual(string toCompare, string compareAgainst)
        {
            return (IpAddressToLongBackwards(toCompare) == IpAddressToLongBackwards(compareAgainst));
        }

        public static bool IsGreater(string toCompare, string compareAgainst)
        {
            return (IpAddressToLongBackwards(toCompare) > IpAddressToLongBackwards(compareAgainst));
        }

        public static bool IsGreaterOrEqual(string toCompare, string compareAgainst)
        {
            return (IpAddressToLongBackwards(toCompare) >= IpAddressToLongBackwards(compareAgainst));
        }

        public static bool IsIpAddressBanned(string ipAddress)
        {
            if (!IsValidIp(ipAddress.Trim()))
            {
                throw new Exception("The following isn't a valid IP address: " + ipAddress);
            }
            if (Enumerable.Any<BannedIpAddress>(BannedIpAddress.GetAll(),
                                                (Func<BannedIpAddress, bool>)(ip => IsEqual(ipAddress, ip.IpAddress))))
            {
                return true;
            }
            List<BannedIpNetwork> ipNetworkCollection = BannedIpNetwork.GetAll();
            foreach (BannedIpNetwork ipNetwork in ipNetworkCollection)
            {
                List<string> exceptionItem = new List<string>();
                exceptionItem.AddRange(ipNetwork.IpException.Split(";".ToCharArray()));
                if (exceptionItem.Contains(ipAddress))
                {
                    return false;
                }
                if (!IsValidIp(ipNetwork.StartIpAddress))
                {
                    throw new Exception("The following isn't a valid IP address: " + ipNetwork.StartIpAddress);
                }
                if (!IsValidIp(ipNetwork.EndIpAddress))
                {
                    throw new Exception("The following isn't a valid IP address: " + ipNetwork.EndIpAddress);
                }
                if (IsGreaterOrEqual(ipAddress, ipNetwork.StartIpAddress) &&
                    IsLessOrEqual(ipAddress, ipNetwork.EndIpAddress))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsLess(string toCompare, string compareAgainst)
        {
            return (IpAddressToLongBackwards(toCompare) < IpAddressToLongBackwards(compareAgainst));
        }

        public static bool IsLessOrEqual(string toCompare, string compareAgainst)
        {
            return (IpAddressToLongBackwards(toCompare) <= IpAddressToLongBackwards(compareAgainst));
        }

        public static bool IsValidIp(string ipAddress)
        {
            try
            {
                IPAddress ip = IPAddress.Parse(ipAddress);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static string LongToIpAddress(uint ipAddress)
        {
            return new IPAddress((long)ipAddress).ToString();
        }

        #endregion

        #endregion
    }
}



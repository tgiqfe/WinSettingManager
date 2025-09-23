using System.Text.RegularExpressions;

namespace TestCode01.Lib
{
    internal class IPAddressControl
    {
        /// <summary>
        /// Prefixlength -> Subnetmask
        /// </summary>
        /// <param name="prefixLength"></param>
        /// <returns></returns>
        public static string IntToSubnetmask(int prefixLength)
        {
            int length = prefixLength >= 0 || prefixLength <= 32 ? prefixLength : 24;
            string byteStr = new string('1', length) + new string('0', 32 - length);
            return string.Format("{0}.{1}.{2}.{3}",
                Convert.ToInt32(byteStr.Substring(0, 8), 2),
                Convert.ToInt32(byteStr.Substring(8, 8), 2),
                Convert.ToInt32(byteStr.Substring(16, 8), 2),
                Convert.ToInt32(byteStr.Substring(24, 8), 2));
        }

        /// <summary>
        /// Subetmask -> Prefixlength
        /// </summary>
        /// <param name="subnetmask"></param>
        /// <returns></returns>
        public static int SubnetmaskToInt(string subnetmask)
        {
            string prefix =
                string.Join("", subnetmask.Split('.').Select(x => Convert.ToString(int.Parse(x), 2)).ToArray());
            return Regex.Matches(prefix, @"^1+")[0].Value.Length;
        }
    }
}

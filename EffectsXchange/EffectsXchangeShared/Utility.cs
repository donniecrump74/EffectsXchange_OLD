using System.Web;

namespace EffectsXchangeShared; 
public class Utility {
    public static string ParameterEncode(string parameter) {
        string rtn;
        rtn = HttpUtility.UrlEncode(parameter);
        return rtn;
    }
    public static string ParameterDecode(string parameter) {
        string rtn;
        rtn = HttpUtility.UrlDecode(parameter);
        return rtn;
    }
}
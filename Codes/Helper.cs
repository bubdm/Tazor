using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Tazor.Codes {
    public class Helper {

        static public string ConvertDictToCssStyle(Dictionary<string, string> dict) {
            var str = new StringBuilder();
            foreach (var pair in dict) {
                str.Append(String.Format(" {0}:{1};", pair.Key, pair.Value));
            }
            return str.ToString();
        }

    }
}

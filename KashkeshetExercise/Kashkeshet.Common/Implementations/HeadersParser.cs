﻿using System.Collections.Generic;

namespace Kashkeshet.Common.Implementations
{
    public class HeadersParser : Abstractions.IParser<Dictionary<string, string>>
    {
        public Dictionary<string, string> Parse(string data)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            var headers = data.Split("\r\n");
            foreach (var header in headers)
            {
                if (!string.IsNullOrEmpty(header))
                {
                    var parts = header.Split(':');
                    result[parts[0]] = parts[1];
                }
            }

            return result;
        }
    }
}

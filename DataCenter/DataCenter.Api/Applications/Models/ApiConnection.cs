using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCenter.Api.Applications.Models
{
    public class ApiConnection
    {
        public static List<IApiToken> tokens = new List<IApiToken>();
        public static async Task<IApiToken> getToken(IApiToken token)
        {
            foreach (IApiToken temp in tokens)
            {
                if (temp.TokenName().Equals(token.TokenName()))
                {
                    if (temp.IsExpire())
                    {
                        var recToken = await token.DoGetToken();
                        temp.Token = recToken.Token;
                        temp.ExpireDate = recToken.ExpireDate;
                    }
                    return temp;
                }
            }
            var result = await token.DoGetToken();
            tokens.Add(result);
            return result;
        }
    }
}

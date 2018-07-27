using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnParty.Models
{
    public static class PublicModel
    {
        /// <summary>
        /// 服务器唯一私钥(建议经常更新)
        /// </summary>
        public const string secret = "025FCEAB9418BE86066B60A71BC71485";


        /// <summary>
        /// 签发Token
        /// </summary>
        /// <param name="iss">签发者</param>
        /// <param name="sub">面向用户</param>
        /// <param name="aud">接收方</param>
        /// <param name="exp">活跃的过期时间（分钟数，在指定的多少分钟内没有请求则视为过期）</param>
        /// <param name="iat">签发时间</param>
        /// <param name="jti">Token唯一标识</param>
        /// <returns>token</returns>
        public static string IsSueToken(string iss, string sub, string aud, string exp, string iat, string jti)
        {
            var payload = new Dictionary<string, object>
            {
                {"iss",iss },
                {"sub",sub },
                {"aud",aud },
                {"exp",exp },
                {"iat",iat },
                {"jti",jti }
            };
            IJwtEncoder encoder = new JwtEncoder(new HMACSHA256Algorithm(), new JsonNetSerializer(), new JwtBase64UrlEncoder());
            var token = encoder.Encode(payload, secret);
            return token;
        }

        /// <summary>
        /// Token解密
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static IDictionary<string, object> DecodeToken(string token)
        {
            IDictionary<string, object> dic = new Dictionary<string, object>();
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                dic = decoder.DecodeToObject(token);
            }
            catch (TokenExpiredException)
            {
                Console.WriteLine("Token has expired");
            }
            catch (SignatureVerificationException)
            {
                Console.WriteLine("Token has invalid signature");
            }

            return dic;
        }
    }
}
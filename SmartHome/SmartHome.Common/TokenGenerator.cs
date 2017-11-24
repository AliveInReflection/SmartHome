using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace SmartHome.Common
{
    public class TokenGenerator
    {
        public string Generate(string identity, DateTime expirationDate)
        {
            var payload = new TokenPayload()
            {
                Identity = identity,
                ExpiretionDate = expirationDate
            };

            var token = JsonConvert.SerializeObject(payload);

            token = Crypto.Encrypt(token);

            return token;
        }

        public bool Validate(string token)
        {
            token = Crypto.Decrypt(token);

            var payload = JsonConvert.DeserializeObject<TokenPayload>(token);

            if (DateTime.UtcNow < payload.ExpiretionDate)
            {
                return true;
            }

            return false;
        }

        private class TokenPayload
        {
            public string  Identity { get; set; }
            public DateTime ExpiretionDate { get; set; }
        }
    }
}

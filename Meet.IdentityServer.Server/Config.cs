using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace Meet.IdentityServer.Server
{
    public static class Config
    {
        //客户端注册，客户端能够访问的资源（通过：AllowedScopes）
        public static IEnumerable<Client> GetClient()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId="client",
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedScopes={ "api.policy", "api.policy1" },
                    Claims=new List<Claim>() //set claims for client
                    {
                        new Claim("claim","claim")
                    }
                },
                new Client
                {
                    ClientId="client1",
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedScopes={ "api.policy1" }
                }
            };
        }

        //scopes定义
        public static IEnumerable<ApiResource> GetApiResource()
        {
            return new List<ApiResource>
            {
                new ApiResource("api","api")
                {
                    Scopes=new List<Scope>()
                    {
                        new Scope("api.policy","Full access to API policy")
                        {
                            UserClaims=new List<string>(){ "claim", "claim1" } //get claims for scope
                        },
                        new Scope("api.policy1","Full access to API policy1")
                        {
                            UserClaims=new List<string>(){ "claim1" }
                        },
                    }
                },
                new ApiResource("api1","api1")
                {
                    Scopes=new List<Scope>()
                    {
                        new Scope("api.policy","Full access to API policy")
                        {
                            UserClaims=new List<string>(){ "claim", "claim1" } //get claims for scope
                        },
                        new Scope("api.policy1","Full access to API policy1")
                        {
                            UserClaims=new List<string>(){ "claim1" }
                        },
                    }
                }
            };
        }
    }
}

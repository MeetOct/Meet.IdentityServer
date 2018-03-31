using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace Meet.IdentityServer.Server
{
    public static class Config
    {
        public static IEnumerable<Client> GetClient()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId="client",
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedScopes={ "api.policy", "api.policy1"},  //get first matched apiresource for scope ps:所以不要在不同的api中使用相同的scope？->scope约定唯一
                    ClientClaimsPrefix=string.Empty,
                    Claims=new List<Claim>() //set claims for client
                    {
                        new Claim("claim","claim"),
                        new Claim("claim1","claim")
                    }
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResource()
        {
            return new List<ApiResource>
            {
                new ApiResource("api","api")
                {
                    UserClaims =new List<string>(){"claim" }, //get claims for scope
                    Scopes=new List<Scope>()
                    {
                        new Scope("api.policy","Full access to API policy")
                        {
                            //The claims specified here will be added to the list of claims specified for the API.  how can this work?
                            //UserClaims =new List<string>(){"claim" }, //get claims for scope
                        },
                    }
                },
                new ApiResource("api1","api1")
                {
                    Scopes=new List<Scope>()
                    {
                        new Scope("api1.policy","Full access to API policy")
                        {
                            UserClaims =new List<string>(){"claim1" }, //get claims for scope
                        }
                    }
                },
            };
        }
    }
}

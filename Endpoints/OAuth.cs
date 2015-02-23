using System;
using System.Collections.Generic;
using System.Web;
using Dwolla.SerializableTypes;

namespace Dwolla
{
    public class OAuth : Rest
    {
        /// <summary>
        ///     Returns an OAuth permissions page URL. If no redirect is set,
        ///     the redirect in the Dwolla Application Settings will be use
        ///     If no scope is set, the scope in the settings object will be use
        /// </summary>
        /// <param name="redirect">Redirect destination</param>
        /// <param name="scope">
        ///     OAuth scope string to override the scope
        ///     within the application settings
        /// </param>
        /// <returns>OAuth permissions page Uri</returns>
        public Uri GenAuthUrl(string redirect = null, string scope = null)
        {
            var b = new UriBuilder(
                (C.sandbox ? C.sandbox_host : C.production_host)
                + "oauth/v2/authenticate?client_id=" + HttpUtility.UrlEncode(C.client_id)
                + "&response_type=code&scope=" + (scope ?? C.oauth_scope)
                + (redirect == null ? "" : "&redirect_uri=" + HttpUtility.UrlEncode(redirect)));
            return b.Uri;
        }

        /// <summary>
        ///     Returns an OAuth token + refresh pair in an array. If no redirect
        ///     is set, the redirect in the Dwolla Application Settings will be used
        /// </summary>
        /// <param name="code">Code from redirect response</param>
        /// <param name="redirect">Redirect destination</param>
        /// <returns>OAuthResponse object</returns>
        public OAuthResponse Get(string code, string redirect = null)
        {
            var data = new Dictionary<string, string>
            {
                {"client_id", C.client_id},
                {"client_secret", C.client_secret},
                {"grant_type", "authorization_code"},
                {"code", code}
            };
            if (redirect != null) data["redirect_uri"] = redirect;

            var response = Post("/token", data, "/oauth/v2").Result;
            var oar = Jss.Deserialize<OAuthResponse>(response);
            if (oar.access_token != null) return oar;
            var err = Jss.Deserialize<OAuthError>(response);
            throw new ApiException(err.error);
        }

        /// <summary>
        ///     Returns a newly refreshed access token and refresh token pair.
        /// </summary>
        /// <param name="refreshToken">Refresh token from initial OAuth handshake.</param>
        /// <returns>OAuthResponse object</returns>
        public OAuthResponse Refresh(string refreshToken)
        {
            var response = Post("/token", new Dictionary<string, string>
            {
                {"client_id", C.client_id},
                {"client_secret", C.client_secret},
                {"grant_type", "refresh_token"},
                {"refresh_token", refreshToken}
            }).Result;

            var oar = Jss.Deserialize<OAuthResponse>(response);
            if (oar.access_token != null) return oar;
            throw new OAuthException(Jss.Deserialize<OAuthError>(response).error);
        }
    }
}
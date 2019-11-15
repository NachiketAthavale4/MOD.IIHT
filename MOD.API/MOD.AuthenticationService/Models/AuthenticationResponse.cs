using System.Collections.Generic;

namespace MOD.AuthenticationService.Models
{
    public class AuthenticationResponse
    {
        public IEnumerable<TokenResponse> TokenList { get; set; }
        public Fault Fault { get; set; }
        public bool SuccessIndicator { get; set; }
    }
}

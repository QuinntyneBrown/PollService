using PollService.Security;
using System;
using System.Linq;
using System.Data.Entity;
using PollService.Data.Model;
using System.Collections.Generic;
using System.Security.Claims;
using MediatR;
using PollService.Data;
using System.Threading.Tasks;

namespace PollService.Security
{
    public class AuthenticateCommand
    {
        public class AuthenticateRequest : IRequest<AuthenticateResponse>
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class AuthenticateResponse
        {
            public bool IsAuthenticated { get; set; }
        }

        public class AuthenticateHandler : IAsyncRequestHandler<AuthenticateRequest, AuthenticateResponse>
        {
            public AuthenticateHandler(IPollServiceContext context, IEncryptionService encryptionService)
            {
                _encryptionService = encryptionService;
                _context = context;
            }

            public bool ValidateUser(User user, string transformedPassword)
            {
                if (user == null || transformedPassword == null)
                    return false;

                return user.Password == transformedPassword;
            }

            public async Task<AuthenticateResponse> Handle(AuthenticateRequest message)
            {
                var user = await _context.Users.SingleOrDefaultAsync(x => x.Username.ToLower() == message.Username.ToLower() && !x.IsDeleted);

                return new AuthenticateResponse()
                {
                    IsAuthenticated = ValidateUser(user, _encryptionService.TransformPassword(message.Password))
                };
            }


            protected readonly IPollServiceContext _context;
            private IEncryptionService _encryptionService { get; set; }
        }

    }

}

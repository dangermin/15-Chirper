using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Chirper.API.Infrastructure;
using Chirper.API.Models;
using System.Threading.Tasks;

namespace Chirper.API.Controllers
{
    public class AccountsController : ApiController
    {
        private AuthRepository _repo = new AuthRepository();

        [AllowAnonymous]
        [Route("api/accounts/register")]
        public async Task<IHttpActionResult> Register(RegistrationModel model)
        {
            // 1. Server Side Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // 2. Pass the registration onto AuthRepository
            var result = await _repo.RegisterUser(model);

            // 3. Check to see that the registration was successful
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Registration form was invalid.");
            }

        }

        protected override void Dispose(bool dispose)
        {
            _repo.Dispose();
        }
    }
}

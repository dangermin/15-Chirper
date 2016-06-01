﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Threading.Tasks;
using System.Web;
using Chirper.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Chirper.API.Infrastructure
{
    public class AuthRepository : IDisposable
    {
        private ChirperDataContext _chirperDataContext;
        private UserManager<ChirperUser> _userManager;
        // UserManager -> UserStore -> DataContext //
        public AuthRepository()
        {
            _chirperDataContext = new ChirperDataContext();
            var userStore = new UserStore<ChirperUser> (_chirperDataContext);
            _userManager = new UserManager<ChirperUser>(userStore);
        }

        public async Task<IdentityResult> RegisterUser(RegistrationModel model)
        {
            var user = new ChirperUser
            {
                ChirpName = model.ChirpName,
                UserName = model.EmailAddress,
                Email = model.EmailAddress
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            return result;
        }

        public async Task<ChirperUser> FindUser(string username, string password)
        {
            return await _userManager.FindAsync(username, password);
        }

        public void Dispose()
        {
            _userManager.Dispose();
        }
    }
}
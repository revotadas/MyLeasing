﻿using Microsoft.AspNetCore.Identity;
using MyLeasing.Web.Data.Entities;
using System.Threading.Tasks;

namespace MyLeasing.Web.Helpers {
    public class UserHelper : IUserHelper {
        private readonly UserManager<User> userManager;

        public UserHelper(UserManager<User> userManager) {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password) {
            return await userManager.CreateAsync(user, password);
        }

        public async Task<User> GetUserByEmailAsync(string email) {
            return await userManager.FindByEmailAsync(email);
        }
    }
}

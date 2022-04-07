﻿using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;

        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public async Task Register(RegisterModel registerModel)
        {
            try
            {
                await userRL.Register(registerModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
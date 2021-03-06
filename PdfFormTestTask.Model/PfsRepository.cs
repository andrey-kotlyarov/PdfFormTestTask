﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfFormTestTask.Model
{
    /// <summary>
    /// Service's Data Repository (Singleton)
    /// </summary>
    public class PfsRepository
    {
        private static PfsRepository instance = new PfsRepository();

        public static PfsRepository Current { get { return instance; } }

        public IList<PfsUser> Users = new List<PfsUser>();

        private PfsRepository()
        {
            addTestUsersData();
        }

        /// <summary>
        /// Creates initial test data
        /// </summary>
        private void addTestUsersData()
        {
            //First user have a passoword with special url simbols. 
            Users.Add(new PfsUser() { Id = 1, Username = "user1", Password = "&!sd?_ :/123" });
            Users.Add(new PfsUser() { Id = 2, Username = "user2", Password = "pass2" });
        }

        /// <summary>
        /// Gets user by credentials
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>PfsUser or null</returns>
        public PfsUser GetUser(string username, string password)
        {
            return
                Users
                .Where(u => u.Username == username && u.Password == password)
                .FirstOrDefault();
        }
    }
}

using Data_Base;
using Modelo;
using Models;
using MVCConAzure.cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class UserManager
    {
        private UserCRUDFactory _crud;

        public UserManager()
        {
            _crud = new UserCRUDFactory();
        }


        private bool EnsureGeneralvalidation(User user)
        {
            bool val = false;
            try
            {
                if (user == null)
                {
                    throw new Exception("La comida no debe de ser nula");
                }
                else
                {
                    val = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return val;
        }
        public async Task<User> GetUserById(string id)
        {
            var uc = new UserCRUDFactory();

            return await uc.GetUserById(id);
        }
        public async void Update(User user)
        {
            if (EnsureGeneralvalidation(user))
            {
                await _crud.UpdateItem(user);
            }
        }

        public async Task<List<Food>> GetUserFoodById(string id)
        {
            var uc = new UserCRUDFactory();

            return await uc.GetUserFoodById(id);
        }

        public void CreateUserFood(UserFood food)
        {
            if (food != null)
            {
                _crud.CreateUserFood(food);
            }

        }

        public async void Delete(string userId, string idfood)
        {
            if (userId != "" && idfood != "")
            {
                await _crud.DeleteUserFoodById(userId, idfood);
            }
        }

        public void Create(User user)
        {
            if (EnsureGeneralvalidation(user))
            {
                _crud.Create(user);
            }
        }
        //public async Task<List<Food>> RetrieveAll()
        //{
        //    var uc = new FoodCRUDFactory();

        //    return await uc.RetrieveAll(); // Asegúrate de esperar la tarea
        //}

        public async Task<User> LoginUser(string correo, string password)
        {
            try
            {
                // Call the LoginUserAsync method from UserCRUDFactory
                return await _crud.LoginUser(correo, password);
            }
            catch (Exception ex)
            {
                // Handle exceptions if necessary (e.g., log, throw, etc.)
                throw;
            }
        }

    }
}
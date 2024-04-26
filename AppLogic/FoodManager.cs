using Data_Base;
using Modelo;
using MVCConAzure.cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class FoodManager
    {
        private FoodCRUDFactory _crud;

        public FoodManager()
        {
            _crud = new FoodCRUDFactory();
        }


        private bool EnsureGeneralvalidation(Food food)
        {
            bool val = false;
            try
            {
                if (food == null)
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

        public void Create(Food food)
        {
            if (EnsureGeneralvalidation(food))
            {
                _crud.Create(food);
            }     
        }

        public async void Update(Food food)
        {
            if (EnsureGeneralvalidation(food))
            {
                await _crud.UpdateItem(food);
            }
        }

        public async void Delete(string id)
        {
            if (id != "")
            {
                await _crud.delete_item(id);
            }
        }

        public async Task<List<Food>> RetrieveAll()
        {
            var uc = new FoodCRUDFactory();

            return await uc.RetrieveAll(); // Asegúrate de esperar la tarea
        }
        public async Task<Food> GetFoodById(string id)
        {
            var uc = new FoodCRUDFactory();

            return await uc.GetFoodById(id); 
        }


    }
}
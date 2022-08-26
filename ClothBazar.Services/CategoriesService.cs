﻿using ClothBazar.Database;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ClothBazar.Services
{
    public class CategoriesService
    {
        public Category GetCategory(int ID)
        {
            using (var context = new AppDbContext())
            {
                return context.Categories.Find(ID);
            }
        }

        public List<Category> GetCategories()
        {
            using (var context = new AppDbContext())
            {
                return context.Categories.Include(x => x.Products).ToList();
            }
        }

        public List<Category> GetFeaturedCategories()
        {
            using (var context = new AppDbContext())
            {
                return context.Categories.Where(x => x.IsFeatured == true && x.ImageURL != null).ToList();
            }
        }

        public void SaveCategory(Category category)
        {
            using (var context = new AppDbContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        public void UpdateCategory(Category category)
        {
            using (var context = new AppDbContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteCategory(int ID)
        {
            using (var context = new AppDbContext())
            {
                //context.Entry(category).State = System.Data.Entity.EntityState.Deleted;

                var category = context.Categories.Find(ID);
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}

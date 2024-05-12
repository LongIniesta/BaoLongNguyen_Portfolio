using BussinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SupplierDAO
    {
        private static SupplierDAO instance = null;
        private static readonly object instanceLock = new object();
        private SupplierDAO() { }
        public static SupplierDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new SupplierDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Supplier> GetAll()
        {
            List<Supplier> result;
            try
            {
                var DBConetxt = new BirdMealOrderSystemContext();
                result = DBConetxt.Suppliers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public Supplier GetById(int id)
        {
            Supplier result;
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                result = DBContext.Suppliers.FirstOrDefault(p => p.SupplierId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public void Add(Supplier Supplier)
        {
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                DBContext.Suppliers.Add(Supplier);
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteById(int id)
        {
            try
            {
                Supplier p = null;
                p = GetById(id);
                if (p != null)
                {
                    var DBContext = new BirdMealOrderSystemContext();
                    DBContext.Suppliers.Remove(p);
                    DBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateSupplier(Supplier cdt)
        {
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                DBContext.Suppliers.Update(cdt);
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

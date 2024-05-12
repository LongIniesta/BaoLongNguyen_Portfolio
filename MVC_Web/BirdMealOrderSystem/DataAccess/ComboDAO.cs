using BussinessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ComboDAO
    {
        private static ComboDAO instance = null;
        private static readonly object instanceLock = new object();
        private ComboDAO() { }
        public static ComboDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ComboDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Combo> GetAllByStatus(bool status)
        {
            List<Combo> result = null;
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                result = (from cb in DBContext.Combos.Include(c => c.ComboDetails).ToList()
                                       where cb.ComboStatus == status
                                       select cb).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public void CreateCombo(Combo combo)
        {
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                DBContext.Combos.Add(combo);
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteComboById(int id)
        {
            Combo combo = null;
            combo = getComboById(id);
            try
            {
                if (combo == null)
                {
                    return;
                }
                var DBContext = new BirdMealOrderSystemContext();
                DBContext.Combos.Remove(combo);
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Combo getComboById(int id)
        {
            Combo combo = null;
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                combo = DBContext.Combos.Include(c => c.ComboDetails).FirstOrDefault(cb => cb.ComboId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return combo;
        }
        public void UpdateCombo(Combo cdt)
        {
            try
            {
                var DBContext = new BirdMealOrderSystemContext();
                DBContext.Combos.Update(cdt);
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

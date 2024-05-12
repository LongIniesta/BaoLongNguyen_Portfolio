using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class GiftCommentDAO
    {
        private static GiftCommentDAO instance = null;
        private static readonly object instanceLock = new object();
        private GiftCommentDAO() { }
        public static GiftCommentDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new GiftCommentDAO();
                    }
                    return instance;
                }
            }
        }
        public GiftComment GetByID(int id)
        {
            GiftComment result = null;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.GiftComments.SingleOrDefault(u => u.GiftCommentId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }


        public GiftComment AddGiftComment(GiftComment GiftComment)
        {
            GiftComment result;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.GiftComments.Add(GiftComment).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public GiftComment RemoveGiftComment(int id)
        {
            GiftComment result;
            GiftComment GiftComment = GetByID(id);
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.GiftComments.Remove(GiftComment).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public GiftComment UpdateGiftComment(GiftComment GiftComment)
        {
            GiftComment result;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.GiftComments.Update(GiftComment).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public IEnumerable<GiftComment> GetAll()
        {
            List<GiftComment> result = new List<GiftComment>();
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.GiftComments.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}

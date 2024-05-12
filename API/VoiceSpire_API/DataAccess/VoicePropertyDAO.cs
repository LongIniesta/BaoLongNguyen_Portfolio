using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class VoicePropertyDAO
    {
        private static VoicePropertyDAO instance = null;
        private static readonly object instanceLock = new object();
        private VoicePropertyDAO() { }
        public static VoicePropertyDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new VoicePropertyDAO();
                    }
                    return instance;
                }
            }
        }
        public VoiceProperty Add(VoiceProperty VoiceProperty)
        {
            VoiceProperty result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceProperties.Add(VoiceProperty).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public IEnumerable<VoiceProperty> GetBySellerId(int id)
        {
            List<VoiceProperty> result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceProperties.Where(v => v.VoiceSellerId == id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}

using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class VoiceTypeDAO
    {
        private static VoiceTypeDAO instance = null;
        private static readonly object instanceLock = new object();
        private VoiceTypeDAO() { }
        public static VoiceTypeDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new VoiceTypeDAO();
                    }
                    return instance;
                }
            }
        }
        public VoiceType Add(VoiceType voiceType)
        {
            VoiceType result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceTypes.Add(voiceType).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public IEnumerable<VoiceType> GetBySellerId(int id)
        {
            List<VoiceType> result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceTypes.Where(v => v.VoiceSellerId == id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}

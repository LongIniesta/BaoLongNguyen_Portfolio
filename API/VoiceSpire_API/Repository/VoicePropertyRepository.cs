using BusinessObject;
using DataAccess;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class VoicePropertyRepository : IVoicePropertyRepository
    {
        public VoiceProperty Add(VoiceProperty VoiceProperty) => VoicePropertyDAO.Instance.Add(VoiceProperty);

        public IEnumerable<VoiceProperty> GetBySellerId(int id) => VoicePropertyDAO.Instance.GetBySellerId(id);
    }
}

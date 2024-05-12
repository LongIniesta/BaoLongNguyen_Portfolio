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
    public class VoiceTypeRepostiory : IVoiceTypeRepository
    {
        public VoiceType Add(VoiceType voiceType) => VoiceTypeDAO.Instance.Add(voiceType);

        public IEnumerable<VoiceType> GetBySellerId(int id) => VoiceTypeDAO.Instance.GetBySellerId(id);
    }
}

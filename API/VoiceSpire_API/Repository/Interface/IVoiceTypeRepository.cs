using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IVoiceTypeRepository
    {
        VoiceType Add(VoiceType voiceType);
        IEnumerable<VoiceType> GetBySellerId(int id);
    }
}
